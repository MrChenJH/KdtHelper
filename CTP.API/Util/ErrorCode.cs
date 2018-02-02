using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KdtHelper.Common;

namespace CTP.API.Util
{
    #region 代码级异常错误(3000开头）

    /// <summary>
    /// 回调函数为空
    /// </summary>
    public class NullCallback : SystemException
    {
        /// <summary>
        /// 用户离线错误码
        /// </summary>
        /// <param name="msg"></param>
        public NullCallback() : base("回调函数为空") { }
    }

    /// <summary>
    /// 传入参数为空
    /// </summary>
    public class NullParam : SystemException
    {
        /// <summary>
        /// 用户离线错误码
        /// </summary>
        public NullParam() : base("传入参数为空") { }

        /// <summary>
        /// 单个参数名称
        /// </summary>
        /// <param name="name">参数名称</param>
        public NullParam(string name) : base("{0}传入参数为空".ToFormat(name)) { }
    }

    #endregion.

    #region 页面级异常处理(4000开头）

    #endregion.

    #region 系统运行级异常处理(5000开头）

    /// <summary>
    /// 用户离线错误码
    /// </summary>
    public class OffLine : SystemException
    {
        /// <summary>
        /// 用户离线错误码
        /// </summary>
        public OffLine() : base("用户处于离线状态") { }
    }

    /// <summary>
    /// 不存在的内容
    /// </summary>
    public class NotExist : SystemException
    {
        /// <summary>
        /// 不存在的内容
        /// </summary>
        public NotExist() : base("不存在的内容") { }

        /// <summary>
        /// 指定参数不存在
        /// </summary>
        /// <param name="name"></param>
        public NotExist(string name) : base("{0}不存在".ToFormat(name)) { }
    }

    /// <summary>
    /// 数据库添加、更新、删除操作失败
    /// </summary>
    public class DbExecuteException : SystemException
    {
        /// <summary>
        /// 用户离线错误码
        /// </summary>
        public DbExecuteException() : base("数据库添加、更新、删除操作失败") { }
    }

    #endregion.



}
