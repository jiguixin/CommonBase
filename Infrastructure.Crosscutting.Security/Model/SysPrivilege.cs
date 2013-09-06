/*
* 名称：SysPrivilege
* 功能：
* 创建人：吉桂昕
* 创建时间：2013/9/4 17:58:14
* 修改时间：
* 备注：
*/
using System;
namespace Infrastructure.Crosscutting.Security.Model
{
	/// <summary>
	/// 功能 权限
	/// </summary>
	[Serializable]
	public partial class SysPrivilege
	{
		public SysPrivilege()
		{}
		#region Model
		/// <summary>
		/// 权限编号
		/// </summary>
		public string SysId { get; set; }

		/// <summary>
		/// 权限拥有者，如：用户、角色、部门等 类型
		/// </summary>
		public string PrivilegeMaster { get; set; }

		/// <summary>
		/// 对应权限拥有者的标识编号。如：UserId、RoleId、DepId
		/// </summary>
		public string PrivilegeMasterKey { get; set; }

		/// <summary>
		/// 能被访问的是:菜单、按钮 类型
		/// </summary>
		public string PrivilegeAccess { get; set; }

		/// <summary>
		/// 对应 菜单、按钮Id
		/// </summary>
		public string PrivilegeAccessKey { get; set; }

		/// <summary>
		/// 该权限的操作如：禁用、启用、分配、授权等权限
		/// </summary>
		public int PrivilegeOperation { get; set; }

		/// <summary>
		/// 该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录
		/// </summary>
		public string RecordStatus { get; set; }

		#endregion Model

	}
}

