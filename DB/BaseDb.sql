USE [BaseDB]
GO
/****** Object:  Table [dbo].[Sys_Role]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Role](
	[SysId] [nvarchar](50) NOT NULL,
	[RoleName] [nvarchar](20) NULL,
	[RoleDesc] [nvarchar](150) NULL,
	[RecordStatus] [nvarchar](200) NULL,
 CONSTRAINT [PK_Sys_Role] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Role', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Role', @level2type=N'COLUMN',@level2name=N'RoleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Role', @level2type=N'COLUMN',@level2name=N'RoleDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Role', @level2type=N'COLUMN',@level2name=N'RecordStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Role'
GO
/****** Object:  Table [dbo].[Sys_Privilege]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Privilege](
	[SysId] [nvarchar](50) NOT NULL,
	[PrivilegeMaster] [nvarchar](50) NOT NULL,
	[PrivilegeMasterKey] [nvarchar](50) NOT NULL,
	[PrivilegeAccess] [nvarchar](50) NOT NULL,
	[PrivilegeAccessKey] [nvarchar](50) NOT NULL,
	[PrivilegeOperation] [int] NOT NULL,
	[RecordStatus] [nvarchar](200) NULL,
 CONSTRAINT [PK_Privilege] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Privilege', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限拥有者，如：用户、角色、部门等 类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Privilege', @level2type=N'COLUMN',@level2name=N'PrivilegeMaster'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应权限拥有者的标识编号。如：UserId、RoleId、DepId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Privilege', @level2type=N'COLUMN',@level2name=N'PrivilegeMasterKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'能被访问的是:菜单、按钮 类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Privilege', @level2type=N'COLUMN',@level2name=N'PrivilegeAccess'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应 菜单、按钮Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Privilege', @level2type=N'COLUMN',@level2name=N'PrivilegeAccessKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该权限的操作如：禁用、启用、分配、授权等权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Privilege', @level2type=N'COLUMN',@level2name=N'PrivilegeOperation'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Privilege', @level2type=N'COLUMN',@level2name=N'RecordStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能 权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Privilege'
GO
/****** Object:  Table [dbo].[Sys_Menu]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Menu](
	[SysId] [nvarchar](50) NOT NULL,
	[MenuParentId] [nvarchar](50) NULL,
	[MenuOrder] [int] NULL,
	[MenuName] [nvarchar](20) NULL,
	[MenuLink] [nvarchar](100) NULL,
	[MenuIcon] [nvarchar](100) NULL,
	[IsVisible] [bigint] NULL,
	[IsLeaf] [bigint] NULL,
	[RecordStatus] [nvarchar](200) NULL,
 CONSTRAINT [PK_Sys_Menu] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单编号,该编号会用在Sys_Privilege中PrivilegeAccessKey中' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menu', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应的父菜单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menu', @level2type=N'COLUMN',@level2name=N'MenuParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示顺序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menu', @level2type=N'COLUMN',@level2name=N'MenuOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menu', @level2type=N'COLUMN',@level2name=N'MenuName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单链接' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menu', @level2type=N'COLUMN',@level2name=N'MenuLink'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menu', @level2type=N'COLUMN',@level2name=N'MenuIcon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单是否可见' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menu', @level2type=N'COLUMN',@level2name=N'IsVisible'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否为叶子菜单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menu', @level2type=N'COLUMN',@level2name=N'IsLeaf'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menu', @level2type=N'COLUMN',@level2name=N'RecordStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Menu'
GO
/****** Object:  Table [dbo].[Sys_DataPrivilege]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_DataPrivilege](
	[SysId] [nvarchar](50) NOT NULL,
	[DataPrivilegeView] [nvarchar](20) NULL,
	[DataPrivilegeRule] [nvarchar](max) NULL,
	[RecordStatus] [nvarchar](200) NULL,
 CONSTRAINT [PK_Sys_DataPrivilege] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据 权限表 编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DataPrivilege', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据 权限表 数据源，如:单个表，级联表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DataPrivilege', @level2type=N'COLUMN',@level2name=N'DataPrivilegeView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应数据源中的数据规则，可以是SQL语句' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DataPrivilege', @level2type=N'COLUMN',@level2name=N'DataPrivilegeRule'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DataPrivilege', @level2type=N'COLUMN',@level2name=N'RecordStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据 权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_DataPrivilege'
GO
/****** Object:  Table [dbo].[Sys_Config]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Config](
	[SysId] [nvarchar](50) NOT NULL,
	[SysKey] [nvarchar](20) NOT NULL,
	[SysValue] [nvarchar](2000) NULL,
	[SysParentId] [nvarchar](50) NULL,
	[RecordStatus] [nvarchar](200) NULL,
 CONSTRAINT [PK_Sys_Config] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Sys_Config_Uniq] UNIQUE NONCLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Config', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统配置Key' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Config', @level2type=N'COLUMN',@level2name=N'SysKey'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应KEY的值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Config', @level2type=N'COLUMN',@level2name=N'SysValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父结点编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Config', @level2type=N'COLUMN',@level2name=N'SysParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Config', @level2type=N'COLUMN',@level2name=N'RecordStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统配置' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Config'
GO
/****** Object:  Table [dbo].[Sys_User]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_User](
	[SysId] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[UserPwd] [nvarchar](150) NOT NULL,
	[CreateTime] [datetime] NULL,
	[LastLogin] [datetime] NULL,
	[RecordStatus] [nvarchar](200) NULL,
 CONSTRAINT [PK_Sys_User] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_User', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_User', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_User', @level2type=N'COLUMN',@level2name=N'UserPwd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_User', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后一次登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_User', @level2type=N'COLUMN',@level2name=N'LastLogin'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_User', @level2type=N'COLUMN',@level2name=N'RecordStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_User'
GO
/****** Object:  Table [dbo].[Sys_VariableColum]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_VariableColum](
	[SysId] [nvarchar](50) NOT NULL,
	[TargetTable] [nvarchar](50) NULL,
	[TargetPK] [nvarchar](50) NULL,
	[ColumnName] [nvarchar](50) NULL,
	[Alias] [nvarchar](50) NULL,
	[RecordStatus] [nvarchar](200) NULL,
 CONSTRAINT [PK_Sys_VariableColum] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'要扩展列的目标表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_VariableColum', @level2type=N'COLUMN',@level2name=N'TargetTable'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'要扩展列的目标表主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_VariableColum', @level2type=N'COLUMN',@level2name=N'TargetPK'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扩展的列名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_VariableColum', @level2type=N'COLUMN',@level2name=N'ColumnName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'别名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_VariableColum', @level2type=N'COLUMN',@level2name=N'Alias'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录情况' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_VariableColum', @level2type=N'COLUMN',@level2name=N'RecordStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_VariableColum'
GO
/****** Object:  Table [dbo].[Sys_BusinessDbIndex]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_BusinessDbIndex](
	[SysId] [nvarchar](50) NOT NULL,
	[DbName] [nvarchar](100) NULL,
	[Ip] [nchar](10) NULL,
	[Port] [int] SPARSE  NULL,
	[UserId] [nvarchar](50) NULL,
	[Password] [nvarchar](200) NULL,
	[DbPath] [nvarchar](200) NULL,
	[CreateDt] [datetime] NULL,
	[Creator] [nvarchar](50) NULL,
 CONSTRAINT [PK_BusinessDbIndex] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务数据库分库的系统编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_BusinessDbIndex', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务数据库名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_BusinessDbIndex', @level2type=N'COLUMN',@level2name=N'DbName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ip地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_BusinessDbIndex', @level2type=N'COLUMN',@level2name=N'Ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'端口号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_BusinessDbIndex', @level2type=N'COLUMN',@level2name=N'Port'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库登录帐号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_BusinessDbIndex', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库登录密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_BusinessDbIndex', @level2type=N'COLUMN',@level2name=N'Password'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库文件路径' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_BusinessDbIndex', @level2type=N'COLUMN',@level2name=N'DbPath'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据库创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_BusinessDbIndex', @level2type=N'COLUMN',@level2name=N'CreateDt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' 创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_BusinessDbIndex', @level2type=N'COLUMN',@level2name=N'Creator'
GO
/****** Object:  Table [dbo].[Sys_VariableColumValue]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_VariableColumValue](
	[SysId] [nvarchar](50) NOT NULL,
	[ColumnValue] [nvarchar](max) NULL,
	[ColumnId] [nvarchar](50) NULL,
	[RecordStatus] [nvarchar](200) NULL,
 CONSTRAINT [PK_Sys_VariableColumValue] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'可变列值主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_VariableColumValue', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'可以列主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_VariableColumValue', @level2type=N'COLUMN',@level2name=N'ColumnId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录情况' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_VariableColumValue', @level2type=N'COLUMN',@level2name=N'RecordStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该列对应的值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_VariableColumValue'
GO
/****** Object:  Table [dbo].[Sys_UserRole]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_UserRole](
	[SysId] [nvarchar](50) NOT NULL,
	[UserId] [nvarchar](50) NOT NULL,
	[RoleId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Sys_UserRole] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户角色编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserRole', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserRole', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserRole', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserRole'
GO
/****** Object:  Table [dbo].[Sys_UserInfo]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_UserInfo](
	[SysId] [nvarchar](50) NOT NULL,
	[RealName] [nvarchar](15) NULL,
	[Title] [nvarchar](15) NULL,
	[Sex] [bit] NULL,
	[Phone] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[Email] [nvarchar](20) NULL,
	[QQ] [nvarchar](20) NULL,
	[Address] [nvarchar](250) NULL,
 CONSTRAINT [PK_Sys_UserInfo] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserInfo', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真实名字' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserInfo', @level2type=N'COLUMN',@level2name=N'RealName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserInfo', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserInfo', @level2type=N'COLUMN',@level2name=N'Sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserInfo', @level2type=N'COLUMN',@level2name=N'Phone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'传真' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserInfo', @level2type=N'COLUMN',@level2name=N'Fax'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserInfo', @level2type=N'COLUMN',@level2name=N'Email'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'qq' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserInfo', @level2type=N'COLUMN',@level2name=N'QQ'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserInfo', @level2type=N'COLUMN',@level2name=N'Address'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_UserInfo'
GO
/****** Object:  Table [dbo].[Sys_TableIndex]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_TableIndex](
	[SysId] [nvarchar](50) NOT NULL,
	[TblName] [nvarchar](50) NULL,
	[StartDt] [datetime] NULL,
	[EndDt] [datetime] NULL,
	[CreatDt] [datetime] NULL,
	[Creator] [nvarchar](50) NULL,
	[TblType] [int] NULL,
	[States] [bit] NULL,
	[UnitNO] [nvarchar](50) NULL,
	[DbIndex] [nvarchar](50) NULL,
 CONSTRAINT [PK_TableIndex] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分表的业务数据表编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_TableIndex', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_TableIndex', @level2type=N'COLUMN',@level2name=N'TblName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_TableIndex', @level2type=N'COLUMN',@level2name=N'StartDt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_TableIndex', @level2type=N'COLUMN',@level2name=N'EndDt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_TableIndex', @level2type=N'COLUMN',@level2name=N'CreatDt'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_TableIndex', @level2type=N'COLUMN',@level2name=N'Creator'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_TableIndex', @level2type=N'COLUMN',@level2name=N'TblType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_TableIndex', @level2type=N'COLUMN',@level2name=N'States'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_TableIndex', @level2type=N'COLUMN',@level2name=N'UnitNO'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务数据库编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_TableIndex', @level2type=N'COLUMN',@level2name=N'DbIndex'
GO
/****** Object:  Table [dbo].[Sys_Button]    Script Date: 03/07/2014 10:57:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Button](
	[SysId] [nvarchar](50) NOT NULL,
	[MenuId] [nvarchar](50) NOT NULL,
	[BtnName] [nvarchar](50) NULL,
	[BtnIcon] [nvarchar](50) NULL,
	[BtnOrder] [int] NULL,
	[RecordStatus] [nvarchar](200) NULL,
	[BtnFunction] [nvarchar](50) NULL,
	[IsVisible] [bigint] NULL,
 CONSTRAINT [PK_Sys_Button] PRIMARY KEY CLUSTERED 
(
	[SysId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮编号,该编号会用在Sys_Privilege中PrivilegeAccessKey中' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Button', @level2type=N'COLUMN',@level2name=N'SysId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单系统编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Button', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Button', @level2type=N'COLUMN',@level2name=N'BtnName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Button', @level2type=N'COLUMN',@level2name=N'BtnIcon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮显示顺序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Button', @level2type=N'COLUMN',@level2name=N'BtnOrder'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该条记录的操作情况，用于记录最后一次谁在什么时候创建、修改了该记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Button', @level2type=N'COLUMN',@level2name=N'RecordStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮执行方法' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Button', @level2type=N'COLUMN',@level2name=N'BtnFunction'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Button', @level2type=N'COLUMN',@level2name=N'IsVisible'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Button'
GO
/****** Object:  ForeignKey [FK_Sys_Button_Sys_Menu]    Script Date: 03/07/2014 10:57:08 ******/
ALTER TABLE [dbo].[Sys_Button]  WITH CHECK ADD  CONSTRAINT [FK_Sys_Button_Sys_Menu] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Sys_Menu] ([SysId])
GO
ALTER TABLE [dbo].[Sys_Button] CHECK CONSTRAINT [FK_Sys_Button_Sys_Menu]
GO
/****** Object:  ForeignKey [FK_Sys_Config_Sys_Config]    Script Date: 03/07/2014 10:57:08 ******/
ALTER TABLE [dbo].[Sys_Config]  WITH CHECK ADD  CONSTRAINT [FK_Sys_Config_Sys_Config] FOREIGN KEY([SysParentId])
REFERENCES [dbo].[Sys_Config] ([SysId])
GO
ALTER TABLE [dbo].[Sys_Config] CHECK CONSTRAINT [FK_Sys_Config_Sys_Config]
GO
/****** Object:  ForeignKey [FK_TableIndex_BusinessDbIndex]    Script Date: 03/07/2014 10:57:08 ******/
ALTER TABLE [dbo].[Sys_TableIndex]  WITH CHECK ADD  CONSTRAINT [FK_TableIndex_BusinessDbIndex] FOREIGN KEY([DbIndex])
REFERENCES [dbo].[Sys_BusinessDbIndex] ([SysId])
GO
ALTER TABLE [dbo].[Sys_TableIndex] CHECK CONSTRAINT [FK_TableIndex_BusinessDbIndex]
GO
/****** Object:  ForeignKey [FK_Sys_UserInfo_Sys_User]    Script Date: 03/07/2014 10:57:08 ******/
ALTER TABLE [dbo].[Sys_UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_Sys_UserInfo_Sys_User] FOREIGN KEY([SysId])
REFERENCES [dbo].[Sys_User] ([SysId])
GO
ALTER TABLE [dbo].[Sys_UserInfo] CHECK CONSTRAINT [FK_Sys_UserInfo_Sys_User]
GO
/****** Object:  ForeignKey [FK_Sys_UserRole_Sys_Role]    Script Date: 03/07/2014 10:57:08 ******/
ALTER TABLE [dbo].[Sys_UserRole]  WITH CHECK ADD  CONSTRAINT [FK_Sys_UserRole_Sys_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Sys_Role] ([SysId])
GO
ALTER TABLE [dbo].[Sys_UserRole] CHECK CONSTRAINT [FK_Sys_UserRole_Sys_Role]
GO
/****** Object:  ForeignKey [FK_Sys_UserRole_Sys_User]    Script Date: 03/07/2014 10:57:08 ******/
ALTER TABLE [dbo].[Sys_UserRole]  WITH CHECK ADD  CONSTRAINT [FK_Sys_UserRole_Sys_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Sys_User] ([SysId])
GO
ALTER TABLE [dbo].[Sys_UserRole] CHECK CONSTRAINT [FK_Sys_UserRole_Sys_User]
GO
/****** Object:  ForeignKey [FK_Sys_VariableColumValue_Sys_VariableColum]    Script Date: 03/07/2014 10:57:08 ******/
ALTER TABLE [dbo].[Sys_VariableColumValue]  WITH CHECK ADD  CONSTRAINT [FK_Sys_VariableColumValue_Sys_VariableColum] FOREIGN KEY([ColumnId])
REFERENCES [dbo].[Sys_VariableColum] ([SysId])
GO
ALTER TABLE [dbo].[Sys_VariableColumValue] CHECK CONSTRAINT [FK_Sys_VariableColumValue_Sys_VariableColum]
GO
