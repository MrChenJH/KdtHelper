using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace KdtHelper.Common.Json
{
    [DataContract]
    public class Entity<T> where T : class, new()
    {
        public Entity(T val)
        {
            this.data = val;
        }

        /// <summary>
        /// 读取数据集
        /// </summary>
        [DataMember]
        public T data { get; set; }

        public override string ToString()
        {
            string json = data.ToJson();
            if(json.IndexOf("[") > -1)
            {
                return json.Replace("[", "{\"success\":true,\"data\":[").Replace("]", "]}");
            }
            return json.Replace("{", "{\"success\":true,");
        }
    }
}
