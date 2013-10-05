/*
 *名称：AppModule
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:39:05
 *修改时间：
 *备注：
 */

using System;
using Infrastructure.Crosscutting.Security.Repositorys;
using Infrastructure.Crosscutting.Security.Sql;
using Ninject.Modules;

namespace Infrastructure.Crosscutting.Security.Test
{
    public class AppModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ISql>().To<SysButtonSqlServer>().InSingletonScope().Named("SysButtonSql"); 
            this.Bind<ISql>().To<SysConfigSqlServer>().InSingletonScope().Named("SysConfigSql");
            this.Bind<ISql>().To<SysDataPrivilegeSqlServer>().InSingletonScope().Named("SysDataPrivilegeSql");
            this.Bind<ISql>().To<SysMenuSqlServer>().InSingletonScope().Named("SysMenuSql");
            this.Bind<ISql>().To<SysPrivilegeSqlServer>().InSingletonScope().Named("SysPrivilegeSql");
            this.Bind<ISql>().To<SysRoleSqlServer>().InSingletonScope().Named("SysRoleSql");
            this.Bind<ISql>().To<SysUserInfoSqlServer>().InSingletonScope().Named("SysUserInfoSql");
            this.Bind<ISql>().To<SysUserRoleSqlServer>().InSingletonScope().Named("SysUserRoleSql");
            this.Bind<ISql>().To<SysUserSqlServer>().InSingletonScope().Named("SysUserSql"); 
        }
    }
}