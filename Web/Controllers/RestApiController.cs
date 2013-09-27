using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;

namespace Web.Controllers
{
    using System.Web.Mvc;

    using Infrastructure.Crosscutting.Security.Services;

    using Web.Models;
    using Web.Utility;

    public class RestApiController : AppController
    {
        private ISysRoleService roleService = ServiceFactory.RoleService;
        private ISysMenuService menuService = ServiceFactory.MenuService;
        private ISysUserService userService = ServiceFactory.UserService;
        private ISysPrivilegeService privilegeService = ServiceFactory.PrivilegeService;

        //private BaseController baseController = new BaseController();

        public List<EasyUiTreeResult> GetRolesListForJson()
        {
            var lstRoles = roleService.RoleRepository.GetList();
            List<EasyUiTreeResult> lstResult = new List<EasyUiTreeResult>();
            if (lstRoles != null && lstRoles.Any())
            {
                foreach (var role in lstRoles)
                {
                    lstResult.Add(new EasyUiTreeResult() { id = role.SysId, text = role.RoleName });
                }
                return lstResult;
            }

            return null;
        }
        public List<EasyUiTreeResult> GetUserListForJson()
        {
            var lstUsers = userService.UserRepository.GetList();
            List<EasyUiTreeResult> lstResult = new List<EasyUiTreeResult>();
            if (lstUsers!=null && lstUsers.Any())
            {
                foreach (var lstUser in lstUsers)
                {
                    lstResult.Add(new EasyUiTreeResult() {id = lstUser.SysId, text = lstUser.UserName});
                }
                return lstResult;
            }
            return null;
        }

        public JsonResult GetUserInfo()
        {
            //获取用户消息信息
            return Json(userService.UserRepository.GetModel(UserData.SysId
), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetMenusByLoginUser()
        {
            return Json(menuService.GetPrivilegedSysMenuByUserId(UserData.SysId), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult CheckUser(string userName, string password)
        {
            SysUser user;
            if ((user = userService.CheckUser(userName, password)) != null)
            {
                //FormsAuthentication.SetAuthCookie(userName, true);
                MyFormsPrincipal<SysUser>.SignIn(user.UserName, user, 100);
                return Json(new ResultModel() { Result = true, ResultInfo = "登录成功" });
            }

            return Json(new ResultModel() { Result = false, ResultInfo = "用户名或密码错误请重新输入" });
        }


        //public SysUserInfo GetUser()
        //{
        //    var user = baseController.UserData.SysId; ;

        //    SysUserInfo userInfo = userService.GetUserInfo(user);
        //    if (userInfo != null)
        //    {
        //        return userInfo;
        //    }
        //    return new SysUserInfo();
        //}

        //public IEnumerable<SysMenu> GetMenusByUser()
        //{
        //    IEnumerable<SysMenu> userMenus = menuService.GetPrivilegedSysMenuByUserId(baseController.UserData.SysId);

        //    return userMenus;
        //}

        #region menuContorller

        public JsonResult GetMenusPrivilegeForUser()
        {
            string userId = Request.Params[0].Substring(0, Request.Params[0].IndexOf("?"));
            return Json(GetUserMenusPrivilege(userId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMenusPrivilegeForRole()
        {
            string roleId = Request.Params[0].Substring(0, Request.Params[0].IndexOf("?"));
            return Json(GetRoleMenusPrivilege(roleId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRolesList()
        {
            return Json(GetRolesListForJson(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUsersList()
        {
            return Json(GetUserListForJson(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdatePrivilege(string sysId, PrivilegeMaster privilegeMaster, string menus)
        {
            string[] menuIds = menus.Split(',');
            bool result = privilegeService.SetMenuPrivilege(sysId, privilegeMaster, menuIds);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据角色构建菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<EasyUiTreeResult> GetRoleMenusPrivilege(string roleId)
        {
            ISysRoleService sysRoleService = new SysRoleService();
            IEnumerable<SysPrivilege> sysRolePrivileges = sysRoleService.GetPrivilege(roleId);
            //筛选菜单结果
            List<SysMenu> sysMenus = new List<SysMenu>();
            ISysMenuService sysMenuService = new SysMenuService();
            foreach (SysPrivilege sysUserPrivilege in sysRolePrivileges)
            {
                string menuId = sysUserPrivilege.PrivilegeAccessKey;
                SysMenu sysMenu = sysMenuService.GetSysMenuById(menuId).ToArray()[0];
                sysMenus.Add(sysMenu);
            }

            return BuildTreeMenu(sysMenus);
        }

        /// <summary>
        /// 根据用户构建菜单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<EasyUiTreeResult> GetUserMenusPrivilege(string userId)
        {
            IEnumerable<SysMenu> userMenus = menuService.GetPrivilegedSysMenuByUserId(userId);
            return BuildTreeMenu(userMenus);
        }

        /// <summary>
        /// 传入有权限menu集合，构建符合tree显示数据结构的所有菜单集合
        /// </summary>
        /// <param name="userMenus"></param>
        /// <returns></returns>
        public List<EasyUiTreeResult> BuildTreeMenu(IEnumerable<SysMenu> userMenus)
        {
            IEnumerable<SysMenu> allMenus = menuService.GetAllMenu();
            foreach (SysMenu userMenu in userMenus)
            {
                allMenus = allMenus.Where(x => x.SysId != userMenu.SysId);
                userMenu.HasPrivilege = true;
            }
            allMenus = allMenus.Union(userMenus).OrderBy(x => x.MenuOrder);

            List<EasyUiTreeResult> treeResults = new List<EasyUiTreeResult>();
            foreach (SysMenu allMenu in allMenus)
            {
                if (allMenu.MenuParentId == null)
                {
                    userMenus = allMenus.Where(x => x.MenuParentId == allMenu.SysId);
                    List<EasyUiTreeResult> ccs = new List<EasyUiTreeResult>();
                    foreach (SysMenu userMenu in userMenus)
                    {
                        EasyUiTreeResult cc = new EasyUiTreeResult()
                        {
                            id = userMenu.SysId,
                            text = userMenu.MenuName,
                            iconCls = userMenu.MenuIcon,
                            @checked = userMenu.HasPrivilege
                        };
                        ccs.Add(cc);
                    }
                    var tt = allMenus.Where(x => x.MenuParentId == allMenu.SysId && allMenu.HasPrivilege);
                    EasyUiTreeResult treeResult = new EasyUiTreeResult()
                    {
                        id = allMenu.SysId,
                        text = allMenu.MenuName,
                        iconCls = allMenu.MenuIcon,
                        @checked = tt.Count() > 0 ? false : allMenu.HasPrivilege,
                        children = ccs.ToArray()
                    };

                    treeResults.Add(treeResult);
                }
            }
            return treeResults;
        }

        #endregion
    }
}
