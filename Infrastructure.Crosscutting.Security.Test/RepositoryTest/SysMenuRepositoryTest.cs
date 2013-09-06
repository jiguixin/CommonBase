using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Globalization;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.Repositorys;

namespace Infrastructure.Crosscutting.Security.Test.RepositoryTest
{
    [TestFixture]
    public class SysMenuRepositoryTest
    {
        private IRepository<SysMenu> repository;
        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository = new SysMenuRepository();
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
        public void AddTest()
        {
            var model = new SysMenu
                {
                    SysId = "cf9d52cc-0500-4829-9611-fd0056961468",
                    MenuNo = "2",
                    MenuParentNo = "0",
                    MenuOrder = 1,
                    MenuName = "菜单1",
                    MenuLink = "菜单链接1",
                    MenuIcon = "菜单图标1",
                    IsVisible = 1,
                    IsLeaf = 0,
                    RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "zwt")
                };
            Console.WriteLine(repository.Add(model));
        }

        [Test]
        public void GetTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961468");

            if (model != null)
            {
                Console.WriteLine(model.MenuName);
            }
            else
            {
                Console.WriteLine("没有查到值");
            }
        }

        [Test]
        public void UpdateTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961468");
            model.RecordStatus = string.Format("修改时间：{0},修改人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                               "zwt");
            Console.WriteLine(repository.Update(model));
        }

        [Test]
        public void Delete()
        {
            Console.WriteLine(repository.Delete("cf9d52cc-0500-4829-9611-fd0056961468"));
        }

        [Test]
        public void GetPagedTest()
        {
            int total = 0;
            var s = repository.GetPaged("Sys_Menu", "", "MenuNo='1'", "", 1, 20, 0, out total);
        }
    }
}
