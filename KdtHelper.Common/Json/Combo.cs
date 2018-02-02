using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;

namespace KdtHelper.Common.Json
{
    /// <summary>
    /// COMBO子项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    [DataContract]
    public class ComboItem<T, V>
    {
        public ComboItem()
            : this(default(T), default(V))
        {

        }

        public ComboItem(T p_Key, V p_Val)
        {
            this.key = p_Key;
            this.val = p_Val;
            attr = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 树节点ID
        /// </summary>
        [DataMember]
        public T key { get; set; }

        /// <summary>
        /// 树节点文本
        /// </summary>
        [DataMember]
        public V val { get; set; }

        [DataMember]
        public Dictionary<string, string> attr { get; set; }

        /// <summary>
        /// 添加其他属性信息
        /// </summary>
        /// <param name="p_Key"></param>
        /// <param name="p_Val"></param>
        public void Attr(string p_Key, string p_Val)
        {
            this.attr[p_Key] = p_Val;
        }

        /// <summary>
        /// 添加额外属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void AddAttributes<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            PropertyInfo[] properties = typeof(T).GetProperties();

            properties.ToList().ForEach(property =>
            {
                object propertyValue = property.GetValue(entity, null);

                attr[property.Name] = propertyValue.Convert("");
            });
        }
    }

    public class Combo<T, V> : List<ComboItem<T, V>>
    {
        public Combo() { }

        public ComboItem<T, V> NewItem(T _key, V _val)
        {
            ComboItem<T, V> item = new ComboItem<T, V>();

            item.key = _key;
            item.val = _val;

            return item;
        }

        public override string ToString()
        {
            return this.ToJson();
        }
    }
}
