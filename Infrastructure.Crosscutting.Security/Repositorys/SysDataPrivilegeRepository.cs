﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysDataPrivilegeRepository:Repository<SysDataPrivilege>
    {
        #region 属性
         
        public override string AddProc
        {
            get
            {
                return Constant.ProcSysDataPrivilegeAdd;
            }
        }
         
        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysDataPrivilegeUpdate;
            }
        }

        public override string TableName
        {
            get { return Constant.TableSysDataPrivilege; }
        }

        #endregion

    }
}
