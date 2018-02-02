using KdtHelper.Common;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Threading;

namespace KdtHelper.Core.Adapter
{
    /// <summary>
    /// MYSQL 数据库封装方法
    /// </summary>
    internal class MySqlClientAdapter : KdtAdapter
    {
        #region 构造函数

        public MySqlClientAdapter()
            : base("", CommandType.Text)
        {
        }

        public MySqlClientAdapter(string _connectionStr)
            : base(_connectionStr, CommandType.Text)
        { }

        public MySqlClientAdapter(string _connectionStr, CommandType _cmdtype)
            : base(_connectionStr, _cmdtype)
        { }

        #endregion.

        #region 私有属性

        /// <summary>
        /// 回滚方法
        /// </summary>
        private MySqlTransaction _mySqlTran { get; set; }

        /// <summary>
        /// Sql打开连接执行器
        /// </summary>
        private MySqlConnection _mySqlCn { get; set; }

        private delegate void AsyncExceReader(MySqlCommand cmd);

        #endregion.

        #region 继承属性

        /// <summary>
        /// 数据库连接是否打开状态
        /// </summary>
        public override bool IsOpened
        {
            get
            {
                if (_mySqlCn == null) return false;

                return !(_mySqlCn.State == ConnectionState.Broken || _mySqlCn.State == ConnectionState.Closed);
            }
        }

        /// <summary>
        /// 数据库参数前缀，SQL默认为@符合
        /// </summary>
        public override string ParamPrifix
        {
            get { return "@"; }
        }

        #endregion.

        #region 连接字符串基类抽象方法实现

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public override void Open()
        {
            try
            {
                if (_mySqlCn != null && _mySqlCn.State == ConnectionState.Open)
                {
                    _mySqlCn.Close();
                }
                if (_mySqlCn == null)
                {
                    if (string.IsNullOrEmpty(ConnectionStr))
                        throw new DataException("数据库连接字符串不能为空！");
                    _mySqlCn = new MySqlConnection(ConnectionStr);
                    _mySqlCn.Open();
                    isClosed = false; // 加入这一句修正数据库不能重复打开和关闭的问题!
                }
                if (_mySqlCn.State == ConnectionState.Closed)
                {
                    _mySqlCn.Open();
                }
            }
            catch (Exception e)
            {
                throw new DataException(e.Message);
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public override void Close()
        {
            if (!isClosed)
            {
                if (IsAsync)
                {
                    AsyncDispose dispose = new AsyncDispose(delegate (KdtAdapter adapter)
                    {
                        var mysqladapter = adapter as MySqlClientAdapter;
                        while (mysqladapter.IsAsync) { Thread.Sleep(1000); }

                        if (mysqladapter._mySqlTran != null)
                            mysqladapter._mySqlTran.Rollback();

                        if (mysqladapter._mySqlCn != null)
                        {
                            mysqladapter._mySqlCn.Close();
                            mysqladapter._mySqlCn.Dispose();
                        }

                        GC.SuppressFinalize(mysqladapter);
                    });
                    dispose.BeginInvoke(this, delegate (IAsyncResult result)
                    {
                        var async = result.AsyncState as AsyncDispose;
                        async.EndInvoke(result);
                    }, dispose);
                }
                else
                {
                    if (_mySqlTran != null)
                        _mySqlTran.Rollback();

                    _mySqlCn.Close();
                    _mySqlCn.Dispose();

                    base.Dispose();
                }
            }
            isClosed = true;
        }

        /// <summary>
        /// 实现IDispose方法
        /// </summary>
        public override void Dispose()
        {
            Close();
        }

        #endregion.

        #region 执行语句

        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>受影响的记录数</returns>
        public override int ExecuteNoQuery(string sql, KdtParameterCollection parameters)
        {
            try
            {
                int effected = 0;

                // 执行SQL命令
                using (MySqlCommand cmd = new MySqlCommand(ReplaceSqlText(sql, parameters), _mySqlCn))
                {
                    InitCommand(cmd); // 初始化

                    // 赋值参数
                    var hasConvertParams = ConvertToSqlParameter(parameters);
                    foreach (var item in hasConvertParams)
                    {
                        cmd.Parameters.Add(item.Value);
                    }

                    effected = cmd.ExecuteNonQuery();

                    cmd.Cancel();
                    cmd.Dispose();
                }

                return effected;
            }
            catch (Exception ex)
            {
                KdtLoger.Instance.Error(ex);
                throw new DataException(string.Format("执行非查询SQL语句错误,原因为:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mysqlParameters"></param>
        /// <returns></returns>
        public override List<string> ExecuteQuery(string sql, KdtParameterCollection parameters)
        {
            var strdata = new List<string>();
            using (MySqlConnection connection = new MySqlConnection(ConnectionStr))
            {
                connection.Open();
                try
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 6000;
                    var r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        var objstr = new List<string>();
                        for (int i = 0; i < r.FieldCount; i++)
                        {
                            string fildName = r.GetName(i).Trim();
                            string value = Convert.ToString(r[fildName]);
                            string valueString = string.Empty;
                            var limit = 30000;
                            var num = (value.Length / limit);
                            if (value.Length > 32766)
                            {
                                for (var k = 0; k <= num; k++)
                                {
                                    if (k == num)
                                    {
                                        valueString += Uri.EscapeUriString(value.ToString().Substring(limit * k, value.Length - limit * k));
                                    }
                                    else
                                    {
                                        valueString += Uri.EscapeUriString(value.ToString().Substring(limit * k, limit));
                                    }
                                }
                            }
                            else
                            {

                                valueString = Uri.EscapeUriString(value);
                            }
                            objstr.Add(String.Format("\"{0}\":\"{1}\"", Uri.EscapeUriString(fildName), valueString));

                        }

                        string str = string.Format("{0}{1}{2}", "{", string.Join(",", objstr), "}");
                        strdata.Add(str);
                    }
                    return strdata;
                }
                catch (Exception ex)
                {

                    KdtLoger.Instance.Error(ex);
                    throw new DataException(string.Format("执行非查询SQL语句错误,原因为:{0}", ex.Message));
                }
                finally {
                    connection.Close();
                    connection.Dispose();
                }
            }

        }

        /// <summary>
        /// 执行查询,返回查询结果的第一行第一列
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>查询结果的第一行第一列</returns>
        public override T ExecuteScalar<T>(string sql, KdtParameterCollection parameters)
        {
            try
            {
                T value = default(T);

                // 执行SQL命令
                using (MySqlCommand cmd = new MySqlCommand(ReplaceSqlText(ReplaceSqlText(sql, parameters), parameters), _mySqlCn))
                {
                    InitCommand(cmd); // 初始化

                    // 赋值参数
                    var hasConvertParams = ConvertToSqlParameter(parameters);
                    foreach (var item in hasConvertParams)
                    {
                        cmd.Parameters.Add(item.Value);
                    }

                    value = cmd.ExecuteScalar().Convert<T>();

                    cmd.Cancel();
                    cmd.Dispose();
                }

                return value;
            }
            catch (Exception ex)
            {
                KdtLoger.Instance.Error(ex);
                throw new DataException(string.Format("执行查询,返回查询结果的第一行第一列错误,原因为:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 执行SQL查询,返回数据集合
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>包含查询结果的行集合</returns>
        public override T ExecuteQueryEntity<T>(string sql, KdtParameterCollection parameters)
        {
            try
            {
                T entity = typeof(T).Create<T>();
                // 执行SQL命令
                using (MySqlCommand cmd = new MySqlCommand(ReplaceSqlText(sql, parameters), _mySqlCn))
                {
                    InitCommand(cmd); // 初始化

                    // 赋值参数
                    var hasConvertParams = ConvertToSqlParameter(parameters);
                    foreach (var item in hasConvertParams)
                    {
                        cmd.Parameters.Add(item.Value);
                    }

                    // 执行填充数据
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        entity = GetEntity<T>(reader);
                        // 反射参数值
                        ReflectParamValue(parameters, hasConvertParams);

                        reader.Close();
                        reader.Dispose();
                    }

                    cmd.Cancel();
                    cmd.Dispose();
                }

                return entity;
            }
            catch (MySqlException me)
            {
                KdtLoger.Instance.Error(me);
                throw new DataException(me.Message);
            }
            catch (Exception ex)
            {
                KdtLoger.Instance.Error(ex);
                throw new DataException(string.Format("执行SQL查询,返回数据集合错误,原因为:{0}", ex.Message));
            }
        }

        /// <summary>
        /// 读取KEY VALUE值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override KeyValueCollection ExecuteKVCollection(string sql, KdtParameterCollection parameters)
        {
            try
            {
                KeyValueCollection entity = new KeyValueCollection();
                // 执行SQL命令
                using (MySqlCommand cmd = new MySqlCommand(ReplaceSqlText(sql, parameters), _mySqlCn))
                {
                    InitCommand(cmd); // 初始化

                    // 赋值参数
                    var hasConvertParams = ConvertToSqlParameter(parameters);
                    foreach (var item in hasConvertParams)
                    {
                        cmd.Parameters.Add(item.Value);
                    }

                    // 执行填充数据
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        entity = GetEntity(reader);
                        // 反射参数值
                        ReflectParamValue(parameters, hasConvertParams);

                        reader.Close();
                        reader.Dispose();
                    }

                    cmd.Cancel();
                    cmd.Dispose();
                }

                return entity;
            }
            catch (MySqlException me)
            {
                KdtLoger.Instance.Error(me);
                throw new DataException(me.Message);
            }
            catch (Exception ex)
            {
                KdtLoger.Instance.Error(ex);
                throw new DataException(string.Format("执行SQL查询,返回数据集合错误,原因为:{0}", ex.Message));
            }
        }



        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override List<T> ExecuteQuery<T>(string sql, KdtParameterCollection parameters)
        {
            try
            {
                List<T> entitys = new List<T>();
                // 执行SQL命令
                using (MySqlCommand cmd = new MySqlCommand(ReplaceSqlText(sql, parameters), _mySqlCn))
                {
                    InitCommand(cmd); // 初始化

                    // 赋值参数
                    var hasConvertParams = ConvertToSqlParameter(parameters);
                    foreach (var item in hasConvertParams)
                    {
                        cmd.Parameters.Add(item.Value);
                    }

                    // 执行填充数据
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        entitys = GetEntityCollection<T>(reader);
                        // 反射参数值
                        ReflectParamValue(parameters, hasConvertParams);

                        reader.Close();
                        reader.Dispose();
                    }
                    cmd.Cancel();
                    cmd.Dispose();

                }

                return entitys;
            }
            catch (MySqlException me)
            {
                KdtLoger.Instance.Error(me);
                throw new DataException(me.Message);
            }
            catch (Exception ex)
            {
                KdtLoger.Instance.Error(ex);
                throw new DataException(string.Format("执行SQL查询,返回数据集合错误,原因为:{0}", ex.Message));
            }
        }

        public override List<KeyValueCollection> ExecuteQueryKVCollection(string sql, KdtParameterCollection parameters)
        {
            try
            {
                List<KeyValueCollection> entitys = new List<KeyValueCollection>();
                // 执行SQL命令
                using (MySqlCommand cmd = new MySqlCommand(ReplaceSqlText(sql, parameters), _mySqlCn))
                {
                    InitCommand(cmd); // 初始化

                    // 赋值参数
                    var hasConvertParams = ConvertToSqlParameter(parameters);
                    foreach (var item in hasConvertParams)
                    {
                        cmd.Parameters.Add(item.Value);
                    }

                    // 执行填充数据
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        entitys = GetEntityCollection(reader);
                        // 反射参数值
                        ReflectParamValue(parameters, hasConvertParams);

                        reader.Close();
                        reader.Dispose();
                    }
                    cmd.Cancel();
                    cmd.Dispose();

                }

                return entitys;
            }
            catch (MySqlException me)
            {
                KdtLoger.Instance.Error(me);
                throw new DataException(me.Message);
            }
            catch (Exception ex)
            {
                KdtLoger.Instance.Error(ex);
                throw new DataException(string.Format("执行SQL查询,返回数据集合错误,原因为:{0}", ex.Message));
            }
        }

        #endregion.

        #region 执行分页查询（两种分页查询方法）

        /// <summary>
        /// 执行分页查询（采用DataReader进行分页查询）
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <param name="parameters">参数集合</param>
        /// <param name="offset">读取偏移量</param>
        /// <param name="limit">读取最大值</param>
        /// <returns>包含查询结果集合</returns>
        public override List<T> ExecuteQueryPage<T>(string sql, KdtParameterCollection parameters, int offset, int limit)
        {
            try
            {
                List<T> entitys = new List<T>();
                // 执行SQL命令
                using (MySqlCommand cmd = new MySqlCommand(ReplaceSqlText(sql, parameters), _mySqlCn))
                {
                    InitCommand(cmd); // 初始化

                    // 赋值参数
                    var hasConvertParams = ConvertToSqlParameter(parameters);
                    foreach (var item in hasConvertParams)
                    {
                        cmd.Parameters.Add(item.Value);
                    }

                    // 执行填充数据
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        entitys = GetEntityPage<T>(reader, offset, limit);
                        // 反射参数值
                        ReflectParamValue(parameters, hasConvertParams);

                        reader.Close();
                        reader.Dispose();
                    }
                    cmd.Cancel();
                    cmd.Dispose();

                }

                return entitys;
            }
            catch (MySqlException me)
            {
                KdtLoger.Instance.Error(me);
                throw new DataException(me.Message);
            }
            catch (Exception ex)
            {
                KdtLoger.Instance.Error(ex);
                throw new DataException(string.Format("执行SQL查询,返回数据集合错误,原因为:{0}", ex.Message));
            }
        }

        public override List<KeyValueCollection> ExecuteQueryPageKVCollection(string sql, KdtParameterCollection parameters, int offset, int limit)
        {
            try
            {
                List<KeyValueCollection> entitys = new List<KeyValueCollection>();
                // 执行SQL命令
                using (MySqlCommand cmd = new MySqlCommand(ReplaceSqlText(sql, parameters), _mySqlCn))
                {
                    InitCommand(cmd); // 初始化

                    // 赋值参数
                    var hasConvertParams = ConvertToSqlParameter(parameters);
                    foreach (var item in hasConvertParams)
                    {
                        cmd.Parameters.Add(item.Value);
                    }

                    // 执行填充数据
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        entitys = GetEntityPage(reader, offset, limit);
                        // 反射参数值
                        ReflectParamValue(parameters, hasConvertParams);

                        reader.Close();
                        reader.Dispose();
                    }
                    cmd.Cancel();
                    cmd.Dispose();

                }

                return entitys;
            }
            catch (MySqlException me)
            {
                KdtLoger.Instance.Error(me);
                throw new DataException(me.Message);
            }
            catch (Exception ex)
            {
                KdtLoger.Instance.Error(ex);
                throw new DataException(string.Format("执行SQL查询,返回数据集合错误,原因为:{0}", ex.Message));
            }
        }

        #endregion.

        #region 事务基类抽象方法

        public override void BeginTrans()
        {
            if (_mySqlTran != null)
                throw new DataException("在启动新事务之前必须提交或回滚前一事务!");

            try
            {
                _mySqlTran = _mySqlCn.BeginTransaction();
            }
            catch (Exception e)
            {
                throw new DataException(e.Message);
            }
        }

        public override void CommitTrans()
        {
            if (_mySqlTran == null)
                throw new DataException("未启动数据库事务!");
            try
            {
                _mySqlTran.Commit();
            }
            catch (Exception e)
            {
                throw new DataException(e.Message);
            }
            _mySqlTran = null;
        }

        public override void RollbackTrans()
        {
            if (_mySqlTran == null)
                throw new DataException("未启动数据库事务!");
            try
            {
                _mySqlTran.Rollback();
            }
            catch (Exception e)
            {
                throw new DataException(e.Message);
            }
            _mySqlTran = null;
        }

        #endregion.

        #region 私有方法

        /// <summary>
        /// 反射实体类信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected T GetEntity<T>(MySqlDataReader reader) where T : class, new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            if (properties == null || properties.Length <= 0)
                throw new ArgumentNullException("实体类{0}不包含任何属性信息！".ToFormat(typeof(T).Name));
            // 读取列
            Dictionary<string, string> columns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string cname = reader.GetName(i);
                columns[cname] = cname;
            }

            // 反射创建实体类
            T entity = typeof(T).Create<T>();
            if (reader.Read())
            {
                foreach (PropertyInfo property in properties)
                {
                    string p_name = property.Name;
                    object[] attributes = property.GetCustomAttributes(typeof(Field), true);

                    object[] proc_attributes = property.GetCustomAttributes(typeof(Proc), true);

                    // 存在自定义Field实例处理方法
                    if (attributes != null && attributes.Length > 0)
                    {
                        Field dbfield = attributes[0] as Field;
                        // 检测自增变量信息
                        p_name = dbfield.Name.IsNullOrEmpty() ? p_name : dbfield.Name;
                    }
                    else if (proc_attributes != null && proc_attributes.Length > 0)
                    {
                        Proc dbfield = proc_attributes[0] as Proc;
                        // 检测自增变量信息
                        p_name = dbfield.Name.IsNullOrEmpty() ? p_name : dbfield.Name;
                    }
                    // 不存在时处理方法
                    else { }

                    // 读取数据，并赋值
                    if (columns.ContainsKey(p_name))
                        property.SetValue(entity, reader[columns[p_name]].ChangeTo(property.PropertyType), null);
                }

            }
            return entity;
        }

        /// <summary>
        /// 反射实体类信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected KeyValueCollection GetEntity(MySqlDataReader reader)
        {
            KeyValueCollection entity = new KeyValueCollection();
            // 读取列
            List<string> columns = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i));
            }

            // 反射创建实体类
            if (reader.Read())
            {
                foreach (var col in columns)
                {
                    entity[col] = reader[col];
                }
            }
            return entity;
        }

        /// <summary>
        /// 反射实体类信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected List<T> GetEntityCollection<T>(MySqlDataReader reader) where T : class, new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            if (properties == null || properties.Length <= 0)
                throw new ArgumentNullException("实体类{0}不包含任何属性信息！".ToFormat(typeof(T).Name));
            // 读取列
            Dictionary<string, string> columns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string cname = reader.GetName(i);
                columns[cname] = cname;
            }

            // 反射创建实体类
            List<T> entitys = new List<T>();

            while (reader.Read())
            {
                T entity = typeof(T).Create<T>();
                foreach (PropertyInfo property in properties)
                {
                    string p_name = property.Name;
                    object[] attributes = property.GetCustomAttributes(typeof(Field), true);

                    object[] proc_attributes = property.GetCustomAttributes(typeof(Proc), true);

                    // 存在自定义Field实例处理方法
                    if (attributes != null && attributes.Length > 0)
                    {
                        Field dbfield = attributes[0] as Field;
                        // 检测自增变量信息
                        p_name = dbfield.Name.IsNullOrEmpty() ? p_name : dbfield.Name;
                    }
                    else if (proc_attributes != null && proc_attributes.Length > 0)
                    {
                        Proc dbfield = proc_attributes[0] as Proc;
                        // 检测自增变量信息
                        p_name = dbfield.Name.IsNullOrEmpty() ? p_name : dbfield.Name;
                    }
                    // 不存在时处理方法
                    else { }

                    // 读取数据，并赋值
                    if (columns.ContainsKey(p_name))
                        property.SetValue(entity, reader[columns[p_name]].ChangeTo(property.PropertyType), null);
                }
                entitys.Add(entity);
            }
            return entitys;
        }

        protected List<KeyValueCollection> GetEntityCollection(MySqlDataReader reader)
        {
            List<KeyValueCollection> entitys = new List<KeyValueCollection>();
            // 读取列
            List<string> columns = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i));
            }

            // 反射创建实体类
            while (reader.Read())
            {
                KeyValueCollection entity = new KeyValueCollection();
                foreach (var col in columns)
                {
                    entity[col] = reader[col];
                }
                entitys.Add(entity);
            }
            return entitys;
        }

        /// <summary>
        /// 反射实体类信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected List<T> GetEntityPage<T>(MySqlDataReader reader, int offset, int limit) where T : class, new()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            if (properties == null || properties.Length <= 0)
                throw new ArgumentNullException("实体类{0}不包含任何属性信息！".ToFormat(typeof(T).Name));
            // 读取列
            Dictionary<string, string> columns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string cname = reader.GetName(i);
                columns[cname] = cname;
            }

            // 反射创建实体类
            List<T> entitys = new List<T>();
            int currentIndex = 0;
            while (reader.Read())
            {
                if (offset > currentIndex++) continue;
                if (offset + limit == currentIndex) break;

                T entity = typeof(T).Create<T>();
                foreach (PropertyInfo property in properties)
                {
                    string p_name = property.Name;
                    object[] attributes = property.GetCustomAttributes(typeof(Field), true);

                    object[] proc_attributes = property.GetCustomAttributes(typeof(Proc), true);

                    // 存在自定义Field实例处理方法
                    if (attributes != null && attributes.Length > 0)
                    {
                        Field dbfield = attributes[0] as Field;
                        // 检测自增变量信息
                        p_name = dbfield.Name.IsNullOrEmpty() ? p_name : dbfield.Name;
                    }
                    else if (proc_attributes != null && proc_attributes.Length > 0)
                    {
                        Proc dbfield = proc_attributes[0] as Proc;
                        // 检测自增变量信息
                        p_name = dbfield.Name.IsNullOrEmpty() ? p_name : dbfield.Name;
                    }
                    // 不存在时处理方法
                    else { }

                    // 读取数据，并赋值
                    if (columns.ContainsKey(p_name))
                        property.SetValue(entity, reader[columns[p_name]].ChangeTo(property.PropertyType), null);
                }
                entitys.Add(entity);
            }
            return entitys;
        }

        protected List<KeyValueCollection> GetEntityPage(MySqlDataReader reader, int offset, int limit)
        {
            List<KeyValueCollection> entitys = new List<KeyValueCollection>();
            // 读取列
            List<string> columns = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columns.Add(reader.GetName(i));
            }

            // 反射创建实体类
            int currentIndex = 0;
            while (reader.Read())
            {
                if (offset > currentIndex++) continue;
                if (offset + limit == currentIndex) break;

                KeyValueCollection entity = new KeyValueCollection();
                foreach (var col in columns)
                {
                    entity[col] = reader[col];
                }
                entitys.Add(entity);
            }
            return entitys;
        }

        /// <summary>
        /// 获取表中自增列的值
        /// </summary>
        /// <param name="_tablename">表名</param>
        /// <param name="_filedname">自增的字段名</param>
        /// <returns>自增ID</returns>
        protected internal override int QueryAutoNo(string _tablename, string _filedname)
        {
            return 0;
        }

        /// <summary>
        /// 初始CMD执行器
        /// </summary>
        /// <param name="cmd">SqlCommand执行器</param>
        private void InitCommand(MySqlCommand cmd)
        {
            if (_mySqlTran != null)
                cmd.Transaction = _mySqlTran;

            if (CommandTimeOut.HasValue)
                cmd.CommandTimeout = CommandTimeOut.Value;

            cmd.CommandType = CmdType;
        }

        /// <summary>
        /// 转换成SqlParameter
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <returns>返回转换成SqlParameter集合</returns>
        private Dictionary<string, MySqlParameter> ConvertToSqlParameter(KdtParameterCollection parameters)
        {
            Dictionary<string, MySqlParameter> result = new Dictionary<string, MySqlParameter>(StringComparer.OrdinalIgnoreCase);

            if (parameters != null && parameters.Count > 0)
            {
                foreach (var item in parameters)
                {
                    MySqlParameter param = new MySqlParameter("{0}{1}".ToFormat(ParamPrifix, item.Name), item.Value);

                    switch (item._InPutType)
                    {
                        case ProcInPutEnum.OutPut: param.Direction = ParameterDirection.Output; break;
                        case ProcInPutEnum.ReturnValue: param.Direction = ParameterDirection.ReturnValue; break;
                        case ProcInPutEnum.InputOutPut: param.Direction = ParameterDirection.InputOutput; break;
                        default: param.Direction = ParameterDirection.Input; break;
                    }

                    result[item.Name] = param;
                }
            }

            // 返回转换集合
            return result;
        }

        /// <summary>
        /// 反射参数值信息
        /// </summary>
        /// <param name="parameters">参数集合</param>
        /// <param name="mysqlparams">转换后的参数集合</param>
        private void ReflectParamValue(KdtParameterCollection parameters, Dictionary<string, MySqlParameter> mysqlparams)
        {
            if (parameters != null && parameters.Count > 0)
            {
                foreach (var item in parameters)
                {
                    // 非只输入项时，读取返回值
                    if (item._InPutType != ProcInPutEnum.InPut)
                    {
                        item.Value = mysqlparams[item.Name].Value;
                    }
                }
            }
        }

        #endregion.
    }
}
