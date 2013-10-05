/*
 *名称：SysPrivilegeSqlServer
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:46:03
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public class SysPrivilegeSqlServer : SqlServer
    { 
        public override string AddSql
        {
            get
            {
                return @"INSERT INTO [Sys_Privilege](
	[SysId],[PrivilegeMaster],[PrivilegeMasterKey],[PrivilegeAccess],[PrivilegeAccessKey],[PrivilegeOperation],[RecordStatus]
	)VALUES(
	@SysId,@PrivilegeMaster,@PrivilegeMasterKey,@PrivilegeAccess,@PrivilegeAccessKey,@PrivilegeOperation,@RecordStatus
	)";
            }
        }

        public override string UpdateSql
        {
            get
            {
                return @"UPDATE [Sys_Privilege] SET 
	[PrivilegeMaster] = @PrivilegeMaster,[PrivilegeMasterKey] = @PrivilegeMasterKey,[PrivilegeAccess] = @PrivilegeAccess,[PrivilegeAccessKey] = @PrivilegeAccessKey,[PrivilegeOperation] = @PrivilegeOperation,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId";
            }
        }
    }
}