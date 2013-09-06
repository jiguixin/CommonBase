/*
 *名称：TestClass1
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-03 03:44:58
 *修改时间：
 *备注：
 */

using System;
using System.Data;
using System.Linq;
using Infrastructure.Crosscutting.Security.Model;
using NUnit.Framework;
using Infrastructure.Data.Ado.Dapper;

namespace Infrastructure.Crosscutting.Security.Test
{
    [TestFixture]
    public class TestClass1
    {
        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
        }

        /// <summary>
        /// 为整个TestFixture释放资源
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
        }

        /// <summary>
        /// 为每个Test方法创建资源
        /// </summary>
        [SetUp]
        public void Initialize()
        {
        }

        /// <summary>
        /// 为每个Test方法释放资源
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void CreateUserTest()
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var value = connection.Query("select * from [Sys_User]");

            }

        }

        [Test]
        public void TestProcedureGetList()
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();

                connection.Query("Sys_Button_GetList", commandType: CommandType.StoredProcedure);
            }
        }

        [Test]
        public void TestProcedureAdd()
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
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
        public void TestProcedureGetModel()
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@BtnId", "00000000-0000-0000-0000-000000000000");
                var SysButton = connection.Query<SysButton>("Sys_Button_GetModel", p, commandType: CommandType.StoredProcedure).First();
                Console.WriteLine(SysButton.BtnIcon);
            }
        }

        [Test]
        public void TestProcedureDelete()
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@BtnId", "00000000-0000-0000-0000-000000000000");
                connection.Execute("Sys_Button_Delete", p, commandType: CommandType.StoredProcedure);
            }
        }

        [Test]
        public void TestProcedureExists()
        {
            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@BtnId", "00000000-0000-0000-0000-000000000000");
                connection.Execute("Sys_Button_Exists", p, commandType: CommandType.StoredProcedure);
            }
        }
    }
}