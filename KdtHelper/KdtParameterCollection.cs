using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Common;

namespace KdtHelper.Core
{
    /// <summary>
    /// 参数集合
    /// </summary>
    public class KdtParameterCollection : List<KdtParameter>
    {
        /// <summary>
        /// 添加参数（动态生成参数名称)
        /// </summary>
        /// <param name="p_val">参数值</param>
        public void AddParameter(List<object> p_val)
        {
            if (p_val != null && p_val.Count > 0)
            {
                foreach (var item in p_val)
                {
                    this.Add(new KdtParameter("{0}{1}".ToFormat("Key", this.Count), item, this.Count));
                }
            }

        }

        /// <summary>
        /// 添加参数（动态生成参数名称)
        /// </summary>
        /// <param name="p_val">参数值</param>
        /// <param name="p_type">参数输入类型</param>
        public void AddParameter(object p_val, ProcInPutEnum p_type)
        {
            this.Add(new KdtParameter("{0}{1}".ToFormat("Key", this.Count), p_val, p_type, this.Count));
        }

        /// <summary>
        /// 添加参数（指定参数名称)
        /// </summary>
        /// <param name="p_key">参数名称（此处名称都不需要带有@等符号）</param>
        /// <param name="p_val">参数值</param>
        /// <param name="p_type">参数输入类型</param>
        public void AddParameter(string p_key, object p_val, ProcInPutEnum p_type)
        {
            if (this.Exists(key => key.Name.Equals(p_key, StringComparison.OrdinalIgnoreCase)))
                return;

            this.Add(new KdtParameter(p_key, p_val, p_type, this.Count));
        }


        /// <summary>
        /// 读取返回参数值信息
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="p_name">读取参数</param>
        /// <returns>读取返回值</returns>
        public T Get<T>(string p_name)
        {
            return this.Find(p => p.Name.Equals(p_name, StringComparison.OrdinalIgnoreCase)).Value.Convert<T>();
        }

    }
}
