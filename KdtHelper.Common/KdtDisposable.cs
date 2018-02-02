using System;
using System.Collections.Generic;
using System.Text;

namespace KdtHelper.Common
{
    /// <summary>
    /// 实现IDisposable
    /// </summary>
    public abstract class KdtDisposable : IDisposable
    {
        public KdtDisposable() { }


        #region 实现IDisposable方法

        /// <summary>
        /// 实现GC
        /// </summary>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion.
    }
}
