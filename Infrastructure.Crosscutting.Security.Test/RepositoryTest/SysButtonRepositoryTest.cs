using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Sql;
using NUnit.Framework;
using System.Globalization;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.Repositorys;

namespace Infrastructure.Crosscutting.Security.Test.RepositoryTest
{
    using System.Collections;

    [TestFixture]
    public class SysButtonRepositoryTest
    {
        static SysButtonRepositoryTest()
        {
            InstanceLocator.SetLocator(
           new NinjectContainer().WireDependenciesInAssemblies(typeof(AppModule).Assembly.FullName).Locator);
        }
        private IRepository<SysButton> repository;
        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {  
            repository = RepositoryFactory.ButtonRepository;
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
            for (int i = 1; i < 10; i++)
            {
                var model = new SysButton
                {
                    SysId = "cf9d52cc-0500-4829-9611-fd005696192"+i.ToString(CultureInfo.InvariantCulture),
                    MenuId = "cf9d52cc-0500-4829-9611-fd0056961234",
                    BtnName = "按钮" + i.ToString(CultureInfo.InvariantCulture),
                    BtnIcon = "图标路径",
                    BtnOrder = i,
                    RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "zwt")
                };
                Console.WriteLine(repository.Add(model));
            } 
        }

        [Test]
        public void GetTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961921");

            if (model != null)
            {
                Console.WriteLine(model.MenuId);
            }
            else
            {
                Console.WriteLine("没有查到值");
            }
        }

        [Test]
        public void UpdateTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961921");
            model.RecordStatus = string.Format("修改时间：{0},修改人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                               "zwt");

            Console.WriteLine(repository.Update(model));
        }

        [Test]
        public void Delete()
        {
            Console.WriteLine(repository.Delete("cf9d52cc-0500-4829-9611-fd0056961921"));
        }

        [Test]
        public void GetAll()
        {
            IEnumerable<SysButton> lstResult = repository.GetList();

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
        public void GetPagedTest()
        {
            int total = 0;
            var lstResult = repository.GetPaged("Sys_Button", "", "BtnName='按钮1'", "", 1, 20, 0, out total);

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
