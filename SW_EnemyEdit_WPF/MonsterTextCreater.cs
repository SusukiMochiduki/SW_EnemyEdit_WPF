using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SW_EnemyEdit_WPF
{
	public enum 出力分類
	{
		通常 = 0,
		圧縮,
		圧縮_旧,
		ステータスのみ,
	}
	public class MonsterTextCreater
	{
		public static string Create(
			IEnumerable<魔物> 敵,
			出力分類 style,
			bool isSW25 = false
			)
		{
			string[] alphabet = new string[26];
			for(int i=0;i<alphabet.Count(); i++)
			{
				alphabet[i] = ((char)('A' + i)).ToString();
			}
			string なし = "なし";
			string result = "";
			List<魔物> list = 敵.Select(x => x.Clone()).ToList();

			foreach (var v in list)
			{
				強さ変動(v,isSW25);
				if (isSW25)
				{
					v.特殊能力 = v.特殊能力.Replace("〆", "▶");
					v.特殊能力 = v.特殊能力.Replace("☆", "⏩");
					v.特殊能力 = v.特殊能力.Replace("☑", "💬");
				}
				else
				{
					v.特殊能力 = v.特殊能力.Replace("▶", "〆");
					v.特殊能力 = v.特殊能力.Replace("⏩", "☆");
					v.特殊能力 = v.特殊能力.Replace("💬", "☑");
				}
			}
			int TP = 0;
			foreach (var v in list)
			{
				TP += v.弱点値上昇 == TD弱点値上昇分類.なし ? 0 : int.Parse(v.弱点値上昇.ToString().Remove(0, 2));
				TP += v.先制値上昇 == TD先制値上昇分類.なし ? 0 : int.Parse(v.先制値上昇.ToString().Remove(0, 2));
				foreach( var bui in v.魔物部位)
				{
					TP += bui.瞬間打撃点 == TD瞬間打撃点分類.なし ? 0 : int.Parse(bui.瞬間打撃点.ToString().Remove(0, 2)) * bui.瞬間打撃点回数;
					TP += bui.瞬間防護点 == TD瞬間防護点分類.なし ? 0 : int.Parse(bui.瞬間防護点.ToString().Remove(0, 2));
					TP += bui.瞬間達成値== TD瞬間達成値分類.なし ? 0 : int.Parse(bui.瞬間達成値.ToString().Remove(0, 2));
					TP += bui.追加攻撃== TD追加攻撃分類.なし ? 0 : int.Parse(bui.追加攻撃.ToString().Remove(0, 2));
					TP += bui.呪いの波動== TD呪いの波動分類.なし ? 0 : int.Parse(bui.呪いの波動.ToString().Remove(0, 2));
					TP += bui.世界の汚染== TD世界の汚染分類.なし ? 0 : int.Parse(bui.世界の汚染.ToString().Remove(0, 2));
				}
			}
			if(TP != 0)
			{
				result = $"トレジャーポイント：{TP}\r\n\r\n";
			}
			if (style == 出力分類.通常)
			{
				List<string> resultList = new List<string>();
				var group = list.GroupBy(x => new { x.名称, x.剣のかけら個数 });
				foreach (var g in group)
				{
					var data = g.First();

					string 正式名称 = data.ネームド ? $"{data.名前}（{data.名称}）" : data.名称;
					string strBuild = $@"LV{data.LV}　{正式名称}
知能：{data.知能}　知覚：{data.知覚}　言語：{(string.IsNullOrWhiteSpace(data.言語) ? なし : data.言語)}
生息地：{data.生息地}　反応：{data.反応}
知名度／弱点値：{data.知名度}／{data.弱点値}　先制値：{data.先制値}　移動速度：{data.移動速度}
生命抵抗力：{data.生命抵抗力}（{data.生命抵抗力 + 7}）　精神抵抗力{data.精神抵抗力}（{data.精神抵抗力 + 7}）
弱点：{(string.IsNullOrWhiteSpace(data.弱点) ? なし : data.弱点)}";

					foreach (var v in data.魔物部位)
					{
						strBuild += $"\r\n{(string.IsNullOrWhiteSpace(v.部位名) ? v.攻撃方法 : v.部位名 + "（" + (string.IsNullOrWhiteSpace(v.攻撃方法) ? なし : v.攻撃方法) + "）")}";
						if (string.IsNullOrWhiteSpace(v.攻撃方法))
						{
							strBuild += $"　命中力－　打撃点－";
						}
						else
						{
							strBuild += $"　命中力{v.命中力}（{v.命中力 + 7}）　打撃点{v.打撃点}（{v.打撃点 + 7}）";
						}
						strBuild += $"　回避力{v.回避力}（{v.回避力 + 7}）　防護点{v.防護点}";
						strBuild += $"　HP{v.HP}　MP{v.MP}";
						if (data.魔物部位.Count() > 1)
						{
							strBuild += v.コア部位 ? "　コア部位" : "";
						}
						if (v.剣のかけら個数 > 0)
						{
							strBuild += "　剣のかけら" + v.剣のかけら個数.ToString();
						}
					}
					strBuild += $"\r\n\r\n【特殊能力】\r\n{(string.IsNullOrWhiteSpace(data.特殊能力) ? なし : data.特殊能力)}";
					strBuild += $"\r\n\r\n【戦利品】\r\n{(string.IsNullOrWhiteSpace(data.戦利品) ? なし : data.戦利品)}";
					if (!string.IsNullOrWhiteSpace(data.解説))
					{
						strBuild += $"\r\n\r\n【解説】\r\n{data.解説}";
					}
					resultList.Add(strBuild);
				}
				result += string.Join("\r\n\r\n", resultList);
			}
			else if (style == 出力分類.ステータスのみ)
			{
				var group = list.GroupBy(x => new { x.名称, x.剣のかけら個数 });
				foreach (var g in group)
				{
					string 正式名称 = "";
					var first = g.First();
					正式名称 = first.ネームド ? $"{first.名前}（{first.名称}）" : first.名称;
					string 名称 = first.ネームド ? first.名前 : first.名称;
					if (first.魔物部位.Count() == 1)
					{
						int count = 1;
						foreach (var v in g)
						{
							var data = v.魔物部位.First();
							if (g.Count() == 1)
							{
								result += $"{名称}";
							}
							else
							{
								result += $"{名称}{alphabet[count-1]}";
							}
							result += $"　HP{data.HP}/{data.HP}　MP{data.MP}/{data.MP}";
							if (string.IsNullOrWhiteSpace(data.攻撃方法))
							{
								result += $"　命中力－　打撃点－";
							}
							else
							{
								result += $"　命中力{data.命中力}（{data.命中力+7}）　打撃点2d+{data.打撃点}";
							}
							result += $"　回避力{data.回避力}（{data.回避力 + 7}）　防護点{data.防護点}";
							result += $"　生命抵抗{first.生命抵抗力}（{first.生命抵抗力+7}）　精神抵抗{first.精神抵抗力}（{first.精神抵抗力+7}）";
							if (v.剣のかけら個数 > 0)
							{
								result += "　剣のかけら" + v.剣のかけら個数.ToString();
							}
							result += "\r\n";
							count++;
						}
					}
					else
					{
						int count = 1;
						foreach (var monster in g)
						{
							if (g.Count() == 1)
							{
								result += $"{名称}";
							}
							else
							{
								result += $"{名称}{alphabet[count - 1]}";
							}
							result += $"　生命抵抗{monster.生命抵抗力}（{monster.生命抵抗力+7}）　精神抵抗{monster.精神抵抗力}（{monster.精神抵抗力+7}）";
							result += "\r\n";

							foreach (var data in monster.魔物部位)
							{
								result += $"{data.攻撃方法}（{data.部位名}）　HP{data.HP}/{data.HP}　MP{data.MP}/{data.MP}";
								if (string.IsNullOrWhiteSpace(data.攻撃方法))
								{
									result += $"　命中力－　打撃点－";
								}
								else
								{
									result += $"　命中力{data.命中力}（{data.命中力 + 7}）　打撃点2d+{data.打撃点}";
								}
								result += $"　回避力{data.回避力}（{data.回避力 + 7}）　防護点{data.防護点}";
								if (data.コア部位)
								{
									result += $"　コア部位";
								}
								if (data.剣のかけら個数 > 0)
								{
									result += "　剣のかけら" + data.剣のかけら個数.ToString();
								}
								result += "\r\n";
							}
							count++;
						}
					}
				}
			}
			else if (style == 出力分類.圧縮_旧)
			{
				var group = list.GroupBy(x => new { x.名称, x.剣のかけら個数 });
				foreach (var g in group)
				{
					var first = g.First();
					string 正式名称 = first.ネームド ? $"{first.名前}（{first.名称}）" : first.名称;
					string 名称 = first.ネームド ? first.名前 : first.名称;
					if (first.魔物部位.Count() == 1)
					{
						int count = 1;
						result += $"{名称} 生命抵抗{first.生命抵抗力}({first.生命抵抗力+7}) 精神抵抗{first.精神抵抗力}({first.精神抵抗力+7})";
						result += "\r\n";
						foreach (var v in g)
						{
							var data = v.魔物部位.First();
							if (g.Count() == 1)
							{
							}
							else
							{
								result += $"{名称}{alphabet[count - 1]}";
							}
							result += $"　HP{data.HP}/{data.HP} MP{data.MP}/{data.MP}";
							if (v.剣のかけら個数 > 0)
							{
								result += "　剣のかけら" + v.剣のかけら個数.ToString();
							}
							result += "\r\n";
							count++;
						}
						var 部位first = first.魔物部位.First();
						if (string.IsNullOrWhiteSpace(部位first.攻撃方法))
						{
							result += $"　命中－ 打撃点－";
						}
						else
						{
							result += $"　命中{部位first.命中力}({部位first.命中力 + 7}) 打撃2d+{部位first.打撃点}";
						}
						result += $" 回避{部位first.回避力}({部位first.回避力 + 7})防護{部位first.防護点}";
						result += "\r\n";
					}
					else
					{
						int count = 1;
						foreach (var monster in g)
						{
							if (g.Count() == 1)
							{
								result += $"{名称}";
							}
							else
							{
								result += $"{名称}{alphabet[count - 1]}";
							}
							result +=$"生命抵抗{monster.生命抵抗力}({monster.生命抵抗力+7}) 精神抵抗{monster.精神抵抗力}({monster.精神抵抗力+7})";
							result += "\r\n";

							foreach (var data in monster.魔物部位)
							{
								result += $"{data.部位名}　HP{data.HP}/{data.HP} MP{data.MP}/{data.MP}";
								if (data.コア部位)
								{
									result += $" コア部位";
								}
								if (data.剣のかけら個数 > 0)
								{
									result += " かけら" + data.剣のかけら個数.ToString();
								}
								result += "\r\n";
								if (string.IsNullOrWhiteSpace(data.攻撃方法))
								{
									result += $"　命中－ 打撃点－";
								}
								else
								{
									result += $"　命中{data.命中力}({data.命中力 + 7}) 打撃2d+{data.打撃点}";
								}
								result += $" 回避{data.回避力}({data.回避力 + 7}) 防護{data.防護点}";
								result += "\r\n";
							}
							count++;
						}
					}
				}
				foreach (var g in group)
				{
					result += "\r\n";
					result += "\r\n";
					var data = g.First();
					string 正式名称 = data.ネームド ? $"{data.名前}（{data.名称}）" : data.名称;
					result += $@"LV{data.LV}　{正式名称}　{data.分類}
知能：{data.知能}　知覚：{data.知覚}　言語：{(string.IsNullOrWhiteSpace(data.言語) ? なし : data.言語)}
生息地：{data.生息地}　反応：{data.反応}
知名度／弱点値：{data.知名度}/{data.弱点値}　先制値：{data.先制値}　移動速度：{data.移動速度}
弱点：{(string.IsNullOrWhiteSpace(data.弱点) ? なし : data.弱点)}";

					result += $"\r\n\r\n【特殊能力】\r\n{(string.IsNullOrWhiteSpace(data.特殊能力) ? なし : data.特殊能力)}";
					result += $"\r\n\r\n【戦利品】\r\n{(string.IsNullOrWhiteSpace(data.戦利品) ? なし : data.戦利品)}";
					if (!string.IsNullOrWhiteSpace(data.解説))
					{
						result += $"\r\n\r\n【解説】\r\n{data.解説}";
					}
				}
			}
			else if (style == 出力分類.圧縮)
			{
				List<string> resultList = new List<string>();
				var group = list.GroupBy(x => new { x.名称, x.剣のかけら個数 });
				foreach (var g in group)
				{
					var data = g.First();
					string 特殊能力追加 = "";

					string 正式名称 = data.ネームド ? $"{data.名前}（{data.名称}）" : data.名称;
					string strBuild = $@"LV{data.LV}　{正式名称}　({data.出典})
知能：{data.知能}　知覚：{data.知覚}　言語：{(string.IsNullOrWhiteSpace(data.言語) ? なし : data.言語)}
生息地：{data.生息地}　反応：{data.反応}
知名度/弱点値：{data.知名度}/{data.弱点値}　先制値：{data.先制値}　移動速度：{data.移動速度}
生命抵抗力：{data.生命抵抗力}({data.生命抵抗力 + 7})　精神抵抗力{data.精神抵抗力}({data.精神抵抗力 + 7})
弱点：{(string.IsNullOrWhiteSpace(data.弱点) ? なし : data.弱点)}"+ "\r\n";

					if (data.弱点値上昇 != TD弱点値上昇分類.なし)
					{
						strBuild += "トレジャー強化「弱点値上昇+" + (int)data.弱点値上昇 + "」\r\n";
					}
					if (data.先制値上昇 != TD先制値上昇分類.なし)
					{
						strBuild += "トレジャー強化「先制値上昇+" + (int)data.先制値上昇 + "」\r\n";
					}

					foreach (var v in data.魔物部位)
					{
						strBuild += $"\r\n{(string.IsNullOrWhiteSpace(v.部位名) ? v.攻撃方法 : v.部位名 + "(" + (string.IsNullOrWhiteSpace(v.攻撃方法) ? なし : v.攻撃方法) + ")")}";
						strBuild += $"　HP{v.HP}　MP{v.MP}";
						if (data.魔物部位.Count() > 1)
						{
							strBuild += v.コア部位 ? "　コア部位" : "";
						}
						if (v.剣のかけら個数 > 0)
						{
							strBuild += "　剣のかけら" + v.剣のかけら個数.ToString();
						}
						if (v.瞬間打撃点 != TD瞬間打撃点分類.なし)
						{
							strBuild += "　瞬間打撃点+" + (int)v.瞬間打撃点 + "×" + v.瞬間打撃点回数;
							特殊能力追加 += "\r\n「トレジャー強化：瞬間打撃点」";
							特殊能力追加 += "\r\n　打撃点決定の2dを振り終わった後に、打撃点を「指定された値」上昇させられます。";
							特殊能力追加 += "\r\n　1Rの間に対象1体ごとに1回しか使えません。";
							特殊能力追加 += "\r\n　1日に指定した回数しか使えません。";
						}
						if (v.瞬間防護点 != TD瞬間防護点分類.なし)
						{
							strBuild += "　瞬間防護点+" + (int)v.瞬間防護点;
							特殊能力追加 += "\r\n「トレジャー強化：瞬間防護点」";
							特殊能力追加 += "\r\n　物理ダメージを受けて防護点を適用する時、防護点を「指定された値」上昇させられます。";
							特殊能力追加 += "\r\n　1日に1回しか使えません。";
						}
						if (v.瞬間達成値 != TD瞬間達成値分類.なし)
						{
							strBuild += "　瞬間達成値+" + (int)v.瞬間達成値;
							特殊能力追加 += "\r\n「トレジャー強化：瞬間達成値」";
							特殊能力追加 += "\r\n　行為判定の達成値を求めた後に、達成値を「指定された値」上昇させられます。";
							特殊能力追加 += "\r\n　1日に1回しか使えません。";
						}
						if (v.追加攻撃 != TD追加攻撃分類.なし)
						{
							switch (v.追加攻撃)
							{
								case TD追加攻撃分類.TP1:
									strBuild += "　追加攻撃:⑥／１";
									break;
								case TD追加攻撃分類.TP2:
									strBuild += "　追加攻撃:⑤⑥／１";
									break;
								case TD追加攻撃分類.TP4:
									strBuild += "　追加攻撃:⑤⑥／２";
									break;
								case TD追加攻撃分類.TP7:
									strBuild += "　追加攻撃:④⑤⑥／２";
									break;
								case TD追加攻撃分類.TP10:
									strBuild += "　追加攻撃:④⑤⑥／３";
									break;
							}
							特殊能力追加 += "\r\n「トレジャー強化：追加攻撃」";
							特殊能力追加 += "\r\n　「対象の出目」／「発動回数」";
							特殊能力追加 += "\r\n　自身の手番終了時に1dを振り、「対象の出目」が出ると近接攻撃を1回追加で行えます。";
							特殊能力追加 += "\r\n　1日に「発動回数」回しか発動しません";
						}
						if (v.呪いの波動 != TD呪いの波動分類.なし)
						{
							strBuild += "　呪いの波動:" + (int)v.呪いの波動 + "点";
							特殊能力追加 += "\r\n「トレジャー強化：⏩呪いの波動」";
							特殊能力追加 += "\r\n　使用宣言後、6Rの間持続します。";
							特殊能力追加 += "\r\n　自身の手番終了時に自動的に「射程：接触」「対象：１体」に「抵抗：必中」で「指定された値」点の呪い属性の確定ダメージを与えます";
							特殊能力追加 += "\r\n　1日に1回しか使えません。";
						}
						if (v.世界の汚染 != TD世界の汚染分類.なし)
						{
							strBuild += "　世界の汚染:威力" + (int)v.世界の汚染;
							特殊能力追加 += "\r\n「トレジャー強化：世界の汚染」";
							特殊能力追加 += "\r\n　戦闘行為によって始めて自身のHPにダメージを受けたとき、自動的に";
							特殊能力追加 += "\r\n　「射程：自身」「対象：全エリア（半径20m）／すべて」に「抵抗：必中」で、「指定された威力／🄫10」の毒属性魔法ダメージを与えます。";
							特殊能力追加 += "\r\n　対象から任意のキャラクターを除外できます。";
							特殊能力追加 += "\r\n　1日に1回しか発動しません。";
						}
						if (string.IsNullOrWhiteSpace(v.攻撃方法))
						{
							strBuild += $"\r\n　命中－　打撃－";
						}
						else
						{
							strBuild += $"\r\n　命中{v.命中力}({v.命中力 + 7})　打撃{v.打撃点}({v.打撃点 + 7})";
						}
						strBuild += $"　回避{v.回避力}({v.回避力 + 7})　防護{v.防護点}";
					}
					strBuild += $"\r\n\r\n【特殊能力】\r\n{(string.IsNullOrWhiteSpace(data.特殊能力+ 特殊能力追加) ? なし : data.特殊能力+ 特殊能力追加)}";
					strBuild += $"\r\n\r\n【戦利品】\r\n{(string.IsNullOrWhiteSpace(data.戦利品) ? なし : data.戦利品)}";
					if (!string.IsNullOrWhiteSpace(data.解説))
					{
						strBuild += $"\r\n\r\n【解説】\r\n{data.解説}";
					}
					resultList.Add(strBuild);
					result += string.Join("\r\n\r\n\r\n", resultList);
				}
			}

			return result;
		}
		public static void 強さ変動(
			魔物 data,
			bool isSW25 = false
			)
		{
			switch (data.強さ変動)
			{
				case 強さ変動分類.変動プラス1:
					{
						if(data.ネームド)
						{
							data.名前 += "+1";
						}
						else
						{
							data.名称 += "+1";
						}
						data.LV++;
						data.知名度++;
						data.弱点値++;
						data.先制値++;
						data.生命抵抗力++;
						data.精神抵抗力++;
						foreach (var v in data.魔物部位)
						{
							v.HP += 5;
							v.MP += 2;
							v.命中力 += 1;
							v.回避力 += 1;
						}
						break;
					}
				case 強さ変動分類.変動マイナス1:
					{
						if (data.ネームド)
						{
							data.名前 += "-1";
						}
						else
						{
							data.名称 += "-1";
						}
						data.LV--;
						data.知名度--;
						data.弱点値--;
						data.先制値--;
						data.生命抵抗力--;
						data.精神抵抗力--;
						foreach (var v in data.魔物部位)
						{
							v.HP -= 5;
							v.MP -= 2;
							v.命中力 -= 1;
							v.回避力 -= 1;
						}
						break;
					}
			}
			int sum剣のかけら = data.魔物部位.Sum(x => x.剣のかけら個数);
			if (data.剣のかけら個数 > 0)
			{
				if(data.剣のかけら振分 != 剣のかけら振分分類.任意)
				{
					foreach(var v in data.魔物部位)
					{
						v.剣のかけら個数 = 0;
					}
					List<魔物部位> 振分対象 = new List<魔物部位>();
					if (data.魔物部位.Count == 1)
					{
						振分対象.Add(data.魔物部位.First());
					}
					else if (data.剣のかけら振分 == 剣のかけら振分分類.コア)
					{
						振分対象.AddRange(data.魔物部位.Where(x => x.コア部位));
					}
					else if (data.剣のかけら振分 == 剣のかけら振分分類.均等)
					{
						振分対象.AddRange(data.魔物部位);
					}
					for (int i = 0; i < data.剣のかけら個数;)
					{
						foreach (var v in 振分対象)
						{
							if (i >= data.剣のかけら個数)
							{
								break;
							}
							v.剣のかけら個数++;
							i++;
						}
					}
				}
				foreach (var v in data.魔物部位)
				{
					v.HP += 5 * v.剣のかけら個数;
					v.MP += 1 * v.剣のかけら個数;
				}
			}
			if (isSW25)
			{
				int 変動値=0;
				//抵抗も上げる
				if (data.剣のかけら個数 > 0 || data.魔物部位.Sum(x => x.剣のかけら個数) > 0)
				{
					int かけら = data.剣のかけら個数 + data.魔物部位.Sum(x => x.剣のかけら個数);
					if (data.剣のかけら個数 <= 5)
					{
						変動値 = 1;
					}
					else if (data.剣のかけら個数 <= 10)
					{
						変動値 = 2;
					}
					else if (data.剣のかけら個数 <= 15)
					{
						変動値 = 3;
					}
					else
					{
						変動値 = 4;
					}
				}
				data.生命抵抗力 += 変動値;
				data.精神抵抗力 += 変動値;
			}
			if(data.弱点値上昇 != TD弱点値上昇分類.なし)
			{
				data.弱点値 += (int)data.弱点値上昇;
			}
			if (data.先制値上昇 != TD先制値上昇分類.なし)
			{
				data.先制値 += (int)data.先制値上昇;
			}
		}

	}
}
