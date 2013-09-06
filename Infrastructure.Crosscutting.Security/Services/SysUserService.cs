using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
             
            var lstResult = UserRepository.GetList(
                Constant.TableSysUser,
                "SysId",
                string.Format("UserName='{0}' and UserPwd='{1}'", name.Trim(), pwd.Trim()));
             
            return false;
        }

    }
}
