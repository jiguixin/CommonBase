using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.SqlImple;

namespace Infrastructure.Crosscutting.Security.Repositorys
{ 
    public class SysUserRoleRepository:Repository<SysUserRole>
    {
        public SysUserRoleRepository()
            : base(InstanceLocator.Current.GetInstance<ISql>("SysUserRoleSql"))
        {
            
        }

        #region 属性
           
        public override string TableName
        {
            get { return Constant.TableSysUserRole; }
        }

        #endregion

        public  int DeleteByUserId(string sysId, System.Data.IDbTransaction trans)
        { 
            return base.DeleteByWhere(string.Format("UserId='{0}'",sysId), trans);
        }

        public int DeleteByRoleId(string sysId, System.Data.IDbTransaction trans)
        {
            return base.DeleteByWhere(string.Format("RoleId='{0}'", sysId), trans);
        }
    }
}
