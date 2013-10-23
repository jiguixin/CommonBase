/*
 *名称：SysRoleSqlServer
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:46:35
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public class SysRoleSqlServer : SqlServer
    {
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