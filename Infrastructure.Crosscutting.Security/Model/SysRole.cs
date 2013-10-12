/*
* 名称：SysRole
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
    using System.Collections.Generic;

    /// <summary>
	/// 角色
	/// </summary>
	[Serializable]
    public partial class SysRole : EntityBase
	{
		public SysRole()
		{}
		#region Model
		/// <summary>
		/// 角色编号
		/// </summary>
		//public string SysId { get; set; }

        [DisplayName("角色名")]
        [StringLength(20, ErrorMessage = "长度不可超过20")]
		public string RoleName { get; set; }

        [DisplayName("角色描述")]
        [StringLength(150, ErrorMessage = "长度不可超过150")]
		public string RoleDesc { get; set; }

        [DisplayName("该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
		public string RecordStatus { get; set; }

		#endregion Model

        public IEnumerable<SysUser> Users { get; set; }


	}
}

