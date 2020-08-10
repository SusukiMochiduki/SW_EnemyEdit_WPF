using SW_EnemyEdit_WPF.Models;
using SW_EnemyEdit_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SW_EnemyEdit_WPF
{
	/// <summary>
	/// MainWindow.xaml の相互作用ロジック
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindowViewModel ViewModel { get; set; }
		public MainWindow()
		{
			InitializeComponent();
			this.ViewModel = new MainWindowViewModel();
			this.DataContext = this.ViewModel;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			Reload();
		}

		private void Button編集_Click(object sender, RoutedEventArgs e)
		{ 
			if(MainDataGrid.SelectedItem != null)
			{
				var item = (魔物)MainDataGrid.SelectedItem;

				魔物編集Window window = new 魔物編集Window(item.Id);
				window.ShowDialog();
				if (window.IsOK)
				{
					Reload();
				}
			}
		}

		private void Reload()
		{
			using (var context = new DatabaseEntities())
			{
				IQueryable<魔物> data = context.魔物;
				if (CheckBoxFilterSW20.IsChecked.Value)
				{
					data = data.Where(x => x.SW20 == true);
				}
				if (CheckBoxFilterSW25.IsChecked.Value)
				{
					data = data.Where(x => x.SW25 == true);
				}
				魔物分類 Filter魔物分類 = (魔物分類)(ComboBox魔物分類.SelectedItem);
				if(Filter魔物分類 != 魔物分類.なし)
				{
					data = data.Where(x => x.分類 == Filter魔物分類.ToString());
				}

				data = data.OrderBy(x => x.分類);
				表示並び順分類 ソート順 = (表示並び順分類)(ComboBoxSort.SelectedItem);
				switch (ソート順)
				{
					case 表示並び順分類.ID:
						data = data.OrderBy(x => x.Id);
						break;
					case 表示並び順分類.レベル:
						data = data.OrderBy(x => x.LV);
						break;
				}
				
				this.ViewModel.魔物List = new ObservableCollection<魔物>(data.ToList());
				foreach (var v in data)
				{
					//魔物部位の読み込み
					v.魔物部位.OrderBy(x => x.No).ToList();
				}
			}
			MainDataGrid.Columns.Single(x => x.Header.ToString() == nameof(魔物.剣のかけら振分)).Visibility = Visibility.Hidden;
			MainDataGrid.Columns.Single(x => x.Header.ToString() == nameof(魔物.剣のかけら個数)).Visibility = Visibility.Hidden;
			MainDataGrid.Columns.Single(x => x.Header.ToString() == nameof(魔物.強さ変動)).Visibility = Visibility.Hidden;
			MainDataGrid.Columns.Single(x => x.Header.ToString() == nameof(魔物.弱点値上昇)).Visibility = Visibility.Hidden;
			MainDataGrid.Columns.Single(x => x.Header.ToString() == nameof(魔物.先制値上昇)).Visibility = Visibility.Hidden;
			MainDataGrid.Columns.Single(x => x.Header.ToString() == nameof(魔物.魔物部位)).Visibility = Visibility.Hidden;
			MainDataGrid.Columns.Single(x => x.Header.ToString() == nameof(魔物.戦利品)).Visibility = Visibility.Hidden;
			MainDataGrid.Columns.Single(x => x.Header.ToString() == nameof(魔物.特殊能力)).Visibility = Visibility.Hidden;
			MainDataGrid.Columns.Single(x => x.Header.ToString() == nameof(魔物.解説)).Visibility = Visibility.Hidden;

			MainDataGrid.Columns.Single(x => x.Header.ToString() == nameof(魔物.言語)).MaxWidth = 100;
		}

		private void Button削除_Click(object sender, RoutedEventArgs e)
		{
			if (MainDataGrid.SelectedItem != null)
			{
				var item = (魔物)MainDataGrid.SelectedItem;

				MessageBoxResult result = MessageBox.Show("本当に削除してもよろしいですか？", "", 
					MessageBoxButton.OKCancel, 
					MessageBoxImage.Question, 
					MessageBoxResult.Cancel);
				if(result == MessageBoxResult.Cancel)
				{
					return;
				}

				using (var context = new DatabaseEntities())
				{
					var hoge = context.魔物.Single(x => x.Id == item.Id);
					this.ViewModel.魔物List.Remove(item);
					context.魔物.Remove(hoge);

					var items = context.魔物部位
						.Where(x => x.魔物Id == item.Id)
						.ToList();

					foreach(var v in items)
					{
						context.魔物部位.Remove(v);
					}
					context.SaveChanges();
				}
			}
		}
		private void Button作成_Click(object sender, RoutedEventArgs e)
		{
			魔物分類 新規作成魔物分類 = (魔物分類)(ComboBox新規作成魔物分類.SelectedItem);
			if (新規作成魔物分類 == 魔物分類.なし)
			{
				魔物編集Window window = new 魔物編集Window();
				window.ShowDialog();
				if (window.IsOK)
				{
					window.ViewModel.魔物.魔物部位 = window.ViewModel.魔物部位List;
					this.ViewModel.魔物List.Add(window.ViewModel.魔物);
					//Reload();
				}
			}
			else
			{
				魔物 m = new 魔物();
				m.初期化();
				m.魔物部位 = new List<魔物部位>();
				m.分類 = 新規作成魔物分類.ToString();
				switch (新規作成魔物分類)
				{
					case 魔物分類.アンデッド:
						m.穢れ点 = 5;
						m.特殊能力 = "「○毒無効」「○病気無効」「○精神効果（弱）無効」";
						break;
					case 魔物分類.魔動機:
						m.特殊能力 = "「○毒無効」「○病気無効」「○精神効果無効」「○感知される」";
						break;
					case 魔物分類.魔法生物:
						m.特殊能力 = "「○毒無効」「○病気無効」「○精神効果無効」「○感知される」";
						break;
					case 魔物分類.妖精:
						m.特殊能力 = "「○正体露見＝フェアリーテイマー技能」「○ルーンフォークに対して透明」";
						break;
					default:
						break;
				}

				魔物編集Window window = new 魔物編集Window(m, true);
				window.ShowDialog();
				if (window.IsOK)
				{
					window.ViewModel.魔物.魔物部位 = window.ViewModel.魔物部位List;
					this.ViewModel.魔物List.Add(window.ViewModel.魔物);
					//Reload();
				}
			}
			
		}
		private void Buttonコピー作成_Click(object sender, RoutedEventArgs e)
		{
			if(MainDataGrid.SelectedItem == null)
			{
				return;
			}
			魔物 m = MainDataGrid.SelectedItem as 魔物;
			魔物編集Window window = new 魔物編集Window(m.Clone(), true);
			window.ShowDialog();
			if (window.IsOK)
			{
				window.ViewModel.魔物.魔物部位 = window.ViewModel.魔物部位List;
				this.ViewModel.魔物List.Add(window.ViewModel.魔物);
				//Reload();
			}
		}
		private void Buttonフィルタ更新_Click(object sender, RoutedEventArgs e)
		{
			this.Reload();
		}
		


		private void MainDataGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			Button編集_Click(null, null);
		}


		private void MainDataGrid_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.Enter)
			{
				Button編集_Click(null, null);
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.F1)
			{
				Button作成_Click(null, null);
			}
		}

		private void Buttonレブナント作成_Click(object sender, RoutedEventArgs e)
		{
			if (MainDataGrid.SelectedItem == null)
			{
				return;
			}
			魔物 m = MainDataGrid.SelectedItem as 魔物;
			魔物 clone = m.Clone();
			clone.LV += 1;
			clone.名称 += "・レブナント";
			clone.出典 += "Ⅰ,p456、" + clone.出典;
			clone.穢れ点 = 5;
			clone.知能 = "低い";
			clone.知覚 = "魔法";
			clone.反応 = "敵対的";
			clone.言語 = "なし";
			clone.生息地 = "さまざま";
			clone.知名度 = 8;
			clone.弱点値 = 14;
			clone.弱点 = "回復効果ダメージ+3点";
			clone.先制値 -= 2;
			foreach(var v in clone.魔物部位)
			{
				v.HP += 10;
				v.MP = 0;
				v.命中力 += 2;
				v.打撃点 += 2;
				v.回避力 += 2;
			}
			clone.特殊能力 = @"「○毒無効」「○病気無効」
「○精神効果無効」
「○再生＝３点」
　手番の終了時にHPが「3」点回復します。HPが0以下になるとこの能力は失われます。
" + clone.特殊能力;
			clone.解説 = @"　無念の死を遂げ、弔われなかった死体に穢れた魂が取り憑いた動く死体です。人族・蛮族・動物・幻獣がレブナントとなります。
" + clone.解説;
			clone.戦利品 = @"2-5：なし
6-10：穢れた骨（50G／赤A）
11-：穢れた頭蓋骨（300G／赤A）
" + clone.戦利品;
			魔物編集Window window = new 魔物編集Window(clone, true);
			window.ShowDialog();
			if (window.IsOK)
			{
				window.ViewModel.魔物.魔物部位 = window.ViewModel.魔物部位List;
				this.ViewModel.魔物List.Add(window.ViewModel.魔物);
				//Reload();
			}
		}
		private void Buttonハイレブナント作成_Click(object sender, RoutedEventArgs e)
		{
			if (MainDataGrid.SelectedItem == null)
			{
				return;
			}
			魔物 m = MainDataGrid.SelectedItem as 魔物;
			魔物 clone = m.Clone();
			clone.LV += 1;
			clone.名称 += "・ハイレブナント";
			clone.出典 += "Ⅱ,p413、" + clone.出典;
			clone.穢れ点 = 5;
			clone.知能 = "人間並み";
			clone.知覚 = "魔法";
			clone.反応 = "中立";
			clone.生息地 = "さまざま";
			clone.知名度 = 12;
			clone.弱点値 = 17;
			clone.弱点 = "回復効果ダメージ+3点";
			clone.先制値 -= 2;
			clone.生命抵抗力 += 2;
			clone.精神抵抗力 += 2;
			foreach (var v in clone.魔物部位)
			{
				v.HP += 20;
				v.命中力 += 2;
				v.打撃点 += 2;
				v.回避力 += 2;
			}
			clone.特殊能力 = @"「○毒無効」「○病気無効」「○精神効果（弱）無効」
「○再生＝８点」
　手番の終了時にHPが「8」点回復します。HPが0以下になるとこの能力は失われます。
" + clone.特殊能力;
			clone.解説 = @"　死者が強力な怨念や執念によって蘇った存在です。
　ハイレブナントとなるのは、レベルが7以上の存在です。人族・蛮族・動物・幻獣がハイレブナントとなります。
" + clone.解説;
			clone.戦利品 = @"自動：穢れた頭蓋骨（300G／赤A）
2-10：穢れた骨（50G／赤A）
11-：穢れた仙骨（2400G／赤S）
" + clone.戦利品;
			魔物編集Window window = new 魔物編集Window(clone, true);
			window.ShowDialog();
			if (window.IsOK)
			{
				window.ViewModel.魔物.魔物部位 = window.ViewModel.魔物部位List;
				this.ViewModel.魔物List.Add(window.ViewModel.魔物);
				//Reload();
			}
		}
		private void Buttonマギレプリカ作成_Click(object sender, RoutedEventArgs e)
		{
			if (MainDataGrid.SelectedItem == null)
			{
				return;
			}
			魔物 m = MainDataGrid.SelectedItem as 魔物;
			魔物 clone = m.Clone();
			clone.LV += 1;
			clone.出典 = "Ⅲ,p396、" + clone.出典;
			clone.穢れ点 = 0;
			clone.知能 = "命令を聞く";
			clone.知覚 = "機械";
			clone.反応 = "命令による";
			clone.言語 = "魔動機文明語";
			clone.名称 += "・マギレプリカ";
			clone.生息地 = "遺跡";
			clone.弱点 = "雷属性ダメージ+3点";
			clone.コア部位 = "なし";
			foreach (var v in clone.魔物部位)
			{
				v.HP += 5;
				v.命中力 += 1;
				v.打撃点 += 2;
				v.防護点 += 1;
			}
			clone.特殊能力 = @"「○毒無効」「○病気無効」「○精神効果無効」「○感知される」
「○機械の身体」
　刃武器から、クリティカルを受けません。
" + clone.特殊能力;
			clone.解説 = @"　マギレプリカは、動物をモデルにして魔動機として再現したものです。
" + clone.解説;
			clone.戦利品 = @"自動：鉄（20G／黒B）
2-5：粗悪な魔導部品（100G／黒白A）
6-9：魔導部品（300G／黒白A）
10-：希少な魔動部品（900G／黒白A）
";
			魔物編集Window window = new 魔物編集Window(clone, true);
			window.ShowDialog();
			if (window.IsOK)
			{
				window.ViewModel.魔物.魔物部位 = window.ViewModel.魔物部位List;
				this.ViewModel.魔物List.Add(window.ViewModel.魔物);
				//Reload();
			}
		}
	}
}
