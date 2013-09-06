using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysPrivilegeRepository:Repository<SysPrivilege>
    {
        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysPrivilegeExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysPrivilegeAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysPrivilegeGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysPrivilegeGetModel;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysPrivilegeUpdate;
            }
        }

        public override string DeleteProc
        {
            get
            {
                return Constant.ProcSysPrivilegeDelete;
            }
        }
    }
}
