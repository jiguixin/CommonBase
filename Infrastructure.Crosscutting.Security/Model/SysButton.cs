/*
* 名称：SysButton
* 功能：
* 创建人：吉桂昕
* 创建时间：2013/9/4 17:58:13
* 修改时间：
* 备注：
*/
using System;
namespace Infrastructure.Crosscutting.Security.Model
{
	/// <summary>
	/// 按钮
	/// </summary>
	[Serializable]
	public partial class SysButton:EntityBase
	{
		public SysButton()
		{}

		#region Model
		/// <summary>
        /// 按钮编号,该编号会用在Sys_Privilege中PrivilegeAccessKey中
		/// </summary>
		//public string SysId { get; set; }

		/// <summary>
		/// 菜单系统编号
		/// </summary>
		public string MenuId { get; set; }
          
		/// <summary>
		/// 按钮名
		/// </summary>
		public string BtnName { get; set; }
          
		/// <summary>
		/// 按钮图标
		/// </summary>
		public string BtnIcon { get; set; }

		/// <summary>
		/// 按钮显示顺序
		/// </summary>
		public int? BtnOrder { get; set; }

		/// <summary>
		/// 该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录
		/// </summary>
		public string RecordStatus { get; set; }

		#endregion Model

        public SysMenu Menu { get; set; }

	}
}

