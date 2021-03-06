﻿using CodeM.Common.Orm;
using CodeM.Common.Orm.Serialize;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class UnitTest5
    {
        [TestInitialize]
        public void Init()
        {
            OrmUtils.RegisterProcessor("EncryptDeposit", "UnitTest.Processors.EncryptDeposit");
            OrmUtils.RegisterProcessor("DecryptDeposit", "UnitTest.Processors.DecryptDeposit");

            string modelPath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\models");
            OrmUtils.ModelPath = modelPath;
            OrmUtils.Load();

            OrmUtils.RemoveTables();
        }

        [TestMethod]
        public void Test()
        {
            Test1();
            Test2();
            Test3();
            Test4();
            Test5();
            Test6();
            Test7();
            Test8();
            Test9();
        }

        [Description("创建模型的物理表。")]
        public void Test1()
        {
            bool ret = OrmUtils.TryCreateTables(true);
            Assert.IsTrue(ret);
        }

        [Description("向机构表插入一条数据。")]
        public void Test2()
        {
            dynamic neworg = ModelObject.New("Org");
            neworg.Code = "XXTech";
            neworg.Name = "XX科技";
            bool ret = OrmUtils.Model("Org").SetValues(neworg).Save();
            Assert.IsTrue(ret);
        }

        [Description("向用户表插入一条数据，所属机构为XX科技")]
        public void Test3()
        {
            List<dynamic> orgList = OrmUtils.Model("Org").Equals("Name", "XX科技").Query();
            dynamic newuser = ModelObject.New("User");
            newuser.Name = "wangxm";
            newuser.Org = orgList[0].Code;
            bool ret = OrmUtils.Model("User").SetValues(newuser).Save();
            Assert.IsTrue(ret);
        }

        [Description("查询名称为wangxm的所属机构Id和名称，应为1和XX科技")]
        public void Test4()
        {
            List<dynamic> userList = OrmUtils.Model("User").Equals("Name", "wangxm").GetValue("Name", "Org.Id", "Org.Name").Query();
            Assert.AreEqual("1-XX科技", userList[0].Org.Id + "-" + userList[0].Org.Name);
        }

        [Description("向机构表插入一条数据。")]
        public void Test5()
        {
            dynamic neworg = ModelObject.New("Org");
            neworg.Code = "YYTech";
            neworg.Name = "YY科技";
            bool ret = OrmUtils.Model("Org").SetValues(neworg).Save();
            Assert.IsTrue(ret);
        }

        [Description("向用户表插入一条数据，所属机构为YY科技")]
        public void Test6()
        {
            List<dynamic> orgList = OrmUtils.Model("Org").Equals("Name", "YY科技").Query();
            dynamic newuser = ModelObject.New("User");
            newuser.Name = "huxy";
            newuser.Org = orgList[0].Code;
            bool ret = OrmUtils.Model("User").SetValues(newuser).Save();
            Assert.IsTrue(ret);
        }

        [Description("查询所属机构为YY科技的第一条用户的名称，应为huxy。")]
        public void Test7()
        {
            List<dynamic> userList = OrmUtils.Model("User").Equals("Org.Name", "YY科技").GetValue("Name").Top(1).Query();
            Assert.AreEqual("huxy", userList[0].Name);
        }

        [Description("根据用户所属机构名称进行升序排序，第一条用户名应为wangxm。")]
        public void Test8()
        {
            List<dynamic> userList = OrmUtils.Model("User").GetValue("Name").AscendingSort("Org.Name").Top(1).Query();
            Assert.AreEqual("wangxm", userList[0].Name);
        }

        [Description("根据用户所属机构名称进行降序排序，第一条用户名应为huxy。")]
        public void Test9()
        {
            List<dynamic> userList = OrmUtils.Model("User").GetValue("Name").DescendingSort("Org.Name").Top(1).Query();
            Assert.AreEqual("huxy", userList[0].Name);
        }
    }
}
