using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Crosscutting.Security.Services
{
    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;
    using Infrastructure.Data.Ado.Dapper;

    public class SysButtonService : ISysButtonService
    {
        public SysButtonRepository ButtonRepository
        {
            get { return RepositoryFactory.ButtonRepository; }
        }

        public SysButtonService()
        {
        }

        public IEnumerable<SysPrivilege> GetPrivilege(string buttonId)
        {
            var p = new DynamicParameters();
            p.Add(Constant.ColumnSysId, buttonId.Trim());
            p.Add(Constant.ColumnSysPrivilegePrivilegeAccess, (int)PrivilegeAccess.Button);

            return ButtonRepository.GetListByTable<SysPrivilege>(Constant.SqlTableButtonPrivilegeJoin, Constant.SqlFieldsPrivilegeJoin, string.Format("p.{0}={2}{0} and b.{1} = {2}{1}", Constant.ColumnSysPrivilegePrivilegeAccess, Constant.ColumnSysId, Constant.SqlReplaceParameterPrefix),p);
 
        }
          
        /// <summary>
        /// 根据用户id和菜单id获取当前菜单可用按钮
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public IEnumerable<SysButton> GetButtonsPrivilegeByUserAndMenu(string userId, string menuId)
        {
            var privileges = ServiceFactory.MenuService.GetPrivilegesByUserId(userId);
            privileges = privileges.Where(x => x.PrivilegeAccess == PrivilegeAccess.Button && x.PrivilegeOperation == PrivilegeOperation.Disable);

            var buttons = ButtonRepository.GetList().Where(x => x.MenuId == menuId).ToList();

            List<SysButton> resultBts = new List<SysButton>();
            foreach (SysPrivilege sysPrivilege in privileges)
            {
                var bts = buttons.Where(x => x.SysId == sysPrivilege.PrivilegeAccessKey);

                resultBts.AddRange(bts);
            }
            foreach (SysButton bt in resultBts)
            {
                buttons.Remove(bt);
            }

            return buttons;
        }


        /// <summary>
        /// 根据用户id和菜单id获取当前菜单可用按钮
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="menuId"></param>
        /// <param name="master"></param>
        /// <param name="buttons"></param>
        /// <param name="privileges"></param>
        /// <returns></returns>
        public IEnumerable<SysButton> GetButtonsPrivilegeByUserAndMenu(string userId, string menuId, PrivilegeMaster master,
                                                                              IEnumerable<SysButton> buttons, IEnumerable<SysPrivilege> privileges)
        {
            privileges = privileges.Where(x => x.PrivilegeAccess == PrivilegeAccess.Button && x.PrivilegeOperation == PrivilegeOperation.Disable);

            var resultButtons=buttons.Where(x => x.MenuId == menuId).ToList();

            List<SysButton> disableButtons = new List<SysButton>();
            foreach (SysPrivilege sysPrivilege in privileges)
            {
                var bts = buttons.Where(x => x.SysId == sysPrivilege.PrivilegeAccessKey);

                disableButtons.AddRange(bts);
            }
            //foreach (SysButton bt in disableButtons)
            //{
            //    resultButtons.Remove(bt);
            //}
            resultButtons = resultButtons.Where(x => (!disableButtons.Contains(x))).ToList();
            return resultButtons;
        }

        //public IEnumerable<SysButton> TT(string userId, string menuId)
        //{
        //    var privileges = ServiceFactory.MenuService.GetPrivilegesByUserId(userId);
        //    privileges = privileges.Where(x => x.PrivilegeAccess == PrivilegeAccess.Button && x.PrivilegeOperation == PrivilegeOperation.Disable);

        //    var buttons = ButtonRepository.GetList().Where(x => x.MenuId == menuId).ToList();

        //    List<SysButton> resultBts = new List<SysButton>();
        //    foreach (SysPrivilege sysPrivilege in privileges)
        //    {
        //        var bts = buttons.Where(x => x.SysId == sysPrivilege.PrivilegeAccessKey);

        //        resultBts.AddRange(bts);
        //    }
        //    foreach (SysButton bt in resultBts)
        //    {
        //        buttons.Remove(bt);
        //    }

        //    return buttons;
        //}

        public IEnumerable<SysButton> InitialAddModifyDelBtn(string menuId, string recordStatus)
        {
            List<SysButton> lstResult = new List<SysButton>
                                            {
                                                new SysButton
                                                    {
                                                        BtnName = "新增",
                                                        BtnFunction = "NewItem",
                                                        BtnIcon = "icon-add",
                                                        BtnOrder = 10,
                                                        MenuId = menuId,
                                                        RecordStatus = recordStatus,
                                                        IsVisible = 1
                                                    },
                                                new SysButton
                                                    {
                                                        BtnName = "编辑",
                                                        BtnFunction = "EditItem",
                                                        BtnIcon = "icon-edit",
                                                        BtnOrder = 20,
                                                        MenuId = menuId,
                                                        RecordStatus = recordStatus,
                                                        IsVisible = 1
                                                    },
                                                new SysButton
                                                    {
                                                        BtnName = "废弃",
                                                        BtnFunction = "DelItem",
                                                        BtnIcon = "icon-remove",
                                                        BtnOrder = 30,
                                                        MenuId = menuId,
                                                        RecordStatus = recordStatus,
                                                        IsVisible = 1
                                                    }
                                            };

            return lstResult;

        }
        public IEnumerable<SysButton> InitialAddModifyDelBtn(string menuId, string recordStatus,bool creatBt,bool modifyBt,bool delBt)
        {
            List<SysButton> lstResult = new List<SysButton>();
            if (creatBt)
            {
                lstResult.Add(new SysButton
                    {
                        BtnName = "新增",
                        BtnFunction = "NewItem",
                        BtnIcon = "icon-add",
                        BtnOrder = 10,
                        MenuId = menuId,
                        RecordStatus = recordStatus
                    });
            }
            if (modifyBt)
            {
                lstResult.Add(new SysButton
                    {
                        BtnName = "编辑",
                        BtnFunction = "EditItem",
                        BtnIcon = "icon-edit",
                        BtnOrder = 20,
                        MenuId = menuId,
                        RecordStatus = recordStatus
                    });
            }
            if (delBt)
            {
                lstResult.Add(new SysButton
                    {
                        BtnName = "废弃",
                        BtnFunction = "DelItem",
                        BtnIcon = "icon-remove",
                        BtnOrder = 30,
                        MenuId = menuId,
                        RecordStatus = recordStatus
                    });
            }
            return lstResult;

        }
        
    }
}
