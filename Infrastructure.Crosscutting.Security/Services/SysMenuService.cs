/*
 *名称：SysMenuService
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-13 11:12:25
 *修改时间：
 *备注：
 */

using System;
using System.Collections;
using System.Linq;

namespace Infrastructure.Crosscutting.Security.Services
{
    using System.Collections.Generic;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;

    public class SysMenuService : ISysMenuService
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

        public IEnumerable<SysMenu> GetSysMenuById(string menuId)
        {
            IEnumerable<SysMenu> sysMenus = MenuRepository.GetList<SysMenu>(Constant.TableSysMenu, "SysId,MenuParentId,MenuOrder,MenuName,MenuLink,MenuIcon,IsVisible,IsLeaf,RecordStatus", string.Format("SysId='{0}'", menuId));
            return sysMenus;
        }

        /// <summary>
        /// 根据用户ID获取可用菜单集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<SysMenu> GetPrivilegedSysMenuByUserId(string userId)
        {
            //todo : 用户权限和角色权限优先性需要测试修正
            ISysUserService sysUserService = new SysUserService();

            //通过用户id获取权限菜单数据
            IEnumerable<SysPrivilege> sysUserPrivileges = sysUserService.GetPrivilege(userId);
            sysUserPrivileges = sysUserPrivileges.Where(x => x.PrivilegeAccess == PrivilegeAccess.Menu).ToList();

            //通过用户id获取角色，通过角色获取权限菜单数据
            IEnumerable<SysRole> sysRoles = sysUserService.GetRoles(userId);
            ISysRoleService sysRoleService = new SysRoleService();
            SysRole[] sysRoles1 = sysRoles.ToArray();
            //一个用户是否会有多个角色？
            for (int i = 0; i < sysRoles1.Count(); i++)
            {
                string roleId = sysRoles1[0].SysId;
                IEnumerable<SysPrivilege> sysRolePrivileges = sysRoleService.GetPrivilege(roleId);

                //排除同用户权限相同菜单数据
                SysPrivilege[] sysUserPrivileges1 = sysUserPrivileges.ToArray();
                for (int j = 0; j < sysUserPrivileges1.Length; j++)
                {
                    sysRolePrivileges =
                        sysRolePrivileges.Where(
                            x =>
                            x.PrivilegeAccessKey != sysUserPrivileges1[j].PrivilegeAccessKey &&
                            x.PrivilegeAccess == PrivilegeAccess.Menu).ToList();
                }

                sysUserPrivileges = sysUserPrivileges.Union(sysRolePrivileges);
                sysUserPrivileges = sysUserPrivileges.Where(x => x.PrivilegeOperation == PrivilegeOperation.Enable);
            }
            //筛选菜单结果
            List<SysMenu> sysMenus = new List<SysMenu>();
            ISysMenuService sysMenuService = new SysMenuService();
            foreach (SysPrivilege sysUserPrivilege in sysUserPrivileges)
            {
                string menuId = sysUserPrivilege.PrivilegeAccessKey;
                SysMenu sysMenu = sysMenuService.GetSysMenuById(menuId).ToArray()[0];
                sysMenus.Add(sysMenu);
            }
            return sysMenus.OrderBy(x => x.MenuOrder);
        }

        /// <summary>
        /// 获取所有菜单列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SysMenu> GetAllMenu()
        {
            IEnumerable<SysMenu> sysMenus = MenuRepository.GetList<SysMenu>(Constant.TableSysMenu, "*", null);
            return sysMenus.OrderBy(x => x.MenuOrder);
        }

        

    }
}