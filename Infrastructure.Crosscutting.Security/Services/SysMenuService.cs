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
        }

        public SysMenuRepository MenuRepository
        {
            get { return RepositoryFactory.MenuRepository; }
        }
        public SysButtonRepository ButtonRepository
        {
            get { return RepositoryFactory.ButtonRepository; }
        }

        public IEnumerable<SysPrivilege> GetPrivilege(string menuId)
        {
            return MenuRepository.GetListByTable<SysPrivilege>(Constant.SqlTableMenuPrivilegeJoin, Constant.SqlFieldsPrivilegeJoin, string.Format("p.PrivilegeAccess={0} and m.SysId = '{1}'", (int)PrivilegeAccess.Menu, menuId));
        }

        public IEnumerable<SysMenu> GetSysMenuById(string menuId)
        {
            IEnumerable<SysMenu> sysMenus = MenuRepository.GetListByTable<SysMenu>(Constant.TableSysMenu, "SysId,MenuParentId,MenuOrder,MenuName,MenuLink,MenuIcon,IsVisible,IsLeaf,RecordStatus", string.Format("SysId='{0}'", menuId));
            return sysMenus;
        }

        /// <summary>
        /// 根据用户ID获取菜单和按钮权限信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>SysPrivilege集合</returns>
        public IEnumerable<SysPrivilege> GetPrivilegesByUserId(string userId)
        {
            //通过用户id获取权限菜单数据
            IEnumerable<SysPrivilege> sysUserPrivileges = ServiceFactory.UserService.GetPrivilege(userId);

            //通过用户id获取角色，通过角色获取权限菜单数据
            IEnumerable<SysRole> sysRoles = ServiceFactory.UserService.GetRoles(userId);

            //一个用户会有多个角色
            foreach (SysRole sysRole in sysRoles)
            {
                string roleId = sysRole.SysId;
                IEnumerable<SysPrivilege> sysRolePrivileges = ServiceFactory.RoleService.GetPrivilege(roleId);

                //获取角色中菜单权限
                var roleMenuPrivileges = sysRolePrivileges.Where(x => x.PrivilegeAccess == PrivilegeAccess.Menu);
                //获取角色中的不可用按钮数据
                var roleDisBts =
                    sysRolePrivileges.Where(
                        x =>
                        x.PrivilegeAccess == PrivilegeAccess.Button &&
                        x.PrivilegeOperation == PrivilegeOperation.Disable);

                //排除同用户权限相同菜单数据
                foreach (SysPrivilege privilege in sysUserPrivileges.Where(x => x.PrivilegeAccess == PrivilegeAccess.Menu))
                {
                    roleMenuPrivileges =
                       roleMenuPrivileges.Where(
                           x =>
                           x.PrivilegeAccessKey != privilege.PrivilegeAccessKey).ToList();
                }
                sysUserPrivileges = sysUserPrivileges.Union(roleMenuPrivileges);

                //排除同用户权限相同按钮数据
                foreach (SysPrivilege privilege in sysUserPrivileges.Where(x => x.PrivilegeAccess == PrivilegeAccess.Button))
                {
                    roleDisBts =
                       roleDisBts.Where(
                           x =>
                           x.PrivilegeAccessKey == privilege.PrivilegeAccessKey).ToList();

                }
                sysUserPrivileges = sysUserPrivileges.Union(roleDisBts);

            }

            return sysUserPrivileges;
        }


        /// <summary>
        /// 根据用户ID获取可用菜单和按钮集合
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>SysMenu集合</returns>
        public IEnumerable<SysMenu> GetPrivilegedSysMenuByUserId(string userId)
        {

            var sysUserPrivileges = GetPrivilegesByUserId(userId);

            var allButtons = ServiceFactory.ButtonService.ButtonRepository.GetList();
             

            //筛选菜单结果
            List<SysMenu> sysMenus = new List<SysMenu>();

            foreach (SysPrivilege sysUserPrivilege in sysUserPrivileges)
            {
                string menuId = sysUserPrivilege.PrivilegeAccessKey;
                var c = ServiceFactory.MenuService.GetSysMenuById(menuId);
                if (c.Count() > 0)
                {

                    List<SysButton> bts = new List<SysButton>();
                    //根据用户获取按钮
                    var userBt = ServiceFactory.ButtonService.GetButtonsPrivilegeByUserAndMenu(userId, menuId, PrivilegeMaster.User, allButtons, sysUserPrivileges);
                    if (userBt != null)
                    {
                        bts = bts.Union(userBt).ToList();
                    }

                    SysMenu sysMenu = c.FirstOrDefault();
                    sysMenu.IsVisible = (long)sysUserPrivilege.PrivilegeOperation;
                    sysMenu.Buttons = bts;

                    sysMenus.Add(sysMenu);
                }

            }
            return sysMenus.OrderBy(x => x.MenuOrder);
        }


        /// <summary>
        /// 根据角色ID获取可用菜单和按钮集合
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>SysMenu集合</returns>
        public IEnumerable<SysMenu> GetPrivilegedSysMenuByRoleId(string roleId)
        {
            //todo:此方法有问题
            IEnumerable<SysPrivilege> sysRolePrivileges = ServiceFactory.RoleService.GetPrivilege(roleId);

            //todo:如果sysRolePrivileges 没有查到菜单是不是就直接返回空集合，不用在去查所有菜单数据

            //获取所有按钮数据
            IEnumerable<SysButton> allButtons = ButtonRepository.GetListByTable<SysButton>(Constant.TableSysButton,
                                                                                    "SysId,MenuId,BtnName,BtnIcon,BtnOrder,BtnFunction,RecordStatus",
                                                                                    null);

            //筛选菜单结果
            List<SysMenu> sysMenus = new List<SysMenu>();

            foreach (SysPrivilege sysUserPrivilege in sysRolePrivileges)
            {
                string menuId = sysUserPrivilege.PrivilegeAccessKey;
                var c = ServiceFactory.MenuService.GetSysMenuById(menuId);
                if (c.Count() > 0)
                {
                    SysMenu sysMenu = ServiceFactory.MenuService.GetSysMenuById(menuId).ToArray()[0];//todo:前面已经读取了菜单，怎么还要读一次
                    sysMenu.IsVisible = (long)sysUserPrivilege.PrivilegeOperation;
                    sysMenu.Buttons = ServiceFactory.ButtonService.GetButtonsPrivilegeByUserAndMenu(roleId, menuId, PrivilegeMaster.Role, allButtons, sysRolePrivileges);

                    sysMenus.Add(sysMenu);
                }

            }

            return sysMenus;
        }



        /// <summary>
        /// 获取所有菜单列表（不包含按钮）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SysMenu> GetAllMenu()
        {
            IEnumerable<SysMenu> sysMenus = MenuRepository.GetListByTable<SysMenu>(Constant.TableSysMenu, "*", null);
            return sysMenus.OrderBy(x => x.MenuOrder);
        }

    }
}