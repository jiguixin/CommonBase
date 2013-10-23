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

       // public static readonly string ProcGetPaged = "proc_DataPagination";
       // public static readonly string ProcGetList = "proc_GetList";

        //根据条件删除指定表的数据
        //public static readonly string ProcDeleteByWhere = "proc_Delete_By_Where";
          
        #region Sys_Button
         
        public static readonly string SqlSysButtonAdd = "Sys_Button_ADD";
            
        public static readonly string SqlSysButtonUpdate = "Sys_Button_Update";
         
        #endregion

        #region Sys_Config

        public static readonly string SqlSysConfigAdd = "Sys_Config_ADD";
           
        public static readonly string SqlSysConfigUpdate = "Sys_Config_Update";

        #endregion

        #region Sys_DataPrivilege
         
        public static readonly string SqlSysDataPrivilegeAdd = "Sys_DataPrivilege_ADD";
          
        public static readonly string SqlSysDataPrivilegeUpdate = "Sys_DataPrivilege_Update";

        #endregion

        #region Sys_Menu

        public static readonly string SqlSysMenuAdd = "Sys_Menu_ADD";
          
        public static readonly string SqlSysMenuUpdate = "Sys_Menu_Update";

        #endregion

        #region Sys_Privilege

        public static readonly string SqlSysPrivilegeAdd = "Sys_Privilege_ADD";
          
        public static readonly string SqlSysPrivilegeUpdate = "Sys_Privilege_Update";
          
        #endregion

        #region Sys_Role

        public static readonly string SqlSysRoleAdd = "Sys_Role_ADD";
          
        public static readonly string SqlSysRoleUpdate = "Sys_Role_Update";

        #endregion

        #region Sys_UserInfo

        public static readonly string SqlSysUserInfoAdd = "Sys_UserInfo_ADD";
          
        public static readonly string SqlSysUserInfoUpdate = "Sys_UserInfo_Update";

        #endregion

        #region Sys_UserRole

        public static readonly string SqlSysUserRoleAdd = "Sys_UserRole_ADD";
          
        public static readonly string SqlSysUserRoleUpdate = "Sys_UserRole_Update";

        #endregion

        #region Sys_User

        public static readonly string SqlSysUserAdd = "Sys_User_ADD";
          
        public static readonly string SqlSysUserUpdate = "Sys_User_Update";

        #endregion

        #endregion

        #region 字符串常量

        public static readonly string OracleParameterPrefix = ":";
        public static readonly string SqlServerParameterPrefix = "@";

        /// <summary>
        /// 在不同数据库中，构造sql语句会用不同的前缀，如sqlserver为'@'，oracle为':'
        /// 为了在通用查询中不依赖具体数据库，所以用此常量来统一作为sql的参数，
        /// 在Repository中在替换该字符串，
        /// </summary>
        public static readonly string SqlReplaceParameterPrefix = "$$";



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

        #region 列名

        public static string ColumnSysId = "SysId";
         
        #region Sys_User
        
        public static readonly string ColumnSysUserUserName = "UserName";
        public static readonly string ColumnSysUserUserPwd = "UserPwd";

        #endregion

        #region Sys_Button

        public static readonly string ColumnSysButtonMenuId = "MenuId";
         
        #endregion

        #region Sys_Privilege

        public static readonly string ColumnSysPrivilegePrivilegeMaster = "PrivilegeMaster";
        public static readonly string ColumnSysPrivilegePrivilegeMasterKey = "PrivilegeMasterKey";

        public static readonly string ColumnSysPrivilegePrivilegeAccess = "PrivilegeAccess";
        public static readonly string ColumnSysPrivilegePrivilegeAccessKey = "PrivilegeAccessKey";
         
        #endregion


        #endregion

        #region Sql 语句常量

        /// <summary>
        /// COUNT(1)
        /// </summary>
        public static readonly string SqlCount = "COUNT(1)";

        /// <summary>
        /// 将用户角色，用户名色表进行关系查询
        /// Sql语句：
        /// Sys_User u inner join Sys_UserRole ur on u.SysId=ur.UserId inner join Sys_Role r on ur.RoleId = r.SysId
        /// </summary>
        public static readonly string  SqlTableUserAndRoleJoin =
                "Sys_User u inner join Sys_UserRole ur on u.SysId=ur.UserId inner join Sys_Role r on ur.RoleId = r.SysId";

        /// <summary>
        /// 将用户角色，用户名色表进行关系查询,包括UserInfo表
        /// Sql语句：
        /// Sys_User u inner join Sys_UserInfo ui on u.SysId=ui.SysId inner join Sys_UserRole ur on u.SysId=ur.UserId inner join Sys_Role r on ur.RoleId = r.SysId
        /// </summary>
        public static readonly string SqlTableUserAndRoleIncludeUserInfoJoin =
                "Sys_User u inner join Sys_UserInfo ui on u.SysId=ui.SysId inner join Sys_UserRole ur on u.SysId=ur.UserId inner join Sys_Role r on ur.RoleId = r.SysId";

        /// <summary>
        /// 用于获取用户包括到UserInfo的级联查询要显示的列
        /// Sql语句：
        /// u.CreateTime,u.LastLogin,u.RecordStatus,u.SysId,u.UserName,u.UserPwd,ui.SysId,ui.Address,ui.Email,ui.Fax,ui.Phone,ui.QQ,ui.RealName,ui.Sex,ui.Title
        /// </summary>
        public static readonly string SqlFieldsUserAndRoleIncludeUserInfoJoin = "u.CreateTime,u.LastLogin,u.RecordStatus,u.SysId,u.UserName,u.UserPwd,ui.SysId,ui.Address,ui.Email,ui.Fax,ui.Phone,ui.QQ,ui.RealName,ui.Sex,ui.Title";


        /// <summary>
        /// 用于，角色关联权限 的join查询
        /// Sql语句:
        /// Sys_Role r inner join Sys_Privilege p on r.SysId=p.PrivilegeMasterKey
        /// </summary>
       public static readonly string SqlTableRolePrivilegeJoin = "Sys_Role r inner join Sys_Privilege p on r.SysId=p.PrivilegeMasterKey";

       /// <summary>
       /// 用于，用户关联权限 的join查询
       /// Sql语句:
       /// Sys_User u inner join Sys_Privilege p on u.SysId=p.PrivilegeMasterKey
       /// </summary>
       public static readonly string SqlTableUserPrivilegeJoin = "Sys_User u inner join Sys_Privilege p on u.SysId=p.PrivilegeMasterKey";

        /// <summary>
        /// 用于，角色关联权限，用户关联权限，查询具体的权限数据列
        /// Sql语句:
        /// p.SysId,p.PrivilegeAccess,p.PrivilegeAccessKey,p.PrivilegeMaster,p.PrivilegeMasterKey, p.PrivilegeOperation,p.RecordStatus
        /// </summary>
        public static readonly string SqlFieldsPrivilegeJoin ="p.SysId,p.PrivilegeAccess,p.PrivilegeAccessKey,p.PrivilegeMaster,p.PrivilegeMasterKey,p.PrivilegeOperation,p.RecordStatus";

        /// <summary>
        /// 用于，菜单关联权限 的join查询
        /// Sql语句:
        /// Sys_Menu m inner join Sys_Privilege p on m.SysId = p.PrivilegeAccessKey
        /// </summary>
        public static readonly string SqlTableMenuPrivilegeJoin =
            "Sys_Menu m inner join Sys_Privilege p on m.SysId = p.PrivilegeAccessKey";

        /// <summary>
        /// 用于，按钮关联权限 的join查询
        /// Sql语句:
        /// Sys_Button b inner join Sys_Privilege p on b.SysId = p.PrivilegeAccessKey
        /// </summary>
        public static readonly string SqlTableButtonPrivilegeJoin =
            "Sys_Button b inner join Sys_Privilege p on b.SysId = p.PrivilegeAccessKey";

        /// <summary>
        /// 用于检查该条权限记录是否存在
        /// Sql语句:
        /// PrivilegeMaster='{0}' and PrivilegeMasterKey='{1}' and PrivilegeAccess = '{2}' and PrivilegeAccessKey='{3}'
        /// </summary>
        public static readonly string SqlExistsSysPrivilegeWhere = "PrivilegeMaster='{0}' and PrivilegeMasterKey='{1}' and PrivilegeAccess = '{2}' and PrivilegeAccessKey='{3}'";

        #endregion
    }
}