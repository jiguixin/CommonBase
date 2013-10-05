/*
 *名称：SysRoleSqlServer
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:46:35
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public class SysRoleSqlServer : SqlServer
    { 
        public override string AddSql
        {
            get
            {
                return @"INSERT INTO [Sys_Role](
	[SysId],[RoleName],[RoleDesc],[RecordStatus]
	)VALUES(
	@SysId,@RoleName,@RoleDesc,@RecordStatus
	)";
            }
        }

        public override string UpdateSql
        {
            get
            {
                return @"UPDATE [Sys_Role] SET 
	[RoleName] = @RoleName,[RoleDesc] = @RoleDesc,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId";
            }
        }
    }
}