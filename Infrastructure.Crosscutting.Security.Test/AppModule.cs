/*
 *名称：AppModule
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:39:05
 *修改时间：
 *备注：
 */

using Infrastructure.Crosscutting.Security.SqlImple;
using Ninject.Modules;

namespace Infrastructure.Crosscutting.Security.Test
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