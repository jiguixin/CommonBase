/*
 *名称：SysButtonBase
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 14:29:06
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    using Infrastructure.Crosscutting.Security.Core;

    public class SysButtonBase:SqlBase
    {
        public SysButtonBase(IAppContext appContext)
            : base(appContext)
        {
        }

        public override string AddSql
        {
            get
            {
                return string.Format(@"INSERT INTO Sys_Button(
	SysId,MenuId,BtnName,BtnIcon,BtnOrder,RecordStatus,IsVisible,BtnFunction
	)VALUES(
	{0}SysId,{0}MenuId,{0}BtnName,{0}BtnIcon,{0}BtnOrder,{0}RecordStatus,{0}IsVisible,{0}BtnFunction
	)", ParameterPrefix);
            }
        }

        public override string UpdateSql
        {
            get
            {
                return string.Format(@"UPDATE Sys_Button SET 
	MenuId = {0}MenuId,BtnName = {0}BtnName,BtnIcon = {0}BtnIcon,BtnOrder = {0}BtnOrder,RecordStatus = {0}RecordStatus,IsVisible = {0}IsVisible,BtnFunction={0}BtnFunction
	WHERE SysId={0}SysId", ParameterPrefix);
            }
        } 
    }
}