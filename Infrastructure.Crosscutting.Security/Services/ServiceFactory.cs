/*
 *名称：ServiceFactory
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-16 11:25:46
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Services
{
    public static class ServiceFactory
    {
        public static readonly SysButtonService ButtonService = new SysButtonService(); 
        public static readonly SysConfigService ConfigService = new SysConfigService();
        public static readonly SysMenuService MenuService = new SysMenuService();
        public static readonly SysRoleService RoleService = new SysRoleService();
        public static readonly SysUserService UserService = new SysUserService();
        public static readonly SysPrivilegeService PrivilegeService = new SysPrivilegeService(); 

        
    }
}