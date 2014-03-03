/*
 *名称：SysPrivilegeBase
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 14:34:08
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    using Infrastructure.Crosscutting.Security.Core;

    public class SysPrivilegeBase:SqlBase
    {
        public SysPrivilegeBase(IAppContext appContext)
            : base(appContext)
        {
        }

        public override string AddSql
        {
            get
            {
                return string.Format(@"INSERT INTO Sys_Privilege(
	SysId,PrivilegeMaster,PrivilegeMasterKey,PrivilegeAccess,PrivilegeAccessKey,PrivilegeOperation,RecordStatus
	)VALUES(
	{0}SysId,{0}PrivilegeMaster,{0}PrivilegeMasterKey,{0}PrivilegeAccess,{0}PrivilegeAccessKey,{0}PrivilegeOperation,{0}RecordStatus
	)", ParameterPrefix);
            }
        }

        public override string UpdateSql
        {
            get
            {
                return string.Format(@"UPDATE Sys_Privilege SET 
	PrivilegeMaster = {0}PrivilegeMaster,PrivilegeMasterKey = {0}PrivilegeMasterKey,PrivilegeAccess = {0}PrivilegeAccess,PrivilegeAccessKey = {0}PrivilegeAccessKey,PrivilegeOperation = {0}PrivilegeOperation,RecordStatus = {0}RecordStatus
	WHERE SysId={0}SysId", ParameterPrefix);
            }
        } 
    }
}