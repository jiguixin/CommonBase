/*
* 名称：SysDataPrivilege
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
	/// 数据 权限
	/// </summary>
	[Serializable]
	public partial class SysDataPrivilege
	{
		public SysDataPrivilege()
		{}
		#region Model
		/// <summary>
		/// 数据 权限表 编号
		/// </summary>
		public string SysId { get; set; }

		/// <summary>
		/// 数据 权限表 数据源，如:单个表，级联表
		/// </summary>
		public string DataPrivilegeView { get; set; }

		/// <summary>
		/// 对应数据源中的数据规则，可以是SQL语句
		/// </summary>
		public string DataPrivilegeRule { get; set; }

		/// <summary>
		/// 该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录
		/// </summary>
		public string RecordStatus { get; set; }

		#endregion Model

	}
}

