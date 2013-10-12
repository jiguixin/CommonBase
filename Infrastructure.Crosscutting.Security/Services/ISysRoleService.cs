/*
 *名称：ISysRoleService
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-12 04:26:30
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Services
{
    using System.Collections.Generic;

    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;

    public interface ISysRoleService
    {
        SysRoleRepository RoleRepository { get; }

        bool AddUserRole(SysUserRole userRole);

        bool SetUserRole(List<SysUserRole> userRoles, string userName);

        IEnumerable<SysUser> GetUsers(string roleId);

        IEnumerable<SysRole> GetAllRols();

        IEnumerable<SysPrivilege> GetPrivilege(string roleId);
    }
}