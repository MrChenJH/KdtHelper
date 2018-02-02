using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;

namespace KdtHelper.Common.Json
{
    [DataContract]
    public class Text
    {
        public Text()
            : this(false)
        { }

        public Text(bool _success, string _msg = "", int _tag = 0, string _map = "")
        {
            success = _success;
            msg = _msg;
            this.Tag = _tag;
            this.map = _map;
            attr = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 读取数据是否成功
        /// </summary>
        [DataMember]
        public bool success { get; set; }

        [DataMember]
        public string msg { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        [DataMember]
        public int Tag { get; set; }

        [DataMember]
        public string map { get; set; }

        /// <summary>
        /// 树节点其他属性值
        /// </summary>
        [DataMember]
        public Dictionary<string, string> attr { get; set; }

        public void SetValue<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            PropertyInfo[] properties = typeof(T).GetProperties();

            properties.ToList().ForEach(property =>
            {
                object propertyValue = property.GetValue(entity, null);

                switch (property.Name.Trim().ToLower())
                {
                    case "success": success = propertyValue.Convert(false); break;
                    case "msg": msg = propertyValue.Convert(""); break;
                    default: attr[property.Name] = propertyValue.Convert(""); break;
                }
            });
        }

        /// <summary>
        /// 添加其他属性信息
        /// </summary>
        /// <param name="p_Key"></param>
        /// <param name="p_Val"></param>
        public void Attr(string p_Key, string p_Val)
        {
            this.attr[p_Key] = p_Val;
        }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
