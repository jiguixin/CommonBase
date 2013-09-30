﻿/*
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
    using System.Globalization;
    using System.Linq;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;

    public class SysPrivilegeService : ISysPrivilegeService
    {

        private static SysMenuRepository MenuRepository
        {
            get
            {
                return RepositoryFactory.MenuRepository;
            }
        } 

        private SysButtonRepository ButtonRepository
        {
            get
            {
                return RepositoryFactory.ButtonRepository;
            }
        }

        private static SysRoleRepository RoleRepository
        {
            get
            {
               return  RepositoryFactory.RoleRepository;
            }
        } 


        private static SysUserRepository UserRepository
        {
            get
            {
                return RepositoryFactory.UserRepository;
            }
        }

        private static SysPrivilegeRepository PrivilegeRepository
        {
            get
            {
                return RepositoryFactory.PrivilegeRepository;
            }
        } 

        /// <summary> 
        /// 遍历角色数据，分别创建，菜单和按钮权限记录，默认权限不可用。
        /// 注：在添加时要优先检查该权限记录是否存在。存在则不添加，不存在则添加。
        /// </summary>
        public void InitDataByRole()
        {

            InitData<SysRole>(lstSource: RoleRepository.GetList(), master: PrivilegeMaster.Role);

            /*
            int num = 1;
            //获取现在已有的菜单、按钮数据。在将按钮添加到菜单属性中。
            var lstMenu = this.MenuRepository.GetList();
              
            foreach (var sysMenu in lstMenu)
            {
                sysMenu.Buttons = this.MenuRepository.GetButtons(sysMenu.SysId);
            }

            //获取在该系统中的角色数据
            var lstRole = RoleRepository.GetList();

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
                    
                    var chkResult =  PrivilegeRepository.GetList<int>(
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

                        Console.WriteLine(PrivilegeRepository.Add(model));
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
                        
                        chkResult = PrivilegeRepository.GetList<int>(
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
                            Console.WriteLine(PrivilegeRepository.Add(model));
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
            InitData(UserRepository.GetList(), PrivilegeMaster.User);
        }

        /// <summary>
        /// 根据权限拥有者ID重新设定权限
        /// </summary>
        /// <param name="sysId"></param>
        /// <param name="privilegeMaster"></param>
        /// <param name="menuIds"></param>
        /// <returns></returns>
        public bool SetMenuPrivilege(string sysId, PrivilegeMaster privilegeMaster, string[] menuIds)
        {
            using (IDbTransaction tran = PrivilegeRepository.Connection.BeginTransaction())
            {
                 PrivilegeRepository.DeleteSysPrivilegeByMaster(sysId, (int)privilegeMaster, tran);

                for (int i = 0; i < menuIds.Count(); i++)
                {
                    SysPrivilege sysPrivilege = new SysPrivilege
                                                    {
                                                        PrivilegeAccess = PrivilegeAccess.Menu,
                                                        PrivilegeAccessKey = menuIds[i],
                                                        PrivilegeMaster = privilegeMaster,
                                                        PrivilegeMasterKey = sysId
                                                    };

                    var addResult = PrivilegeRepository.Add(sysPrivilege, tran);

                    if (addResult==0)
                    {
                        return false;
                    }
                }

                tran.Commit();
                return true;
            }
        }
        #region helper

        public void InitData<T>(IEnumerable<T> lstSource, PrivilegeMaster master) where T : EntityBase
        {
            int num = 1;
            //获取现在已有的菜单、按钮数据。在将按钮添加到菜单属性中。
            var lstMenu = MenuRepository.GetList();

            foreach (var sysMenu in lstMenu)
            {
                sysMenu.Buttons = MenuRepository.GetButtons(sysMenu.SysId);
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

                    var chkResult = PrivilegeRepository.GetList<int>(
                        Constant.SqlCount,
                        string.Format(
                            Constant.SqlExistsSysPrivilegeWhere,
                            (int)master,
                            source.SysId,
                            (int)PrivilegeAccess.Menu,
                            sysMenu.SysId));

                    if (chkResult.FirstOrDefault() == 0)
                    {
                        Console.WriteLine(PrivilegeRepository.Add(model));
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

                        chkResult = PrivilegeRepository.GetList<int>(
                              Constant.SqlCount,
                              string.Format(
                                  Constant.SqlExistsSysPrivilegeWhere,
                                  (int)master,
                                  source.SysId,
                                  (int)PrivilegeAccess.Button,
                                  sysButton.SysId));

                        if (chkResult.FirstOrDefault() == 0)
                        {
                            Console.WriteLine(PrivilegeRepository.Add(model));
                        }

                        #endregion
                    }
                }
            }
        }


        #endregion
    }
}