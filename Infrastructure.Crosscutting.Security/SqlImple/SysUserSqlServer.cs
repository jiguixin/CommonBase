/*
 *名称：SysUserSqlServer
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:48:05
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public class SysUserSqlServer : SqlServer
    {
        public override string AddSql
        {
            get
            {
                return string.Format(@"INSERT INTO Sys_User(
	SysId,UserName,UserPwd,CreateTime,LastLogin,RecordStatus
	)VALUES(
	{0}SysId,{0}UserName,{0}UserPwd,{0}CreateTime,{0}LastLogin,{0}RecordStatus
	)", ParameterPrefix);
            }
        }

        public override string UpdateSql
        {
            get
            {
                return string.Format(@"UPDATE Sys_User SET 
	UserName = {0}UserName,UserPwd = {0}UserPwd,CreateTime = {0}CreateTime,LastLogin = {0}LastLogin,RecordStatus = {0}RecordStatus
	WHERE SysId={0}SysId", ParameterPrefix);
            }
        }
    }
}