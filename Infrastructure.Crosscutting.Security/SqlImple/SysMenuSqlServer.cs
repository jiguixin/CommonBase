/*
 *名称：SysMenuSqlServer
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-05 01:45:09
 *修改时间：
 *备注：
 */

namespace Infrastructure.Crosscutting.Security.SqlImple
{
    public class SysMenuSqlServer : SqlServer
    { 
        public override string AddSql
        {
            get
            {
                return @"INSERT INTO [Sys_Menu](
	[SysId],[MenuParentId],[MenuOrder],[MenuName],[MenuLink],[MenuIcon],[IsVisible],[IsLeaf],[RecordStatus]
	)VALUES(
	@SysId,@MenuParentId,@MenuOrder,@MenuName,@MenuLink,@MenuIcon,@IsVisible,@IsLeaf,@RecordStatus
	)";
            }
        }

        public override string UpdateSql
        {
            get
            {
                return @"UPDATE [Sys_Menu] SET 
	[MenuParentId] = @MenuParentId,[MenuOrder] = @MenuOrder,[MenuName] = @MenuName,[MenuLink] = @MenuLink,[MenuIcon] = @MenuIcon,[IsVisible] = @IsVisible,[IsLeaf] = @IsLeaf,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId";
            }
        }
    }
}