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
        SysUserRepository UserRepository { get; }

        /// <summary>
        /// 根据用户名、密码查询该用户是否存在，如果不存在者会返回null
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        SysUser CheckUser(string name, string pwd);

        int AddUser(SysUser model);

        IEnumerable<SysRole> GetRoles(string userId);

        IEnumerable<SysPrivilege> GetPrivilege(string userId);

    }
}
