/*
 *名称：RepositoryFactory
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-13 17:40:33
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    using System.Collections.Generic;

    public static class RepositoryFactory
    {
        public static readonly IDictionary<string, object> Factories;

        //todo:将其它对象用于工厂
        static RepositoryFactory()
        {
            Factories = new Dictionary<string,object>();
            Factories.Add("SysUserRepository", new SysUserRepository());
            Factories.Add("SysUserInfoRepository", new SysUserInfoRepository());
            Factories.Add("SysUserRoleRepository", new SysUserRoleRepository());
            Factories.Add("SysRoleRepository", new SysRoleRepository());
            Factories.Add("SysDataPrivilegeRepository", new SysDataPrivilegeRepository());
            Factories.Add("SysButtonRepository", new SysButtonRepository());
            Factories.Add("SysMenuRepository", new SysMenuRepository());
            Factories.Add("SysPrivilegeRepository", new SysPrivilegeRepository());
            Factories.Add("SysConfigRepository", new SysConfigRepository());

        }

    }
}