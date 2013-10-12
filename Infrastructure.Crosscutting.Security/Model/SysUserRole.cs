/*
* 名称：SysUserRole
* 功能：
* 创建人：吉桂昕
* 创建时间：2013/9/4 17:58:15
* 修改时间：
* 备注：
*/
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Crosscutting.Security.Model
{
	/// <summary>
	/// 用户角色
	/// </summary>
	[Serializable]
    public partial class SysUserRole : EntityBase
	{
		public SysUserRole()
		{}
		#region Model
		/// <summary>
		/// 用户角色编号
		/// </summary>
		//public string SysId { get; set; }

        [DisplayName("用户编号")]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
		public string UserId { get; set; }

        [DisplayName("角色编号")]
        [Required(ErrorMessage = "不能为空")]
		public string RoleId { get; set; }

		#endregion Model

	}
}

