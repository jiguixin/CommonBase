﻿/*
 *名称：SysDataPrivilegeBase
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 14:32:45
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    using Infrastructure.Crosscutting.Security.Core;

    public class SysDataPrivilegeBase:SqlBase
    {
        public SysDataPrivilegeBase(IAppContext appContext)
            : base(appContext)
        {
        }

        public override string AddSql
        {
            get
            {
                return string.Format(@"INSERT INTO Sys_DataPrivilege(
	SysId,DataPrivilegeView,DataPrivilegeRule,RecordStatus
	)VALUES(
	{0}SysId,{0}DataPrivilegeView,{0}DataPrivilegeRule,{0}RecordStatus
	)", ParameterPrefix);
            }
        }

        public override string UpdateSql
        {
            get
            {
                return string.Format(@"UPDATE Sys_DataPrivilege SET 
	DataPrivilegeView = {0}DataPrivilegeView,DataPrivilegeRule = {0}DataPrivilegeRule,RecordStatus = {0}RecordStatus
	WHERE SysId={0}SysId", ParameterPrefix);
            }
        } 
    }
}