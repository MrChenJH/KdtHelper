using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KdtHelper.Common
{
    public class JsonConfigurationHelper
    {
        /// <summary>
        /// 静态类日志类
        /// </summary>
        public static JsonConfigurationHelper Instance
        {
            get
            {
                return new JsonConfigurationHelper();
            }
        }

        public T GetAppSettings<T>(string key) where T : class, new()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .Add(new JsonConfigurationSource { Path = "app.json", Optional = false, ReloadOnChange = true })
                .Build();
            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<T>(config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return appconfig;
        }

        public string GetAppSetting(string key)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .Add(new JsonConfigurationSource { Path = "app.json", Optional = false, ReloadOnChange = true })
                .Build();
            return config[key];
        }
    }

    /// <summary>
    /// 日志工具，采用异步多线程日志方式
    /// </summary>
    public class KdtLoger : IDisposable
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private static KdtLoger _Instance = new KdtLoger();

        private ILog _Log;

        /// <summary>
        /// 默认构建日志记录器
        /// </summary>
        public KdtLoger()
        {
            JsonConfigurationHelper _configuration = new JsonConfigurationHelper();
            string _Log4NetConfig = _configuration.GetAppSetting("log4net");

            if (!string.IsNullOrEmpty(_Log4NetConfig))
            {
                ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
                XmlConfigurator.Configure(repository,new FileInfo(_Log4NetConfig));
                this._Log = LogManager.GetLogger(repository.Name, "NETCorelog4net");
            }
        }

        /// <summary>
        /// 改变Log保存配置路径信息
        /// </summary>
        /// <param name="argPath"></param>
        public void SetPath(string argPath)
        {
            JsonConfigurationHelper _configuration = new JsonConfigurationHelper();
            string _Log4NetConfig = _configuration.GetAppSetting("log4net");

            if (!string.IsNullOrEmpty(_Log4NetConfig))
            {
                ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
                XmlConfigurator.Configure(repository, new FileInfo(_Log4NetConfig));
                this._Log = LogManager.GetLogger(repository.Name, "NETCorelog4net");
            }
        }

        /// <summary>
        /// 静态类日志类
        /// </summary>
        public static KdtLoger Instance
        {
            get
            {
                return KdtLoger._Instance;
            }
        }

        /// <summary>
        /// 写入一般消息
        /// </summary>
        /// <param name="argContent">消息内容</param>
        public void Info(string argContent)
        {
            if (argContent == null)
            {
                return;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("------------------------------" + DateTime.Now.ToString() + "---------------------------------------");
            stringBuilder.AppendLine(argContent);

            this._Log.Info(stringBuilder.ToString());
        }

        /// <summary>
        /// 写入对象详细消息信息
        /// </summary>
        /// <param name="argContent">对象名称</param>
        public void Info(object argContent)
        {
            Type type = argContent.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetField);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("------------------------------" + string.Format("对象：{0} [{1}]", argContent.GetType().Name, DateTime.Now.ToString()) + "---------------------------------------");
            FieldInfo[] array = fields;
            for (int i = 0; i < array.Length; i++)
            {
                FieldInfo fieldInfo = array[i];
                stringBuilder.AppendFormat("{0}：", fieldInfo.Name).AppendLine(fieldInfo.GetValue(argContent).ToString());
            }

            this._Log.Info(stringBuilder.ToString());
        }

        /// <summary>
        /// 写入错误信息
        /// </summary>
        /// <param name="argException">Exception</param>
        public void Error(Exception argException)
        {
            if (argException == null)
            {
                return;
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("------------------------------" + DateTime.Now.ToString() + "---------------------------------------");
            stringBuilder.Append("Exception DESCRIPTION :").AppendLine(argException.Message);
            stringBuilder.Append("Exception SOURCE      :").AppendLine(argException.Source);
            stringBuilder.Append("InnerException        :").AppendLine(argException.InnerException == null ? "" : argException.InnerException.Message);
            stringBuilder.Append("Exception STACK       :").AppendLine(argException.StackTrace);

            this._Log.Error(stringBuilder.ToString());
        }


        #region 实现IDisposable

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion.
    }
}
