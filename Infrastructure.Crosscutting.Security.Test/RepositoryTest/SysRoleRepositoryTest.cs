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
    public class SysRoleRepositoryTest
    {
        private IRepository<SysRole> repository;
        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository = new SysRoleRepository();
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
            var model = new SysRole
                {
                    SysId = "cf9d52cc-0500-4829-9611-fd0056961488",
                    RoleName = "管理员",
                    RoleDesc = "管理系统的用户",
                    RecordStatus = string.Format("创建时间：{0},创建人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture), "zwt")
                };
            Console.WriteLine(repository.Add(model));
        }

        [Test]
        public void GetTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961488");

            if (model != null)
            {
                Console.WriteLine(model.RoleName);
            }
            else
            {
                Console.WriteLine("没有查到值");
            }
        }

        [Test]
        public void UpdateTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961488");
            model.RecordStatus = string.Format("修改时间：{0},修改人：{1}", DateTime.Now.ToString(CultureInfo.InvariantCulture),
                                               "zwt");
            Console.WriteLine(repository.Update(model));
        }

        [Test]
        public void Delete()
        {
            //todo 删除角色时，要删除用户和角色表，同时要删除权限表
            Console.WriteLine(repository.Delete("cf9d52cc-0500-4829-9611-fd0056961488"));
        }

        [Test]
        public void GetPagedTest()
        {
            int total = 0;
            var s = repository.GetPaged("Sys_Role", "", "RoleName='管理员'", "", 1, 20, 0, out total);
        }
    }
}
