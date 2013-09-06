using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysDataPrivilegeRepository:Repository<SysDataPrivilege>
    {
        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysDataPrivilegeExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysDataPrivilegeAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysDataPrivilegeGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysDataPrivilegeGetModel;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysDataPrivilegeUpdate;
            }
        }

        public override string DeleteProc
        {
            get
            {
                return Constant.ProcSysDataPrivilegeDelete;
            }
        }
    }
}
