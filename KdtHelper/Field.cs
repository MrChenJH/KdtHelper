using System;
using System.Collections.Generic;
using System.Text;

namespace KdtHelper.Core
{
    /// <summary>
    /// 字段属性
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class Field : System.Attribute
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_name">字段数据库名称</param>
        /// <param name="_max">字符串最大长度</param>
        /// <param name="_crypt">加密或转码存储方式（入库为加密，查询为解密方式。MD5方式不可逆）</param>
        /// <param name="_regex">格式验证（入库时参数）</param>
        /// <param name="_default">默认值(入库时为填入的默认信息)</param>
        public Field(string _name = "", int _max = 0, string _request = "", string _regex = null, object _default = null)
        {
            this.Name = _name;
            this.MaxLength = _max;
            this.Request = _request;
            this.Default = _default;
            this.Regex = _regex;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_name">字段数据库名称</param>
        /// <param name="_max">字符串最大长度</param>
        /// <param name="_default">默认值(入库时为填入的默认信息)</param>
        public Field(int _max, string _name = "", string _request = "", string _regex = null)
            : this(_name, _max, "", _regex)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_name">字段数据库名称</param>
        /// <param name="_default">字符串最大长度</param>
        public Field(object _default, string _name = "")
            : this(_name, 0, "", null, _default)
        { }

        #endregion.

        /// <summary>
        /// 字段数据库名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字符串最大长度
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// web 请求格式
        /// </summary>
        public string Request { get; set; }

        /// <summary>
        /// 默认值(入库时为填入的默认信息)
        /// </summary>
        public object Default { get; set; }

        /// <summary>
        /// 格式验证（入库时参数）
        /// </summary>
        public string Regex { get; set; }
    }
}
