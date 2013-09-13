using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Crosscutting.Security.Services
{
    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;

    public class SysButtonService : ISysButtonService
    {
        public SysButtonRepository ButtonRepository { get; private set; }

        public SysButtonService()
        {
            ButtonRepository = new SysButtonRepository();
        }

        public IEnumerable<SysPrivilege> GetPrivilege(string buttonId)
        {
            return ButtonRepository.GetList<SysPrivilege>(Constant.SqlTableButtonPrivilegeJoin, Constant.SqlFieldsPrivilegeJoin, string.Format("p.PrivilegeAccess={0} and b.SysId = '{1}'", (int)PrivilegeAccess.Button, buttonId));  
        }
    }
}
