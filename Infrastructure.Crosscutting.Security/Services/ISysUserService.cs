using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Model;

namespace Infrastructure.Crosscutting.Security.Services
{
    using Infrastructure.Crosscutting.Security.Repositorys;

    public interface ISysUserService
    {
        IRepository<SysUser> UserRepository{get;}

        bool CheckUser(string name, string pwd);

        int AddUser(SysUser model);
    }
}
