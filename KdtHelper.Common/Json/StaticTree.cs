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
    public class StaticTree<T> : List<TreeStaticNode<T>>
    {
        /// <summary>
        /// JSON树构造函数
        /// </summary>
        public StaticTree() { }

        /// <summary>
        /// 新建一个数节点
        /// </summary>
        public TreeStaticNode<T> NewNode()
        {
            return new TreeStaticNode<T>();
        }

        /// <summary>
        /// 新建一个数节点
        /// </summary>
        public TreeStaticNode<T> GetNode(T val)
        {
            TreeStaticNode<T> _treenode = this.Find(t => t.id.CompareTo(val));
            if (_treenode != null)
                return _treenode;
            else
                return new TreeStaticNode<T>();
        }

        public TreeStaticNode<T> NewNode<V>(V entity) where V : class
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            TreeStaticNode<T> node = new TreeStaticNode<T>();

            PropertyInfo[] properties = typeof(V).GetProperties();

            properties.ToList().ForEach(property =>
            {
                object propertyValue = property.GetValue(entity, null);

                switch (property.Name.Trim().ToLower())
                {
                    case "id": node.id = propertyValue.Convert<T>(); break;
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
    public class TreeStaticNode<T>
    {
        /// <summary>
        /// Json Tree 转换
        /// </summary>
        internal TreeStaticNode()
        {
            this.id = default(T);
            this.name = "";
            this.isHidden = false;
            this.isParent = false;
            this.iconSkin = "";
            this.attr = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            this.children = new List<TreeStaticNode<T>>();
        }

        /// <summary>
        /// 树节点ID
        /// </summary>
        [DataMember]
        public T id { get; set; }

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
        /// 树节点是否为子节点
        /// </summary>
        [DataMember]
        public bool isParent { get; set; }

        /// <summary>
        /// 随意拖拽
        /// </summary>
        [DataMember]
        public bool open { get; set; }

        /// <summary>
        /// 树节点图片
        /// </summary>
        [DataMember]
        public string iconSkin { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        [DataMember]
        public List<TreeStaticNode<T>> children { get; set; }

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
