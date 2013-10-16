/*
* 名称：SysUserInfo
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
	/// <summary>
	/// 用户信息
	/// </summary>
	[Serializable]
    public partial class SysUserInfo : EntityBase
	{
		public SysUserInfo()
		{}
		#region Model
         
        [DisplayName("真实名字")]
        [StringLength(15, ErrorMessage = "长度不可超过15")]
		public string RealName { get; set; }

        [DisplayName("职位")]
        [StringLength(15, ErrorMessage = "长度不可超过15")]
		public string Title { get; set; }

        [DisplayName("性别")] 
		public bool Sex { get; set; }


        [DisplayName("手机")]
        [StringLength(20, ErrorMessage = "长度不可超过20")]
		public string Phone { get; set; }

        [DisplayName("传真")]
        [StringLength(20, ErrorMessage = "长度不可超过20")]
		public string Fax { get; set; }

        [DisplayName("邮箱")]
        [StringLength(20, ErrorMessage = "长度不可超过20")]
        [RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$",
            ErrorMessage = "请输入正确的Email格式\n示例：abc@123.com")]
		public string Email { get; set; }

        [DisplayName("QQ")]
        [StringLength(20, ErrorMessage = "长度不可超过20")]
		public string QQ { get; set; }

        [DisplayName("地址")]
        [StringLength(250, ErrorMessage = "长度不可超过250")]
		public string Address { get; set; }

		#endregion Model

	}
}

