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
using System.Windows.Shapes;

namespace SW_EnemyEdit_WPF
{
	/// <summary>
	/// 魔物出力.xaml の相互作用ロジック
	/// </summary>
	public partial class 魔物出力 : Window
	{
		public 魔物出力ViewModel ViewModel { get; set; }
		public List<魔物> 全魔物 { get; set; }
		public 魔物出力()
		{
			InitializeComponent();
			this.ViewModel = new 魔物出力ViewModel();
			this.DataContext = this.ViewModel;
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

			using (var context = new DatabaseEntities())
			{
				var data = context.魔物
					?.Where(x=>x.SW25 == true)
					?.OrderBy(x => x.名称)
					?.OrderBy(x => x.分類)
					?.OrderBy(x => x.LV)
					?.ToList();
				全魔物 = data ?? new List<魔物>();
				foreach (var v in this.全魔物)
				{
					//魔物部位の読み込み
					v.魔物部位
						.OrderBy(x=>x.No)
						.ToList();
				}
				this.ViewModel.全魔物 = new ObservableCollection<魔物>(this.全魔物);
			}
		}

		private void Button_ClickAdd(object sender, RoutedEventArgs e)
		{
			if(DataGrid全魔物.SelectedItem != null)
			{
				var data = DataGrid全魔物.SelectedItem as 魔物;
				var clone = data.Clone();
				ViewModel.ピックアップ魔物.Add(clone);
			}
		}
		private void Button_ClickRemove(object sender, RoutedEventArgs e)
		{
			if (DataGridピックアップ魔物.SelectedItem != null)
			{
				var data = DataGrid全魔物.SelectedItem as 魔物;
				ViewModel.ピックアップ魔物.Remove(data);
			}
		}
		
		private void Button検索_Click(object sender, RoutedEventArgs e)
		{
			IEnumerable<魔物> temp = this.全魔物;

			if (checkBoxSW25.IsChecked.Value)
			{
				temp = temp.Where(x => x.SW25 == true);
			}
			if (checkBoxSW20.IsChecked.Value)
			{
				temp = temp.Where(x => x.SW20 == true);
			}
			if (!string.IsNullOrWhiteSpace(TextBox検索名前.Text))
			{
				temp = temp.Where(x => x.名称.Contains(TextBox検索名前.Text));
			}
			if (!string.IsNullOrWhiteSpace(TextBox検索分類.Text))
			{
				temp = temp.Where(x => x.分類.Contains(TextBox検索分類.Text));
			}
			if (!string.IsNullOrWhiteSpace(TextBox検索タグ.Text))
			{
				temp = temp.Where(x => x.名称.Contains(TextBox検索タグ.Text));
			}

			this.ViewModel.全魔物 = new ObservableCollection<魔物>(temp.ToList());
		}

		private void TextBoxOutput_GotFocus(object sender, RoutedEventArgs e)
		{
			var box = sender as TextBox;
			box.SelectAll();
		}

		private void ButtonOutput_Click(object sender, RoutedEventArgs e)
		{
			if( ComboBoxOutput.SelectedItem == null ) {
				return;
			}
			出力分類 分類 = (出力分類)ComboBoxOutput.SelectedItem;
			TextBoxOutput.Text = MonsterTextCreater.Create(this.ViewModel.ピックアップ魔物, 分類, checkBoxOutputSW25.IsChecked.Value);
		}

		private void DataGrid全魔物_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Key.F1 && DataGrid全魔物.SelectedItem != null)
			{
				魔物 m = DataGrid全魔物.SelectedItem as 魔物;
				var window = new 魔物編集Window(m.Id, true);
				window.ShowDialog();
			}
		}
		
		private void DataGridピックアップ魔物_KeyDown(object sender, KeyEventArgs e)
		{
			if( DataGridピックアップ魔物.SelectedItem == null ) {
				return;
			}
			if (e.Key == Key.F1)
			{
				魔物 m = DataGridピックアップ魔物.SelectedItem as 魔物;
				魔物 copy = m.Clone();
				var window = new 魔物編集Window(copy);
				window.ShowDialog();
				if (window.IsOK)
				{
					foreach(var v in copy.魔物部位)
					{
						m.魔物部位.Single(x => x.Id == v.Id).SetTPInfo(v);
					}
					m.剣のかけら個数 = copy.魔物部位.Sum(x => x.剣のかけら個数);
					m.剣のかけら振分 = 剣のかけら振分分類.任意;
				}
				this.ViewModel.ピックアップ魔物 = new ObservableCollection<魔物>(this.ViewModel.ピックアップ魔物);
			}
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.F1)
			{
				if (DataGrid全魔物.IsFocused && DataGrid全魔物.SelectedItem != null)
				{
					魔物 m = DataGrid全魔物.SelectedItem as 魔物;
					var window = new 魔物編集Window(m.Id, true);
					window.ShowDialog();
				}
				else if (DataGridピックアップ魔物.IsFocused && DataGridピックアップ魔物.SelectedItem != null)
				{
					魔物 m = DataGridピックアップ魔物.SelectedItem as 魔物;
					var window = new 魔物編集Window(m);
					var dr = window.ShowDialog();
				}
			}
		}
	}
}
