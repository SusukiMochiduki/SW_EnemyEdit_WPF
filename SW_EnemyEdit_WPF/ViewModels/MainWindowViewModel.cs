using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_EnemyEdit_WPF.ViewModels
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private ObservableCollection<魔物> _魔物List;
		public ObservableCollection<魔物> 魔物List
		{
			get {
				return _魔物List;
			}
			set
			{
				_魔物List = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("魔物List"));
			}
		}

		public MainWindowViewModel()
		{
			this.魔物List = new ObservableCollection<魔物>();
		}
	}
}
