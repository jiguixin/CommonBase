/*
 *名称：SysUserSqlServer
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:48:05
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public class SysUserSqlServer : SqlServer
    { 
        public override string AddSql
        {
            get
            {
                return @"INSERT INTO [Sys_User](
	[SysId],[UserName],[UserPwd],[CreateTime],[LastLogin],[RecordStatus]
	)VALUES(
	@SysId,@UserName,@UserPwd,@CreateTime,@LastLogin,@RecordStatus
	)";
            }
        }

        public override string UpdateSql
        {
            get
            {
                return @"UPDATE [Sys_User] SET 
	[UserName] = @UserName,[UserPwd] = @UserPwd,[CreateTime] = @CreateTime,[LastLogin] = @LastLogin,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId";
            }
        }
    }
}