using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysButtonRepository:Repository<SysButton>
    {
        public override string ExistsProc
        {
            get
            {
                return Constant.ProcSysButtonExists;
            }
        }

        public override string AddProc
        {
            get
            {
                return Constant.ProcSysButtonAdd;
            }
        }

        public override string GetListProc
        {
            get
            {
                return Constant.ProcSysButtonGetList;
            }
        }

        public override string GetModelProc
        {
            get
            {
                return Constant.ProcSysButtonGetModel;
            }
        }

        public override string UpdateProc
        {
            get
            {
                return Constant.ProcSysButtonUpdate;
            }
        }

        public override string DeleteProc
        {
            get
            {
                return Constant.ProcSysButtonDelete;
            }
        }
    }
}
