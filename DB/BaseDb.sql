USE [master]
GO
/****** Object:  Database [BaseDB]    Script Date: 09/13/2013 18:01:25 ******/
CREATE DATABASE [BaseDB] ON  PRIMARY 
( NAME = N'BaseDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\BaseDB.mdf' , SIZE = 236544KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'BaseDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\BaseDB_log.ldf' , SIZE = 676608KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [BaseDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BaseDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BaseDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [BaseDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [BaseDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [BaseDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [BaseDB] SET ARITHABORT OFF
GO
ALTER DATABASE [BaseDB] SET AUTO_CLOSE ON
GO
ALTER DATABASE [BaseDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [BaseDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [BaseDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [BaseDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [BaseDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [BaseDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [BaseDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [BaseDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [BaseDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [BaseDB] SET  DISABLE_BROKER
GO
ALTER DATABASE [BaseDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [BaseDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [BaseDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [BaseDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [BaseDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [BaseDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [BaseDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [BaseDB] SET  READ_WRITE
GO
ALTER DATABASE [BaseDB] SET RECOVERY FULL
GO
ALTER DATABASE [BaseDB] SET  MULTI_USER
GO
ALTER DATABASE [BaseDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [BaseDB] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'BaseDB', N'ON'
GO
USE [BaseDB]
GO
/****** Object:  Table [dbo].[Sys_Config]    Script Date: 09/13/2013 18:01:26 ******/
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
/****** Object:  StoredProcedure [dbo].[proc_GetList]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
支持多表查询分页存储过程(事理改进)2012.3
--多表联查1 
exec [proc_GetList] 'SysId','SL_Article ','a.UserId=u.UserId' 
*/

/*注意:多表联查,如果两个表有相同的列名,必须指定要查询的列名,不然会报错*/
CREATE  PROCEDURE [dbo].[proc_GetList]
(
    @Table nvarchar(1000),--表名,支持多表联查
    @Fields nvarchar(2000) = N'*',--字段名
    @Where nvarchar(1000) = N''--where条件,不需要加where   
)
AS
BEGIN
       
    --选取字段
    if @Fields is null or @Fields = ''
        set @Fields='*'

    --过滤条件
    if @Where is null or @Where=''
        set @Where=''
    else
        set @Where=' WHERE '+@Where
 

    /*执行查询语句,返回查询结果*/
    exec
    (
		'SELECT '+@Fields+' FROM ' +@Table+@Where 
    )
     
END
GO
/****** Object:  StoredProcedure [dbo].[proc_Delete_By_Where]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
根据指定条件删除表数据
时间：2013/9/10 21:00:39
*/
CREATE PROCEDURE [dbo].[proc_Delete_By_Where]
(
@Table nvarchar(1000),
@Where nvarchar(1000) = N''--where条件,不需要加where
)
AS
BEGIN
--过滤条件
    if @Where is null or @Where=''
        set @Where=''
    else
        set @Where=' WHERE '+@Where

	/*执行查询语句,返回查询结果*/
    exec
    (
		'DELETE FROM ' +@Table+@Where 
    )
     
END
GO
/****** Object:  StoredProcedure [dbo].[proc_DataPagination]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
支持多表查询分页存储过程(事理改进)2012.3
--多表联查1
declare @Count int
exec [proc_DataPagination] 'SL_Article a,SL_User u','u.RealName,a.*','a.UserId=u.UserId','',1,20,0,@Count output
select @Count
--多表联查2
declare @Count int
exec proc_DataPagination 'SL_LANAndWANPermissionLog l left join SL_Plate p on l.PlateId=p.PlateId left join SL_Admin a on l.AddUserId=a.UserId','l.*,p.PlateName,a.RealName as AddUserRealName','','Id',1,20,0,@Count output
select @Count
*/
/*注意:多表联查,如果两个表有相同的列名,必须指定要查询的列名,不然会报错*/
CREATE  PROCEDURE [dbo].[proc_DataPagination]
(
    @Table nvarchar(1000),--表名,支持多表联查
    @Fields nvarchar(2000) = N'*',--字段名
    @Where nvarchar(1000) = N'',--where条件,不需要加where
    @OrderBy nvarchar(500) = N'',--排序条件，不需要加order by
    @CurrentPage int = 1, --当前页,从1开始,不是0
    @PageSize int = 10,--每页显示多少条数据
    @GetCount int =0,--获取的记录总数，0则获取记录总数，不为0则不获取
    @Count int = 0 output--总数
)
AS
BEGIN
    --没有提供排序字段,默认主键排序
    if @OrderBy is null or @OrderBy=''
    begin
        declare @tempTable varchar(200)
        --多表联查如果没有提供排序字段,自动找第一个表的主键进行排序
        if charindex(' on ',@Table)>0
            set @tempTable=substring(@Table,0,charindex(' ',@Table))
        else if charindex(',',@Table)>0
            begin
                set @tempTable=substring(@Table,0,charindex(',',@Table))
                --如果有别名如Article a,User u
                if(charindex(' ',@tempTable)>0)
                    set @tempTable=substring(@tempTable,0,charindex(' ',@tempTable))
            end
        else
            set @tempTable=@Table--单表查询

        --查询表是否存在
        if not exists(select * from sysobjects where [name]=@tempTable)
          begin
            raiserror('查询表%s不存在',12,12,@tempTable)
            return
          end    

        --查询排序主键
        select @OrderBy=d.name from sysindexes a,sysobjects b,sysindexkeys c,syscolumns d 
        where c.id = object_id(@tempTable) and c.id = b.parent_obj   
            and a.name = b.name and b.xtype= 'PK ' and a.indid = 1 and d.colid = c.colid and d.id = c.id
        --如果没有主键,如视图
        if @OrderBy is null or @OrderBy = ''
          begin
            raiserror('%s必须提供排序字段',12,12,@tempTable)
            return
          end
    end

    --分页大小
    if @PageSize < 1
       set @PageSize=10

    --默认当前页
    if @CurrentPage < 1
        set @CurrentPage = 1

    --选取字段
    if @Fields is null or @Fields = ''
        set @Fields='*'

    --过滤条件
    if @Where is null or @Where=''
        set @Where=''
    else
        set @Where=' WHERE '+@Where

    /*设置分页参数*/
    declare @startRow varchar(50),@endRow varchar(50)
    set @startRow = cast(((@CurrentPage - 1)*@PageSize + 1) as nvarchar(50))
    set @endRow = cast(@CurrentPage*@PageSize as nvarchar(50))

    /*执行查询语句,返回查询结果*/
    exec
    (
        'SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY '+@OrderBy+') AS rownumber,'+@Fields+
        ' FROM '+@Table+@Where+') AS tempdt WHERE rownumber BETWEEN '+@startRow+' AND '+@endRow
    )
    /*
    如果@GetCount=0,则计算总页数(这样设计可以只在第一次计算总页数,以后调用时,
    把总页数传回给存储过程,避免再次计算总页数,当数据量很大时,select count(*)速度也要几秒钟)
    */
    if(@GetCount=0)
      begin
        declare @strsql nvarchar(1200)
        set @strsql='SELECT @i=COUNT(*) FROM '+@Table+@Where    
        execute sp_executesql @strsql,N'@i int out',@Count OUT--返回总记录数
      end
    else
        set @Count=@GetCount
END
GO
/****** Object:  Table [dbo].[Sys_DataPrivilege]    Script Date: 09/13/2013 18:01:30 ******/
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
/****** Object:  Table [dbo].[Sys_Menu]    Script Date: 09/13/2013 18:01:30 ******/
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
/****** Object:  Table [dbo].[Sys_Privilege]    Script Date: 09/13/2013 18:01:30 ******/
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
/****** Object:  Table [dbo].[Sys_Role]    Script Date: 09/13/2013 18:01:30 ******/
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
/****** Object:  Table [dbo].[Sys_User]    Script Date: 09/13/2013 18:01:30 ******/
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
/****** Object:  Table [dbo].[Sys_UserInfo]    Script Date: 09/13/2013 18:01:30 ******/
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
/****** Object:  StoredProcedure [dbo].[Sys_User_Update]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：修改一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_User_Update]
@SysId nvarchar(50),
@UserName nvarchar(50),
@UserPwd nvarchar(150),
@CreateTime datetime,
@LastLogin datetime,
@RecordStatus nvarchar(200)
 AS 
	UPDATE [Sys_User] SET 
	[UserName] = @UserName,[UserPwd] = @UserPwd,[CreateTime] = @CreateTime,[LastLogin] = @LastLogin,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId
GO
/****** Object:  StoredProcedure [dbo].[Sys_User_ADD]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：增加一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_User_ADD]
@SysId nvarchar(50),
@UserName nvarchar(50),
@UserPwd nvarchar(150),
@CreateTime datetime,
@LastLogin datetime,
@RecordStatus nvarchar(200)

 AS 
	INSERT INTO [Sys_User](
	[SysId],[UserName],[UserPwd],[CreateTime],[LastLogin],[RecordStatus]
	)VALUES(
	@SysId,@UserName,@UserPwd,@CreateTime,@LastLogin,@RecordStatus
	)
GO
/****** Object:  StoredProcedure [dbo].[Sys_Role_Update]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：修改一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_Role_Update]
@SysId nvarchar(50),
@RoleName nvarchar(20),
@RoleDesc nvarchar(150),
@RecordStatus nvarchar(200)
 AS 
	UPDATE [Sys_Role] SET 
	[RoleName] = @RoleName,[RoleDesc] = @RoleDesc,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId
GO
/****** Object:  StoredProcedure [dbo].[Sys_Role_ADD]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：增加一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_Role_ADD]
@SysId nvarchar(50),
@RoleName nvarchar(20),
@RoleDesc nvarchar(150),
@RecordStatus nvarchar(200)

 AS 
	INSERT INTO [Sys_Role](
	[SysId],[RoleName],[RoleDesc],[RecordStatus]
	)VALUES(
	@SysId,@RoleName,@RoleDesc,@RecordStatus
	)
GO
/****** Object:  StoredProcedure [dbo].[Sys_Privilege_Update]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：修改一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_Privilege_Update]
@SysId nvarchar(50),
@PrivilegeMaster nvarchar(50),
@PrivilegeMasterKey nvarchar(50),
@PrivilegeAccess nvarchar(50),
@PrivilegeAccessKey nvarchar(50),
@PrivilegeOperation int,
@RecordStatus nvarchar(200)
 AS 
	UPDATE [Sys_Privilege] SET 
	[PrivilegeMaster] = @PrivilegeMaster,[PrivilegeMasterKey] = @PrivilegeMasterKey,[PrivilegeAccess] = @PrivilegeAccess,[PrivilegeAccessKey] = @PrivilegeAccessKey,[PrivilegeOperation] = @PrivilegeOperation,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId
GO
/****** Object:  StoredProcedure [dbo].[Sys_Privilege_ADD]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：增加一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_Privilege_ADD]
@SysId nvarchar(50),
@PrivilegeMaster nvarchar(50),
@PrivilegeMasterKey nvarchar(50),
@PrivilegeAccess nvarchar(50),
@PrivilegeAccessKey nvarchar(50),
@PrivilegeOperation int,
@RecordStatus nvarchar(200)

 AS 
	INSERT INTO [Sys_Privilege](
	[SysId],[PrivilegeMaster],[PrivilegeMasterKey],[PrivilegeAccess],[PrivilegeAccessKey],[PrivilegeOperation],[RecordStatus]
	)VALUES(
	@SysId,@PrivilegeMaster,@PrivilegeMasterKey,@PrivilegeAccess,@PrivilegeAccessKey,@PrivilegeOperation,@RecordStatus
	)
GO
/****** Object:  StoredProcedure [dbo].[Sys_Menu_Update]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：修改一条记录 
--项目名称：
--说明：
--时间：2013/9/9 10:58:59
------------------------------------
CREATE PROCEDURE [dbo].[Sys_Menu_Update]
@SysId nvarchar(50),
@MenuParentId nvarchar(50),
@MenuOrder int,
@MenuName nvarchar(20),
@MenuLink nvarchar(100),
@MenuIcon nvarchar(100),
@IsVisible bigint,
@IsLeaf bigint,
@RecordStatus nvarchar(200)
 AS 
	UPDATE [Sys_Menu] SET 
	[MenuParentId] = @MenuParentId,[MenuOrder] = @MenuOrder,[MenuName] = @MenuName,[MenuLink] = @MenuLink,[MenuIcon] = @MenuIcon,[IsVisible] = @IsVisible,[IsLeaf] = @IsLeaf,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId
GO
/****** Object:  StoredProcedure [dbo].[Sys_Menu_ADD]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：增加一条记录 
--项目名称：
--说明：
--时间：2013/9/9 10:58:59
------------------------------------
CREATE PROCEDURE [dbo].[Sys_Menu_ADD]
@SysId nvarchar(50),
@MenuParentId nvarchar(50),
@MenuOrder int,
@MenuName nvarchar(20),
@MenuLink nvarchar(100),
@MenuIcon nvarchar(100),
@IsVisible bigint,
@IsLeaf bigint,
@RecordStatus nvarchar(200)

 AS 
	INSERT INTO [Sys_Menu](
	[SysId],[MenuParentId],[MenuOrder],[MenuName],[MenuLink],[MenuIcon],[IsVisible],[IsLeaf],[RecordStatus]
	)VALUES(
	@SysId,@MenuParentId,@MenuOrder,@MenuName,@MenuLink,@MenuIcon,@IsVisible,@IsLeaf,@RecordStatus
	)
GO
/****** Object:  StoredProcedure [dbo].[Sys_DataPrivilege_Update]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：修改一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_DataPrivilege_Update]
@SysId nvarchar(50),
@DataPrivilegeView nvarchar(20),
@DataPrivilegeRule nvarchar(MAX),
@RecordStatus nvarchar(200)
 AS 
	UPDATE [Sys_DataPrivilege] SET 
	[DataPrivilegeView] = @DataPrivilegeView,[DataPrivilegeRule] = @DataPrivilegeRule,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId
GO
/****** Object:  StoredProcedure [dbo].[Sys_DataPrivilege_ADD]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：增加一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_DataPrivilege_ADD]
@SysId nvarchar(50),
@DataPrivilegeView nvarchar(20),
@DataPrivilegeRule nvarchar(MAX),
@RecordStatus nvarchar(200)

 AS 
	INSERT INTO [Sys_DataPrivilege](
	[SysId],[DataPrivilegeView],[DataPrivilegeRule],[RecordStatus]
	)VALUES(
	@SysId,@DataPrivilegeView,@DataPrivilegeRule,@RecordStatus
	)
GO
/****** Object:  StoredProcedure [dbo].[Sys_Config_Update]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：修改一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_Config_Update]
@SysId nvarchar(50),
@SysKey nvarchar(20),
@SysValue nvarchar(2000),
@SysParentId nvarchar(50),
@RecordStatus nvarchar(200)
 AS 
	UPDATE [Sys_Config] SET 
	[SysKey] = @SysKey,[SysValue] = @SysValue,[SysParentId] = @SysParentId,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId
GO
/****** Object:  StoredProcedure [dbo].[Sys_Config_ADD]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：增加一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_Config_ADD]
@SysId nvarchar(50),
@SysKey nvarchar(20),
@SysValue nvarchar(2000),
@SysParentId nvarchar(50),
@RecordStatus nvarchar(200)

 AS 
	INSERT INTO [Sys_Config](
	[SysId],[SysKey],[SysValue],[SysParentId],[RecordStatus]
	)VALUES(
	@SysId,@SysKey,@SysValue,@SysParentId,@RecordStatus
	)
GO
/****** Object:  Table [dbo].[Sys_Button]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Button](
	[SysId] [nvarchar](50) NOT NULL,
	[MenuId] [nvarchar](50) NOT NULL,
	[BtnName] [nvarchar](10) NULL,
	[BtnIcon] [nvarchar](10) NULL,
	[BtnOrder] [int] NULL,
	[RecordStatus] [nvarchar](200) NULL,
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
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按钮' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sys_Button'
GO
/****** Object:  Table [dbo].[Sys_UserRole]    Script Date: 09/13/2013 18:01:30 ******/
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
/****** Object:  StoredProcedure [dbo].[Sys_UserRole_Update]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：修改一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_UserRole_Update]
@SysId nvarchar(50),
@UserId nvarchar(50),
@RoleId nvarchar(50)
 AS 
	UPDATE [Sys_UserRole] SET 
	[UserId] = @UserId,[RoleId] = @RoleId
	WHERE SysId=@SysId
GO
/****** Object:  StoredProcedure [dbo].[Sys_UserRole_ADD]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：增加一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_UserRole_ADD]
@SysId nvarchar(50),
@UserId nvarchar(50),
@RoleId nvarchar(50)

 AS 
	INSERT INTO [Sys_UserRole](
	[SysId],[UserId],[RoleId]
	)VALUES(
	@SysId,@UserId,@RoleId
	)
GO
/****** Object:  StoredProcedure [dbo].[Sys_UserInfo_Update]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：修改一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_UserInfo_Update]
@SysId nvarchar(50),
@RealName nvarchar(15),
@Title nvarchar(15),
@Sex bit,
@Phone nvarchar(20),
@Fax nvarchar(20),
@Email nvarchar(20),
@QQ nvarchar(20),
@Address nvarchar(250)
 AS 
	UPDATE [Sys_UserInfo] SET 
	[RealName] = @RealName,[Title] = @Title,[Sex] = @Sex,[Phone] = @Phone,[Fax] = @Fax,[Email] = @Email,[QQ] = @QQ,[Address] = @Address
	WHERE SysId=@SysId
GO
/****** Object:  StoredProcedure [dbo].[Sys_UserInfo_ADD]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：增加一条记录 
--项目名称：
--说明：
--时间：2013/9/5 11:54:03
------------------------------------
CREATE PROCEDURE [dbo].[Sys_UserInfo_ADD]
@SysId nvarchar(50),
@RealName nvarchar(15),
@Title nvarchar(15),
@Sex bit,
@Phone nvarchar(20),
@Fax nvarchar(20),
@Email nvarchar(20),
@QQ nvarchar(20),
@Address nvarchar(250)

 AS 
	INSERT INTO [Sys_UserInfo](
	[SysId],[RealName],[Title],[Sex],[Phone],[Fax],[Email],[QQ],[Address]
	)VALUES(
	@SysId,@RealName,@Title,@Sex,@Phone,@Fax,@Email,@QQ,@Address
	)
GO
/****** Object:  StoredProcedure [dbo].[Sys_Button_Update]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：修改一条记录 
--项目名称：
--说明：
--时间：2013/9/9 11:00:39
------------------------------------
CREATE PROCEDURE [dbo].[Sys_Button_Update]
@SysId nvarchar(50),
@MenuId nvarchar(50),
@BtnName nvarchar(10),
@BtnIcon nvarchar(10),
@BtnOrder int,
@RecordStatus nvarchar(200)
 AS 
	UPDATE [Sys_Button] SET 
	[MenuId] = @MenuId,[BtnName] = @BtnName,[BtnIcon] = @BtnIcon,[BtnOrder] = @BtnOrder,[RecordStatus] = @RecordStatus
	WHERE SysId=@SysId
GO
/****** Object:  StoredProcedure [dbo].[Sys_Button_ADD]    Script Date: 09/13/2013 18:01:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------
--用途：增加一条记录 
--项目名称：
--说明：
--时间：2013/9/9 11:00:39
------------------------------------
CREATE PROCEDURE [dbo].[Sys_Button_ADD]
@SysId nvarchar(50),
@MenuId nvarchar(50),
@BtnName nvarchar(10),
@BtnIcon nvarchar(10),
@BtnOrder int,
@RecordStatus nvarchar(200)

 AS 
	INSERT INTO [Sys_Button](
	[SysId],[MenuId],[BtnName],[BtnIcon],[BtnOrder],[RecordStatus]
	)VALUES(
	@SysId,@MenuId,@BtnName,@BtnIcon,@BtnOrder,@RecordStatus
	)
GO
/****** Object:  ForeignKey [FK_Sys_Config_Sys_Config]    Script Date: 09/13/2013 18:01:26 ******/
ALTER TABLE [dbo].[Sys_Config]  WITH CHECK ADD  CONSTRAINT [FK_Sys_Config_Sys_Config] FOREIGN KEY([SysParentId])
REFERENCES [dbo].[Sys_Config] ([SysId])
GO
ALTER TABLE [dbo].[Sys_Config] CHECK CONSTRAINT [FK_Sys_Config_Sys_Config]
GO
/****** Object:  ForeignKey [FK_Sys_UserInfo_Sys_User]    Script Date: 09/13/2013 18:01:30 ******/
ALTER TABLE [dbo].[Sys_UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_Sys_UserInfo_Sys_User] FOREIGN KEY([SysId])
REFERENCES [dbo].[Sys_User] ([SysId])
GO
ALTER TABLE [dbo].[Sys_UserInfo] CHECK CONSTRAINT [FK_Sys_UserInfo_Sys_User]
GO
/****** Object:  ForeignKey [FK_Sys_Button_Sys_Menu]    Script Date: 09/13/2013 18:01:30 ******/
ALTER TABLE [dbo].[Sys_Button]  WITH CHECK ADD  CONSTRAINT [FK_Sys_Button_Sys_Menu] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Sys_Menu] ([SysId])
GO
ALTER TABLE [dbo].[Sys_Button] CHECK CONSTRAINT [FK_Sys_Button_Sys_Menu]
GO
/****** Object:  ForeignKey [FK_Sys_UserRole_Sys_Role]    Script Date: 09/13/2013 18:01:30 ******/
ALTER TABLE [dbo].[Sys_UserRole]  WITH CHECK ADD  CONSTRAINT [FK_Sys_UserRole_Sys_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Sys_Role] ([SysId])
GO
ALTER TABLE [dbo].[Sys_UserRole] CHECK CONSTRAINT [FK_Sys_UserRole_Sys_Role]
GO
/****** Object:  ForeignKey [FK_Sys_UserRole_Sys_User]    Script Date: 09/13/2013 18:01:30 ******/
ALTER TABLE [dbo].[Sys_UserRole]  WITH CHECK ADD  CONSTRAINT [FK_Sys_UserRole_Sys_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[Sys_User] ([SysId])
GO
ALTER TABLE [dbo].[Sys_UserRole] CHECK CONSTRAINT [FK_Sys_UserRole_Sys_User]
GO
