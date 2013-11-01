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
    public class SysButtonSqlServer:SqlServer
    {
        public override string AddSql
        {
            get
            {
                return string.Format(@"INSERT INTO Sys_Button(
	SysId,MenuId,BtnName,BtnIcon,BtnOrder,RecordStatus,IsVisible
	)VALUES(
	{0}SysId,{0}MenuId,{0}BtnName,{0}BtnIcon,{0}BtnOrder,{0}RecordStatus,{0}IsVisible
	)", ParameterPrefix);
            }
        }

        public override string UpdateSql
        {
            get
            {
                return string.Format(@"UPDATE Sys_Button SET 
	MenuId = {0}MenuId,BtnName = {0}BtnName,BtnIcon = {0}BtnIcon,BtnOrder = {0}BtnOrder,RecordStatus = {0}RecordStatus,IsVisible = {0}IsVisible
	WHERE SysId={0}SysId", ParameterPrefix);
            }
        }
    }
}