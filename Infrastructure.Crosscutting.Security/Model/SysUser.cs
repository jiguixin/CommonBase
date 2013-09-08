/*
* 名称：SysUser
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
	/// 用户
	/// </summary>
	[Serializable]
	public partial class SysUser
	{
		public SysUser()
		{}
		#region Model
		/// <summary>
		/// 用户编号
		/// </summary>
		public string SysId { get; set; }

		/// <summary>
		/// 用户名
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		public string UserPwd { get; set; }

		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 最后一次登录时间
		/// </summary>
		public DateTime? LastLogin { get; set; }

		/// <summary>
		/// 该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录
		/// </summary>
		public string RecordStatus { get; set; }

        /// <summary>
        /// 用户的详细信息
        /// </summary>
        public SysUserInfo UserInfo { get; set; }

		#endregion Model

	}
}

