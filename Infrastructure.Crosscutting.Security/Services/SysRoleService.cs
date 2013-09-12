/*
 *名称：SysRoleService
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-12 16:29:09
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Services
{
    using System.Collections.Generic;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;

    public class SysRoleService:ISysRoleService
    {
        public SysRoleRepository RoleRepository { get; private set; }
        public SysUserRepository UserRepository { get; private set; }

        public SysRoleService()
        {
            RoleRepository = new SysRoleRepository();
            UserRepository = new SysUserRepository();
        }
        public IEnumerable<SysUser> GetUsers(string roleId)
        {
            return UserRepository.GetUserIncludeUserInfo(
                Constant.SqlTableUserAndRoleIncludeUserInfoJoin, "u.CreateTime,u.LastLogin,u.RecordStatus,u.SysId,u.UserName,u.UserPwd,ui.SysId,ui.Address,ui.Email,ui.Fax,ui.Phone,ui.QQ,ui.RealName,ui.Sex,ui.Title",
                string.Format("r.SysId ='{0}'",roleId)); 
        }

        public IEnumerable<SysPrivilege> GetPrivilege(string roleId)
        {
            return RoleRepository.GetList<SysPrivilege>(
                "Sys_Role r inner join Sys_Privilege p on r.SysId=p.PrivilegeMasterKey",
                Constant.SqlFieldsPrivilegeJoin,
                string.Format("p.PrivilegeMaster = {0} and r.SysId='{1}'", (int)PrivilegeMaster.Role, roleId)); 
        }
    }
}