/*
 *名称：SysButtonSql
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:42:13
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public class SysButtonOracle : Oracle
    { 
        public override string AddSql
        {
            get { return @"INSERT INTO Sys_Button(
	SysId,MenuId,BtnName,BtnIcon,BtnOrder,RecordStatus
	)VALUES(
	:SysId,:MenuId,:BtnName,:BtnIcon,:BtnOrder,:RecordStatus
	)"; }
        }

        public override string UpdateSql
        {
            get { return @"UPDATE Sys_Button SET 
	MenuId = :MenuId,BtnName = :BtnName,BtnIcon = :BtnIcon,BtnOrder = :BtnOrder,RecordStatus = :RecordStatus
	WHERE SysId=:SysId"; }
        }
    }
}