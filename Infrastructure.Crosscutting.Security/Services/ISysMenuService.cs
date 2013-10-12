/*
 *名称：ISysMenuService
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-13 11:10:05
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Services
{
    using System.Collections.Generic;

    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;

    public interface ISysMenuService
    {
        SysMenuRepository MenuRepository { get; }
        IEnumerable<SysPrivilege> GetPrivilege(string menuId);
        IEnumerable<SysPrivilege> GetPrivilegesByUserId(string userId);
        IEnumerable<SysMenu> GetSysMenuById(string menuId);
        IEnumerable<SysMenu> GetPrivilegedSysMenuByUserId(string userId);
        IEnumerable<SysMenu> GetPrivilegedSysMenuByRoleId(string roleId);
        IEnumerable<SysMenu> GetAllMenu();
        //List<EasyUiTreeResult> GetMenusPrivilegeForRole(string roleId);
        //List<EasyUiTreeResult> GetMenusPrivilegeForUser(string userId);

    }
}