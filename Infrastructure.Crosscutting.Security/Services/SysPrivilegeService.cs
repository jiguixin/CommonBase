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
using Infrastructure.Data.Ado.Dapper;

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
        /// <param name="IDs">包括菜单、按钮编号</param>
        /// <param name="userName">当前操作用户</param>
        /// <returns></returns>
        public bool SetMenuPrivilege(string sysId, PrivilegeMaster privilegeMaster, string[] IDs, string userName)
        {
            //获取所有菜单
            IEnumerable<SysMenu> allMenus = menuRepository.GetList();
            //获取所有按钮
            IEnumerable<SysButton> allButtons = buttonRepository.GetList();

            //存储传入的menuId集合
            List<string> menus = (from menu in allMenus where IDs.Contains(menu.SysId) select menu.SysId).ToList();

            //存储传入的buttonId集合 
            List<string> buttons = (from button in allButtons where IDs.Contains(button.SysId) select button.SysId).ToList();

            using (IDbTransaction tran = privilegeRepository.Connection.BeginTransaction())
            {
                //根据用户ID获取原来用户可用菜单
                //用于和现在选中的菜单进行判断，是否屏蔽原来可用菜单
                IEnumerable<SysMenu> userMenus = ServiceFactory.MenuService.GetPrivilegedSysMenuByUserId(sysId);

                this.privilegeRepository.DeleteSysPrivilegeByMaster(sysId, privilegeMaster, tran);

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
                                PrivilegeOperation = PrivilegeOperation.Disable,
                                RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now, userName)
                            };

                            int addResult = privilegeRepository.Add(sysPrivilege, tran);

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
                                            PrivilegeOperation = PrivilegeOperation.Disable,
                                            RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now, userName)
                                        };

                                        addResult = privilegeRepository.Add(sysPrivilege, tran);

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
                        PrivilegeOperation = PrivilegeOperation.Enable,
                        RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now, userName)
                    };

                    int addResult = privilegeRepository.Add(sysPrivilege, tran);

                    if (addResult == 0)
                    {
                        tran.Rollback();
                        return false;
                    }

                    #region 存储菜单下不可用按钮

                    var menuBts = allButtons.Where(x => x.MenuId == menu);

                    menuBts = menuBts.Where(x => (!buttons.Contains(x.SysId)));
                    foreach (SysButton sysButton in menuBts)
                    {
                        sysPrivilege = new SysPrivilege
                        {
                            PrivilegeAccess = PrivilegeAccess.Button,
                            PrivilegeAccessKey = sysButton.SysId,
                            PrivilegeMaster = privilegeMaster,
                            PrivilegeMasterKey = sysId,
                            PrivilegeOperation = PrivilegeOperation.Disable,
                            RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now, userName)
                        };

                        addResult = privilegeRepository.Add(sysPrivilege, tran);

                        if (addResult == 0)
                        {
                            tran.Rollback();
                            return false;
                        }
                    }

                    #endregion
                }
                #endregion

                tran.Commit();
                return true;
            }
        }

        #region helper

        public void InitData<T>(IEnumerable<T> lstSource, PrivilegeMaster master) where T : EntityBase
        { 
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
                        RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "System")
                    };

                    #region 检查该条数据是否存在
                    var p = new DynamicParameters();
                    p.Add("PrivilegeMaster", (int)master);
                    p.Add("PrivilegeMasterKey", source.SysId.Trim());
                    p.Add("PrivilegeAccess", (int) PrivilegeAccess.Menu);
                    p.Add("PrivilegeAccessKey", sysMenu.SysId.Trim());

                    var chkResult = privilegeRepository.GetList<int>(Constant.SqlCount,
                                                                     Constant.SqlExistsSysPrivilegeWhere, p);
                     
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
                            RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "System")
                        };

                        #region 检查该条数据是否存在

                        p = new DynamicParameters();
                        p.Add("PrivilegeMaster", (int)master);
                        p.Add("PrivilegeMasterKey", source.SysId.Trim());
                        p.Add("PrivilegeAccess", (int)PrivilegeAccess.Button);
                        p.Add("PrivilegeAccessKey", sysButton.SysId.Trim());
                        chkResult = privilegeRepository.GetList<int>(Constant.SqlCount,
                                                                     Constant.SqlExistsSysPrivilegeWhere, p);
                          
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