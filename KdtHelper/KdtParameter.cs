using System;
using System.Collections.Generic;
using System.Text;

namespace KdtHelper.Core
{
    /// <summary>
    /// 参数类
    /// </summary>
    public class KdtParameter
    {
        /// <summary>
        /// 构造参数类型
        /// </summary>
        /// <param name="_pkey">参数键</param>
        /// <param name="_pvalue">参数值</param>
        internal KdtParameter(string _pkey, object _pvalue, int _index)
            : this(_pkey, _pvalue, ProcInPutEnum.InPut, _index)
        {
        }

        /// <summary>
        /// 构造参数类型
        /// </summary>
        /// <param name="_pkey">参数键</param>
        /// <param name="_pvalue">参数值</param>
        /// <param name="_ptype">输入方式</param>
        internal KdtParameter(string _pkey, object _pvalue, ProcInPutEnum _ptype, int _index)
        {
            this.Name = _pkey;
            this._InPutType = _ptype;
            this.Value = _pvalue;
            this.Idx = _index;
        }

        /// <summary>
        /// 参数键值
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 参数取代位置
        /// </summary>
        public int Idx { get; private set; }

        /// <summary>
        /// 输入方式
        /// </summary>
        public ProcInPutEnum _InPutType { get; private set; }
    }
}
