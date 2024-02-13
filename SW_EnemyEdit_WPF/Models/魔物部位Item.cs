using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SW_EnemyEdit_WPF.Models
{
	[Table("魔物部位")]
	public class 魔物部位Item
	{
		[Key]
		[Required]
		public int Id { set; get; }
		[Required]
		public int 魔物Id { set; get; }
		[Required]
		public int No { set; get; }
		[Required]
		public string 攻撃方法 { set; get; }
		[Required]
		public int 命中力 { set; get; }
		[Required]
		public int 打撃点 { set; get; }
		[Required]
		public int 回避力 { set; get; }
		[Required]
		public int 防護点 { set; get; }
		[Required]
		public int HP { set; get; }
		[Required]
		public int MP { set; get; }
		public 魔物部位Item()
		{
			攻撃方法 = "";
		}

		internal 魔物部位Item Clone()
		{
			魔物部位Item newItem = new 魔物部位Item {
				Id = this.Id,
				魔物Id = this.魔物Id,
				No = this.No,
				攻撃方法 = this.攻撃方法,
				命中力 = this.命中力,
				打撃点 = this.打撃点,
				回避力 = this.回避力,
				HP = this.HP,
				MP = this.MP
			};

			return newItem;
		}
	}
}
