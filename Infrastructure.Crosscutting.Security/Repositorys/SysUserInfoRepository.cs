using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysUserInfoRepository:Repository<SysUserInfo>
    {
        #region 存储过程名
         
        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysUserInfoExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysUserInfoAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysUserInfoGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysUserInfoGetModel;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysUserInfoUpdate;
            }
        }

        public override string DeleteProc
        {
            get
            {
                return Constant.ProcSysUserInfoDelete;
            }
        }

         #endregion
    }
}
