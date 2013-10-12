using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Infrastructure.Crosscutting.Security.SqlImple;
using Ninject.Modules;

namespace Web.Utility
{
    public class AppModule : NinjectModule
    {
        public override void Load()
        {
            #region Sql Server

            this.Bind<ISql>().To<SysButtonSqlServer>().InSingletonScope().Named("SysButtonSql");
            this.Bind<ISql>().To<SysConfigSqlServer>().InSingletonScope().Named("SysConfigSql");
            this.Bind<ISql>().To<SysDataPrivilegeSqlServer>().InSingletonScope().Named("SysDataPrivilegeSql");
            this.Bind<ISql>().To<SysMenuSqlServer>().InSingletonScope().Named("SysMenuSql");
            this.Bind<ISql>().To<SysPrivilegeSqlServer>().InSingletonScope().Named("SysPrivilegeSql");
            this.Bind<ISql>().To<SysRoleSqlServer>().InSingletonScope().Named("SysRoleSql");
            this.Bind<ISql>().To<SysUserInfoSqlServer>().InSingletonScope().Named("SysUserInfoSql");
            this.Bind<ISql>().To<SysUserRoleSqlServer>().InSingletonScope().Named("SysUserRoleSql");
            this.Bind<ISql>().To<SysUserSqlServer>().InSingletonScope().Named("SysUserSql");

            #endregion

            #region Oracle

          /*  this.Bind<ISql>().To<SysButtonOracle>().InSingletonScope().Named("SysButtonSql");
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