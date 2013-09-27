﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.Repositorys;

namespace Infrastructure.Crosscutting.Security.Services
{
    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Cryptography;
    using Infrastructure.Data.Ado.Dapper;

    public class SysUserService : ISysUserService
    {
        public SysUserService()
        {
            UserRepository = RepositoryFactory.UserRepository;
            UserRoleRepository = RepositoryFactory.UserRoleRepository;
            RoleRepository = RepositoryFactory.RoleRepository;
            UserInfoRepository = RepositoryFactory.UserInfoRepository;
        }

        public SysUserRepository UserRepository { get; private set; }

        public SysUserRoleRepository UserRoleRepository { get; private set; }

        public SysRoleRepository RoleRepository { get; private set; }

        public SysUserInfoRepository UserInfoRepository { get; private set; }

        public SysUser CheckUser(string name, string pwd)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(pwd)) return null;

            var lstResult = UserRepository.GetList("",
              string.Format(
                  "{0}='{1}' and {2}='{3}'",
                  Constant.ColumnSysUserUserName,
                  name.Trim(),
                  Constant.ColumnSysUserUserPwd,
                  Crypto.Encrypt(pwd.Trim())));

            return lstResult.FirstOrDefault();

        }



        public int AddUser(SysUser model)
        {
            model.UserPwd = Crypto.Encrypt(model.UserPwd.Trim());

            return UserRepository.Add(model);

        }

        public IEnumerable<SysRole> GetRoles(string userId)
        {
            return UserRoleRepository.GetList<SysRole>(Constant.SqlTableUserAndRoleJoin, "r.SysId,r.RoleDesc,r.RoleName,r.RecordStatus", string.Format("u.SysId='{0}'", userId));
        }

        public IEnumerable<SysPrivilege> GetPrivilege(string userId)
        {

            return
                UserRoleRepository.GetList<SysPrivilege>(
                    Constant.SqlTableUserPrivilegeJoin, Constant.SqlFieldsPrivilegeJoin
                    ,
                    string.Format("p.PrivilegeMaster = {0} and u.SysId='{1}'", (int)PrivilegeMaster.User, userId));
        }

        /// <summary>
        /// 根据用户id获取用户资料
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public SysUserInfo GetUserInfo(string userId)
        {
            IEnumerable<SysUserInfo> userInfos = UserInfoRepository.GetList<SysUserInfo>(Constant.TableSysUserInfo, "RealName,Title,Sex,Phone,Fax,Email,QQ,Address", string.Format("SysId='{0}'", userId));
            if (userInfos.Count() == 0)
            {
                return null;
            }
            return userInfos.ToArray()[0];
        }
    }
}
