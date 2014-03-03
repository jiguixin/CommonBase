using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.SqlImple;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using Infrastructure.Crosscutting.Security.Core;

    public class SysDataPrivilegeRepository:Repository<SysDataPrivilege>
    { 
        public SysDataPrivilegeRepository(ISql sql)
            : base(sql)
        {
        }
        #region 属性
           
        public override string TableName
        {
            get { return Constant.TableSysDataPrivilege; }
        }

        #endregion

    }
}
