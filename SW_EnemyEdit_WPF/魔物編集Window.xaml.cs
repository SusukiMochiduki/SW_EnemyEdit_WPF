using SW_EnemyEdit_WPF.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SW_EnemyEdit_WPF
{
	/// <summary>
	/// 魔物編集Window.xaml の相互作用ロジック
	/// </summary>
	public partial class 魔物編集Window : Window
	{
		public 魔物編集ViewModel ViewModel { get; set; }

		public bool IsOK { get; set; }
		private bool Is新規作成 { get; set; }
		private bool Isローカルデータ編集 { get; set; }
		private bool IsReadOnly { get; set; }
		private DatabaseEntities Entities { get; set; }

		public 魔物編集Window()
		{
			InitializeComponent();
			this.Entities = new DatabaseEntities();

			this.ViewModel = new 魔物編集ViewModel();
			this.DataContext = this.ViewModel;

			this.Title = "魔物作成";
			Is新規作成 = true;

			var controls = WalkControlTree(MainCanvas)
				.Where(x => x.GetType() == typeof(TextBox))
				.Select(x => x as TextBox)
				.ToList();
			foreach(var c in controls)
			{
				c.GotFocus += (sender, e) => this.Dispatcher.InvokeAsync(() => { Task.Delay(0); (sender as TextBox).SelectAll(); });
			}
		}
		public 魔物編集Window(int 魔物id, bool isReadOnly = false)
		{
			InitializeComponent();
			IsReadOnly = isReadOnly;
			this.Entities = new DatabaseEntities();

			var monster = this.Entities.魔物
				.Where(x => x.Id == 魔物id)
				.SingleOrDefault();
			var 部位 = monster.魔物部位
				.OrderBy(x => x.No)
				.ToList();

			this.ViewModel = new 魔物編集ViewModel(monster, 部位);
			this.DataContext = this.ViewModel;

			var controls = WalkControlTree(MainCanvas)
				.Where(x => x.GetType() == typeof(TextBox))
				.Select(x => x as TextBox)
				.ToList();
			foreach (var c in controls)
			{
				c.GotFocus += (sender, e) => this.Dispatcher.InvokeAsync(() => { Task.Delay(0); (sender as TextBox).SelectAll(); });
			}
			this.MainGrid.IsEnabled = !isReadOnly;
		}
		public 魔物編集Window(魔物 m, bool is新規作成 = false)
		{
			InitializeComponent();
			this.Entities = new DatabaseEntities();

			if (is新規作成)
			{
				m.魔物部位 = new List<魔物部位>();
			}
			var 部位 = m.魔物部位
				.OrderBy(x => x.No)
				.ToList();

			this.ViewModel = new 魔物編集ViewModel(m, 部位);
			this.DataContext = this.ViewModel;

			var controls = WalkControlTree(MainCanvas)
				.Where(x => x.GetType() == typeof(TextBox))
				.Select(x => x as TextBox)
				.ToList();
			foreach (var c in controls)
			{
				c.GotFocus += (sender, e) => this.Dispatcher.InvokeAsync(() => { Task.Delay(0); (sender as TextBox).SelectAll(); });
			}
			if (is新規作成)
			{
				this.Title = "魔物作成";
				Is新規作成 = true;
			}
			else
			{
				this.Title = "剣のかけら・トレジャーポイント振り分け";
				Isローカルデータ編集 = true;
				MainCanvas.IsEnabled = false;
				Grid補助たち.IsEnabled = false;
				TextBox戦利品.IsEnabled = false;
				TextBox解説.IsEnabled = false;
				TextBox特殊能力.IsEnabled = false;
				DataGrid部位.CanUserAddRows = false;
			}
		}
		private IEnumerable<DependencyObject> WalkControlTree(DependencyObject obj)
		{
			List<DependencyObject> list = new List<DependencyObject>();
			foreach (var child in LogicalTreeHelper.GetChildren(obj))
			{
				if (child is DependencyObject)
				{
					list.Add(child as DependencyObject);
					list.AddRange(WalkControlTree(child as DependencyObject));
				}
			}
			return list;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.IsOK = true;
			this.Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (IsOK)
			{
				if (Isローカルデータ編集)
				{
					//foreach (var newbui in ViewModel.魔物部位List)
					//{
					//	var bui = ViewModel.魔物.魔物部位.SingleOrDefault(x => x.Id == newbui.Id);
					//	bui.剣のかけら個数 = newbui.剣のかけら個数;
					//	bui.瞬間打撃点 = newbui.瞬間打撃点;
					//	bui.瞬間打撃点回数 = newbui.瞬間打撃点回数;
					//	bui.瞬間防護点 = newbui.瞬間防護点;
					//	bui.瞬間達成値 = newbui.瞬間達成値;
					//	bui.追加攻撃 = newbui.追加攻撃;
					//	bui.呪いの波動 = newbui.呪いの波動;
					//	bui.世界の汚染 = newbui.世界の汚染;
					//}
				}
				else if (Is新規作成)
				{
					this.Entities.魔物.Add(ViewModel.魔物);
					this.Entities.SaveChanges();
					if (this.ViewModel.魔物部位List.Count > 0)
					{
						int i = 0;
						foreach (var v in this.ViewModel.魔物部位List)
						{
							v.魔物Id = this.ViewModel.魔物.Id;
							v.No = i;
							if (v.部位名 == null) v.部位名 = "";
							if (v.攻撃方法 == null) v.攻撃方法 = "";

							i++;
						}
						this.Entities.魔物部位.AddRange(this.ViewModel.魔物部位List);
					}
					this.Entities.SaveChanges();
				}
				else
				{
					var full部位リスト = this.Entities.魔物部位
						.Where(x => x.魔物Id == this.ViewModel.Id)
						.ToList();
					foreach(var v in full部位リスト)
					{
						//削除処理
						if (v.Id == 0)
						{
							continue;
						}
						var item = this.ViewModel.魔物部位List
							.SingleOrDefault(x => x.Id == v.Id);
						if(item == null)
						{
							//リストから削除されているので、データベースからも取り除く
							this.Entities.魔物部位.Remove(v);
						}
					}
					if (this.ViewModel.魔物部位List.Count(x => x.Id == 0) > 0)
					{
						int i = 0;
						foreach (var v in this.ViewModel.魔物部位List)
						{
							//魔物Idと番号をふる
							v.魔物Id = this.ViewModel.魔物.Id;
							v.No = i;
							if( v.部位名 == null ) v.部位名 = "";
							if( v.攻撃方法 == null ) v.攻撃方法 = "";
							i++;
							if (v.Id == 0)
							{
								//追加処理
								this.Entities.魔物部位.Add(v);
							}
						}
					}
					this.Entities.SaveChanges();
				}
			}
			this.Entities.Dispose();
		}
		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			TextBox名前.IsEnabled = true;
		}
		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			TextBox名前.IsEnabled = false;
		}
		private void DataGrid部位_CurrentCellChanged(object sender, EventArgs e)
		{
			Label部位数.Content = this.ViewModel.魔物部位List.Count().ToString();

			if (this.ViewModel.魔物部位List.Any(x => x.コア部位))
			{
				string text = "";
				if (this.ViewModel.魔物部位List.Count() == 1)
				{
				}
				else if (this.ViewModel.魔物部位List.All(x => x.コア部位))
				{
					text = "全て";
				}
				else 
				{
					text = String.Join(",", this.ViewModel.魔物部位List
						.Where(x => x.コア部位)
						.Select(x => x.部位名)
						);
				}
				TextBoxコア部位.Text = text;
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.F1 && !this.IsReadOnly)
			{
				Button_OK.Focus();
				Button_Click(null, null);
			}
			else if (e.Key == Key.Escape)
			{
				this.Close();
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (Isローカルデータ編集)
			{
				var targets = new string[]{
					nameof(魔物部位.HP),
					nameof(魔物部位.MP),
					nameof(魔物部位.命中力),
					nameof(魔物部位.回避力),
					nameof(魔物部位.打撃点),
					nameof(魔物部位.攻撃方法),
					nameof(魔物部位.部位名),
					nameof(魔物部位.防護点),
					nameof(魔物部位.コア部位),
				};
				foreach(var t in targets ) {
					DataGrid部位.Columns.Single(x => x.Header.ToString() == t).IsReadOnly = true;
				}
			}
			else {
				var targets = new string[]{
					nameof(魔物部位.剣のかけら個数),
					nameof(魔物部位.瞬間打撃点),
					nameof(魔物部位.瞬間打撃点回数),
					nameof(魔物部位.瞬間防護点),
					nameof(魔物部位.瞬間達成値),
					nameof(魔物部位.追加攻撃),
					nameof(魔物部位.呪いの波動),
					nameof(魔物部位.世界の汚染),
				};
				foreach(var t in targets ) {
					DataGrid部位.Columns.Single(x => x.Header.ToString() == t).Visibility = Visibility.Hidden;
				}
			}
			foreach( var t in new string[] { nameof(魔物部位.Id), nameof(魔物部位.魔物Id), nameof(魔物部位.No), nameof(魔物部位.魔物) } ) {
				DataGrid部位.Columns.Single(x => x.Header.ToString() == t).Visibility = Visibility.Hidden;
			}
		}

		private void Button常動型_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TextBox特殊能力.Text))
			{
				TextBox特殊能力.Text += Environment.NewLine;
			}
			TextBox特殊能力.Text += "〇";
			TextBox特殊能力.Focus();
			TextBox特殊能力.Select(TextBox特殊能力.Text.Length, 0);
		}

		private void Button主動作型_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TextBox特殊能力.Text))
			{
				TextBox特殊能力.Text += Environment.NewLine;
			}
			TextBox特殊能力.Text += (ViewModel.SW25 ? "▶" : "〆");
			TextBox特殊能力.Focus();
			TextBox特殊能力.Select(TextBox特殊能力.Text.Length, 0);
		}

		private void Button補助動作型_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TextBox特殊能力.Text))
			{
				TextBox特殊能力.Text += Environment.NewLine;
			}
			TextBox特殊能力.Text += (ViewModel.SW25 ? "⏩" : "☆");
			TextBox特殊能力.Focus();
			TextBox特殊能力.Select(TextBox特殊能力.Text.Length, 0);
		}

		private void Button宣言型_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TextBox特殊能力.Text))
			{
				TextBox特殊能力.Text += Environment.NewLine;
			}
			TextBox特殊能力.Text += (ViewModel.SW25 ? "💭" : "☑");
			TextBox特殊能力.Focus();
			TextBox特殊能力.Select(TextBox特殊能力.Text.Length, 0);
		}

		private void Button条件型_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TextBox特殊能力.Text))
			{
				TextBox特殊能力.Text += Environment.NewLine;
			}
			TextBox特殊能力.Text += "▽";
			TextBox特殊能力.Focus();
			TextBox特殊能力.Select(TextBox特殊能力.Text.Length, 0);
		}

		private void Button条件選択型_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TextBox特殊能力.Text))
			{
				TextBox特殊能力.Text += Environment.NewLine;
			}
			TextBox特殊能力.Text += "▼";
			TextBox特殊能力.Focus();
			TextBox特殊能力.Select(TextBox特殊能力.Text.Length, 0);
		}

		private void Button戦闘準備型_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TextBox特殊能力.Text))
			{
				TextBox特殊能力.Text += Environment.NewLine;
			}
			TextBox特殊能力.Text += "△";
			TextBox特殊能力.Focus();
			TextBox特殊能力.Select(TextBox特殊能力.Text.Length, 0);
		}
	}
}
