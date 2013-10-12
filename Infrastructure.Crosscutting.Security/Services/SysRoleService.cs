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

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;

    public class SysRoleService:ISysRoleService
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

        public SysRoleService()
        {  
        }

        public bool AddUserRole(SysUserRole userRole)
        {
            return UserRoleRepository.Add(userRole)==0?false:true;
        }

        public bool SetUserRole(List<SysUserRole> userRoles, string userName)
        {
            using (IDbTransaction tran = ConnectionFactory.CreateMsSqlConnection().BeginTransaction())
            {
                int deleteResult = UserRoleRepository.DeleteByUserId(userRoles[0].UserId, tran);

                foreach (SysUserRole sysUserRole in userRoles)
                {
                    int addResult = UserRoleRepository.Add(sysUserRole, tran);
                    if (addResult==0)
                    {
                        tran.Rollback();
                        return false;
                    }
                }
                tran.Commit();
            }
            return true;
        }

        public IEnumerable<SysUser> GetUsers(string roleId)
        { 
            return UserRepository.GetUserIncludeUserInfo(
                Constant.SqlTableUserAndRoleIncludeUserInfoJoin, Constant.SqlFieldsUserAndRoleIncludeUserInfoJoin,
                string.Format("r.SysId ='{0}'",roleId)); 
        }

        public IEnumerable<SysRole> GetAllRols()
        {
            return RoleRepository.GetList<SysRole>(Constant.TableSysRole, "*", null);
        }

        public IEnumerable<SysPrivilege> GetPrivilege(string roleId)
        {
            return RoleRepository.GetList<SysPrivilege>(
                Constant.SqlTableRolePrivilegeJoin,
                Constant.SqlFieldsPrivilegeJoin,
                string.Format("p.PrivilegeMaster = {0} and r.SysId='{1}'", (int)PrivilegeMaster.Role, roleId)); 
        }
    }
}