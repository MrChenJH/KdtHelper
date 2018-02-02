using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Linq;

namespace KdtHelper.Common.Json
{
    /// <summary>
    /// JSON树
    /// </summary>
    public class Tree<V> : List<TreeNode<V>>
    {
        /// <summary>
        /// JSON树构造函数
        /// </summary>
        public Tree() { }

        /// <summary>
        /// 新建一个数节点
        /// </summary>
        public TreeNode<V> NewNode()
        {
            return new TreeNode<V>();
        }

        public TreeNode<V> NewNode<T>(T entity) where T : class
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            TreeNode<V> node = new TreeNode<V>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            properties.ToList().ForEach(property =>
            {
                object propertyValue = property.GetValue(entity, null);

                switch (property.Name.Trim().ToLower())
                {
                    case "id": node.id = propertyValue.Convert(default(V)); break;
                    case "name": node.name = propertyValue.Convert(""); break;
                    case "pid": node.pId = propertyValue.Convert(""); break;
                    case "hide": node.isHidden = propertyValue.Convert(false); break;
                    case "leaf": node.isParent = !propertyValue.Convert(false); break;
                    case "icon": node.iconSkin = propertyValue.Convert(""); break;
                    default:
                        break;
                }
            });

            return node;
        }

        /// <summary>
        /// 返回JSON树字符串
        /// </summary>
        /// <returns>JSON字符串</returns>
        public override string ToString()
        {
            return this.ToJson();
        }
    }

    /// <summary>
    /// JSON Tree String
    /// </summary>
    [DataContract]
    public class TreeNode<V>
    {
        /// <summary>
        /// Json Tree 转换
        /// </summary>
        internal TreeNode()
        {
            this.id = default(V);
            this.name = "";
            this.isHidden = false;
            this.isParent = false;
            this.iconSkin = "";
            this.attr = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 树节点ID
        /// </summary>
        [DataMember]
        public V id { get; set; }

        /// <summary>
        /// 树节点文本
        /// </summary>
        [DataMember]
        public string name { get; set; }

        /// <summary>
        /// 树节点文本
        /// </summary>
        [DataMember]
        public string pId { get; set; }

        /// <summary>
        /// 树节点是否展开
        /// </summary>
        [DataMember]
        public bool isHidden { get; set; }

        /// <summary>
        /// 随意拖拽
        /// </summary>
        [DataMember]
        public bool open { get; set; }

        /// <summary>
        /// 树节点是否为子节点
        /// </summary>
        [DataMember]
        public bool isParent { get; set; }

        /// <summary>
        /// 树节点图片
        /// </summary>
        [DataMember]
        public string iconSkin { get; set; }

        /// <summary>
        /// 树节点其他属性值
        /// </summary>
        [DataMember]
        public Dictionary<string, string> attr { get; set; }

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
}
