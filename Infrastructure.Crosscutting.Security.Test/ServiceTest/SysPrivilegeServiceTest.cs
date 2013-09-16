/*
 *名称：SysPrivilegeServiceTest
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-16 05:47:32
 *修改时间：
 *备注：
 */

using System;

using NUnit.Framework;

namespace Infrastructure.Crosscutting.Security.Test.ServiceTest
{
    using Infrastructure.Crosscutting.Security.Services;

    [TestFixture]
    public class SysPrivilegeServiceTest
    {
        private ISysPrivilegeService privilegeService;
        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            this.privilegeService = ServiceFactory.PrivilegeService;
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
        public void InitDataByRoleTest()
        {
            privilegeService.InitDataByRole();
        }

    }
}