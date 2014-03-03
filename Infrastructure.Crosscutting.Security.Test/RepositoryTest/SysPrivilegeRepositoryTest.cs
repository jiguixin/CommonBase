using System;
using System.Linq;
using Infrastructure.Crosscutting.Security.Common;
using Infrastructure.Crosscutting.Security.Ioc;
using NUnit.Framework;
using System.Globalization;
using Infrastructure.Crosscutting.Security.Model;
using Infrastructure.Crosscutting.Security.Repositorys;

namespace Infrastructure.Crosscutting.Security.Test.RepositoryTest
{
    [TestFixture]
    public class SysPrivilegeRepositoryTest
    {
        static SysPrivilegeRepositoryTest()
        {
            InstanceLocator.SetLocator(
           new NinjectContainer().WireDependenciesInAssemblies(typeof(AppModule).Assembly.FullName).Locator);
        }
        private IRepository<SysPrivilege> repository;

        private SysMenuRepository menuRepository;

        private SysRoleRepository roleRepository;
        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        { 
            repository = RepositoryFactory.PrivilegeRepository;

            this.menuRepository = RepositoryFactory.MenuRepository;
            this.roleRepository = RepositoryFactory.RoleRepository;
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
            var model = new SysPrivilege
                {
                    SysId = "cf9d52cc-0500-4829-9611-fd0056961123",
                    PrivilegeMaster =  PrivilegeMaster.User,
                    PrivilegeMasterKey = "cf9d52cc-0500-4829-9611-fd0056961468",
                    PrivilegeAccess = PrivilegeAccess.Menu,
                    PrivilegeAccessKey = "cf9d52cc-0500-4829-9611-fd0056961234",
                    PrivilegeOperation = PrivilegeOperation.Enable,
                    RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "jim")
                };
            Console.WriteLine(repository.Add(model));

            model = new SysPrivilege
            {
                SysId = "cf9d52cc-0500-4829-9611-fd0056961124",
                PrivilegeMaster =PrivilegeMaster.Role,
                PrivilegeMasterKey = "cf9d52cc-0500-4829-9611-fd0056961488",
                PrivilegeAccess = PrivilegeAccess.Menu,
                PrivilegeAccessKey = "cf9d52cc-0500-4829-9611-fd0056961234",
                PrivilegeOperation = PrivilegeOperation.Enable,
                RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "jim")
            };
            Console.WriteLine(repository.Add(model));

            model = new SysPrivilege
            {
                SysId = "cf9d52cc-0500-4829-9611-fd0056961125",
                PrivilegeMaster = PrivilegeMaster.Role,
                PrivilegeMasterKey = "cf9d52cc-0500-4829-9611-fd0056961488",
                PrivilegeAccess = PrivilegeAccess.Button,
                PrivilegeAccessKey = "cf9d52cc-0500-4829-9611-fd0056961921",
                PrivilegeOperation = PrivilegeOperation.Enable,
                RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "jim")
            };
           Console.WriteLine(repository.Add(model));
        }

        [Test]
        public void GetTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961123");

            if (model != null)
            {
                Console.WriteLine(model.RecordStatus);
            }
            else
            {
                Console.WriteLine("没有查到值");
            }
        }

        [Test]
        public void UpdateTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961123");
            model.RecordStatus = string.Format("修改时间：{0},修改人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                               "jim");
            Console.WriteLine(repository.Update(model));
        }

        [Test]
        public void Delete()
        {
            Console.WriteLine(repository.Delete("cf9d52cc-0500-4829-9611-fd0056961123"));
        }

        [Test]
        public void GetPagedTest()
        {
            int total = 0;
            var lstResult = repository.GetPaged("Sys_Privilege", "", "PrivilegeMasterKey='cf9d52cc-0500-4829-9611-fd0056961488'", "", 1, 20, 0, out total);

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
        public void AddByRole()
        {
           
        }
    }
}
