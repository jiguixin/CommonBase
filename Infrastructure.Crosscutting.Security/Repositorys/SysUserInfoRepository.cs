using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.SqlImple;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public class SysUserInfoRepository:Repository<SysUserInfo>
    {
        public SysUserInfoRepository()
            : base(InstanceLocator.Current.GetInstance<ISql>("SysUserInfoSql"))
        {
            
        }
        #region 属性
          
        public override string TableName
        {
            get { return Constant.TableSysUserInfo; }
        }

        #endregion
    }
}
