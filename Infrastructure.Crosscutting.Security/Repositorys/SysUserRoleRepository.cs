﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysUserRoleRepository:Repository<SysUserRole>
    {
        #region 存储过程名
        
        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysUserRoleExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysUserRoleAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysUserRoleGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysUserRoleGetModel;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysUserRoleUpdate;
            }
        }

        public override string DeleteProc
        {
            get
            {
                return Constant.ProcSysUserRoleDelete;
            }
        }

        #endregion
    }
}
