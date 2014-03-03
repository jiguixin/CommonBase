/*
 *名称：SysConfigRepositoryTest
 *功能：
 *创建人：吉桂昕
 *创建时间：2013-09-04 03:34:45
 *修改时间：
 *备注：
 */

using System;
using System.Linq;
using Infrastructure.Crosscutting.Security.Ioc;
using NUnit.Framework;

namespace Infrastructure.Crosscutting.Security.Test.RepositoryTest
{
    using System.Globalization;

    using Infrastructure.Crosscutting.Security.Core;
    using Infrastructure.Crosscutting.Security.Model;
    using Infrastructure.Crosscutting.Security.Repositorys;
    using Infrastructure.Crosscutting.Security.SqlImple;

    [TestFixture]
    public class SysConfigRepositoryTest
    {
        static SysConfigRepositoryTest()
        {
            InstanceLocator.SetLocator(
           new NinjectContainer().WireDependenciesInAssemblies(typeof(AppModule).Assembly.FullName).Locator);
        }
        private IRepository<SysConfig> repository;

        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository = RepositoryFactory.ConfigRepository;
            /*repository =
                new SysConfigRepository(InstanceLocator.Current.GetInstance<ISql>("SysConfigImple"));*/
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
            SysConfig model = new SysConfig()
                                  {
                                      SysId = "cf9d52cc-0500-4829-9611-fd0056961468",
                                      SysKey = "Abc",
                                      SysValue = "123",
                                      RecordStatus =
                                          string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "jim")
                                  };

            Console.WriteLine(repository.Add(model));
        }

        [Test]
        public void GetTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961468");

            if (model != null)
            {
                Console.WriteLine(model.SysKey);
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
            model.SysKey = "abd";
            model.SysValue = "23223";
            model.RecordStatus = "aaa";
           // model.SysParentId = "1000048";

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
            var lstResult = repository.GetPaged("Sys_Config", "", "", "", 1, 20, 0, out total);

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