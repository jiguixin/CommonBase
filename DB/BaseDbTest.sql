



select * from dbo.Sys_User u inner join dbo.Sys_UserInfo i on u.SysId = i.SysId

select * from dbo.Sys_Role

select * from dbo.Sys_UserRole

select * from dbo.Sys_Privilege

select * from dbo.Sys_Menu

select * from dbo.Sys_Button


select COUNT (1) from Sys_Button


select r.SysId,r.RoleDesc,r.RoleName from Sys_User u inner join Sys_UserRole ur on u.SysId=ur.UserId inner join Sys_Role r on ur.RoleId = r.SysId
 
 
--得到指定用户有那些角色
select * from Sys_User u inner join Sys_UserRole ur on 
u.SysId=ur.UserId inner join Sys_Role r on ur.RoleId = r.SysId where u.SysId = 'cf9d52cc-0500-4829-9611-fd0056961468'
 


--得到指定用户有那些权限
select p.SysId,p.PrivilegeAccess,p.PrivilegeAccessKey,p.PrivilegeMaster,p.PrivilegeMasterKey,p.PrivilegeOperation,p.RecordStatus
 from Sys_User u inner join Sys_Privilege p on u.SysId=p.PrivilegeMasterKey 
 where p.PrivilegeMaster = 10 and u.SysId='cf9d52cc-0500-4829-9611-fd0056961468'


--得到指定角色有那些用户
select u.CreateTime,u.LastLogin,u.RecordStatus,u.SysId,u.UserName,u.UserPwd from Sys_User u inner join Sys_UserRole ur on 
u.SysId=ur.UserId inner join Sys_Role r on ur.RoleId = r.SysId where r.SysId = 'cf9d52cc-0500-4829-9611-fd0056961489'


--得到指定角色有那些权限
select p.SysId,p.PrivilegeAccess,p.PrivilegeAccessKey,p.PrivilegeMaster,p.PrivilegeMasterKey,p.PrivilegeOperation,p.RecordStatus
 from Sys_User u inner join Sys_Privilege p on u.SysId=p.PrivilegeMasterKey 
 where p.PrivilegeMaster = 10 and u.SysId='cf9d52cc-0500-4829-9611-fd0056961468'



--------------------------------
delete from Sys_Role