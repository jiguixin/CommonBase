using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Infrastructure.Crosscutting.Security.SqlImple;
using Ninject.Modules;

namespace Web.Utility
{
    using Infrastructure.Crosscutting.Security.Core;

    public class AppModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAppContext>().To<AppSqlServerContext>().InSingletonScope().Named("AppSqlServerContext");
            //用于配置数连接的数据库
            // this.Bind<IAppContext>().To<AppOracleContext>().InSingletonScope().Named("AppOracleContext");

            this.Bind<IConnectionFactory>().To<OracleConnectionFactory>().InSingletonScope().Named("OracleConnectionFactory");
            this.Bind<IConnectionFactory>().To<SqlServerConnectionFactory>().InSingletonScope().Named("SqlServerConnectionFactory");

            this.Bind<ISql>().To<SysButtonBase>().InSingletonScope().Named("SysButtonBaseImple");
            this.Bind<ISql>().To<SysDataPrivilegeBase>().InSingletonScope().Named("SysDataPrivilegeBaseImple");
            this.Bind<ISql>().To<SysMenuBase>().InSingletonScope().Named("SysMenuBaseImple");
            this.Bind<ISql>().To<SysPrivilegeBase>().InSingletonScope().Named("SysPrivilegeBaseImple");
            this.Bind<ISql>().To<SysRoleBase>().InSingletonScope().Named("SysRoleBaseImple");
            this.Bind<ISql>().To<SysUserBase>().InSingletonScope().Named("SysUserBaseImple");
            this.Bind<ISql>().To<SysUserInfoBase>().InSingletonScope().Named("SysUserInfoBaseImple");
            this.Bind<ISql>().To<SysConfigBase>().InSingletonScope().Named("SysConfigBaseImple");
            this.Bind<ISql>().To<SysUserRoleBase>().InSingletonScope().Named("SysUserRoleBaseImple");

            //this.Bind<SysConfigBase>().ToSelf().Named("SysConfigOracle").WithConstructorArgument("appContext", c => c.Kernel.Get<IAppContext>("SysConfigOracle"));

            // this.Bind<ISql>().To<SysConfigBase>().InSingletonScope().Named("SysConfigOracle").WithConstructorArgument("appContext", InstanceLocator.Current.GetInstance<IAppContext>("AppOracleContext"));

            #region Sql Server

            /*this.Bind<ISql>().To<SysButtonSqlServer>().InSingletonScope().Named("SysButtonSql"); 
            this.Bind<ISql>().To<SysConfigSqlServer>().InSingletonScope().Named("SysConfigSql");
            this.Bind<ISql>().To<SysDataPrivilegeSqlServer>().InSingletonScope().Named("SysDataPrivilegeSql");
            this.Bind<ISql>().To<SysMenuSqlServer>().InSingletonScope().Named("SysMenuSql");
            this.Bind<ISql>().To<SysPrivilegeSqlServer>().InSingletonScope().Named("SysPrivilegeSql");
            this.Bind<ISql>().To<SysRoleSqlServer>().InSingletonScope().Named("SysRoleSql");
            this.Bind<ISql>().To<SysUserInfoSqlServer>().InSingletonScope().Named("SysUserInfoSql");
            this.Bind<ISql>().To<SysUserRoleSqlServer>().InSingletonScope().Named("SysUserRoleSql");
            this.Bind<ISql>().To<SysUserSqlServer>().InSingletonScope().Named("SysUserSql");*/

            #endregion

            #region Oracle

            /*
            this.Bind<ISql>().To<SysButtonOracle>().InSingletonScope().Named("SysButtonSql");
            this.Bind<ISql>().To<SysConfigOracle>().InSingletonScope().Named("SysConfigSql");
            this.Bind<ISql>().To<SysDataPrivilegeOracle>().InSingletonScope().Named("SysDataPrivilegeSql");
            this.Bind<ISql>().To<SysMenuOracle>().InSingletonScope().Named("SysMenuSql");
            this.Bind<ISql>().To<SysPrivilegeOracle>().InSingletonScope().Named("SysPrivilegeSql");
            this.Bind<ISql>().To<SysRoleOracle>().InSingletonScope().Named("SysRoleSql");
            this.Bind<ISql>().To<SysUserInfoOracle>().InSingletonScope().Named("SysUserInfoSql");
            this.Bind<ISql>().To<SysUserRoleOracle>().InSingletonScope().Named("SysUserRoleSql");
            this.Bind<ISql>().To<SysUserOracle>().InSingletonScope().Named("SysUserSql");
             */

            #endregion
        }
    }
}