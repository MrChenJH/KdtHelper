using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Common;

namespace KdtHelper.Core.ExecuterEx
{
    /// <summary>
    /// KDT字段符
    /// </summary>
    public class KdtFeildEx
    {
        /// <summary>
        /// 字段命名（非填写信息）
        /// </summary>
        public string SetName { get; set; }

        /// <summary>
        /// 字段所在表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 字符名称（对应数据库字段名称
        /// </summary>
        public string FeildName { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public object FeildValue { get; set; }

        /// <summary>
        /// 是否为自增字段
        /// </summary>
        public bool IsIncr { get; set; }

        /// <summary>
        /// 是否为为主键
        /// </summary>
        public bool IsKey { get; set; }

        /// <summary>
        /// 是否存在值
        /// </summary>
        public bool HasValue
        {
            get
            {
                return FeildValue != null;
            }
        }

        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <returns></returns>
        public T Value<T>(T _default = default(T))
        {
            return FeildValue.Convert<T>(_default);
        }
    }
}
