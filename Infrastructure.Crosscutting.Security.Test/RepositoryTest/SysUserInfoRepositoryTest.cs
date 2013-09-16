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
    public class SysUserInfoRepositoryTest
    {
        private IRepository<SysUserInfo> repository;
        /// <summary>
        /// 为整个TestFixture初始化资源
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            repository = RepositoryFactory.UserInfoRepository;
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
            var model = new SysUserInfo
                {
                    SysId = "cf9d52cc-0500-4829-9611-fd0056961468",
                    RealName = "用户1",
                    Title = "标题？？？",
                    Sex = true,
                    Phone = "13888888888",
                    Fax = "028-12345678",
                    Email = "12345678@qq.com",
                    QQ = "12345678",
                    Address = "武科东四路科泰地理信息技术有限公司"
                };
            Console.WriteLine(repository.Add(model));
        }

        [Test]
        public void GetTest()
        {
            var model = repository.GetModel("cf9d52cc-0500-4829-9611-fd0056961468");

            if (model != null)
            {
                Console.WriteLine(model.RealName);
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
            model.RealName = "用户2";
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
            var s = repository.GetPaged("Sys_UserInfo", "", "RealName='用户2'", "", 1, 20, 0, out total);
        }
    }
}
