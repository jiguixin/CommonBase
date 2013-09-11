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
        #region 属性
         
        public override string AddProc
        {
            get
            {
                return Constant.ProcSysUserInfoAdd;
            }
        }
          
        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysUserInfoUpdate;
            }
        }

        public override string TableName
        {
            get { return Constant.TableSysUserInfo; }
        }

        #endregion
    }
}
