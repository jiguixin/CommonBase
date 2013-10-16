/*
 *名称：Enums
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-03 14:29:28
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.Common
{
    /// <summary>
    /// 权限拥有者 如：用户、角色、部门等 类型
    /// </summary>
    public enum PrivilegeMaster
    { 
        /// <summary>
        /// 10
        /// </summary>
        User =10,
        /// <summary>
        /// 11
        /// </summary>
        Role,
        Department, 
    }

    /// <summary>
    /// 能被访问的是:菜单、按钮 类型
    /// </summary>
    public enum PrivilegeAccess
    {
        /// <summary>
        /// 100
        /// </summary>
        Menu =100, 
        /// <summary>
        /// 101
        /// </summary>
        Button, 
        Data
    }

    /// <summary>
    /// 权限操作,如，可见，不可用
    /// </summary>
    public enum PrivilegeOperation
    {
        Disable=0,  //不可用
        Enable,   //可用  
    } 
}