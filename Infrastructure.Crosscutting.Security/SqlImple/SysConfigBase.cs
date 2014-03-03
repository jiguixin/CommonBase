/*
 *名称：SysConfigBase
 *功能：
 *创建人：吉桂昕
 *创建时间：2014-02-27 11:15:04
 *修改时间：
 *备注：
 */

using System;

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    using Infrastructure.Crosscutting.Security.Core;

    public class SysConfigBase:SqlBase
    {
        public SysConfigBase(IAppContext appContext)
            : base(appContext)
        {
        }

        public override string AddSql
        {
            get
            {
                return string.Format(@"INSERT INTO Sys_Config(
	SysId,SysKey,SysValue,SysParentId,RecordStatus
	)VALUES(
	{0}SysId,{0}SysKey,{0}SysValue,{0}SysParentId,{0}RecordStatus
	)", ParameterPrefix);
            }
        }

        public override string UpdateSql
        {
            get
            {
                return string.Format(@"UPDATE Sys_Config SET 
	SysKey = {0}SysKey,SysValue = {0}SysValue,SysParentId = {0}SysParentId,RecordStatus = {0}RecordStatus
	WHERE SysId={0}SysId", ParameterPrefix);
            }
        }
    }
}