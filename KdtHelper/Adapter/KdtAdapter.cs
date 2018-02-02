using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Common;
using System.Data.SqlClient;
using System.Data;

namespace KdtHelper.Core.Adapter
{
    /// <summary>
    /// 异步回调清除数据库连接及事务
    /// </summary>
    /// <typeparam name="adtapter">KdtAdapter类型</typeparam>
    internal delegate void AsyncDispose(KdtAdapter adtapter);

    /// <summary>
    /// 数据读取器
    /// </summary>
    public abstract class KdtAdapter : IDisposable
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="_connectionStr">数据库连接字符串</param>
        /// <param name="_cmdtype">执行语句类型</param>
        public KdtAdapter(string _connectionStr, CommandType _cmdtype)
        {
            this.ConnectionStr = _connectionStr;
            this.CmdType = _cmdtype;
            isClosed = true;
        }

        #region 私有属性

        /// <summary>
        /// 是否处于关闭状态
        /// </summary>
        protected bool isClosed { get; set; }

        /// <summary>
        /// 是否处于异步执行状态
        /// </summary>
        protected bool IsAsync { get; set; }


        #endregion.

        #region 公开属性

        /// <summary>
        /// 执行SQL语法类型
        /// </summary>
        public CommandType CmdType { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionStr { get; set; }

        /// <summary>
        /// 最终Command执行T-SQL
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        /// 启动器默认绑定设置
        /// </summary>
        public KdtDriver BindFlag { get; set; }

        /// <summary>
        /// 执行语句过时时间
        /// 为Null时采用默认，
        /// 为0是表示无限制，
        /// 为其他大于0时，单位为分钟。
        /// </summary>
        public int? CommandTimeOut { get; set; }

        /// <summary>
        /// 连接是否打开状态
        /// </summary>
        public abstract bool IsOpened { get; }

        /// <summary>
        /// 参数前缀
        /// </summary>
        public abstract string ParamPrifix { get; }

        #endregion.

        #region 私有继承类方法

        /// <summary>
        /// 替换SQL字符串信息值
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>带参数SQL语句</returns>
        protected virtual string ReplaceSqlText(string sql, KdtParameterCollection parameters)
        {
            if (parameters == null || parameters.Count <= 0) return sql;

            string[] orderParmas = new string[parameters.Count];

            foreach (var item in parameters)
            {
                orderParmas[item.Idx] = BindFlag.script.ToFormat("{0}{1}".ToFormat(ParamPrifix, item.Name));
            }

            return sql.ToFormat(orderParmas);
        }

        /// <summary>
        /// 获取表中自增列的值
        /// </summary>
        /// <param name="_tablename">表名</param>
        /// <param name="_filedname">自增的字段名</param>
        /// <returns>自增ID</returns>
        internal protected abstract int QueryAutoNo(string _tablename, string _filedname);

        #endregion.

        #region 对外方法

        /// <summary>
        /// 启动一个事务
        /// </summary>
        public abstract void BeginTrans();

        /// <summary>
        /// 提交一个事务
        /// </summary>
        public abstract void CommitTrans();

        /// <summary>
        /// 回滚一个事务
        /// </summary>
        public abstract void RollbackTrans();

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public abstract void Open();

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public abstract void Close();

        #endregion.

        #region 执行非查询SQL语句方法

        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>受影响的记录数</returns>
        public virtual int ExecuteNoQuery(string sql)
        {
            return ExecuteNoQuery(sql, null);
        }

        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>受影响的记录数</returns>
        public abstract int ExecuteNoQuery(string sql, KdtParameterCollection parameters);

        public virtual List<string> ExecuteQuery(string sql, KdtParameterCollection parameters){
            return  null ;
        }

        #endregion.

        #region 执行非查询SQL语句方法

        /// <summary>
        /// 执行查询,返回查询结果的第一行第一列
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sql">SQL查询语句</param>
        /// <returns>查询结果的第一行第一列</returns>
        public virtual T ExecuteScalar<T>(string sql)
        {
            return ExecuteScalar<T>(sql, null);
        }

        /// <summary>
        /// 执行查询,返回查询结果的第一行第一列
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>查询结果的第一行第一列</returns>
        public abstract T ExecuteScalar<T>(string sql, KdtParameterCollection parameters);

  

        #endregion.

        #region 执行SQL查询方法

        /// <summary>
        /// 执行SQL查询,返回数据集合
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns>包含查询结果的行集合</returns>
        public virtual T ExecuteQueryEntity<T>(string sql) where T: class, new()
        {
            return ExecuteQueryEntity<T>(sql, null);
        }

        /// <summary>
        ///  执行SQL查询,返回数据集合
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>包含查询结果的集合</returns>
        public abstract T ExecuteQueryEntity<T>(string sql, KdtParameterCollection parameters) where T : class, new();

        public virtual KeyValueCollection ExecuteKVCollection(string sql)
        {
            return ExecuteKVCollection(sql, null);
        }

        public abstract KeyValueCollection ExecuteKVCollection(string sql, KdtParameterCollection parameters);

        /// <summary>
        /// 执行SQL查询,返回数据集合
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns>包含查询结果的行集合</returns>
        public virtual List<T> ExecuteQuery<T>(string sql) where T : class, new()
        {
            return ExecuteQuery<T>(sql, null);
        }

        /// <summary>
        ///  执行SQL查询,返回数据集合
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>包含查询结果的集合</returns>
        public abstract List<T> ExecuteQuery<T>(string sql, KdtParameterCollection parameters) where T : class, new();

        public virtual List<KeyValueCollection> ExecuteQueryKVCollection(string sql)
        {
            return ExecuteQueryKVCollection(sql, null);
        }
        public abstract List<KeyValueCollection> ExecuteQueryKVCollection(string sql, KdtParameterCollection parameters);

        /// <summary>
        /// 执行分页查询（采用DataReader进行分页查询）
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="offset">读取偏移量</param>
        /// <param name="limit">读取最大值</param>
        /// <returns>包含查询结果集合</returns>
        public abstract List<T> ExecuteQueryPage<T>(string sql, KdtParameterCollection parameters, int offset, int limit) where T : class, new();

        public abstract List<KeyValueCollection> ExecuteQueryPageKVCollection(string sql, KdtParameterCollection parameters, int offset, int limit);

        #endregion.

        #region 实现IDisposable方法

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion.
    }
}
