/*
 *名称：MenuServiceTest
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-13 11:25:31
 *修改时间：
 *备注：
 */

using System;

using NUnit.Framework;

namespace Infrastructure.Crosscutting.Security.Test.ServiceTest
{
    using System.Linq;

    using Infrastructure.Crosscutting.Security.Services;

    [TestFixture]
    public class MenuServiceTest
    {
        private ISysMenuService MenuService; 
        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            MenuService = new SysMenuService();
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
        public void GetButtonTest()
        {
            var lstResult = MenuService.MenuRepository.GetButtons("cf9d52cc-0500-4829-9611-fd0056961234");

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
            var lstResult = MenuService.GetPrivilege("cf9d52cc-0500-4829-9611-fd0056961234");

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