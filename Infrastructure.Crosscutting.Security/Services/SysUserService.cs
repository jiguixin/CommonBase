using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.Repositorys;

namespace Infrastructure.Crosscutting.Security.Services
{
    using System.Data;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Cryptography;
    using Infrastructure.Data.Ado.Dapper;

    public class SysUserService : ISysUserService
    {
        public SysUserService()
        {   
        }

        public SysUserRepository UserRepository
        {
            get { return RepositoryFactory.UserRepository; }
        }

        public SysUserRoleRepository UserRoleRepository
        {
            get { return RepositoryFactory.UserRoleRepository; }
        }

        public SysRoleRepository RoleRepository
        {
            get { return RepositoryFactory.RoleRepository; }
        }

        public SysUserInfoRepository UserInfoRepository
        {
            get { return RepositoryFactory.UserInfoRepository; }
        }

        public SysUser CheckUser(string name, string pwd)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(pwd)) return null;

            var p = new DynamicParameters();
            p.Add(Constant.ColumnSysUserUserName, name.Trim());
            p.Add(Constant.ColumnSysUserUserPwd, Crypto.Encrypt(pwd.Trim()));

            var lstResult = UserRepository.GetList("",
             string.Format(
                 "{0}={1}{0} and {2}={1}{2}",
                 Constant.ColumnSysUserUserName,
                 Constant.SqlReplaceParameterPrefix,
                 Constant.ColumnSysUserUserPwd
                 ), p);
              
            return lstResult.FirstOrDefault();

        }


        /// <summary>
        /// 新增用户，增加了密码加密
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddUser(SysUser model)
        {
            model.UserPwd = Crypto.Encrypt(model.UserPwd.Trim());

            return UserRepository.Add(model); 
        }

        /// <summary>
        /// 修改用户，实现了将密码进行加密
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateUser(SysUser model)
        {
            model.UserPwd = Crypto.Encrypt(model.UserPwd.Trim());
            return UserRepository.Update(model);  
        }
            
        public IEnumerable<SysRole> GetRoles(string userId)
        {
            var p = new DynamicParameters();
            p.Add(Constant.ColumnSysId, userId.Trim());

            return UserRoleRepository.GetListByTable<SysRole>(Constant.SqlTableUserAndRoleJoin, "r.SysId,r.RoleDesc,r.RoleName,r.RecordStatus", string.Format("u.{1}={0}{1}", Constant.SqlReplaceParameterPrefix, Constant.ColumnSysId), p);

        }

        public IEnumerable<SysPrivilege> GetPrivilege(string userId)
        {
            var p = new DynamicParameters();
            p.Add(Constant.ColumnSysId, userId.Trim());
            p.Add(Constant.ColumnSysPrivilegePrivilegeMaster, (int)PrivilegeMaster.User);

            return
                UserRoleRepository.GetListByTable<SysPrivilege>(
                    Constant.SqlTableUserPrivilegeJoin, Constant.SqlFieldsPrivilegeJoin
                    ,
                    string.Format("p.{0} = {2}{0} and u.{1}={2}{1}", Constant.ColumnSysPrivilegePrivilegeMaster, Constant.ColumnSysId, Constant.SqlReplaceParameterPrefix), p);
        } 
    }
}
