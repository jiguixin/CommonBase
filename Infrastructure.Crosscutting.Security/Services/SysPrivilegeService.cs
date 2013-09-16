/*
 *名称：SysPrivilegeService
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-16 17:23:32
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.Services
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;
    using Infrastructure.Crosscutting.Utility;

    public class SysPrivilegeService:ISysPrivilegeService
    {
        private readonly IRepository<SysPrivilege> repository;

        private readonly SysMenuRepository menuRepository;

        private readonly SysRoleRepository roleRepository;

        public SysPrivilegeService()
        {
            repository = RepositoryFactory.PrivilegeRepository; 
            this.menuRepository = RepositoryFactory.MenuRepository;
            this.roleRepository = RepositoryFactory.RoleRepository;
        }

        /// <summary> 
        /// 遍历角色数据，分别创建，菜单和按钮权限记录，默认权限不可用。
        /// 注：在添加时要优先检查该权限记录是否存在。存在者不添加，不存在者添加。
        /// </summary>
        public void InitDataByRole()
        {
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
                        SysId = Util.NewSequentialGuid().ToString(),
                        PrivilegeMaster = PrivilegeMaster.Role,
                        PrivilegeMasterKey = sysRole.SysId,
                        PrivilegeAccess = PrivilegeAccess.Menu,
                        PrivilegeAccessKey = sysMenu.SysId,
                        PrivilegeOperation = PrivilegeOperation.Disable,
                        RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "zwt")
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
                        Console.WriteLine(repository.Add(model));
                    }

                    #endregion

                    foreach (var sysButton in sysMenu.Buttons)
                    {
                        model = new SysPrivilege
                        {
                            SysId = Util.NewSequentialGuid().ToString(),
                            PrivilegeMaster = PrivilegeMaster.Role,
                            PrivilegeMasterKey = sysRole.SysId,
                            PrivilegeAccess = PrivilegeAccess.Button,
                            PrivilegeAccessKey = sysButton.SysId,
                            PrivilegeOperation = PrivilegeOperation.Disable,
                            RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "zwt")
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
                            Console.WriteLine(repository.Add(model));
                        }

                        #endregion

                    }
                }
            }
             
        }

        public void InitDataByUser()
        {
            throw new NotImplementedException();
        }

        #region helper

        

        #endregion
    }
}