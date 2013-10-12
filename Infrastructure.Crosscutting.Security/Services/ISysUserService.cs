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

        /// <summary>
        /// 新增用户，增加了密码加密
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddUser(SysUser model);

        /// <summary>
        /// 修改用户，实现了将密码进行加密
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int UpdateUser(SysUser model);

        int AddUserInfo(SysUserInfo model);

        int DeleteUser(string sysId);

        int UpdateUserInfo(SysUserInfo model);

        IEnumerable<SysRole> GetRoles(string userId);

        IEnumerable<SysPrivilege> GetPrivilege(string userId);

        SysUserInfo GetUserInfo(string userId);

    }
}
