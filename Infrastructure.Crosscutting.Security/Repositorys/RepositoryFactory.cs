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

    public static class RepositoryFactory
    { 
        public static readonly SysUserRepository UserRepository = new SysUserRepository();
        public static readonly SysUserInfoRepository UserInfoRepository = new SysUserInfoRepository();
        public static readonly SysUserRoleRepository UserRoleRepository = new SysUserRoleRepository();
        public static readonly SysRoleRepository RoleRepository = new SysRoleRepository();
        public static readonly SysDataPrivilegeRepository DataPrivilegeRepository = new SysDataPrivilegeRepository();
        public static readonly SysButtonRepository ButtonRepository = new SysButtonRepository();
        public static readonly SysMenuRepository MenuRepository = new SysMenuRepository();
        public static readonly SysPrivilegeRepository PrivilegeRepository = new SysPrivilegeRepository();
        public static readonly SysConfigRepository ConfigRepository = new SysConfigRepository(); 
         
    }
}