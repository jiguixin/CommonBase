/*
 *名称：SysPrivilegeService
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-16 17:23:32
 *修改时间：
 *备注：
 */

using System;
using System.Data;

namespace Infrastructure.Crosscutting.Security.Services
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;

    public class SysPrivilegeService : ISysPrivilegeService
    {
        private readonly SysMenuRepository menuRepository = RepositoryFactory.MenuRepository;

        private readonly SysButtonRepository buttonRepository = RepositoryFactory.ButtonRepository;

        private readonly SysRoleRepository roleRepository = RepositoryFactory.RoleRepository;

        private readonly SysUserRepository userRepository = RepositoryFactory.UserRepository;

        private SysPrivilegeRepository privilegeRepository = RepositoryFactory.PrivilegeRepository;


        public SysPrivilegeService()
        {
        }

        /// <summary> 
        /// 遍历角色数据，分别创建，菜单和按钮权限记录，默认权限不可用。
        /// 注：在添加时要优先检查该权限记录是否存在。存在则不添加，不存在则添加。
        /// </summary>
        public void InitDataByRole()
        {

            InitData<SysRole>(lstSource: roleRepository.GetList(), master: PrivilegeMaster.Role);

            /*
            int num = 1;
            //获取现在已有的菜单、按钮数据。在将按钮添加到菜单属性中。
            var lstMenu = this.menuRepository.GetList();
              
            foreach (var sysMenu in lstMenu)
            {
                sysMenu.Buttons = this.menuRepository.GetButtons(sysMenu.SysId);
            }

            //获取在该系统中的角色数据
            var lstRole = roleRepository.GetList();

            foreach (var sysRole in lstRole)
            {
                foreach (var sysMenu in lstMenu)
                {
                    var model = new SysPrivilege
                    { 
                        PrivilegeMaster = PrivilegeMaster.Role,
                        PrivilegeMasterKey = sysRole.SysId,
                        PrivilegeAccess = PrivilegeAccess.Menu,
                        PrivilegeAccessKey = sysMenu.SysId,
                        PrivilegeOperation = PrivilegeOperation.Disable,
                        RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), num++)
                    };
                     
                    #region 检查该条数据是否存在
                    
                    var chkResult =  repository.GetList<int>(
                        Constant.SqlCount,
                        string.Format(
                            Constant.SqlExistsSysPrivilegeWhere,
                            (int)PrivilegeMaster.Role,
                            sysRole.SysId,
                            (int)PrivilegeAccess.Menu,
                            sysMenu.SysId));

                    if (chkResult.FirstOrDefault() == 0)
                    { 
                        model.SysId = Util.NewId();

                        Console.WriteLine(repository.Add(model));
                    }

                    #endregion

                    foreach (var sysButton in sysMenu.Buttons)
                    {
                        model = new SysPrivilege
                        { 
                            PrivilegeMaster = PrivilegeMaster.Role,
                            PrivilegeMasterKey = sysRole.SysId,
                            PrivilegeAccess = PrivilegeAccess.Button,
                            PrivilegeAccessKey = sysButton.SysId,
                            PrivilegeOperation = PrivilegeOperation.Disable,
                            RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), num++)
                        }; 

                        #region 检查该条数据是否存在
                        
                        chkResult = repository.GetList<int>(
                              Constant.SqlCount,
                              string.Format(
                                  Constant.SqlExistsSysPrivilegeWhere,
                                  (int)PrivilegeMaster.Role,
                                  sysRole.SysId,
                                  (int)PrivilegeAccess.Button,
                                  sysButton.SysId));

                        if (chkResult.FirstOrDefault() == 0)
                        { 
                            model.SysId = Util.NewId();
                            Console.WriteLine(repository.Add(model));
                        }

                        #endregion

                    }
                }
            }*/
        }

        /// <summary> 
        /// 遍历用户数据，分别创建，菜单和按钮权限记录，默认权限不可用。
        /// 注：在添加时要优先检查该权限记录是否存在。存在则不添加，不存在则添加。
        /// </summary>
        public void InitDataByUser()
        {
            InitData<SysUser>(userRepository.GetList(), PrivilegeMaster.User);
        }

        /// <summary>
        /// 根据权限拥有者类别ID重新设定权限
        /// </summary>
        /// <param name="sysId"></param>
        /// <param name="privilegeMaster"></param>
        /// <param name="menuIds"></param>
        /// <param name="userName">当前操作用户</param>
        /// <returns></returns>
        public bool SetMenuPrivilege(string sysId, PrivilegeMaster privilegeMaster, string[] menuIds, string userName)
        {
            //获取所有菜单
            IEnumerable<SysMenu> allMenus = ServiceFactory.MenuService.GetAllMenu();
            //获取所有按钮数据
            IEnumerable<SysButton> allButtons = buttonRepository.GetList<SysButton>(Constant.TableSysButton,
                                                                                    "SysId,MenuId,BtnName,BtnIcon,BtnOrder,BtnFunction,RecordStatus",
                                                                                    null);

            //存储传入的menuid集合
            List<string> menus = new List<string>();
            //存储传入的buttonid集合
            List<string> buttons = new List<string>();

            foreach (SysMenu menu in allMenus)
            {
                if (menuIds.Contains(menu.SysId))
                    menus.Add(menu.SysId);
            }
            foreach (SysButton button in allButtons)
            {
                if (menuIds.Contains(button.SysId))
                    buttons.Add(button.SysId);
            }

            using (IDbTransaction tran = ConnectionFactory.CreateMsSqlConnection().BeginTransaction())
            {
                //根据用户ID获取原来用户可用菜单
                //用于和现在选中的菜单进行判断，是否屏蔽原来可用菜单
                IEnumerable<SysMenu> userMenus = ServiceFactory.MenuService.GetPrivilegedSysMenuByUserId(sysId);

                int deleteResult = privilegeRepository.DeleteSysPrivilegeByMaster(sysId, privilegeMaster, tran);

                #region 如果是用户权限，先将用户和角色的权限进行对比，将用户禁用而角色启用的菜单和按钮设置为禁用
                if (privilegeMaster == PrivilegeMaster.User)
                {
                    #region 设置菜单禁用
                    foreach (SysMenu userMenu in userMenus)
                    {
                        if (!menus.Contains(userMenu.SysId))
                        {
                            SysPrivilege sysPrivilege = new SysPrivilege
                            {
                                PrivilegeAccess = PrivilegeAccess.Menu,
                                PrivilegeAccessKey = userMenu.SysId,
                                PrivilegeMaster = privilegeMaster,
                                PrivilegeMasterKey = sysId,
                                PrivilegeOperation = PrivilegeOperation.Disable
                            };

                            int addResult = privilegeRepository.AddSysPrivilegeByAccess(sysPrivilege, userName, tran);

                            if (addResult == 0)
                            {
                                tran.Rollback();
                                return false;
                            }

                            #region 设置按钮禁用
                            if (userMenu.Buttons!=null)
                            {
                                foreach (SysButton button in userMenu.Buttons)
                                {
                                    if (!buttons.Contains(button.SysId))
                                    {
                                        SysPrivilege sysPrivilegeBt = new SysPrivilege
                                        {
                                            PrivilegeAccess = PrivilegeAccess.Button,
                                            PrivilegeAccessKey = button.SysId,
                                            PrivilegeMaster = privilegeMaster,
                                            PrivilegeMasterKey = sysId,
                                            PrivilegeOperation = PrivilegeOperation.Disable
                                        };

                                        addResult = privilegeRepository.AddSysPrivilegeByAccess(sysPrivilegeBt, userName, tran);

                                        if (addResult == 0)
                                        {
                                            tran.Rollback();
                                            return false;
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                #endregion

                #region 角色权限，只存储可用菜单，不进行和现在选中菜单判断
                foreach (string menu in menus)
                {
                    SysPrivilege sysPrivilege = new SysPrivilege
                    {
                        PrivilegeAccess = PrivilegeAccess.Menu,
                        PrivilegeAccessKey = menu,
                        PrivilegeMaster = privilegeMaster,
                        PrivilegeMasterKey = sysId,
                        PrivilegeOperation = PrivilegeOperation.Enable
                    };

                    int addResult = privilegeRepository.AddSysPrivilegeByAccess(sysPrivilege, userName, tran);

                    if (addResult == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    #region 存储菜单下不可用按钮

                    var menuBts = allButtons.Where(x => x.MenuId == menu);
                    //foreach (string buttonId in buttons)
                    //{
                    //    menuBts = menuBts.Where(x => (buttons.Contains(x.SysId)));
                    //}
                    menuBts = menuBts.Where(x => (!buttons.Contains(x.SysId)));
                    foreach (SysButton sysButton in menuBts)
                    {
                        sysPrivilege = new SysPrivilege
                        {
                            PrivilegeAccess = PrivilegeAccess.Button,
                            PrivilegeAccessKey = sysButton.SysId,
                            PrivilegeMaster = privilegeMaster,
                            PrivilegeMasterKey = sysId,
                            PrivilegeOperation = PrivilegeOperation.Disable
                        };

                        addResult = privilegeRepository.AddSysPrivilegeByAccess(sysPrivilege, userName, tran);

                        if (addResult == 0)
                        {
                            tran.Rollback();
                            return false;
                        }
                    }

                    #endregion
                }
                #endregion

                #region 角色权限，只存储可用按钮，不进行和现在选中按钮判断

                //foreach (string id in buttons)
                //{
                //    allButtons = allButtons.Where(x => x.SysId != id);
                //}
                //allButtons = allButtons.Where(x => (!buttons.Contains(x.SysId)));
                //foreach (SysButton button in allButtons)
                //{
                //    SysPrivilege sysPrivilege = new SysPrivilege
                //    {
                //        PrivilegeAccess = PrivilegeAccess.Button,
                //        PrivilegeAccessKey = button.SysId,
                //        PrivilegeMaster = privilegeMaster,
                //        PrivilegeMasterKey = sysId,
                //        PrivilegeOperation = PrivilegeOperation.Enable
                //    };

                //    int addResult = privilegeRepository.AddSysPrivilegeByAccess(sysPrivilege, userName, tran);

                //    if (addResult == 0)
                //    {
                //        tran.Rollback();
                //        return false;
                //    }
                //}
                #endregion

                tran.Commit();
                return true;
            }
        }

        #region helper

        public void InitData<T>(IEnumerable<T> lstSource, PrivilegeMaster master) where T : EntityBase
        {
            int num = 1;
            //获取现在已有的菜单、按钮数据。在将按钮添加到菜单属性中。
            var lstMenu = this.menuRepository.GetList();

            foreach (var sysMenu in lstMenu)
            {
                sysMenu.Buttons = this.menuRepository.GetButtons(sysMenu.SysId);
            }

            foreach (var source in lstSource)
            {
                foreach (var sysMenu in lstMenu)
                {
                    var model = new SysPrivilege
                    {
                        PrivilegeMaster = master,
                        PrivilegeMasterKey = source.SysId,
                        PrivilegeAccess = PrivilegeAccess.Menu,
                        PrivilegeAccessKey = sysMenu.SysId,
                        PrivilegeOperation = PrivilegeOperation.Disable,
                        RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), num++)
                    };

                    #region 检查该条数据是否存在

                    var chkResult = privilegeRepository.GetList<int>(
                        Constant.SqlCount,
                        string.Format(
                            Constant.SqlExistsSysPrivilegeWhere,
                            (int)master,
                            source.SysId,
                            (int)PrivilegeAccess.Menu,
                            sysMenu.SysId));

                    if (chkResult.FirstOrDefault() == 0)
                    {
                        Console.WriteLine(privilegeRepository.Add(model));
                    }

                    #endregion

                    foreach (var sysButton in sysMenu.Buttons)
                    {
                        model = new SysPrivilege
                        {
                            PrivilegeMaster = master,
                            PrivilegeMasterKey = source.SysId,
                            PrivilegeAccess = PrivilegeAccess.Button,
                            PrivilegeAccessKey = sysButton.SysId,
                            PrivilegeOperation = PrivilegeOperation.Disable,
                            RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), num++)
                        };

                        #region 检查该条数据是否存在

                        chkResult = privilegeRepository.GetList<int>(
                              Constant.SqlCount,
                              string.Format(
                                  Constant.SqlExistsSysPrivilegeWhere,
                                  (int)master,
                                  source.SysId,
                                  (int)PrivilegeAccess.Button,
                                  sysButton.SysId));

                        if (chkResult.FirstOrDefault() == 0)
                        {
                            Console.WriteLine(privilegeRepository.Add(model));
                        }

                        #endregion
                    }
                }
            }
        }


        #endregion
    }
}