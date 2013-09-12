using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.CrossCutting.Cryptography;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.Repositorys;

namespace Infrastructure.Crosscutting.Security.Services
{
    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Data.Ado.Dapper;

    public class SysUserService:ISysUserService
    {  
        public SysUserService()
        {
            UserRepository = new SysUserRepository();
            UserRoleRepository = new SysUserRoleRepository();
            RoleRepository = new SysRoleRepository();
        }

        public SysUserRepository UserRepository { get; private set; }

        public SysUserRoleRepository UserRoleRepository { get; private set; }

        public SysRoleRepository RoleRepository { get; private set; }

        public bool CheckUser(string name, string pwd)
        { 
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(pwd)) return false;

            IEnumerable<int> lstResult = UserRepository.GetList<int>(
                Constant.SqlCount,
                string.Format(
                    "{0}='{1}' and {2}='{3}'",
                    Constant.ColumnSysUserUserName,
                    name.Trim(),
                    Constant.ColumnSysUserUserPwd,
                    Crypto.Encrypt(pwd.Trim())));

            if (lstResult != null && lstResult.Any())
            {
                return true;
            } 
            return false;
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
                    "Sys_User u inner join Sys_Privilege p on u.SysId=p.PrivilegeMasterKey ",Constant.SqlFieldsPrivilegeJoin
                    ,
                    string.Format("p.PrivilegeMaster = {0} and u.SysId='{1}'", (int)PrivilegeMaster.User, userId)); 
        }
    }
}
