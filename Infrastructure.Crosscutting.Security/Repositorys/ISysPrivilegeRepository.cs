/*
 *名称：ISysPrivilegeRepository
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-08 09:27:41
 *修改时间：
 *备注：
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Crosscutting.Security.Common;

namespace Infrastructure.Crosscutting.Security.Repositorys
{
    public interface ISysPrivilegeRepository
    {
        ///// <summary>
        ///// 为指定的用户或角色添加相应的权限
        ///// </summary>
        ///// <param name="master"></param>
        ///// <param name="sourceKey"></param>
        ///// <returns></returns>
        //int AddSysPrivilegeByMaster(PrivilegeMaster master,IEnumerable<string> sourceKey);

        /// <summary>
        /// 删除指定的用户或角色权限数据
        /// </summary>
        /// <param name="master">拥有者主休</param>
        /// <param name="sysId">拥有者的编号</param>
        /// <returns></returns>
        int DeleteSysPrivilegeByMaster(PrivilegeMaster master, string sysId);

        /// <summary>
        /// 删除指定的菜单或按钮权限数据
        /// </summary>
        /// <param name="access">菜单或按钮</param>
        /// <param name="sysId">菜单或按钮编号</param>
        /// <returns></returns>
        int DeleteSysPrivilegeByAccess(PrivilegeAccess access, string sysId); 
        
    }
}