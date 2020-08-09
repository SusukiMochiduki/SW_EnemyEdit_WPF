using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_EnemyEdit_WPF
{
	public class 魔物出力ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private ObservableCollection<魔物> _魔物List;
		public ObservableCollection<魔物> 全魔物
		{
			get
			{
				return _魔物List;
			}
			set
			{
				_魔物List = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("全魔物"));
			}
		}
		private ObservableCollection<魔物> _ピックアップ魔物;
		public ObservableCollection<魔物> ピックアップ魔物
		{
			get
			{
				return _ピックアップ魔物;
			}
			set
			{
				_ピックアップ魔物 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("ピックアップ魔物"));
			}
		}

		public 魔物出力ViewModel()
		{
			this.全魔物 = new ObservableCollection<魔物>();
			this.ピックアップ魔物 = new ObservableCollection<魔物>();
		}
	}
}
