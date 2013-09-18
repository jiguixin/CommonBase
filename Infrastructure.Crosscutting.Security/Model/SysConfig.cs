/*
* 名称：SysConfig
* 功能：
* 创建人：吉桂昕
* 创建时间：2013/9/4 17:58:13
* 修改时间：
* 备注：
*/
using System;
namespace Infrastructure.Crosscutting.Security.Model
{
	/// <summary>
	/// 系统配置
	/// </summary>
	[Serializable]
	public partial class SysConfig:EntityBase
	{
		public SysConfig()
		{}
		#region Model
		/// <summary>
		/// 编号
		/// </summary>
		//public string SysId { get; set; }

		/// <summary>
		/// 系统配置Key
		/// </summary>
		public string SysKey { get; set; }

		/// <summary>
		/// 对应KEY的值
		/// </summary>
		public string SysValue { get; set; }

		/// <summary>
		/// 父结点编号
		/// </summary>
		public string SysParentId { get; set; }

		/// <summary>
		/// 该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录
		/// </summary>
		public string RecordStatus { get; set; }

		#endregion Model

	}
}

