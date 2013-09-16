/*
 *名称：SysMenuService
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-13 11:12:25
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

    public class SysMenuService:ISysMenuService
    {
        public SysMenuService()
        {
            MenuRepository = RepositoryFactory.MenuRepository;
        }

        public SysMenuRepository MenuRepository { get; private set; }

        public IEnumerable<SysPrivilege> GetPrivilege(string menuId)
        {
            return MenuRepository.GetList<SysPrivilege>(Constant.SqlTableMenuPrivilegeJoin, Constant.SqlFieldsPrivilegeJoin, string.Format("p.PrivilegeAccess={0} and m.SysId = '{1}'", (int)PrivilegeAccess.Menu, menuId)); 
        }

    }
}