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
	/// Menu.xaml の相互作用ロジック
	/// </summary>
	public partial class Menu : Window
	{
		public Menu()
		{
			InitializeComponent();
		}

		private void Button_Click編集(object sender, RoutedEventArgs e)
		{
			var window = new MainWindow();
			window.ShowDialog();
		}

		private void Button_Click作成(object sender, RoutedEventArgs e)
		{
			var window = new 魔物出力();
			window.ShowDialog();
		}
	}
}
