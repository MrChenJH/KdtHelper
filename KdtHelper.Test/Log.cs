using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using KdtHelper.Common;
using System;
using KdtHelper.Core;

namespace KdtHelper.Test
{
    [TestClass]
    public class Log
    {
        [TestMethod]
        public void WriteInfoLog()
        {
            KdtLoger.Instance.Info("测试基础日志输出");
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void WriteErrorLog()
        {
            try
            {
                throw new Exception("测试错误信息");
            }
            catch (Exception ex)
            {
                KdtLoger.Instance.Error(ex);
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void ReadCofingEntity()
        {
            try
            {
                KdtDriver val = JsonConfigurationHelper.Instance.GetAppSettings<KdtDriver>("main");

                DbDriverMember sqldb = JsonConfigurationHelper.Instance.GetAppSettings<DbDriverMember>("sqlserverdb");

                Assert.IsTrue(val != null && !string.IsNullOrEmpty(val.prefix));
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
