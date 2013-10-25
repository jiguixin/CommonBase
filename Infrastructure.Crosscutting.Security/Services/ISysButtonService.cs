/*
 *名称：ISysButtonService
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-13 01:54:35
 *修改时间：
 *备注：
 */

using System;
using Infrastructure.Crosscutting.Security.Common;

namespace Infrastructure.Crosscutting.Security.Services
{
    using System.Collections.Generic;

    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;

    public interface ISysButtonService
    {
        SysButtonRepository ButtonRepository { get; }
        IEnumerable<SysPrivilege> GetPrivilege(string buttonId);
         
        IEnumerable<SysButton> GetButtonsPrivilegeByUserAndMenu(string userId, string menuId);

        IEnumerable<SysButton> GetButtonsPrivilegeByUserAndMenu(string sysId, string menuId,
                                                                       PrivilegeMaster master,
                                                                       IEnumerable<SysButton> buttons,
                                                                       IEnumerable<SysPrivilege> privileges);

        IEnumerable<SysButton> InitialAddModifyDelBtn(string menuId, string recordStatus);
    }
}