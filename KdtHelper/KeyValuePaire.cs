using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace KdtHelper.Core
{
    /// <summary>
    /// 键值数据
    /// </summary>
    [DataContract]
    public class KeyValuePaire
    {
        /// <summary>
        /// 键
        /// </summary>
        [DataMember]
        public string key { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [DataMember]
        public object val { get; set; }
    }
}
