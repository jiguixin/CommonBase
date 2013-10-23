using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Crosscutting.Security.Services
{
    using Infrastructure.Crosscutting.Security.Common;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;

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
            return ButtonRepository.GetListByTable<SysPrivilege>(Constant.SqlTableButtonPrivilegeJoin, Constant.SqlFieldsPrivilegeJoin, string.Format("p.PrivilegeAccess={0} and b.SysId = '{1}'", (int)PrivilegeAccess.Button, buttonId));
        }

        public IEnumerable<SysButton> GetAllButons()
        {
            return RepositoryFactory.ButtonRepository.GetListByTable<SysButton>(Constant.TableSysButton,
                                                                         "SysId,MenuId,BtnName,BtnIcon,BtnOrder,BtnFunction,RecordStatus",
                                                                         null);
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

            var buttons = GetAllButons().Where(x => x.MenuId == menuId).ToList();

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

        public IEnumerable<SysButton> TT(string userId, string menuId)
        {
            var privileges = ServiceFactory.MenuService.GetPrivilegesByUserId(userId);
            privileges = privileges.Where(x => x.PrivilegeAccess == PrivilegeAccess.Button && x.PrivilegeOperation == PrivilegeOperation.Disable);

            var buttons = GetAllButons().Where(x => x.MenuId == menuId).ToList();

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
                                                        RecordStatus = recordStatus
                                                    },
                                                new SysButton
                                                    {
                                                        BtnName = "编辑",
                                                        BtnFunction = "EditItem",
                                                        BtnIcon = "icon-edit",
                                                        BtnOrder = 20,
                                                        MenuId = menuId,
                                                        RecordStatus = recordStatus
                                                    },
                                                new SysButton
                                                    {
                                                        BtnName = "废弃",
                                                        BtnFunction = "DelItem",
                                                        BtnIcon = "icon-remove",
                                                        BtnOrder = 30,
                                                        MenuId = menuId,
                                                        RecordStatus = recordStatus
                                                    }
                                            };

            return lstResult;

        }
    }
}
