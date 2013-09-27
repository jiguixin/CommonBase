/*
 *名称：TestClass1
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-03 03:44:58
 *修改时间：
 *备注：
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.Services;
using Infrastructure.Data.Ado.Dapper;
using NUnit.Framework;

namespace Infrastructure.Crosscutting.Security.Test
{
    [TestFixture]
    public class TestClass1
    {
        /// <summary>
        ///     为每个Test方法创建资源
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        /// <summary>
        ///     为每个Test方法释放资源
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }

        /// <summary>
        ///     为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        /// <summary>
        ///     为整个TestFixture释放资源
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        private static int GetRandomSeed()
        {
            int iSeed = 10;
            var ro = new Random(10);
            long tick = DateTime.Now.Ticks;
            var ran = new Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));

            return ro.Next(0, 10);
        }

        [Test]
        public void CreateUserTest()
        {
            using (IDbConnection connection = ConnectionFactory.CreateMsSqlConnection())
            {
                IEnumerable<dynamic> value = connection.Query("select * from [Sys_User]");
            }
        }

        [Test]
        public void GetUserMenu()
        {
            string userId = "cf9d52cc-0500-4829-9611-fd0056961469";
            ISysUserService sysUserService = new SysUserService();

            //通过用户id获取权限菜单数据
            IEnumerable<SysPrivilege> sysUserPrivileges = sysUserService.GetPrivilege(userId);
            sysUserPrivileges = sysUserPrivileges.Where(x => x.PrivilegeAccess == PrivilegeAccess.Menu).ToList();

            //通过用户id获取角色，通过角色获取权限菜单数据

            IEnumerable<SysRole> sysRoles = sysUserService.GetRoles(userId);
            ISysRoleService sysRoleService = new SysRoleService();
            SysRole[] sysRoles1 = sysRoles.ToArray();
            //一个用户是否会有多个角色？
            for (int i = 0; i < sysRoles1.Count(); i++)
            {
                string roleId = sysRoles1[0].SysId;
                IEnumerable<SysPrivilege> sysRolePrivileges = sysRoleService.GetPrivilege(roleId);

                //排除同用户权限相同菜单数据
                SysPrivilege[] sysUserPrivileges1 = sysUserPrivileges.ToArray();
                for (int j = 0; j < sysUserPrivileges1.Length; j++)
                {
                    sysRolePrivileges =
                        sysRolePrivileges.Where(
                            x =>
                            x.PrivilegeAccessKey != sysUserPrivileges1[j].PrivilegeAccessKey &&
                            x.PrivilegeAccess == PrivilegeAccess.Menu).ToList();
                }

                sysUserPrivileges = sysUserPrivileges.Union(sysRolePrivileges);
            }
            //sysUserPrivileges结果为最终菜单权限
            List<SysMenu> sysMenus = new List<SysMenu>();
            ISysMenuService sysMenuService = new SysMenuService();
            foreach (SysPrivilege sysUserPrivilege in sysUserPrivileges)
            {
                string menuId = sysUserPrivilege.PrivilegeAccessKey;
                SysMenu sysMenu=sysMenuService.GetSysMenuById(menuId).ToArray()[0];
                sysMenus.Add(sysMenu);
            }
            sysMenus = sysMenus.Where(x => x.IsVisible==1).OrderBy(x => x.MenuOrder).ToList();
        }

        [Test]
        public void AddMenu()
        {
            string sysId = Guid.NewGuid().ToString();
            using (IDbConnection connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@SysId", sysId);
                p.Add("@MenuParentId", "4300916d-b838-4126-9bf3-abcc6615d908");
                p.Add("@MenuOrder", 502);
                p.Add("@MenuName", "角色菜单权限配置");
                p.Add("@MenuLink", "/MenuSetting/Index1");
                p.Add("@MenuIcon", "icon-nav");
                p.Add("@@IsLeaf", false);
                p.Add("@IsVisible", true);
                p.Add("@RecordStatus", "修改时间" + DateTime.Now + ",修改人：JF");

                connection.Execute("Sys_Menu_ADD", p, commandType: CommandType.StoredProcedure);

            }
            //using (IDbConnection connection = ConnectionFactory.CreateMsSqlConnection())
            //{
            //    var p = new DynamicParameters();
            //    p.Add("@SysId", Guid.NewGuid().ToString());
            //    p.Add("@PrivilegeMaster", PrivilegeMaster.Role);
            //    p.Add("@PrivilegeMasterKey", "cf9d52cc-0500-4829-9611-fd0056961488");
            //    p.Add("@PrivilegeAccess", PrivilegeAccess.Menu);
            //    p.Add("@PrivilegeAccessKey", sysId);
            //    p.Add("@PrivilegeOperation", true);
            //    p.Add("@RecordStatus", "创建时间：" + DateTime.Now + ",创建人：JF");

            //    connection.Execute("Sys_Privilege_ADD", p, commandType: CommandType.StoredProcedure);
            //}
        }

        [Test]
        public void AddMenuToPrivilege()
        {
            using (IDbConnection connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@SysId", Guid.NewGuid().ToString());
                p.Add("@PrivilegeMaster", PrivilegeMaster.Role);
                p.Add("@PrivilegeMasterKey", "cf9d52cc-0500-4829-9611-fd0056961488");
                p.Add("@PrivilegeAccess", PrivilegeAccess.Menu);
                p.Add("@PrivilegeAccessKey", "f9199270-abd9-4b83-9020-e0c88e5c8b52");
                p.Add("@PrivilegeOperation", true);
                p.Add("@RecordStatus", "创建时间："+DateTime.Now+",创建人：JF");

                connection.Execute("Sys_Privilege_ADD", p, commandType: CommandType.StoredProcedure);
            }
        }

        [Test]
        public void AddUserToRole()
        {
            using (IDbConnection connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@SysId", Guid.NewGuid().ToString());
                p.Add("@UserId", "cf9d52cc-0500-4829-9611-fd0056961468");
                p.Add("@RoleId", "cf9d52cc-0500-4829-9611-fd0056961488");

                connection.Execute("Sys_UserRole_ADD", p, commandType: CommandType.StoredProcedure);
            }
        }

        [Test]
        public void Test()
        {
            var ran = new Random();
            ;
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(DateTime.Now.ToString("yyyyMMddHHmmssfffff"));
                // Console.WriteLine(ran.Next(10000, 99999) + "--");
            }
        }

        [Test]
        public void TestProcedureAdd()
        {
            using (IDbConnection connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@SysId", new Guid());
                p.Add("@MenuId", "1");
                p.Add("@MenuNo", "1");
                p.Add("@BtnName", "按钮1");
                p.Add("@BtnNo", "1");
                p.Add("@BtnIcon", "src='1.png'");
                p.Add("@BtnOrder", 1);
                p.Add("@RecordStatus", "RecordStatus");
                connection.Execute("Sys_Button_Add", p, commandType: CommandType.StoredProcedure);
            }
        }

        [Test]
        public void TestProcedureDelete()
        {
            using (IDbConnection connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@BtnId", "00000000-0000-0000-0000-000000000000");
                connection.Execute("Sys_Button_Delete", p, commandType: CommandType.StoredProcedure);
            }
        }

        [Test]
        public void TestProcedureExists()
        {
            using (IDbConnection connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@BtnId", "00000000-0000-0000-0000-000000000000");
                connection.Execute("Sys_Button_Exists", p, commandType: CommandType.StoredProcedure);
            }
        }

        [Test]
        public void TestProcedureGetList()
        {
            using (IDbConnection connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();

                connection.Query("Sys_Button_GetList", commandType: CommandType.StoredProcedure);
            }
        }

        [Test]
        public void TestProcedureGetModel()
        {
            using (IDbConnection connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@BtnId", "00000000-0000-0000-0000-000000000000");
                SysButton SysButton =
                    connection.Query<SysButton>("Sys_Button_GetModel", p, commandType: CommandType.StoredProcedure)
                              .First();
                Console.WriteLine(SysButton.BtnIcon);
            }
        }
    }
}