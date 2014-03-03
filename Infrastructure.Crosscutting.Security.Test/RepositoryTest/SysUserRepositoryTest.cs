using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Crosscutting.Security.Ioc;
using Infrastructure.Crosscutting.Security.Services;
using NUnit.Framework;
using System.Globalization;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.Repositorys;

namespace Infrastructure.Crosscutting.Security.Test.RepositoryTest
{
    [TestFixture]
    public class SysUserRepositoryTest
    {
        static SysUserRepositoryTest()
        {
            InstanceLocator.SetLocator(
           new NinjectContainer().WireDependenciesInAssemblies(typeof(AppModule).Assembly.FullName).Locator);
        }

        private SysUserRepository repository;

        private ISysUserService service;
        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository =RepositoryFactory.UserRepository;
            service = ServiceFactory.UserService;
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
        public void LoginTest()
        {
            SysUserService userService = new SysUserService();
            var rValue = userService.CheckUser("admin", "123456");
            Console.WriteLine(rValue !=null);
        }

        [Test]
        public void AddTest()
        {
            var model = new SysUser
                {
                    SysId = "cf9d52cc-0500-4829-9611-fd0056961468",
                    UserName = "admin",
                    UserPwd = "123456",
                    CreateTime = DateTime.Now,
                    LastLogin = DateTime.Now,
                    RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "jim"),
                    UserInfo = new SysUserInfo(){ SysId= "cf9d52cc-0500-4829-9611-fd0056961468", Address="武科东4路", Email="jim@qq.com", Fax="028-34332234", Phone="13550235489", QQ="241542368", RealName="张文涛", Sex=true, Title="开发工程师"}
                };
             Console.WriteLine(service.AddUser(model));

            model = new SysUser
            {
                SysId = "cf9d52cc-0500-4829-9611-fd0056961469",
                UserName = "admin1",
                UserPwd = "123456",
                CreateTime = DateTime.Now,
                LastLogin = DateTime.Now,
                RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "jim"),
                UserInfo = new SysUserInfo() { SysId = "cf9d52cc-0500-4829-9611-fd0056961469", Address = "武科东4路2", Email = "zwt2@qq.com", Fax = "028-34332234", Phone = "13550235489", QQ = "241542368", RealName = "张文涛2", Sex = true, Title = "开发工程师2" }
            };
            Console.WriteLine(service.AddUser(model));
        }

        [Test]
        public void GetTest()
        {
            var model = service.UserRepository.GetModel("cf9d52cc-0500-4829-9611-fd0056961468");

            if (model != null)
            {
                Console.WriteLine(model.LastLogin);
            }
            else
            {
                Console.WriteLine("没有查到值");
            }
        }

        [Test]
        public void UpdateTest()
        {
            var model = service.UserRepository.GetModel("cf9d52cc-0500-4829-9611-fd0056961468");
            if (model == null)
                return;

            model.RecordStatus = string.Format("修改时间：{0},修改人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                               "jim");

            model.UserInfo.RealName = "张问";

            Console.WriteLine(service.UserRepository.Update(model));
        }

        [Test]
        public void Delete()
        {
            Console.WriteLine(service.UserRepository.Delete("cf9d52cc-0500-4829-9611-fd0056961468"));
            Console.WriteLine(service.UserRepository.Delete("cf9d52cc-0500-4829-9611-fd0056961469"));
        }

        [Test]
        public void GetPagedTest()
        {
            int total = 0;
            var lstResult = repository.GetPaged("Sys_User", "", "UserName='admin'", "", 1, 20, 0, out total);

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
