/*
 *名称：SysConfigSqlServer
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:43:38
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public class SysConfigOracle : Oracle
    { 
        public override string AddSql
        {
            get
            {
                return @"INSERT INTO Sys_Config(
	SysId,SysKey,SysValue,SysParentId,RecordStatus
	)VALUES(
	:SysId,:SysKey,:SysValue,:SysParentId,:RecordStatus
	)";
            }
        }

        public override string UpdateSql
        {
            get
            {
                return @"UPDATE Sys_Config SET 
	SysKey = :SysKey,SysValue = :SysValue,SysParentId = :SysParentId,RecordStatus = :RecordStatus
	WHERE SysId=:SysId";
            }
        }
    }
}