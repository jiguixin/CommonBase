/*
 *名称：UserServiceTest
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-11 05:55:34
 *修改时间：
 *备注：
 */

using System;
using Infrastructure.Crosscutting.Security.Ioc;
using NUnit.Framework;

namespace Infrastructure.Crosscutting.Security.Test.ServiceTest
{
    using System.Linq;

    using Infrastructure.Crosscutting.Security.Services;

    [TestFixture]
    public class UserServiceTest
    {
        private ISysUserService UserService;

        static UserServiceTest()
        {
            InstanceLocator.SetLocator(
                new NinjectContainer().WireDependenciesInAssemblies(typeof(AppModule).Assembly.FullName).Locator);
        }

        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            UserService = ServiceFactory.UserService;
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
        public void GetRolesTest()
        {
            var lstResult = UserService.GetRoles("cf9d52cc-0500-4829-9611-fd0056961468");

            if (lstResult != null && lstResult.Any())
            {
                Console.WriteLine(lstResult.Count());
            }
            else
            {
                Console.WriteLine("没有查到值");
            }
        }

        [Test]
        public void GetPrivilegeTest()
        {
            var lstResult = UserService.GetPrivilege("cf9d52cc-0500-4829-9611-fd0056961468");

            if (lstResult != null && lstResult.Any())
            {
                Console.WriteLine(lstResult.Count());
            }
            else
            {
                Console.WriteLine("没有查到值");
            }
        }

    }
}