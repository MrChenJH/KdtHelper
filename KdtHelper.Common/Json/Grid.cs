using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace KdtHelper.Common.Json
{
    public class Grid<T> where T : class, new()
    {
        public Grid()
        {
            this.success = false;
            this.total = 0;
            this.data = new List<T>();
        }

        /// <summary>
        /// 读取数据是否成功
        /// </summary>
        [DataMember]
        public bool success { get; set; }

        /// <summary>
        /// 读取数据总数
        /// </summary>
        [DataMember]
        public int total { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [DataMember]
        public string error { get; set; }

        /// <summary>
        /// 读取数据集
        /// </summary>
        [DataMember]
        public List<T> data { get; set; }

        public void Add(T value)
        {
            data.Add(value);
            success = true;
        }

        public void AddRange(IEnumerable<T> values)
        {
            foreach (T item in values)
            {
                data.Add(item);
            }
            success = true;
        }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
