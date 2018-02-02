using System;
using System.Collections.Generic;
using System.Text;

namespace KdtHelper.Core.ExecuterEx
{
    /// <summary>
    /// 表扩展
    /// </summary>
    public class KdtTableFeildEx
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public KdtTableFeildEx()
        {
            Fields = new List<KdtFeildEx>();
            HasIncr = false;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public List<KdtFeildEx> Fields { get; set; }

        /// <summary>
        /// 包含多个关联
        /// </summary>
        public bool HasRelation { get; set; }

        /// <summary>
        /// 关联字段
        /// </summary>
        public string RelFeild { get; set; }

        /// <summary>
        /// 是否存在主键
        /// </summary>
        public bool HasIncr { get; set; }

        /// <summary>
        /// 自增字段
        /// </summary>
        public KdtFeildEx IncrFeild { get; set; }
    }
}
