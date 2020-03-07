﻿using CodeM.Common.Orm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace UnitTest
{
    [TestClass]
    public class UnitTest3
    {

        [TestInitialize]
        public void Init()
        {
            string modelPath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\models");
            OrmUtils.ModelPath = modelPath;
            OrmUtils.Load();
        }

        [TestMethod]
        public void Test()
        {
            Test1();
            Test2();
            Test3();
            Test4();
            Test7();
            Test5();
        }

        [Description("创建User模型的物理表。")]
        public void Test1()
        {
            bool ret = OrmUtils.Model("User").CreateTable(true);
            Assert.IsTrue(ret);
        }

        [Description("向User模型的物理表写入一条数据。")]
        public void Test2()
        {
            dynamic newuser = ModelObject.New("User");
            newuser.Name = "wangxm";
            newuser.Age = 18;
            newuser.Birthday = new DateTime(1980, 6, 14);
            newuser.Deposit = 10000000.58;
            newuser.IsAdmin = true;
            bool ret = newuser.Save();
            Assert.IsTrue(ret);
        }

        [Description("向User模型的物理表写入一条数据。")]
        public void Test3()
        {
            try
            {
                dynamic newuser = ModelObject.New("User");
                newuser.Name = "wangxm";
                newuser.Age = 18;
                newuser.Birthday = new DateTime(1980, 6, 14);
                newuser.Deposit = 10000000.58;
                newuser.IsAdmin = true;
                bool ret = newuser.Save();
                Assert.IsFalse(ret);
            }
            catch (Exception exp)
            {
                Assert.IsTrue(exp.Message.ToUpper().Contains("UNIQUE") || exp.Message.ToUpper().Contains("DUPLICATE"));
            }
        }

        [Description("根据名称更新Test3中写入的数据，修改对应Age为25，Deposit为99999999")]
        public void Test4()
        {
            dynamic newuser = ModelObject.New("User");
            newuser.Name = "wangxm";
            newuser.Age = 25;
            newuser.Deposit = 99999999;
            bool ret = newuser.Update("uc_name");
            Assert.IsTrue(ret);
        }

        public void Test7()
        {
            OrmUtils.Model("User").TruncateTable();
        }

        [Description("删除Test1测试中创建的User模型物理表，应成功。")]
        public void Test5()
        {
            bool ret = OrmUtils.Model("User").RemoveTable();
            Assert.IsTrue(ret);
        }

    }
}
