/*
 *名称：RepositoryFactory
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-13 17:40:33
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Collections.Generic;
    using System.Runtime.Remoting.Messaging;

    using Infrastructure.Crosscutting.Security.Core;
    using Infrastructure.Crosscutting.Security.Ioc;
    using Infrastructure.Crosscutting.Security.SqlImple;

    public static class RepositoryFactory
    {
        static RepositoryFactory()
        { 

        }

        //todo: 这样直接实例化不是太好，不利于扩展

        public static readonly SysConfigRepository ConfigRepository = new SysConfigRepository(InstanceLocator.Current.GetInstance<ISql>("SysConfigBaseImple"));


        public static readonly SysUserRepository UserRepository = new SysUserRepository(InstanceLocator.Current.GetInstance<ISql>("SysUserBaseImple"));

        public static readonly SysUserInfoRepository UserInfoRepository = new SysUserInfoRepository(InstanceLocator.Current.GetInstance<ISql>("SysUserInfoBaseImple"));

        public static readonly SysUserRoleRepository UserRoleRepository = new SysUserRoleRepository(InstanceLocator.Current.GetInstance<ISql>("SysUserRoleBaseImple"));

        public static readonly SysRoleRepository RoleRepository = new SysRoleRepository(InstanceLocator.Current.GetInstance<ISql>("SysRoleBaseImple"));

        public static readonly SysDataPrivilegeRepository DataPrivilegeRepository = new
SysDataPrivilegeRepository(InstanceLocator.Current.GetInstance<ISql>("SysDataPrivilegeBaseImple"));

        public static readonly SysButtonRepository ButtonRepository = new SysButtonRepository(InstanceLocator.Current.GetInstance<ISql>("SysButtonBaseImple"));

        public static readonly SysMenuRepository MenuRepository = new SysMenuRepository(InstanceLocator.Current.GetInstance<ISql>("SysMenuBaseImple"));

        public static readonly SysPrivilegeRepository PrivilegeRepository = new SysPrivilegeRepository(InstanceLocator.Current.GetInstance<ISql>("SysPrivilegeBaseImple")); 

       
        

        //public static readonly SysUserRepository UserRepository = new SysUserRepository();
        //public static readonly SysUserInfoRepository UserInfoRepository = new SysUserInfoRepository();
        //public static readonly SysUserRoleRepository UserRoleRepository = new SysUserRoleRepository();
        //public static readonly SysRoleRepository RoleRepository = new SysRoleRepository();
        //public static readonly SysDataPrivilegeRepository DataPrivilegeRepository = new SysDataPrivilegeRepository();
        //public static readonly SysButtonRepository ButtonRepository = new SysButtonRepository();
        //public static readonly SysMenuRepository MenuRepository = new SysMenuRepository();
        //public static readonly SysPrivilegeRepository PrivilegeRepository = new SysPrivilegeRepository();
        //public static readonly SysConfigRepository ConfigRepository = new SysConfigRepository(); 
         
    }
}