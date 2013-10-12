/*
* 名称：SysDataPrivilege
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
	/// <summary>
	/// 数据 权限
	/// </summary>
	[Serializable]
    public partial class SysDataPrivilege : EntityBase
	{
		public SysDataPrivilege()
		{}
		#region Model
		/// <summary>
		/// 数据 权限表 编号
		/// </summary>
		//public string SysId { get; set; }

        [DisplayName("数据 权限表 数据源，如:单个表，级联表")]
        [StringLength(20, ErrorMessage = "长度不可超过20")]
		public string DataPrivilegeView { get; set; }

        [DisplayName("对应数据源中的数据规则，可以是SQL语句")]
		public string DataPrivilegeRule { get; set; }

        [DisplayName("该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
		public string RecordStatus { get; set; }

		#endregion Model

	}
}

