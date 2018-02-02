using System;
using System.Collections.Generic;
using System.Text;

namespace KdtHelper.Core
{
    /// <summary>
    /// DbDriver子项
    /// </summary>
    public class DbDriverMember
    {
        public DbDriverMember() { }

        /// <summary>
        /// 数据库配置项信息
        /// </summary>
        /// <param name="_name">驱动名称</param>
        /// <param name="_driver">驱动器类型</param>
        /// <param name="_server">数据库连接字符串</param>
        /// <param name="_prefix">参数前缀</param>
        public DbDriverMember(string _driver, string _server, string _prefix)
        {
            this.Prefix = _prefix;
            this.Server = _server;
            this.Driver = _driver;
        }


        /// <summary>
        /// 驱动器类型
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 参数前缀
        /// </summary>
        public string Server { get; set; }
    }
}
