using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Data.Ado.Dapper;
using NUnit.Framework;

namespace Infrastructure.Crosscutting.Security.Test
{
    [TestFixture]
    public class Test1
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
        public void SqlTest()
        {
            string sql = "select @Fields from @Table where 2=2";

            using (var connection = ConnectionFactory.CreateMsSqlConnection())
            {
                var p = new DynamicParameters();
                p.Add("@Table", "Sys_User", DbType.String, ParameterDirection.Input, 1000);
                p.Add("@Fields", "*", DbType.String, ParameterDirection.Input, 2000);
                p.Add("@Where", "1=1", DbType.String, ParameterDirection.Input, 1000);

                var lst = connection.Query<SysUser>(sql, p, commandType: CommandType.Text);
            }

        }


    }
}
