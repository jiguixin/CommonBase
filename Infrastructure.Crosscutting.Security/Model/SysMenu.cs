/*
* 名称：SysMenu
* 功能：
* 创建人：吉桂昕
* 创建时间：2013/9/4 17:58:14
* 修改时间：
* 备注：
*/
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Crosscutting.Security.Model
{
    using System.Collections.Generic;

    /// <summary>
	/// 菜单
	/// </summary>
	[Serializable]
	public partial class SysMenu:EntityBase
	{
		public SysMenu()
		{}
		#region Model

//        [DisplayName("菜单编号,该编号会用在Sys_Privilege中PrivilegeAccessKey中")]
//        [Required(ErrorMessage = "不能为空")]
//        [StringLength(50, ErrorMessage = "长度不可超过50")]
//		public string SysId { get; set; }

        [DisplayName("对应的父菜单编号")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public string MenuParentId { get; set; }

        [DisplayName("显示顺序")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
		public int? MenuOrder { get; set; }

		[DisplayName("菜单名")]
        [StringLength(20, ErrorMessage = "长度不可超过20")]
		public string MenuName { get; set; }

        [DisplayName("菜单链接")]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
		public string MenuLink { get; set; }

        [DisplayName("菜单图标")]
        [StringLength(100, ErrorMessage = "长度不可超过100")]
		public string MenuIcon { get; set; }

        [DisplayName("菜单是否可见")]
		public long? IsVisible { get; set; }

		[DisplayName("是否为叶子菜单")]
		public long? IsLeaf { get; set; }

        [DisplayName("该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
		public string RecordStatus { get; set; }


        #endregion Model

        public IEnumerable<SysButton> Buttons { get; set; }
	}
}

