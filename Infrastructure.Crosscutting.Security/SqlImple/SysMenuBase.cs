/*
 *名称：SysMenuBase
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 14:33:30
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    using Infrastructure.Crosscutting.Security.Core;

    public class SysMenuBase:SqlBase
    {
        public SysMenuBase(IAppContext appContext)
            : base(appContext)
        {
        }

        public override string AddSql
        {
            get
            {
                return string.Format(@"INSERT INTO Sys_Menu(
	SysId,MenuParentId,MenuOrder,MenuName,MenuLink,MenuIcon,IsVisible,IsLeaf,RecordStatus
	)VALUES(
	{0}SysId,{0}MenuParentId,{0}MenuOrder,{0}MenuName,{0}MenuLink,{0}MenuIcon,{0}IsVisible,{0}IsLeaf,{0}RecordStatus
	)", ParameterPrefix);
            }
        }

        public override string UpdateSql
        {
            get
            {
                return string.Format(@"UPDATE Sys_Menu SET 
	MenuParentId = {0}MenuParentId,MenuOrder = {0}MenuOrder,MenuName = {0}MenuName,MenuLink = {0}MenuLink,MenuIcon = {0}MenuIcon,IsVisible = {0}IsVisible,IsLeaf = {0}IsLeaf,RecordStatus = {0}RecordStatus
	WHERE SysId={0}SysId", ParameterPrefix);
            }
        } 
    }
}