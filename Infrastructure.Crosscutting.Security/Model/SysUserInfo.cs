/*
* 名称：SysUserInfo
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
	/// 用户信息
	/// </summary>
	[Serializable]
	public partial class SysUserInfo
	{
		public SysUserInfo()
		{}
		#region Model
		/// <summary>
		/// 用户编号
		/// </summary>
		public string SysId { get; set; }

		/// <summary>
		/// 真实名字
		/// </summary>
		public string RealName { get; set; }

		/// <summary>
		/// 职位
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 性别
		/// </summary>
		public bool Sex { get; set; }

		/// <summary>
		/// 手机
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// 传真
		/// </summary>
		public string Fax { get; set; }

		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// qq
		/// </summary>
		public string QQ { get; set; }

		/// <summary>
		/// 地址
		/// </summary>
		public string Address { get; set; }

		#endregion Model

	}
}

