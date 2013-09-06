using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysRoleRepository:Repository<SysRole>
    {
        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysRoleExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysRoleAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysRoleGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysRoleGetModel;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysRoleUpdate;
            }
        }

        public override string DeleteProc
        {
            get
            {
                return Constant.ProcSysRoleDelete;
            }
        }
    }
}
