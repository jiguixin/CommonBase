/*
 *名称：OracleTest
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-10-08 02:56:45
 *修改时间：
 *备注：
 */

using System;

using NUnit.Framework;

namespace Infrastructure.Crosscutting.Security.Test
{
    using Infrastructure.Crosscutting.Security.SqlImple;

    [TestFixture]
    public class OracleTest
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
        public void GetDataTest()
        {
            PublishService ps = new PublishService();
            int total;
            var lst = ps.GetPaged<Ps>("PUBLICSERVICE", "", out total);


        }

    }

    public class Ps
    {
        public string PS_ID { get; set; }
        public string BU_ID { get; set; }
        public string PS_NAME { get; set; }
        public string PS_TYPEID { get; set; }
        public string PS_JZMJ { get; set; }
        public string PS_YDMJ { get; set; }
        public string PS_QS { get; set; }
        public string PS_ADDRESS { get; set; }
        public DateTime PS_CREATETIME { get; set; }
    }


    public class PublishService : Oracle
    {

        public override string AddSql
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string UpdateSql
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}