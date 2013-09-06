/*
* 名称：SysRole
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
	/// 角色
	/// </summary>
	[Serializable]
	public partial class SysRole
	{
		public SysRole()
		{}
		#region Model
		/// <summary>
		/// 角色编号
		/// </summary>
		public string SysId { get; set; }

		/// <summary>
		/// 角色名
		/// </summary>
		public string RoleName { get; set; }

		/// <summary>
		/// 角色描述
		/// </summary>
		public string RoleDesc { get; set; }

		/// <summary>
		/// 该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录
		/// </summary>
		public string RecordStatus { get; set; }

		#endregion Model

	}
}

