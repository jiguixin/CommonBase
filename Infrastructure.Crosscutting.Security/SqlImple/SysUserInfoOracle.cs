/*
 *名称：SysUserInfoSqlServer
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:47:03
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public class SysUserInfoOracle : Oracle
    { 
        public override string AddSql
        {
            get
            {
                return string.Format(@"INSERT INTO Sys_UserInfo(
	SysId,RealName,Title,Sex,Phone,Fax,Email,QQ,Address
	)VALUES(
	{0}SysId,{0}RealName,{0}Title,{0}Sex,{0}Phone,{0}Fax,{0}Email,{0}QQ,{0}Address
	)", ParameterPrefix);
            }
        }

        public override string UpdateSql
        {
            get
            {
                return string.Format(@"UPDATE Sys_UserInfo SET 
	RealName = {0}RealName,Title = {0}Title,Sex = {0}Sex,Phone = {0}Phone,Fax = {0}Fax,Email = {0}Email,QQ = {0}QQ,Address = {0}Address
	WHERE SysId={0}SysId", ParameterPrefix);
            }
        }
    }
}