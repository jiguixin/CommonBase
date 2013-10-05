/*
 *名称：SysUserRoleSqlServer
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:48:34
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public class SysUserRoleSqlServer : SqlServer
    { 

        public override string AddSql
        {
            get
            {
                return @"INSERT INTO [Sys_UserRole](
	[SysId],[UserId],[RoleId]
	)VALUES(
	@SysId,@UserId,@RoleId
	)";
            }
        }

        public override string UpdateSql
        {
            get
            {
                return @"UPDATE [Sys_UserRole] SET 
	[UserId] = @UserId,[RoleId] = @RoleId
	WHERE SysId=@SysId";
            }
        }
    }
}