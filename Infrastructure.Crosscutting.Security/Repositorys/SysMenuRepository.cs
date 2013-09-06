using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysMenuRepository:Repository<SysMenu>
    {
        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysMenuExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysMenuAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysMenuGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysMenuGetModel;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysMenuUpdate;
            }
        }

        public override string DeleteProc
        {
            get
            {
                return Constant.ProcSysMenuDelete;
            }
        }
    }
}
