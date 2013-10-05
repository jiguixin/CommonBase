using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.SqlImple;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysDataPrivilegeRepository:Repository<SysDataPrivilege>
    {
        public SysDataPrivilegeRepository()
            : base(InstanceLocator.Current.GetInstance<ISql>("SysDataPrivilegeSql"))
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
