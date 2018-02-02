using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KdtHelper.Core
{
    /// <summary>
    /// 集合抽象类
    /// </summary>
    public abstract class KdtCollection : IEnumerable, IEnumerator
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public KdtCollection()
        {
            this.iIndex = -1;
        }

        #endregion.

        #region 实现集合接口

        /// <summary>
        /// 定义索引
        /// </summary>
        protected int iIndex { get; set; }

        /// <summary>
        /// 集合信息
        /// </summary>
        protected dynamic Data { get; set; }

        /// <summary>
        /// 查询个数
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public dynamic this[int idx]
        {
            get
            {
                if (Data != null && Data.Count > 0)
                    return Data[idx];

                return null;
            }
        }

        /// <summary>
        /// 获取数据中指定数据
        /// </summary>
        /// <param name="itemid">编号</param>
        /// <returns></returns>
        public abstract dynamic getItem(int itemid);

        /// <summary>
        /// 实现IEnumerator接口的Current属性，返回一个自定义的point结构，即point数组的第index元素
        /// </summary>
        public object Current
        {
            get { return Data[iIndex]; }
        }

        /// <summary>
        /// 实现IEnumerator接口的MoveNext方法，用于向前访问集合元素，如果超出集合范围，返回false
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            iIndex++;
            return iIndex >= Data.Count ? false : true;
        }

        /// <summary>
        /// 实现IEnumerable接口的GetEnumerator方法，返回一个IEnumerator，这里返回我们的自定义类，因为要对这个类的对象进行迭代
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        /// <summary>
        /// 获取集合所有数据
        /// </summary>
        /// <returns></returns>
        public dynamic GetAll()
        {
            return Data;
        }

        /// <summary>
        /// 实现IEnumerator接口的Reset方法，将集合索引置于第一个元素之前
        /// </summary>
        public void Reset()
        {
            iIndex = -1;
        }

        #endregion.
    }
}
