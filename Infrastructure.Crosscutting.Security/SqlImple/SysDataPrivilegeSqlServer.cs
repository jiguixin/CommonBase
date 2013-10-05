/*
 *名称：SysDataPrivilegeSqlServer
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:44:21
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public class SysDataPrivilegeSqlServer:SqlServer
    {
         
        public override string AddSql
        {
            get
            {
                return @"INSERT INTO [Sys_DataPrivilege](
	[SysId],[DataPrivilegeView],[DataPrivilegeRule],[RecordStatus]
	)VALUES(
	@SysId,@DataPrivilegeView,@DataPrivilegeRule,@RecordStatus
	)";
            }
        }

        public override string UpdateSql
        {
            get
            {
                return @"UPDATE [Sys_DataPrivilege] SET 
	[DataPrivilegeView] = @DataPrivilegeView,[DataPrivilegeRule] = @DataPrivilegeRule,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId";
            }
        }
    }
}