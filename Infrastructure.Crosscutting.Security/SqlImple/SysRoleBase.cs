/*
 *名称：SysRoleBase
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 14:35:02
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    using Infrastructure.Crosscutting.Security.Core;

    public class SysRoleBase:SqlBase
    {
        public SysRoleBase(IAppContext appContext)
            : base(appContext)
        {
        }

        public override string AddSql
        {
            get
            {
                return string.Format(@"INSERT INTO Sys_Role(
	SysId,RoleName,RoleDesc,RecordStatus
	)VALUES(
	{0}SysId,{0}RoleName,{0}RoleDesc,{0}RecordStatus
	)", ParameterPrefix);
            }
        }

        public override string UpdateSql
        {
            get
            {
                return string.Format(@"UPDATE Sys_Role SET 
	RoleName = {0}RoleName,RoleDesc = {0}RoleDesc,RecordStatus = {0}RecordStatus
	WHERE SysId={0}SysId", ParameterPrefix);
            }
        } 
    }
}