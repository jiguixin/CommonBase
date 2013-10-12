/*
* 名称：SysConfig
* 功能：
* 创建人：吉桂昕
* 创建时间：2013/9/4 17:58:13
* 修改时间：
* 备注：
*/
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [DisplayName("系统配置Key")]
        [Required(ErrorMessage = "不能为空")]
        [StringLength(20, ErrorMessage = "长度不可超过20")]
		public string SysKey { get; set; }

        [DisplayName("对应KEY的值")]
        [StringLength(2000, ErrorMessage = "长度不可超过2000")]
		public string SysValue { get; set; }

        [DisplayName("父结点编号")]
        [StringLength(50, ErrorMessage = "长度不可超过50")]
		public string SysParentId { get; set; }

        [DisplayName("该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录")]
        [StringLength(200, ErrorMessage = "长度不可超过200")]
		public string RecordStatus { get; set; }

		#endregion Model

	}
}

