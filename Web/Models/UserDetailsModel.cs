using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    using System.ComponentModel;

    public class UserDetailsModel
    {
        /// <summary>
        /// 用户编号
        /// </summary> 
        public string SysId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("用户密码")] 
        public string UserPwd { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
         [DisplayName("创建时间")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        [DisplayName("最后一次登录时间")]
        public DateTime? LastLogin { get; set; }

        /// <summary>
        /// 该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录
        /// </summary>
        public string RecordStatus { get; set; }

        /// <summary>
        /// 真实名字
        /// </summary>
        [DisplayName("真实名字")]
        public string RealName { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
         [DisplayName("职位")]
        public string Title { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")] 
        public string Sex { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [DisplayName("手机")]
        public string Phone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [DisplayName("传真")]
        public string Fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
         [DisplayName("邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// qq
        /// </summary>
        [DisplayName("QQ")]
        public string QQ { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
         [DisplayName("地址")]
        public string Address { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
         [DisplayName("角色")]
         public string Roles { get; set; }
    }
}