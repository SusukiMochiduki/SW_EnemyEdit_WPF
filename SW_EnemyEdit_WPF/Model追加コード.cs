using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_EnemyEdit_WPF
{
	public enum 強さ変動分類
	{
		変動なし = 0,
		変動マイナス1,
		変動プラス1
	}
	public enum 剣のかけら振分分類
	{
		コア = 0,
		均等,
		任意
	}
	public enum TD弱点値上昇分類
	{
		なし=0,
		TP1=1,
		TP2=2,
		TP4=3,
		TP6=4,
		TP8=5,
		TP10=6
	}
	public enum TD先制値上昇分類
	{
		なし = 0,
		TP1 = 1,
		TP2 = 2,
		TP4 = 3,
		TP6 = 4,
		TP8 = 5,
		TP10 = 6
	}
	public enum TD瞬間打撃点分類
	{
		なし = 0,
		TP1 = 2,
		TP2 = 4,
		TP3=6,
		TP4 = 8,
		TP6 = 10,
		TP8 = 12,
		TP10 = 14
	}
	public enum TD瞬間防護点分類
	{
		なし = 0,
		TP1 = 2,
		TP2 = 4,
		TP3 = 6,
		TP4 = 8,
		TP6 = 10,
		TP8 = 12,
		TP10 = 14
	}
	public enum TD瞬間達成値分類
	{
		なし = 0,
		TP1 = 1,
		TP2 = 2,
		TP4 = 3,
		TP6 = 4,
		TP8 = 5,
		TP10 = 6
	}
	public enum TD追加攻撃分類
	{
		なし = 0,
		TP1,
		TP2,
		TP4,
		TP7,
		TP10
	}
	public enum TD呪いの波動分類
	{
		なし = 0,
		TP1 = 1,
		TP3 = 2,
		TP5 = 3,
		TP7 = 4,
		TP10 = 5
	}
	public enum TD世界の汚染分類
	{
		なし = 0,
		TP2 = 10,
		TP4 = 20,
		TP6 = 30,
		TP8 = 40,
		TP10 = 50
	}
	public partial class 魔物
	{
		public 強さ変動分類 強さ変動 { get; set; }
		public 剣のかけら振分分類 剣のかけら振分 { get; set; }
		public int 剣のかけら個数 { get; set; }
		public TD弱点値上昇分類 弱点値上昇 { get; set; }
		public TD先制値上昇分類 先制値上昇 { get; set; }

		/// <summary>
		///		魔物の新しいインスタンスを作り、コピーします。</summary>
		/// <returns></returns>
		public 魔物 Clone()
		{
			魔物 result = new 魔物
			{
				Id = Id,
				LV = LV,
				SW20 = SW20,
				SW25 = SW25,
				オリジナル = オリジナル,
				出典 = 出典,
				コア部位 = コア部位,
				タグ = タグ,
				ネームド = ネームド,
				先制値 = 先制値,
				分類 = 分類,
				反応 = 反応,
				名前 = 名前,
				名称 = 名称,
				弱点 = 弱点,
				弱点値 = 弱点値,
				戦利品 = 戦利品,
				特殊能力 = 特殊能力,
				生命抵抗力 = 生命抵抗力,
				生息地 = 生息地,
				知名度 = 知名度,
				知能 = 知能,
				知覚 = 知覚,
				移動速度 = 移動速度,
				穢れ点 = 穢れ点,
				精神抵抗力 = 精神抵抗力,
				解説 = 解説,
				言語 = 言語,
				部位数 = 部位数,
				部位数内訳 = 部位数内訳,
				魔物部位 = 魔物部位
					.OrderBy(x => x.No)
					.Select(x => x.Clone())
					.ToList(),
				強さ変動 = 強さ変動,
				剣のかけら個数 = 剣のかけら個数,
				剣のかけら振分 = 剣のかけら振分,
				弱点値上昇 = 弱点値上昇,
				先制値上昇 = 先制値上昇
			};

			return result;
		}
		/// <summary>
		/// NULL非許容の項目を空文字で埋めます
		/// </summary>
		public void 初期化()
		{
			タグ = "";
			コア部位 = "";
			分類 = "";
			反応 = "";
			名前 = "";
			名称 = "";
			弱点 = "";
			戦利品 = "";
			特殊能力 = "";
			生息地 = "";
			知能 = "";
			知覚 = "";
			解説 = "";
			言語 = "";
			部位数内訳 = "";
			移動速度 = "";
		}
	}
	public partial class 魔物部位
	{
		public int 剣のかけら個数 { get; set; }
		public TD瞬間打撃点分類 瞬間打撃点 { get; set; }
		public int 瞬間打撃点回数 { get; set; }
		public TD瞬間防護点分類 瞬間防護点 { get; set; }
		public TD瞬間達成値分類 瞬間達成値 { get; set; }
		public TD追加攻撃分類 追加攻撃 { get; set; }
		public TD呪いの波動分類 呪いの波動 { get; set; }
		public TD世界の汚染分類 世界の汚染 { get; set; }

		public 魔物部位 Clone()
		{
			魔物部位 result = new 魔物部位()
			{
				Id = Id,
				魔物Id = 魔物Id,
				No = No,
				コア部位 = コア部位,
				部位名 = 部位名,
				攻撃方法 = 攻撃方法,
				命中力 = 命中力,
				打撃点 = 打撃点,
				回避力 = 回避力,
				防護点 = 防護点,
				HP = HP,
				MP = MP,
				剣のかけら個数 = 剣のかけら個数,
				瞬間打撃点 = 瞬間打撃点,
				瞬間打撃点回数 = 瞬間打撃点回数,
				瞬間防護点 = 瞬間防護点,
				瞬間達成値 = 瞬間達成値,
				追加攻撃 = 追加攻撃,
				呪いの波動 = 呪いの波動,
				世界の汚染 = 世界の汚染
			};
			return result;
		}
	}
}
