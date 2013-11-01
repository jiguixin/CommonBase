/*
 *名称：SysRoleService
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-12 16:29:09
 *修改时间：
 *备注：
 */

using System;
using System.Data;

namespace Infrastructure.Crosscutting.Security.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;
    using Infrastructure.Data.Ado.Dapper;

    public class SysRoleService : ISysRoleService
    {
        public SysRoleRepository RoleRepository
        {
            get { return RepositoryFactory.RoleRepository; }
        }

        public SysUserRepository UserRepository
        {
            get { return RepositoryFactory.UserRepository; }
        }

        public SysUserRoleRepository UserRoleRepository
        {
            get { return RepositoryFactory.UserRoleRepository; }
        }

        public bool AddUserRole(SysUserRole userRole)
        {
            return this.UserRoleRepository.Add(userRole) != 0;
        }

        public bool SetUserRole(List<SysUserRole> userRoles)
        {
            using (IDbTransaction tran = RoleRepository.Connection.BeginTransaction())
            {
                this.UserRoleRepository.DeleteByRoleId(userRoles[0].RoleId, tran);

                //如果传来的是一个空userid的实体，意味着没有选中有任何用户，直接清空该角色下所有用户即可
                if (userRoles.Count == 1 && string.IsNullOrEmpty(userRoles[0].UserId))
                {
                    tran.Commit();
                    return true;
                }

                if (userRoles.Select(sysUserRole => this.UserRoleRepository.Add(sysUserRole, tran)).Any(addResult => addResult == 0))
                {
                    tran.Rollback();
                    return false;
                }
                tran.Commit();
            }
            return true;
        }

        public IEnumerable<SysUser> GetUsers(string roleId)
        {
            var p = new DynamicParameters();
            p.Add("RoleId", roleId.Trim());

            return UserRepository.GetUserIncludeUserInfo(
                Constant.SqlTableUserAndRoleIncludeUserInfoJoin, Constant.SqlFieldsUserAndRoleIncludeUserInfoJoin,
                string.Format("ur.{0} ={1}{0}", "RoleId", Constant.SqlReplaceParameterPrefix), p);
        }

        public IEnumerable<SysPrivilege> GetPrivilege(string roleId)
        {
            var p = new DynamicParameters();
            p.Add(Constant.ColumnSysId, roleId.Trim());
            p.Add(Constant.ColumnSysPrivilegePrivilegeMaster, (int)PrivilegeMaster.Role);

            return RoleRepository.GetListByTable<SysPrivilege>(
                Constant.SqlTableRolePrivilegeJoin,
                Constant.SqlFieldsPrivilegeJoin,
                 string.Format("p.{0} = {2}{0} and r.{1}={2}{1}", Constant.ColumnSysPrivilegePrivilegeMaster, Constant.ColumnSysId, Constant.SqlReplaceParameterPrefix), p);

        }
    }
}