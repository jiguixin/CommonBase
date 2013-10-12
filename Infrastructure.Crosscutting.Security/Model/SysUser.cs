/*
* 名称：SysUser
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
    using System.Collections.Generic;

    /// <summary>
	/// 用户
	/// </summary>
	[Serializable]
    public partial class SysUser : EntityBase
	{
		public SysUser()
		{}
		#region Model
          
        [DisplayName("用户名")]
        [Required(ErrorMessage = "用户名 不能为空")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
		public string UserName { get; set; }

        [DisplayName("用户密码")]
        [Required(ErrorMessage = "用户密码 不能为空")]
        [DataType(DataType.Password)]
        [StringLength(150, ErrorMessage = "长度不可超过150")]
		public string UserPwd { get; set; }

        [DisplayName("创建时间")]
        [DataType(DataType.DateTime, ErrorMessage = "时间格式不正确")]
		public DateTime? CreateTime { get; set; }

        [DisplayName("最后一次登录时间")]
        [DataType(DataType.DateTime, ErrorMessage = "时间格式不正确")]
		public DateTime? LastLogin { get; set; }

        [DisplayName("该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
		public string RecordStatus { get; set; }

        /// <summary>
        /// 用户的详细信息
        /// </summary>
        [DisplayName("用户详情")]
        public SysUserInfo UserInfo { get; set; }
         
		#endregion Model

        public IEnumerable<SysRole> Roles { get; set; }
	}
}

