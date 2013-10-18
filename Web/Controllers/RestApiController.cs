using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Cryptography;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.Repositorys;

namespace Web.Controllers
{
    using System.Web.Mvc;

    using Infrastructure.Crosscutting.Security.Services;

    using Web.Models;
    using Web.Models.EasyUi;
    using Web.Utility;

    public class RestApiController : BaseController
    {
        private ISysRoleService roleService = ServiceFactory.RoleService;
        private ISysMenuService menuService = ServiceFactory.MenuService;
        private ISysUserService userService = ServiceFactory.UserService;
        private ISysPrivilegeService privilegeService = ServiceFactory.PrivilegeService;

        private ISysButtonService buttonService = ServiceFactory.ButtonService;

        #region Json

        #region 用户

        //获取全部用户列表(tree格式)
        public JsonResult GetUsersListTree()
        {
            return Json(GetUserListForJson(), JsonRequestBehavior.AllowGet);
        }

        //获取当前用户详细信息
        public JsonResult GetCurrentUserInfo()
        {
            //获取用户消息信息
            return Json(userService.UserRepository.GetModel(UserData.SysId
                            ), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取用户信息，注：并将密码进行解密， 主要用于在修改用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetCompleteUserInfoById(string id)
        {
            //获取用户消息信息
            var model = userService.UserRepository.GetModel(id);
            model.UserPwd = Crypto.Decrypt(model.UserPwd);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
         
        //获取登录用户菜单列表
        public JsonResult GetMenusByLoginUser()
        {
            return Json(menuService.GetPrivilegedSysMenuByUserId(UserData.SysId), JsonRequestBehavior.AllowGet);
        }
         
        //获取某用户可用菜单（tree格式）
        public JsonResult GetMenusPrivilegeTreeForUser()
        {
            int index = Request.Params[0].IndexOf("?");
            string userId = UserData.SysId;
            if (index!=-1)
            {
                userId = Request.Params[0].Substring(0, index);
            }
            return Json(GetUserMenusPrivilege(userId), JsonRequestBehavior.AllowGet);
        }

        //获取所有用户包含用户详细信息
        public JsonResult GetAllUerInfo()
        {
            var lstUsers = userService.UserRepository.GetList();

            List<UserDetailsModel> userDetails = (from user in lstUsers
                                                  let userInfo = this.userService.GetUserInfo(user.SysId)
                                                  select
                                                      new UserDetailsModel()
                                                          {
                                                              SysId = user.SysId,
                                                              UserName = user.UserName,
                                                              RealName = userInfo.RealName,
                                                              Title = userInfo.Title,
                                                              Sex = userInfo.Sex ? "男" : "女",
                                                              Phone = userInfo.Phone,
                                                              Fax = userInfo.Fax,
                                                              Email = userInfo.Email,
                                                              QQ = userInfo.QQ,
                                                              Address = userInfo.Address
                                                          }).ToList();

            return Json(userDetails, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        ////检查用户名和密码匹配
        //public JsonResult CheckUser(string userName, string password)
        //{
        //    SysUser user;
        //    if ((user = userService.CheckUser(userName, password)) != null)
        //    {
        //        //FormsAuthentication.SetAuthCookie(userName, true);
        //        MyFormsPrincipal<SysUser>.SignIn(user.UserName, user, 1);
        //        return Json(new ResultModel() { Result = true, ResultInfo = "登录成功" });
        //    }

        //    return Json(new ResultModel() { Result = false, ResultInfo = "用户名或密码错误请重新输入" });
        //}

        [HttpPost]
        public JsonResult AddUser(SysUser user)
        {
            if (!ModelState.IsValid) Json(false);

            user.RecordStatus = CreateRecordMsg;

            if (userService.AddUser(user) > 0)
            {
                return Json(true);
            }

            return Json(false);
        }

        [HttpPost]
        public JsonResult UpdateUser(SysUser user)
        {
            if (!ModelState.IsValid) Json(false);

            user.RecordStatus = UpdateRecordMsg;

            if (userService.UpdateUser(user) > 0)
            {
                return Json(true);
            }

            return Json(false);
        } 

        [HttpPost]
        //删除用户
        public JsonResult DeleteUser(string SysId)
        {
            if (userService.DeleteUser(SysId) > 0)
            {
                return Json(true);
            }
            return Json(false);
        }

        #endregion

        #region 角色

        //获取全部角色列表(tree格式)
        public JsonResult GetRolesListTree()
        {
            return Json(GetRolesListForJson(), JsonRequestBehavior.AllowGet);
        }

        //获取全部用户
        public JsonResult GetRoleList()
        {
            return Json(roleService.RoleRepository.GetList(), JsonRequestBehavior.AllowGet);
        }

        //获取某用户的角色列表（tree格式）
        public JsonResult GetRolesTreeForUser()
        {
            string userId = Request.Params[0].Substring(0, Request.Params[0].IndexOf("?"));
            return Json(GetUserRoles(userId), JsonRequestBehavior.AllowGet);
        }

        //获取某角色可用菜单（tree格式）
        public JsonResult GetMenusPrivilegeTreeForRole()
        {
            string roleId = Request.Params[0].Substring(0, Request.Params[0].IndexOf("?"));
            return Json(GetRoleMenusPrivilege(roleId), JsonRequestBehavior.AllowGet);
        }

        //修改某用户的角色
        public JsonResult UpdateRolesForUser(string userId, string roleId)
        {
            string[] roleIds = roleId.Split(',');
            List<SysUserRole> userRoles = new List<SysUserRole>();
            for (int i = 0; i < roleIds.Length; i++)
            {
                SysUserRole userRole = new SysUserRole()
                {
                    SysId = Infrastructure.Crosscutting.Security.Common.Util.NewId(),
                    UserId = userId,
                    RoleId = roleIds[i]
                };
                userRoles.Add(userRole);
            }

            return Json(roleService.SetUserRole(userRoles, UserData.UserName), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 权限

        /// <summary>
        /// 根据用户或者角色SysId重新配置权限
        /// </summary>
        /// <param name="sysId"></param>
        /// <param name="privilegeMaster"></param>
        /// <param name="menus">可用菜单的ID数组</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdatePrivilege(string sysId, PrivilegeMaster privilegeMaster, string menus)
        {
            string[] menuIds = menus.Split(',');
            string userName = userService.UserRepository.GetModel(UserData.SysId).UserInfo.RealName;
            bool result = privilegeService.SetMenuPrivilege(sysId, privilegeMaster, menuIds, userName);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
         

        public JsonResult GetCurrentUserButtonsPrivilege(string menuId)
        {
            var lstSource = buttonService.GetButtonsPrivilegeByUserAndMenu(UserData.SysId, menuId);

            if (lstSource == null) return Json(null);


            var lstResult = lstSource.OrderBy(o=>o.BtnOrder).Select(item => new toolbar() { handler = item.BtnFunction, iconCls = item.BtnIcon, text = item.BtnName }).ToList();

            return Json(lstResult,
                JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 菜单

        [HttpPost]
        public JsonResult AddMenu(SysMenu menu, bool IsAddButton)
        {
            if (!ModelState.IsValid) Json(false);

            menu.RecordStatus = CreateRecordMsg;

            if (IsAddButton && !string.IsNullOrEmpty(menu.MenuParentId) && menu.MenuParentId.Trim().Length > 0)
            {
                menu.SysId = Util.NewId();

                if (menuService.MenuRepository.AddOrModifyTrans(
                    menu,
                    buttonService.InitialAddModifyDelBtn(menu.SysId, CreateRecordMsg),
                    menuService.MenuRepository.Add,
                    buttonService.ButtonRepository.Add) > 0)
                {
                    return Json(true);
                }
            }
            else
            { 
                if (menuService.MenuRepository.Add(menu) > 0)
                {
                    return Json(true);
                }
            }




            return Json(false);
        }

        [HttpPost]
        public JsonResult UpdateMenu(SysMenu menu)
        {
            if (!ModelState.IsValid) Json(false);

            if (menu.SysId == menu.MenuParentId) return Json(true);

            menu.RecordStatus = UpdateRecordMsg;

            if (menuService.MenuRepository.Update(menu) > 0)
            {
                return Json(true);
            }

            return Json(false);
        }

        [HttpPost]
        public JsonResult DeleteMenu(string SysId)
        {
            if (menuService.MenuRepository.Delete(SysId) > 0)
            {
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult GetMenu(string id)
        {
            var model = menuService.MenuRepository.GetModel(id);

            return model == null ? null : this.Json(model, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAllMenusRetTreeGrid()
        {
            IEnumerable<SysMenu> lstSource = (IEnumerable<SysMenu>)menuService.MenuRepository.GetAllMenusByLoop();
            if (lstSource == null) return Json(null);
              
            return Json(BuildAllTreeMenu(lstSource),
              JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<EasyUiTreeResult> BuildAllTreeMenu(IEnumerable<SysMenu> lstSource)
        {
            var lstResult = new List<EasyUiTreeResult>();

            foreach (var s in lstSource)
            {
                var model = new EasyUiTreeResult()
                                {
                                    id = s.SysId,
                                    link = s.MenuLink,
                                    order = s.MenuOrder,
                                    text = s.MenuName,
                                    iconCls = s.MenuIcon,
                                    visible = s.IsVisible != null && ((PrivilegeOperation)(s.IsVisible.Value)) == PrivilegeOperation.Enable ? "启用" : "禁用"
                                };
                if (s.Children != null && s.Children.Any())
                {
                    model.children = this.BuildAllTreeMenu(s.Children).ToArray();
                }
                lstResult.Add(model);
            }

            return lstResult;

        }

        #endregion

        #endregion

        #region Json格式构造

        #region tree格式构造

        //获取用于tree显示的所有角色列表
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

        //获取用于tree显示的所有用户列表
        public List<EasyUiTreeResult> GetUserListForJson()
        {
            var lstUsers = userService.UserRepository.GetList();
            List<EasyUiTreeResult> lstResult = new List<EasyUiTreeResult>();
            if (lstUsers != null && lstUsers.Any())
            {
                foreach (var lstUser in lstUsers)
                {
                    lstResult.Add(new EasyUiTreeResult() { id = lstUser.SysId, text = lstUser.UserName });
                }
                return lstResult;
            }
            return null;
        }

        /// <summary>
        /// 根据角色构建菜单列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<EasyUiTreeResult> GetRoleMenusPrivilege(string roleId)
        {
            IEnumerable<SysMenu> sysRolePrivileges = menuService.GetPrivilegedSysMenuByRoleId(roleId);
            return BuildTreeMenu(sysRolePrivileges);
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
            //获取所有菜单
            IEnumerable<SysMenu> allMenus = menuService.GetAllMenu();
            //获取所有按钮数据
            IEnumerable<SysButton> allButtons = RepositoryFactory.ButtonRepository.GetList<SysButton>(Constant.TableSysButton,
                                                                                    "SysId,MenuId,BtnName,BtnIcon,BtnOrder,BtnFunction,RecordStatus",
                                                                                    null);
            //去除掉所有菜单中用户已有权限的菜单
            foreach (SysMenu userMenu in userMenus)
            {
                allMenus = allMenus.Where(x => x.SysId != userMenu.SysId);
            }
            //将无权限菜单和有权限菜单合并
            allMenus = allMenus.Union(userMenus).OrderBy(x => x.MenuOrder);

            List<EasyUiTreeResult> treeResults = new List<EasyUiTreeResult>();
            foreach (SysMenu allMenu in allMenus)
            {
                if (allMenu.MenuParentId == null)
                {
                    userMenus = allMenus.Where(x => x.MenuParentId == allMenu.SysId);

                    #region 构建一组菜单的tree结构集合

                    List<EasyUiTreeResult> ccs = new List<EasyUiTreeResult>();
                    foreach (SysMenu userMenu in userMenus)
                    {
                        var bts = allButtons.Where(x => x.MenuId == userMenu.SysId);

                        #region 构建button的tree结构集合
                        List<EasyUiTreeResult> buttons = new List<EasyUiTreeResult>();
                        if (bts.Count() != 0)
                        {
                            #region 如果此菜单下有可用按钮，判断添加按钮可用或者不可用

                            if (userMenu.Buttons != null)
                            {
                                foreach (SysButton button in userMenu.Buttons)
                                {
                                    EasyUiTreeResult buttonc = new EasyUiTreeResult()
                                    {
                                        id = button.SysId,
                                        text = button.BtnName,
                                        iconCls = button.BtnIcon,
                                        @checked = true,
                                        link = button.BtnFunction,
                                        order = button.BtnOrder,
                                        recordStatus = button.RecordStatus
                                    };
                                    buttons.Add(buttonc);

                                    bts = bts.Where(x => x.SysId != button.SysId);
                                }

                                foreach (SysButton button in bts)
                                {

                                    EasyUiTreeResult buttonc = new EasyUiTreeResult()
                                        {
                                            id = button.SysId,
                                            text = button.BtnName,
                                            iconCls = button.BtnIcon,
                                            @checked = false,
                                            link = button.BtnFunction,
                                            order = button.BtnOrder,
                                            recordStatus = button.RecordStatus
                                        };
                                    buttons.Add(buttonc);

                                }
                            }
                            #endregion

                            #region 如果此菜单无有权限按钮，添加全部按钮

                            else
                            {
                                foreach (SysButton button in bts)
                                {
                                    EasyUiTreeResult buttonc = new EasyUiTreeResult()
                                        {
                                            id = button.SysId,
                                            text = button.BtnName,
                                            iconCls = button.BtnIcon,
                                            @checked = false,
                                            link = button.BtnFunction,
                                            order=button.BtnOrder,
                                            recordStatus = button.RecordStatus
                                        };
                                    buttons.Add(buttonc);
                                }
                            }

                            #endregion
                        }

                        #endregion

                        #region 添加一组子菜单的tree结构

                        EasyUiTreeResult cc = new EasyUiTreeResult()
                        {
                            id = userMenu.SysId,
                            text = userMenu.MenuName,
                            iconCls = userMenu.MenuIcon,
                            @checked = userMenu.IsVisible == (long)PrivilegeOperation.Enable ? true : false,
                            link = userMenu.MenuLink,
                            recordStatus = userMenu.RecordStatus,
                            children = buttons.OrderBy(x=>x.order).ToArray()
                        };

                        ccs.Add(cc);

                        #endregion
                    }

                    EasyUiTreeResult treeResult = new EasyUiTreeResult()
                    {
                        id = allMenu.SysId,
                        text = allMenu.MenuName,
                        iconCls = allMenu.MenuIcon,
                        @checked = allMenu.IsVisible == (long)PrivilegeOperation.Enable ? true : false,
                        children = ccs.ToArray()
                    };

                    treeResults.Add(treeResult);

                    #endregion
                }
            }
            return treeResults;
        }

        /// <summary>
        /// 根据用户构建角色列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<EasyUiTreeResult> GetUserRoles(string userId)
        {
            IEnumerable<SysRole> allRoles = roleService.GetAllRols();
            IEnumerable<SysRole> userRoles = userService.GetRoles(userId);

            List<EasyUiTreeResult> treeResults = new List<EasyUiTreeResult>();
            foreach (SysRole userRole in userRoles)
            {
                EasyUiTreeResult treeResult = new EasyUiTreeResult()
                {
                    id = userRole.SysId,
                    text = userRole.RoleName,
                    recordStatus = userRole.RoleDesc,
                    @checked = true
                };
                allRoles = allRoles.Where(x => x.SysId != userRole.SysId);
                treeResults.Add(treeResult);
            }
            foreach (SysRole allRole in allRoles)
            {
                EasyUiTreeResult treeResult = new EasyUiTreeResult()
                {
                    id = allRole.SysId,
                    text = allRole.RoleName,
                    recordStatus = allRole.RoleDesc,
                    @checked = false
                };
                treeResults.Add(treeResult);
            }

            return treeResults.OrderBy(x => x.id).ToList();
        }

        #endregion

        #endregion

    }
}

