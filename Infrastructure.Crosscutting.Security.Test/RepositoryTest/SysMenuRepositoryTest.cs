using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Ioc;
using NUnit.Framework;
using System.Globalization;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.Repositorys;

namespace Infrastructure.Crosscutting.Security.Test.RepositoryTest
{
    [TestFixture]
    public class SysMenuRepositoryTest
    {
        static SysMenuRepositoryTest()
        {
            InstanceLocator.SetLocator(
           new NinjectContainer().WireDependenciesInAssemblies(typeof(AppModule).Assembly.FullName).Locator);
        }
        private IRepository<SysMenu> repository;
        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        { 
            repository = RepositoryFactory.MenuRepository;
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
                    SysId = "cf9d52cc-0500-4829-9611-fd0056961234", 
                    MenuParentId = null,
                    MenuOrder = 1,
                    MenuName = "文件",
                    MenuLink = "文件菜单链接1",
                    MenuIcon = "文件菜单图标1",
                    IsVisible = 1,
                    IsLeaf = 0,
                    RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "jim")
                };
            Console.WriteLine(repository.Add(model));
        }

        [Test]
        public void GetTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961234");

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
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961234");
            model.RecordStatus = string.Format("修改时间：{0},修改人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                               "jim");
            Console.WriteLine(repository.Update(model));
        }

        [Test]
        public void Delete()
        {
            Console.WriteLine(repository.Delete("cf9d52cc-0500-4829-9611-fd0056961234"));
        }

        [Test]
        public void GetPagedTest()
        {
            int total = 0;
            var lstResult = repository.GetPaged("Sys_Menu", "", "MenuOrder='1'", "", 1, 20, 0, out total);

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
