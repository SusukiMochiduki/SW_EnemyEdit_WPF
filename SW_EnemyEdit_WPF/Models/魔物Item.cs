using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SW_EnemyEdit_WPF.Models
{
	[Table("魔物")]
	public class 魔物Item
	{
		[Key]
		[Required]
		public int Id { set; get; }
		[Required]
		public string 分類 { set; get; }
		[Required]
		public int LV { set; get; }
		[Required]
		public string 名前 { set; get; }
		[Required]
		public string 名称 { set; get; }
		[Required]
		public string 知能 { set; get; }
		[Required]
		public string 知覚 { set; get; }
		[Required]
		public string 反応 { set; get; }
		[Required]
		public string 言語 { set; get; }
		[Required]
		public string 生息地 { set; get; }
		[Required]
		public int 知名度 { set; get; }
		[Required]
		public int 弱点値 { set; get; }
		[Required]
		public string 弱点 { set; get; }
		[Required]
		public int 先制値 { set; get; }
		[Required]
		public string 移動速度 { set; get; }
		[Required]
		public int 生命抵抗力 { set; get; }
		[Required]
		public int 精神抵抗力 { set; get; }

		[Required]
		public int 穢れ点 { set; get; }
		[Required]
		public int 部位数 { set; get; }
		[Required]
		public string 部位数内訳 { set; get; }
		[Required]
		public string コア部位 { set; get; }
		[Required]
		public string 特殊能力 { set; get; }
		[Required]
		public string 戦利品 { set; get; }
		[Required]
		public string 解説 { set; get; }

		public 魔物Item()
		{
			Id			= 0;
			分類		= "";
			LV			= 0;
			名前		= "";
			名称		= "";
			知能		= "";
			知覚		= "";
			反応		= "";
			言語		= "";
			生息地		= "";
			知名度		= 0;
			弱点値		= 0;
			弱点		= "";
			先制値		= 0;
			移動速度	= "";
			生命抵抗力	= 0;
			精神抵抗力	= 0;
			穢れ点		= 0;
			部位数		= 0;
			部位数内訳	= "";
			コア部位	= "";
			特殊能力	= "";
			戦利品		= "";
			解説		= "";
		}

		internal 魔物Item Clone()
		{
			魔物Item newItem = new 魔物Item();
			newItem.Id			=Id;
			newItem.分類		=分類;
			newItem.LV			=LV;
			newItem.名前		=名前;
			newItem.名称		=名称;
			newItem.知能		=知能;
			newItem.知覚		=知覚;
			newItem.反応		=反応;
			newItem.言語		=言語;
			newItem.生息地		=生息地;
			newItem.知名度		=知名度;
			newItem.弱点値		=弱点値;
			newItem.弱点		=弱点;
			newItem.先制値		=先制値;
			newItem.移動速度	=移動速度;
			newItem.生命抵抗力	=生命抵抗力;
			newItem.精神抵抗力	=精神抵抗力;
			newItem.穢れ点		=穢れ点;
			newItem.部位数		=部位数;
			newItem.部位数内訳	=部位数内訳;
			newItem.コア部位	=コア部位;
			newItem.特殊能力	=特殊能力;
			newItem.戦利品		=戦利品;
			newItem.解説		=解説;

			return newItem;
		}
	}
}
