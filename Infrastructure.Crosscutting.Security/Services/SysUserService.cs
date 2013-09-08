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
        }

        public IRepository<SysUser> UserRepository { get; private set; }

        public bool CheckUser(string name, string pwd)
        { 
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(pwd)) return false;
             
            IEnumerable<SysUser> lstResult = UserRepository.GetList(
                Constant.TableSysUser,
                "SysId",
                string.Format("UserName='{0}' and UserPwd='{1}'", name.Trim(), Crypto.Encrypt(pwd.Trim())));

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
    }
}
