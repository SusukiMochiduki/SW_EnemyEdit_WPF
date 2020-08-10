using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW_EnemyEdit_WPF.ViewModels
{
	public class 魔物編集ViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private List<魔物部位> _魔物部位List;
		public List<魔物部位> 魔物部位List
		{
			get
			{
				return _魔物部位List;
			}
			set
			{
				_魔物部位List = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("魔物部位List"));
			}
		}
		private 魔物 _魔物;
		public 魔物 魔物
		{
			get
			{
				return _魔物;
			}
			set
			{
				_魔物 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("魔物"));
			}
		}

		public int Id
		{
			get
			{
				return 魔物.Id;
			}
			set
			{
				魔物.Id = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("Id"));
			}
		}
		public bool オリジナル
		{
			get
			{
				return 魔物.オリジナル;
			}
			set
			{
				魔物.オリジナル = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("オリジナル"));
			}
		}
		public bool SW25
		{
			get
			{
				return 魔物.SW25;
			}
			set
			{
				魔物.SW25 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("SW25"));
			}
		}
		public bool SW20
		{
			get
			{
				return 魔物.SW20;
			}
			set
			{
				魔物.SW20 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("SW20"));
			}
		}
		public string 出典
		{
			get
			{
				return 魔物.出典;
			}
			set
			{
				魔物.出典 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("出典"));
			}
		}
		public string タグ
		{
			get
			{
				return 魔物.タグ;
			}
			set
			{
				魔物.タグ = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("タグ"));
			}
		}
		public string 分類
		{
			get
			{
				return 魔物.分類;
			}
			set
			{
				魔物.分類 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("魔物分類"));
			}
		}
		public int LV
		{
			get
			{
				return 魔物.LV;
			}
			set
			{
				魔物.LV = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("LV"));
			}
		}
		public bool ネームド
		{
			get
			{
				return 魔物.ネームド;
			}
			set
			{
				魔物.ネームド = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("ネームド"));
			}
		}
		public string 名前
		{
			get
			{
				return 魔物.名前;
			}
			set
			{
				魔物.名前 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("名前"));
			}
		}
		public string 名称
		{
			get
			{
				return 魔物.名称;
			}
			set
			{
				魔物.名称 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("名称"));
			}
		}
		public string 知能
		{
			get
			{
				return 魔物.知能;
			}
			set
			{
				魔物.知能 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("知能"));
			}
		}
		public string 知覚
		{
			get
			{
				return 魔物.知覚;
			}
			set
			{
				魔物.知覚 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("知覚"));
			}
		}
		public string 反応
		{
			get
			{
				return 魔物.反応;
			}
			set
			{
				魔物.反応 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("反応"));
			}
		}
		public string 言語
		{
			get
			{
				return 魔物.言語;
			}
			set
			{
				魔物.言語 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("言語"));
			}
		}
		public string 生息地
		{
			get
			{
				return 魔物.生息地;
			}
			set
			{
				魔物.生息地 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("魔物.生息地"));
			}
		}
		public int 知名度
		{
			get
			{
				return 魔物.知名度;
			}
			set
			{
				魔物.知名度 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("知名度"));
			}
		}
		public int 弱点値
		{
			get
			{
				return 魔物.弱点値;
			}
			set
			{
				魔物.弱点値 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("弱点値"));
			}
		}
		public string 弱点
		{
			get
			{
				return _魔物.弱点;
			}
			set
			{
				魔物.弱点 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("弱点"));
			}
		}
		public int 先制値
		{
			get
			{
				return 魔物.先制値;
			}
			set
			{
				魔物.先制値 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("先制値"));
			}
		}
		public string 移動速度
		{
			get
			{
				return 魔物.移動速度;
			}
			set
			{
				魔物.移動速度 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("移動速度"));
			}
		}
		public int 生命抵抗力
		{
			get
			{
				return 魔物.生命抵抗力;
			}
			set
			{
				魔物.生命抵抗力 = value;
				this.生命抵抗力固定値 = $"({value+7})";
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("生命抵抗力"));
			}
		}
		private string _生命抵抗力固定値;
		public string 生命抵抗力固定値
		{
			get
			{
				return _生命抵抗力固定値;
			}
			set
			{
				_生命抵抗力固定値 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("生命抵抗力固定値"));
			}
		}
		public int 精神抵抗力
		{
			get
			{
				return 魔物.精神抵抗力;
			}
			set
			{
				魔物.精神抵抗力 = value;
				this.精神抵抗力固定値 = $"({value + 7})";
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("精神抵抗力"));
			}
		}
		private string _精神抵抗力固定値;
		public string 精神抵抗力固定値
		{
			get
			{
				return _精神抵抗力固定値;
			}
			set
			{
				_精神抵抗力固定値 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("精神抵抗力固定値"));
			}
		}
		public int 穢れ点
		{
			get
			{
				return 魔物.穢れ点;
			}
			set
			{
				魔物.穢れ点 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("穢れ点"));
			}
		}
		public int 部位数
		{
			get
			{
				return 魔物.部位数;
			}
			set
			{
				魔物.部位数 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("部位数"));
			}
		}
		public string 部位数内訳
		{
			get
			{
				return _魔物.部位数内訳;
			}
			set
			{
				魔物.部位数内訳 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("部位数内訳"));
			}
		}
		public string コア部位
		{
			get
			{
				return 魔物.コア部位;
			}
			set
			{
				魔物.コア部位 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("コア部位"));
			}
		}
		public string 特殊能力
		{
			get
			{
				return 魔物.特殊能力;
			}
			set
			{
				魔物.特殊能力 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("特殊能力"));
			}
		}
		public string 戦利品
		{
			get
			{
				return 魔物.戦利品;
			}
			set
			{
				魔物.戦利品 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("魔物.戦利品"));
			}
		}
		public string 解説
		{
			get
			{
				return _魔物.解説;
			}
			set
			{
				魔物.解説 = value;
				var h = PropertyChanged;
				if (h != null) h(this, new PropertyChangedEventArgs("解説"));
			}
		}

		public 魔物編集ViewModel()
		{
			this.魔物 = new 魔物();
			this.魔物.初期化();
			this.魔物部位List = new List<魔物部位>();
		}
		public 魔物編集ViewModel(魔物 monster, IEnumerable<魔物部位> 部位)
		{
			this.魔物 = monster;
			this.魔物部位List = 部位.ToList();
		}
	}
}
