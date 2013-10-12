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
                return @"INSERT INTO Sys_UserInfo(
	SysId,RealName,Title,Sex,Phone,Fax,Email,QQ,Address
	)VALUES(
	:SysId,:RealName,:Title,:Sex,:Phone,:Fax,:Email,:QQ,:Address
	)";
            }
        }

        public override string UpdateSql
        {
            get
            {
                return @"UPDATE Sys_UserInfo SET 
	RealName = :RealName,Title = :Title,Sex = :Sex,Phone = :Phone,Fax = :Fax,Email = :Email,QQ = :QQ,Address = :Address
	WHERE SysId=:SysId";
            }
        }
    }
}