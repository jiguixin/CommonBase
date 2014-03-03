/*
 *名称：SysUserRoleBase
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 14:36:24
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    using Infrastructure.Crosscutting.Security.Core;

    public class SysUserRoleBase:SqlBase
    {
        public SysUserRoleBase(IAppContext appContext)
            : base(appContext)
        {
        }

        public override string AddSql
        {
            get
            {
                return string.Format(@"INSERT INTO Sys_UserRole(
	SysId,UserId,RoleId
	)VALUES(
	{0}SysId,{0}UserId,{0}RoleId
	)", ParameterPrefix);
            }
        }

        public override string UpdateSql
        {
            get
            {
                return string.Format(@"UPDATE Sys_UserRole SET 
	UserId = {0}UserId,RoleId = {0}RoleId
	WHERE SysId={0}SysId", ParameterPrefix);
            }
        } 
    }
}