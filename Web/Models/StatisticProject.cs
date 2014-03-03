using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Infrastructure.Crosscutting.Security.Model;

namespace Web.Models
{
    public class StatisticProject
    {
        [DisplayName("存档登记号")]
        public string SysId { get; set; }

        [DisplayName("项目信息")]
        public string ProjectName { get; set; }

        [DisplayName("所属单位")]
        public string ProjectOwner { get; set; }

        [DisplayName("存档资料")]
        public string ProjectData { get; set; }

        [DisplayName("办理资料")]
        public string ProjectCases { get; set; }

        [DisplayName("办理时间")]
        public DateTime CreateTime { get; set; }

        [DisplayName("状态")]
        public string ProjectState { get; set; }

        [DisplayName("备注")]
        public string Remark { get; set; }
    }
}