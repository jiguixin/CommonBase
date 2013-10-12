/*
* 名称：SysPrivilege
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
    using Infrastructure.Crosscutting.Security.Common;

    /// <summary>
	/// 功能 权限
	/// </summary>
	[Serializable]
    public partial class SysPrivilege : EntityBase
	{
		public SysPrivilege()
		{}
		#region Model
		/// <summary>
		/// 权限编号
		/// </summary>
		//public string SysId { get; set; }

        [DisplayName("权限拥有者，如：用户、角色、部门等 类型")]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public PrivilegeMaster PrivilegeMaster { get; set; }

        [DisplayName("对应权限拥有者的标识编号。如：UserId、RoleId、DepId")]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
		public string PrivilegeMasterKey { get; set; }

        [DisplayName("能被访问的是:菜单、按钮 类型")]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
        public PrivilegeAccess PrivilegeAccess { get; set; }

        [DisplayName("对应 菜单、按钮Id")]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
		public string PrivilegeAccessKey { get; set; }

        [DisplayName("该权限的操作如：禁用、启用、分配、授权等权限")]
        [Required(ErrorMessage = "不能为空")]
        public PrivilegeOperation PrivilegeOperation { get; set; }

        [DisplayName("该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
		public string RecordStatus { get; set; }

		#endregion Model

	}
}

