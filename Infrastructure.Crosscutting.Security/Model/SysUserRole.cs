/*
* 名称：SysUserRole
* 功能：
* 创建人：吉桂昕
* 创建时间：2013/9/4 17:58:15
* 修改时间：
* 备注：
*/
using System;
namespace Infrastructure.Crosscutting.Security.Model
{
	/// <summary>
	/// 用户角色
	/// </summary>
	[Serializable]
	public partial class SysUserRole
	{
		public SysUserRole()
		{}
		#region Model
		/// <summary>
		/// 用户角色编号
		/// </summary>
		public string SysId { get; set; }

		/// <summary>
		/// 用户编号
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// 角色编号
		/// </summary>
		public string RoleId { get; set; }

		#endregion Model

	}
}

