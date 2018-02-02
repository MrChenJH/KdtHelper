using System;
using System.Collections.Generic;
using System.Text;

namespace KdtHelper.Core
{
    public class KeyValueCollection : List<KeyValuePaire>
    {
        /// <summary>
        /// 获取或设置列所对应值
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <returns>读取值</returns>
        public new object this[string _key]
        {
            get
            {
                var found = base.Find(kv => kv.key.Equals(_key, StringComparison.OrdinalIgnoreCase));
                if (found != null)
                    return found.val;
                return null;
            }
            set
            {
                var found = base.Find(kv => kv.key.Equals(_key, StringComparison.OrdinalIgnoreCase));
                if (found != null)
                {
                    found.val = value;
                }
                else
                {
                    base.Add(new KeyValuePaire() { key = _key, val = value });
                }
            }
        }

        public bool hasKey(string _key)
        {
            return this.Exists(kv => kv.key.Equals(_key, StringComparison.OrdinalIgnoreCase));
        }
    }
}
