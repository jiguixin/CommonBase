/*
* 名称：SysButton
* 功能：
* 创建人：吉桂昕
* 创建时间：2013/9/4 17:58:13
* 修改时间：
* 备注：
*/
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [DisplayName("菜单系统编号")]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
		public string MenuId { get; set; }
          
        [DisplayName("按钮名")]
        [StringLength(10, ErrorMessage = "长度不可超过10")]
        public string BtnName { get; set; }

        [DisplayName("图标")]
        [StringLength(10, ErrorMessage = "长度不可超过10")]
		public string BtnIcon { get; set; }

        [DisplayName("按钮显示顺序")]
        [Range(0, 2147483646, ErrorMessage = "数值超出范围")]
        public int? BtnOrder { get; set; }

        [DisplayName("按钮执行方法")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public string BtnFunction { get; set; }

        [DisplayName("该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
        public string RecordStatus { get; set; }

		#endregion Model

        public SysMenu Menu { get; set; }

	}
}

