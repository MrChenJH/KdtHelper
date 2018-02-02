using KdtHelper.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using KdtHelper.Core.Adapter;
using System.Reflection;

namespace KdtHelper.Core.ExecuterEx
{
    /// <summary>
    /// 执行扩展
    /// </summary>
    public abstract class KdtExecuterEx : IDisposable
    {
        #region 基础配置

        /// <summary>
        /// 数据库配置信息
        /// </summary>
        protected abstract DbDriverMember Driver { get; }

        /// <summary>
        /// 执行类型
        /// </summary>
        protected abstract CommandType CmdType { get; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// 连接字符串信息
        /// </summary>
        public string CnStr
        {
            get
            {
                return "{0}|{1}|{2}".ToFormat(Driver.Driver, Driver.Prefix, Driver.Server);
            }
        }

        #endregion.

        #region 继承配置设置

        /// <summary>
        /// 创建驱动配置信息
        /// </summary>
        /// <param name="p_DriverName">驱动器名称</param>
        /// <returns></returns>
        protected virtual DbDriverMember CreateDriver(string p_DriverName)
        {
            if (p_DriverName.IsNullOrEmpty()) throw new System.Data.DataException("驱动名称不能为空");

            var dm = JsonConfigurationHelper.Instance.GetAppSettings<DbDriverMember>(p_DriverName);
            // 存在web config配置情况下，获取配置项信息
            if (dm!=null)
            {
                if (dm.Prefix.IsNullOrEmpty())
                {
                    switch (dm.Driver.Convert(DriverType.sqlserver))
                    {
                        case DriverType.oracle: dm.Prefix = ":"; break;
                        default: dm.Prefix = "@"; break;
                    }
                }
                return dm;
            }

            throw new System.Data.DataException("请确定在config中的dbdriver配置项中包含{0}节点！".ToFormat(p_DriverName));
        }

        /// <summary>
        /// 创建驱动配置信息
        /// </summary>
        /// <param name="__dtype">驱动类型</param>
        /// <param name="_server">连接字符串</param>
        /// <param name="_prefix">后缀连接符</param>
        /// <returns></returns>
        protected virtual DbDriverMember CreateDriver(DriverType __dtype, string _server, string _prefix = "@")
        {
            return new DbDriverMember(__dtype.ToString(), _server, _prefix);
        }

        /// <summary>
        /// 加载执行器
        /// </summary>
        /// <param name="p_CmdType">执行器驱动类型</param>
        /// <returns></returns>
        protected virtual KdtAdapter LoadAdapter()
        {
            if (Driver == null) throw new System.Data.DataException("驱动配置未设置！");

            KdtAdapter adapter = null;
            bool _canauto = Driver.Prefix.Trim() == ":";

            switch (Driver.Driver.Convert(DriverType.sqlserver))
            {
                case DriverType.oracle:
                    adapter = new OracleClientAdapter(Driver.Server, CmdType);
                    adapter.BindFlag = new KdtDriver("@", "UPPER({0})", false);
                    break;
                case DriverType.mysql:
                    adapter = new MySqlClientAdapter(Driver.Server, CmdType);
                    adapter.BindFlag = new KdtDriver("@", "{0}", true);
                    break;
                case DriverType.db2:
                    adapter = new DB2ClientAdapter(Driver.Server, CmdType);
                    adapter.BindFlag = new KdtDriver(":",  "{0}", true);
                    break;
                case DriverType.sqlite:
                    adapter = new SQLiteClientAdapter(Driver.Server, CmdType);
                    adapter.BindFlag = new KdtDriver(Driver.Prefix, "{0}", true);
                    break;
                case DriverType.odbc:
                    adapter = new OdbcClientAdapter(Driver.Server, CmdType);
                    adapter.BindFlag = new KdtDriver(Driver.Prefix, "{0}", true);
                    break;
                //case DriverType.sybase:
                //    adapter = new SyBaseClientAdapter(Driver.Server, CmdType);
                //    adapter.BindFlag = new KdtDriver(Driver.Prefix,  "{0}", true);
                //    break;
                default:
                    adapter = new SqlClientAdapter(Driver.Server, CmdType);
                    adapter.BindFlag = new KdtDriver(Driver.Prefix, "{0}", true);
                    break;
            }
            if (TimeOut > 0)
                adapter.CommandTimeOut = TimeOut;

            return adapter;
        }

        #endregion.

        #region 执行方法

        /// <summary>
        /// 执行添加数据操作
        /// </summary>
        /// <param name="_option">操作实体类</param>
        /// <param name="P_IgnoreCase">是否忽略大小写</param>
        public virtual void Add(KdtFieldEntityEx _option, bool P_IgnoreCase = false)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                bool istran, isincr;
                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Add(out istran, out isincr, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                if (istran) // 执行回滚方法
                {
                    adapter.BeginTrans();
                    try
                    {
                        if (isincr)
                        {
                            var vals = adapter.ExecuteKVCollection(sqltext, props);
                            _option.Affected = vals.Count;
                            if (vals != null && vals.Count > 0)
                            {
                                foreach (var val in vals)
                                {
                                    if (_option.IncrVal.ContainsKey(val.key.TrimStart('p')))
                                    {
                                        _option.IncrVal[val.key.TrimStart('p')] = val.val.Convert(0);
                                    }
                                }
                            }
                        }
                        else
                            _option.Affected = adapter.ExecuteNoQuery(sqltext, props);
                        adapter.CommitTrans();
                    }
                    catch (Exception ex)
                    {
                        // 写日志
                        KdtLoger.Instance.Error(ex);
                        adapter.RollbackTrans();
                    }
                }
                else // 执行非回滚数据
                {
                    if (isincr)
                    {
                        var vals = adapter.ExecuteKVCollection(sqltext, props);
                        _option.Affected = vals.Count;
                        if (vals != null && vals.Count > 0)
                        {
                            foreach (var val in vals)
                            {
                                if (_option.IncrVal.ContainsKey(val.key.TrimStart('p')))
                                {
                                    _option.IncrVal[val.key.TrimStart('p')] = val.val.Convert(0);
                                }
                            }
                        }
                    }
                    else
                        _option.Affected = adapter.ExecuteNoQuery(sqltext, props);
                }
            }
        }

        /// <summary>
        /// 执行更新数据操作
        /// </summary>
        /// <param name="_option">操作实体类</param>
        /// <param name="P_IgnoreCase">是否忽略大小写</param>
        public virtual void Update(KdtFieldEntityEx _option, bool P_IgnoreCase = false)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                bool istran;
                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Update(out istran, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                if (istran) // 执行回滚方法
                {
                    adapter.BeginTrans();
                    try
                    {
                        _option.Affected = adapter.ExecuteNoQuery(sqltext, props);
                        adapter.CommitTrans();
                    }
                    catch (Exception ex)
                    {
                        // 写日志
                        KdtLoger.Instance.Error(ex);
                        adapter.RollbackTrans();
                    }
                }
                else // 执行非回滚数据
                {
                    _option.Affected = adapter.ExecuteNoQuery(sqltext, props);
                }
            }
        }

        /// <summary>
        /// 执行添加或更新数据操作(该方方法目前只支持SQLSERVER,ORACLE数据库）
        /// </summary>
        /// <param name="_option">操作实体类</param>
        /// <param name="P_IgnoreCase">是否忽略大小写</param>
        public virtual void AddOrUpdate(KdtFieldEntityEx _option, bool P_IgnoreCase = false)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                bool istran;
                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                if (adapter is MySqlClientAdapter)
                {
                    // 执行查询是否存在
                    string esql = _option.GetMainTableExist();
                    if (adapter.ExecuteScalar<bool>(esql))
                    {
                        Update(_option, P_IgnoreCase);
                    }
                    else
                    {
                        Add(_option, P_IgnoreCase);
                    }
                }
                else
                {
                    string sqltext = _option.AddOrUpdate(out istran, out props);
                    if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                    if (istran) // 执行回滚方法
                    {
                        adapter.BeginTrans();
                        try
                        {
                            var vals = adapter.ExecuteKVCollection(sqltext, props);
                            if (vals != null && vals.Count > 0)
                            {
                                foreach (var val in vals)
                                {
                                    if (_option.IncrVal.ContainsKey(val.key.TrimStart('p')))
                                    {
                                        _option.IncrVal[val.key.TrimStart('p')] = val.val.Convert(0);
                                    }
                                }
                            }
                            adapter.CommitTrans();
                        }
                        catch (Exception ex)
                        {
                            // 写日志
                            KdtLoger.Instance.Error(ex);
                            adapter.RollbackTrans();
                        }
                    }
                    else // 执行非回滚数据
                    {
                        var vals = adapter.ExecuteKVCollection(sqltext, props);
                        if (vals != null && vals.Count > 0)
                        {
                            _option.Affected = vals.Count;
                            foreach (var val in vals)
                            {
                                if (_option.IncrVal.ContainsKey(val.key.TrimStart('p')))
                                {
                                    _option.IncrVal[val.key.TrimStart('p')] = val.val.Convert(0);
                                }
                            }
                        }
                        else
                            _option.Affected = adapter.ExecuteNoQuery(sqltext, props);
                    }
                }
            }
        }

        /// <summary>
        /// 执行删除数据操作
        /// </summary>
        /// <param name="_option">操作实体类</param>
        /// <param name="P_IgnoreCase">是否忽略大小写</param>
        public virtual void Delete(KdtFieldEntityEx _option, bool P_IgnoreCase = false)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                bool istran;
                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Delete(out istran, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                if (istran) // 执行回滚方法
                {
                    adapter.BeginTrans();
                    try
                    {
                        _option.Affected = adapter.ExecuteNoQuery(sqltext, props);
                        adapter.CommitTrans();
                    }
                    catch (Exception ex)
                    {
                        // 写日志
                        KdtLoger.Instance.Error(ex);
                        adapter.RollbackTrans();
                    }
                }
                else // 执行非回滚数据
                {
                    _option.Affected = adapter.ExecuteNoQuery(sqltext, props);
                }
            }
        }

        /// <summary>
        /// 回滚，无返回值执行方法
        /// </summary>
        /// <param name="_option"></param>
        /// <param name="_selecttype"></param>
        /// <param name="P_IgnoreCase"></param>
        public virtual void TransExecute(KdtFieldEntityEx _option, string _selecttype, bool P_IgnoreCase = false)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Select(_selecttype, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                adapter.BeginTrans();
                try
                {
                    _option.Affected = adapter.ExecuteNoQuery(sqltext, props);
                    adapter.CommitTrans();
                }
                catch (Exception ex)
                {
                    // 写日志
                    KdtLoger.Instance.Error(ex);
                    adapter.RollbackTrans();
                }
            }
        }

        /// <summary>
        /// 回滚，执行sql事务语句
        /// </summary>
        /// <param name="_sqltext"></param>
        /// <param name="P_IgnoreCase"></param>
        public virtual int TransExecuteSql(string _sqltext, bool P_IgnoreCase = false)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法
                int count = 0;
                KdtParameterCollection props;
                if (string.IsNullOrEmpty(_sqltext)) throw new DataException("执行的SQL语句为空！");

                adapter.BeginTrans();
                try
                {
                    count += adapter.ExecuteNoQuery(_sqltext);
                    adapter.CommitTrans();
                }
                catch (Exception ex)
                {
                    // 写日志
                    KdtLoger.Instance.Error(ex);
                    adapter.RollbackTrans();
                }
                return count;
            }
        }

        /// <summary>
        /// 执行数据库SQL语句
        /// </summary>
        /// <param name="_sqltext"></param>
        public virtual void ExecuteNonQuery(string _sqltext, KdtParameterCollection parameters)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                adapter.ExecuteNoQuery(_sqltext, parameters);
            }
        }

        /// <summary>
        /// 执行数据库SQL语句
        /// </summary>
        /// <param name="_sqltext"></param>
        public virtual List<string> ExecuteQuery(string _sqltext)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
             return   adapter.ExecuteQuery(_sqltext, null);
            }
        }
        #endregion.

        #region 查询方法

        /// <summary>
        /// 查询数据集Dataset集合
        /// </summary>
        /// <param name="_option">操作类型</param>
        /// <param name="_selecttype">查询类型</param>
        /// <param name="P_IgnoreCase">是否大小写</param>
        /// <returns>Dataset集合</returns>
        public virtual T SelectField<T>(KdtFieldEntityEx _option, string _selecttype, bool P_IgnoreCase = false)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Select(_selecttype, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                return adapter.ExecuteScalar<T>(sqltext, props);
            }
        }

        /// <summary>
        /// 查询数据实体类
        /// </summary>
        /// <param name="_option">操作类型</param>
        /// <param name="_selecttype">查询类型</param>
        /// <param name="P_IgnoreCase">是否大小写</param>
        /// <returns>Dataset集合</returns>
        public virtual T SelectEntity<T>(KdtFieldEntityEx _option, string _selecttype, bool P_IgnoreCase = false)
            where T : class, new()
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Select(_selecttype, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                return adapter.ExecuteQueryEntity<T>(sqltext, props);
            }
        }

        /// <summary>
        /// 查询数据集Dataset集合
        /// </summary>
        /// <param name="_option">操作类型</param>
        /// <param name="_selecttype">查询类型</param>
        /// <param name="P_IgnoreCase">是否大小写</param>
        /// <returns>Dataset集合</returns>
        public virtual KeyValueCollection SelectKVCollection(KdtFieldEntityEx _option, string _selecttype, bool P_IgnoreCase = false)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Select(_selecttype, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                return adapter.ExecuteKVCollection(sqltext, props);
            }
        }

        /// <summary>
        /// 查询数据实体类集合
        /// </summary>
        /// <param name="_option">操作类型</param>
        /// <param name="_selecttype">查询类型</param>
        /// <param name="P_IgnoreCase">是否大小写</param>
        /// <returns>Dataset集合</returns>
        public virtual List<T> SelectList<T>(KdtFieldEntityEx _option, string _selecttype, bool P_IgnoreCase = false)
            where T : class, new()
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Select(_selecttype, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                return adapter.ExecuteQuery<T>(sqltext, props);
            }
        }

        public virtual List<KeyValueCollection> SelectKVCollectionList(KdtFieldEntityEx _option, string _selecttype, bool P_IgnoreCase = false)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Select(_selecttype, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                return adapter.ExecuteQueryKVCollection(sqltext, props);
            }
        }

        /// <summary>
        /// 查询数据集Dataset集合
        /// </summary>
        /// <param name="_option">操作类型</param>
        /// <param name="_selecttype">查询类型</param>
        /// <param name="P_IgnoreCase">是否大小写</param>
        /// <returns>Dataset集合</returns>
        public virtual List<T> SelectPage<T>(KdtFieldEntityEx _option, KdtPageEx _selecttype, bool P_IgnoreCase = false)
            where T : class, new()
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Select(_selecttype.selpagetotal, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的查询统计总数语句为空！");
                int total = adapter.ExecuteScalar<int>(sqltext, props);
                _selecttype.total = total;

                if (total > 0)
                {
                    //adapter.Open(); // 打开执行数据库
                    sqltext = _option.Select(_selecttype.selpage, out props);
                    if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的查询分页语句为空！");
                    props.AddParameter("start", _selecttype.start, ProcInPutEnum.InPut);
                    props.AddParameter("end", _selecttype.end, ProcInPutEnum.InPut);
                    return adapter.ExecuteQuery<T>(sqltext, props);
                }
                return new List<T>();
            }
        }

        public virtual List<T> SelectPage<T>(KdtFieldEntityEx _option, string _selecttype, int offset, int limit, bool P_IgnoreCase = false) 
            where T : class, new()
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Select(_selecttype, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                return adapter.ExecuteQueryPage<T>(sqltext, props,offset,limit);
            }
        }

        public virtual List<KeyValueCollection> SelectKVCollectionPage(KdtFieldEntityEx _option, string _selecttype, int offset, int limit, bool P_IgnoreCase = false)
        {
            using (var adapter = LoadAdapter())
            {
                adapter.Open(); // 打开执行数据库
                if (!P_IgnoreCase) adapter.BindFlag.script = "{0}"; // 消除插入数据执行T-SQL语法

                KdtParameterCollection props;
                _option.SetAdapter(Driver);
                string sqltext = _option.Select(_selecttype, out props);
                if (string.IsNullOrEmpty(sqltext)) throw new DataException("执行的SQL语句为空！");

                return adapter.ExecuteQueryPageKVCollection(sqltext, props, offset, limit);
            }
        }

        #endregion.

        #region 实现IDisposable方法

        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion.
    }
}
