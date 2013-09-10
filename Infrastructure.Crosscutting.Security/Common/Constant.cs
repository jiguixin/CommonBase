/*
 *名称：Constant
 *功能：主要用于保存一些系统的常量。如：储存过程的名字等
 *创建人：吉桂昕
 *创建时间：2013-09-03 14:31:32
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.Common
{
    /// <summary>
    ///     主要用于保存一些系统的常量。如：储存过程的名字等
    /// </summary>
    public static class Constant
    {
        #region Static Fields 储存过程名

        public static readonly string ProcGetPaged = "proc_DataPagination";
        public static readonly string ProcGetList = "proc_GetList";

        //根据条件删除指定表的数据
        public static readonly string ProcDeleteByWhere = "proc_Delete_By_Where";


         
        #region Sys_Button

        public static readonly string ProcSysButtonExists = "Sys_Button_Exists";

        public static readonly string ProcSysButtonAdd = "Sys_Button_ADD";

        public static readonly string ProcSysButtonDelete = "Sys_Button_Delete";

        public static readonly string ProcSysButtonGetList = "Sys_Button_GetList";

        public static readonly string ProcSysButtonGetModel = "Sys_Button_GetModel";

        public static readonly string ProcSysButtonUpdate = "Sys_Button_Update";

        public static readonly string ProcSysButtonDeleteByMenuId = "Sys_Button_Delete_By_MenuId";
         
        #endregion

        #region Sys_Config

        public static readonly string ProcSysConfigAdd = "Sys_Config_ADD";

        public static readonly string ProcSysConfigDelete = "Sys_Config_Delete";

        public static readonly string ProcSysConfigExists = "Sys_Config_Exists";

        public static readonly string ProcSysConfigGetList = "Sys_Config_GetList";

        public static readonly string ProcSysConfigGetModel = "Sys_Config_GetModel";

        public static readonly string ProcSysConfigUpdate = "Sys_Config_Update";

        #endregion

        #region Sys_DataPrivilege


        public static readonly string ProcSysDataPrivilegeAdd = "Sys_DataPrivilege_ADD";

        public static readonly string ProcSysDataPrivilegeDelete = "Sys_DataPrivilege_Delete";

        public static readonly string ProcSysDataPrivilegeExists = "Sys_DataPrivilege_Exists";

        public static readonly string ProcSysDataPrivilegeGetList = "Sys_DataPrivilege_GetList";

        public static readonly string ProcSysDataPrivilegeGetModel = "Sys_DataPrivilege_GetModel";

        public static readonly string ProcSysDataPrivilegeUpdate = "Sys_DataPrivilege_Update";

        #endregion

        #region Sys_Menu

        public static readonly string ProcSysMenuAdd = "Sys_Menu_ADD";

        public static readonly string ProcSysMenuDelete = "Sys_Menu_Delete";

        public static readonly string ProcSysMenuExists = "Sys_Menu_Exists";

        public static readonly string ProcSysMenuGetList = "Sys_Menu_GetList";

        public static readonly string ProcSysMenuGetModel = "Sys_Menu_GetModel";

        public static readonly string ProcSysMenuUpdate = "Sys_Menu_Update";

        #endregion

        #region Sys_Privilege

        public static readonly string ProcSysPrivilegeAdd = "Sys_Privilege_ADD";

        public static readonly string ProcSysPrivilegeDelete = "Sys_Privilege_Delete";

        public static readonly string ProcSysPrivilegeExists = "Sys_Privilege_Exists";

        public static readonly string ProcSysPrivilegeGetList = "Sys_Privilege_GetList";

        public static readonly string ProcSysPrivilegeGetModel = "Sys_Privilege_GetModel";

        public static readonly string ProcSysPrivilegeUpdate = "Sys_Privilege_Update";

        public static readonly string SysPrivilegeDeleteByPrivilegeMaster = "Sys_Privilege_Delete_By_PrivilegeMaster";

        public static readonly string SysPrivilegeDeleteByPrivilegeAccess = "Sys_Privilege_Delete_By_PrivilegeAccess"; 

        #endregion

        #region Sys_Role

        public static readonly string ProcSysRoleAdd = "Sys_Role_ADD";

        public static readonly string ProcSysRoleDelete = "Sys_Role_Delete";

        public static readonly string ProcSysRoleExists = "Sys_Role_Exists";

        public static readonly string ProcSysRoleGetList = "Sys_Role_GetList";

        public static readonly string ProcSysRoleGetModel = "Sys_Role_GetModel";

        public static readonly string ProcSysRoleUpdate = "Sys_Role_Update";

        #endregion

        #region Sys_UserInfo

        public static readonly string ProcSysUserInfoAdd = "Sys_UserInfo_ADD";

        public static readonly string ProcSysUserInfoDelete = "Sys_UserInfo_Delete";

        public static readonly string ProcSysUserInfoExists = "Sys_UserInfo_Exists";

        public static readonly string ProcSysUserInfoGetList = "Sys_UserInfo_GetList";

        public static readonly string ProcSysUserInfoGetModel = "Sys_UserInfo_GetModel";

        public static readonly string ProcSysUserInfoUpdate = "Sys_UserInfo_Update";

        #endregion

        #region Sys_UserRole

        public static readonly string ProcSysUserRoleAdd = "Sys_UserRole_ADD";

        public static readonly string ProcSysUserRoleDelete = "Sys_UserRole_Delete";

        public static readonly string ProcSysUserRoleExists = "Sys_UserRole_Exists";

        public static readonly string ProcSysUserRoleGetList = "Sys_UserRole_GetList";

        public static readonly string ProcSysUserRoleGetModel = "Sys_UserRole_GetModel";

        public static readonly string ProcSysUserRoleUpdate = "Sys_UserRole_Update";

        #endregion

        #region Sys_User

        public static readonly string ProcSysUserAdd = "Sys_User_ADD";

        public static readonly string ProcSysUserDelete = "Sys_User_Delete";

        public static readonly string ProcSysUserExists = "Sys_User_Exists";

        public static readonly string ProcSysUserGetList = "Sys_User_GetList";

        public static readonly string ProcSysUserGetModel = "Sys_User_GetModel";

        public static readonly string ProcSysUserUpdate = "Sys_User_Update";

        #endregion

        #endregion

        #region 表名

        public static readonly string TableSysButton = "Sys_Button";
        public static readonly string TableSysConfig = "Sys_Config";
        public static readonly string TableSysDataPrivilege = "Sys_DataPrivilege";
        public static readonly string TableSysMenu = "Sys_Menu";
        public static readonly string TableSysPrivilege = "Sys_Privilege";
        public static readonly string TableSysRole = "Sys_Role";
        public static readonly string TableSysUserInfo = "Sys_UserInfo";
        public static readonly string TableSysUserRole = "Sys_UserRole";
        public static readonly string TableSysUser = "Sys_User"; 

        #endregion
    }
}