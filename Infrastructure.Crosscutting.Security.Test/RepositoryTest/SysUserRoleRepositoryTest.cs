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
    public class SysUserRoleRepositoryTest
    {
        private IRepository<SysUserRole> repository;
        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository = RepositoryFactory.UserRoleRepository;
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
            var model = new SysUserRole
                {
                    SysId = "cf9d52cc-0500-4829-9611-fd0056961477",
                    UserId = "cf9d52cc-0500-4829-9611-fd0056961468",
                    RoleId = "cf9d52cc-0500-4829-9611-fd0056961488"
                };
           // Console.WriteLine(repository.Add(model));

            model = new SysUserRole
            {
                SysId = "cf9d52cc-0500-4829-9611-fd0056961478",
                UserId = "cf9d52cc-0500-4829-9611-fd0056961468",
                RoleId = "cf9d52cc-0500-4829-9611-fd0056961489"
            };
            Console.WriteLine(repository.Add(model));
        }

        [Test]
        public void GetTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961477");

            if (model != null)
            {
                Console.WriteLine(model.UserId);
            }
            else
            {
                Console.WriteLine("没有查到值");
            }
        }

        [Test]
        public void UpdateTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961477");

            Console.WriteLine(repository.Update(model));
        }

        [Test]
        public void Delete()
        {
            Console.WriteLine(repository.Delete("cf9d52cc-0500-4829-9611-fd0056961477"));
        }

        [Test]
        public void GetPagedTest()
        {
            int total = 0;
            var s = repository.GetPaged("Sys_UserRole", "", "UserId='cf9d52cc-0500-4829-9611-fd0056961477'", "", 1, 20, 0, out total);
        }
    }
}
