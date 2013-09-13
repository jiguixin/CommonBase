/*
* 名称：SysMenu
* 功能：
* 创建人：吉桂昕
* 创建时间：2013/9/4 17:58:14
* 修改时间：
* 备注：
*/
using System;
namespace Infrastructure.Crosscutting.Security.Model
{
    using System.Collections.Generic;

    /// <summary>
	/// 菜单
	/// </summary>
	[Serializable]
	public partial class SysMenu
	{
		public SysMenu()
		{}
		#region Model
		/// <summary>
        /// 菜单编号,该编号会用在Sys_Privilege中PrivilegeAccessKey中
		/// </summary>
		public string SysId { get; set; }
         
		/// <summary>
		/// 对应的父菜单编号
		/// </summary>
        public string MenuParentId { get; set; }

		/// <summary>
		/// 显示顺序
		/// </summary>
		public int? MenuOrder { get; set; }

		/// <summary>
		/// 菜单名
		/// </summary>
		public string MenuName { get; set; }

		/// <summary>
		/// 菜单链接
		/// </summary>
		public string MenuLink { get; set; }

		/// <summary>
		/// 菜单图标
		/// </summary>
		public string MenuIcon { get; set; }

		/// <summary>
		/// 菜单是否可见
		/// </summary>
		public long? IsVisible { get; set; }

		/// <summary>
		/// 是否为叶子菜单
		/// </summary>
		public long? IsLeaf { get; set; }

		/// <summary>
		/// 该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录
		/// </summary>
		public string RecordStatus { get; set; }

		#endregion Model

        IEnumerable<SysButton> Buttons { get; set; }
	}
}

