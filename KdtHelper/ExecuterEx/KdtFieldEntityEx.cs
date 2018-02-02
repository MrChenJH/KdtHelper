using KdtHelper.Core.Adapter;
using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Common;
using System.Reflection;
using System.Linq;
using System.Data;

namespace KdtHelper.Core.ExecuterEx
{
    /// <summary>
    /// 可执行的KDT执行类
    /// </summary>
    public abstract class KdtFieldEntityEx
    {
        #region T-SQL语法相关

        /// <summary>
        /// 关联其他表字段格式为[表名],[字段名]。
        /// 如：roleinfo,userid
        /// </summary>
        protected abstract Dictionary<string, string> _relationFields { get; }

        /// <summary>
        /// 表更新条件信息
        /// 如：一般格式为 where userid=[@userid] and username like '%[username]%'
        /// </summary>
        protected abstract Dictionary<string, string> _UpdateWhere { get; }

        /// <summary>
        /// 表更新条件信息
        /// 如：一般格式为 where userid=[@userid] and username like '%[username]%'
        /// </summary>
        protected abstract Dictionary<string, string> _AddOrUpdateWhere { get; }

        /// <summary>
        /// 表更新条件信息
        /// 如：一般格式为 where userid=[@userid] and username like '%[username]%'
        /// </summary>
        protected abstract Dictionary<string, string> _DeleteWhere { get; }

        /// <summary>
        /// 查询SQL语句
        /// </summary>
        protected abstract Dictionary<string, string> _SelectSql { get; }

        #endregion.

        #region 对外属性及方法

        /// <summary>
        /// 自增变量值
        /// </summary>
        internal Dictionary<string, int> IncrVal = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 获取自增变量值信息
        /// </summary>
        /// <param name="_tablename"></param>
        /// <returns></returns>
        public int NewId(string _tablename = "")
        {
            if (_tablename.IsNullOrEmpty())
                _tablename = _mainTable;
            if (IncrVal.ContainsKey(_tablename))
            {
                return IncrVal[_tablename];
            }
            throw new Exception("不存在的自增变量值信息");
        }

        /// <summary>
        /// 受影响的函数值
        /// </summary>
        public int Affected { get; internal set; }

        /// <summary>
        /// 日志用户
        /// </summary>
        public abstract string CUser { get; }

        #endregion.

        #region 属性读取相关方法

        /// <summary>
        /// 所有KdtFeildEx字段信息缓存
        /// </summary>
        protected Dictionary<string, KdtFeildEx> _kdtFeilds { get; set; }

        /// <summary>
        /// 获取所有KdtFeildEx字段信息
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<string, KdtFeildEx> GetKdtFeilds()
        {
            try
            {
                if (_kdtFeilds != null) return _kdtFeilds;

                PropertyInfo[] props = _classType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if (props != null && props.Length > 0)
                {
                    Dictionary<string, KdtFeildEx> dic = new Dictionary<string, KdtFeildEx>();
                    foreach (var prop in props)
                    {
                        object value = prop.GetValue(_class, null);
                        if (value is KdtFeildEx)
                            dic[prop.Name] = value as KdtFeildEx;
                    }
                    return dic;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 所有非字段信息缓存
        /// </summary>
        protected Dictionary<string, object> _feilds { get; set; }

        /// <summary>
        /// 获取所有非字段信息
        /// </summary>
        /// <returns></returns>
        protected virtual Dictionary<string, object> GetFeilds()
        {
            try
            {
                if (_feilds != null) return _feilds;
                PropertyInfo[] props = _classType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if (props != null && props.Length > 0)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    foreach (var prop in props)
                    {
                        object value = prop.GetValue(_class, null);
                        dic[prop.Name] = value;
                    }
                    return dic;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="_name">属性名</param>
        /// <returns></returns>
        public virtual T Get<T>(string _name)
        {
            try
            {
                PropertyInfo propertyInfo = _classType.GetProperty(_name); //获取指定名称的属性
                object pvalue = propertyInfo.GetValue(_class, null);
                if (pvalue is KdtFeildEx)
                {
                    Type _filed = typeof(KdtFeildEx);
                    PropertyInfo _filedprop = _filed.GetProperty("FeildValue");
                    return _filedprop.GetValue(pvalue, null).Convert<T>(default(T));
                }
                else
                {
                    return pvalue.Convert<T>(default(T));
                }
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="_name">属性名</param>
        /// <param name="_value">属性值</param>
        public virtual void Set(string _name, object _value)
        {
            try
            {
                PropertyInfo propertyInfo = _classType.GetProperty(_name); //获取指定名称的属性
                object pvalue = propertyInfo.GetValue(_class, null);
                if (pvalue is KdtFeildEx)
                {
                    Type _filed = typeof(KdtFeildEx);
                    PropertyInfo _filedprop = _filed.GetProperty("FeildValue");
                    _filedprop.SetValue(pvalue, _value, null); //给对应属性赋值
                }
                else
                {
                    propertyInfo.SetValue(_classType, _value, null); //给对应属性赋值
                }

            }
            catch { }
        }

        /// <summary>
        /// 设置KdtFeildEx格式字段值
        /// </summary>
        /// <param name="_feildname"></param>
        /// <param name="_value"></param>
        public virtual void SetKdt(string _feildname, object _value)
        {
            try
            {
                var found = GetKdtFeilds().ToList().Find(v => v.Value.FeildName.Equals(_feildname, StringComparison.OrdinalIgnoreCase));
                Set(found.Key, _value);
            }
            catch { }
        }

        #endregion.

        #region 属性基本方法

        /// <summary>
        /// 子类
        /// </summary>
        protected abstract object _class { get; }

        /// <summary>
        /// 子类类型
        /// </summary>
        protected abstract Type _classType { get; }

        /// <summary>
        /// 主表名称
        /// </summary>
        protected abstract string _mainTable { get; }

        
        /// <summary>
        /// 返回TableName
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return _mainTable;
        }
        

        /// <summary>
        /// 主表是否存在判断语法
        /// </summary>
        public virtual string GetMainTableExist()
        {
            var _allfeilds = GetAllKdtFields();
            var foundtable = _allfeilds.Find(t => t.TableName.Equals(_mainTable, StringComparison.OrdinalIgnoreCase));
            if (foundtable != null)
            {
                var _list = foundtable.Fields.FindAll(k => k.IsKey);
                StringBuilder valuestr = new StringBuilder();
                if (_list != null && _list.Count > 0)
                {
                    foreach (var item in _list)
                    {
                        if (item.FeildValue != null)
                        {
                            valuestr.AppendFormat(" {0}='{1}' and", item.FeildName, item.FeildValue.ToString());
                        }
                        else
                        {
                            valuestr.AppendFormat(" {0} is null and", item.FeildName);
                        }
                    }
                    valuestr = valuestr.Replace("and", "", valuestr.Length - 3, 3);
                    return "select count(1) from {0} where {1}".ToFormat(_mainTable, valuestr);
                }
            }
            return "";
        }

        /// <summary>
        /// 适配器
        /// </summary>
        protected AdapterSql Adapter { get; private set; }

        /// <summary>
        /// 设置适配器
        /// </summary>
        /// <param name="_driver"></param>
        public void SetAdapter(DbDriverMember _driver)
        {
            Adapter = new AdapterSql(_driver);
        }

        #endregion.

        #region 组织成SQL语句

        /// <summary>
        /// 获取所有KdtFields
        /// </summary>
        /// <returns></returns>
        private List<KdtTableFeildEx> GetAllKdtFields()
        {
            var _list = GetKdtFeilds();
            if (_list != null)
            {
                List<KdtTableFeildEx> _ALL = new List<KdtTableFeildEx>();
                foreach (var item in _list)
                {
                    item.Value.SetName = item.Key;
                    var found = _ALL.Find(t => t.TableName.Equals(item.Value.TableName, StringComparison.OrdinalIgnoreCase));
                    if (found == null)
                    {
                        found = new KdtTableFeildEx() { TableName = item.Value.TableName };
                        _ALL.Add(found);
                    }
                    if (item.Value.IsIncr)
                    {
                        found.Fields.Add(item.Value);
                        found.HasIncr = true;
                        found.IncrFeild = item.Value;
                    }
                    else if (item.Value.HasValue)
                        found.Fields.Add(item.Value);
                    else
                    {
                        // Nothing to do.
                    }
                }
                IncrVal.Clear();
                foreach (var item in _ALL)
                {
                    IncrVal.Add(item.TableName, 0);
                }
                return _ALL;
            }

            return null;
        }

        /// <summary>
        /// 插入数据库操作解析方法
        /// </summary>
        /// <param name="_needTran">是否进行回滚插入</param>
        /// <param name="_isIncr">自定义参数</param>
        /// <param name="_params">参数值集合</param>
        /// <returns>插入T-SQL语句</returns>
        public virtual string Add(out bool _needTran, out bool _isIncr, out KdtParameterCollection _params)
        {
            if (this.Adapter == null) throw new Exception("未启用适配器信息！");

            StringBuilder sqlText = new StringBuilder();
            _needTran = false;
            _isIncr = false;
            _params = new KdtParameterCollection();
            KdtParameterCollection tempparams;

            var _allfeilds = GetAllKdtFields();
            if (_allfeilds != null && _allfeilds.Count > 0)
            {
                if (_relationFields != null || _allfeilds.Count > 1)
                {
                    // 判断是否为多表情况
                    if (_relationFields.Count > 0)
                    {
                        _needTran = true;
                        _isIncr = false;
                        // 进行有关联的多表插入
                        StringBuilder _returnedsel = new StringBuilder("select");
                        // 第一步，获取主表信息
                        var foundtable = _allfeilds.Find(t => t.TableName.Equals(_mainTable, StringComparison.OrdinalIgnoreCase));
                        if (foundtable == null) throw new Exception("多级插入主表{0}错误！".ToFormat(_mainTable));

                        sqlText.AppendLine(CreateAddSql(foundtable, "r0", out tempparams));
                        _params.AddRange(tempparams);

                        if (foundtable.HasIncr)
                            _returnedsel.AppendFormat(" {0}{1} as p{2},", this.Adapter.Prefix, "r0", foundtable.TableName);
                        _allfeilds.Remove(foundtable);

                        for (int i = 0; i < _allfeilds.Count; i++)
                        {
                            var _table = _allfeilds[i];
                            if (_relationFields.ContainsKey(_table.TableName))
                            {
                                _table.HasRelation = true;
                                _table.RelFeild = _relationFields[_table.TableName];
                            }
                            string _thisincrstr = "r{0}".ToFormat(i + 1);
                            sqlText.AppendLine(CreateAddSql(_table, _thisincrstr, out tempparams));
                            _params.AddRange(tempparams);
                            if (_table.HasIncr)
                                _returnedsel.AppendFormat(" {0}{1} as p{2},", this.Adapter.Prefix, _thisincrstr, _table.TableName);
                        }

                        if (_returnedsel.Length > 6)
                        {
                            _isIncr = true;
                            sqlText.Append(_returnedsel.ToString().TrimEnd(','));
                        }
                        return sqlText.ToString();
                    }
                    else
                    {
                        List<KdtTableFeildEx> myList = new List<KdtTableFeildEx>(_allfeilds);
                        foreach (var item in myList)
                        {
                            if (item.Fields.Count < 2)
                                _allfeilds.Remove(item);
                        }
                        if (_allfeilds.Count > 1)
                        {
                            _needTran = true;
                            _isIncr = false;
                            // 进行无关联的多表插入
                            StringBuilder _returnedsel = new StringBuilder("select");
                            for (int i = 0; i < _allfeilds.Count; i++)
                            {
                                var _thistable = _allfeilds[i];
                                string _thisincrstr = "r{0}".ToFormat(i);
                                sqlText.AppendLine(CreateAddSql(_thistable, _thisincrstr, out tempparams));
                                _params.AddRange(tempparams);
                                if (_thistable.HasIncr)
                                    _returnedsel.AppendFormat(" {0}{1} as p{2},", this.Adapter.Prefix, _thisincrstr, _thistable.TableName);
                            }
                            if (_returnedsel.Length > 6)
                            {
                                _isIncr = true;
                                sqlText.Append(_returnedsel.ToString().TrimEnd(','));
                            }
                            return sqlText.ToString();
                        }
                    }
                }
                // 单表插入
                var tempTable = _allfeilds.First();
                _needTran = false;
                _isIncr = tempTable.HasIncr;
                sqlText.Append(CreateAddSql(tempTable, "r0", out tempparams));
                _params.AddRange(tempparams);
                if (_isIncr)
                    sqlText.AppendFormat(this.Adapter.Select("r0", "p{0}".ToFormat(tempTable.TableName), "", true, true));
                return sqlText.ToString();
            }

            throw new DataException("没有可以添加的字段列信息");
        }

        /// <summary>
        /// 插入数据库操作解析方法
        /// </summary>
        /// <param name="_needTran">是否进行回滚插入</param>
        /// <param name="_params">参数值集合</param>
        /// <returns>插入T-SQL语句</returns>
        public virtual string Update(out bool _needTran, out KdtParameterCollection _params)
        {
            if (this.Adapter == null) throw new Exception("未启用适配器信息！");
            if (_UpdateWhere == null || _UpdateWhere.Count < 1) throw new Exception("不存在可以更新的表");

            StringBuilder sqlText = new StringBuilder();
            _needTran = false;
            _params = new KdtParameterCollection();

            var _allfeilds = new List<KdtTableFeildEx>();
            List<KdtTableFeildEx> temp = new List<KdtTableFeildEx>(GetAllKdtFields());
            List<KdtFeildEx> _allex = new List<KdtFeildEx>();
            foreach (var item in _UpdateWhere) // 清理非存在的更新表信息
            {
                foreach (var table in temp)
                {
                    if (table.TableName.Equals(item.Key, StringComparison.OrdinalIgnoreCase))
                    {
                        _allex.AddRange(table.Fields);
                        _allfeilds.Add(table);
                        break;
                    }
                }
            }

            // 组织T-SQL语句
            _needTran = _allfeilds.Count > 1;
            foreach (var table in _allfeilds)
            {
                // 处理WHERE语句
                string wherestr = _UpdateWhere[table.TableName];
                foreach (var item in _allex)
                {
                    if (wherestr.Contains("[{0}]".ToFormat(item.SetName)))
                    {
                        wherestr = wherestr.Replace("[{0}]".ToFormat(item.SetName), item.FeildValue.ToString());
                        table.Fields.Remove(item);
                    }
                    if (wherestr.Contains("[@{0}]".ToFormat(item.SetName)))
                    {
                        wherestr = wherestr.Replace("[@{0}]".ToFormat(item.SetName), "{0}{1}".ToFormat(Adapter.Prefix, item.SetName));
                        _params.AddParameter(item.SetName, item.FeildValue, ProcInPutEnum.InPut);
                        table.Fields.Remove(item);
                    }
                }

                sqlText.AppendFormat("update {0} set", table.TableName);
                foreach (var field in table.Fields)
                {
                    if (field.HasValue)
                    {
                        sqlText.AppendFormat(" {0}={1}{2},", field.FeildName, this.Adapter.Prefix, field.SetName);
                        _params.AddParameter(field.SetName, field.FeildValue, ProcInPutEnum.InPut);
                    }
                }
                if (sqlText.ToString().Contains("="))
                {
                    sqlText = sqlText.Replace(",", " ", sqlText.Length - 1, 1);
                    sqlText.AppendLine(wherestr);
                }
            }

            return sqlText.ToString();
        }

        /// <summary>
        /// 插入或更新数据库操作解析方法
        /// </summary>
        /// <param name="_needTran">是否进行回滚插入</param>
        /// <param name="_params">参数值集合</param>
        /// <returns>插入T-SQL语句</returns>
        public virtual string AddOrUpdate(out bool _needTran, out KdtParameterCollection _params)
        {
            if (this.Adapter == null) throw new Exception("未启用适配器信息！");

            StringBuilder sqlText = new StringBuilder();
            _needTran = false;
            _params = new KdtParameterCollection();
            KdtParameterCollection tempparams;

            var _allfeilds = GetAllKdtFields();
            List<KdtFeildEx> _allex = new List<KdtFeildEx>();
            foreach (var table in _allfeilds)
            {
                _allex.AddRange(table.Fields);
            }

            if (_allfeilds != null && _allfeilds.Count > 0)
            {
                if (_relationFields != null || _allfeilds.Count > 1)
                {
                    // 判断是否为多表情况
                    if (_relationFields.Count > 0)
                    {
                        _needTran = true;
                        // 第一步，获取主表信息
                        var foundtable = _allfeilds.Find(t => t.TableName.Equals(_mainTable, StringComparison.OrdinalIgnoreCase));
                        if (foundtable == null) throw new Exception("多级插入主表{0}错误！".ToFormat(_mainTable));

                        sqlText.AppendLine(CreateAddOrUpdateSql(foundtable, _allex, "r0", out tempparams));
                        _params.AddRange(tempparams);
                        _allfeilds.Remove(foundtable);

                        for (int i = 0; i < _allfeilds.Count; i++)
                        {
                            var _table = _allfeilds[i];
                            if (_relationFields.ContainsKey(_table.TableName))
                            {
                                _table.HasRelation = true;
                                _table.RelFeild = _relationFields[_table.TableName];
                            }
                            string _thisincrstr = "r{0}".ToFormat(i + 1);
                            sqlText.AppendLine(CreateAddOrUpdateSql(_table, _allex, _thisincrstr, out tempparams));
                            _params.AddRange(tempparams);
                        }
                        return sqlText.ToString();
                    }
                    else
                    {
                        List<KdtTableFeildEx> myList = new List<KdtTableFeildEx>(_allfeilds);
                        foreach (var item in myList)
                        {
                            if (item.Fields.Count < 2)
                                _allfeilds.Remove(item);
                        }
                        if (_allfeilds.Count > 1)
                        {
                            _needTran = true;
                            // 进行无关联的多表插入
                            for (int i = 0; i < _allfeilds.Count; i++)
                            {
                                var _thistable = _allfeilds[i];
                                string _thisincrstr = "r{0}".ToFormat(i);
                                sqlText.AppendLine(CreateAddOrUpdateSql(_thistable, _allex, _thisincrstr, out tempparams));
                                _params.AddRange(tempparams);
                            }
                            return sqlText.ToString();
                        }
                    }
                }
                // 单表插入
                var tempTable = _allfeilds.First();
                _needTran = false;
                sqlText.AppendLine(CreateAddOrUpdateSql(tempTable, _allex, "r0", out tempparams));
                _params.AddRange(tempparams);
                return sqlText.ToString();
            }

            throw new DataException("没有可以添加的字段列信息");
        }

        /// <summary>
        /// 删除数据库操作解析方法
        /// </summary>
        /// <param name="_needTran">是否进行回滚插入</param>
        /// <param name="_params">参数值集合</param>
        /// <returns>删除T-SQL语句</returns>
        public virtual string Delete(out bool _needTran, out KdtParameterCollection _params)
        {
            if (this.Adapter == null) throw new Exception("未启用适配器信息！");
            if (_DeleteWhere == null || _DeleteWhere.Count < 1) throw new Exception("不存在可以删除的表信息");

            StringBuilder sqlText = new StringBuilder();
            _needTran = false;
            _params = new KdtParameterCollection();

            var _allfeilds = GetAllKdtFields();
            List<KdtFeildEx> _allex = new List<KdtFeildEx>();
            foreach (var item in _allfeilds)
            {
                _allex.AddRange(item.Fields);
            }
            _needTran = _DeleteWhere.Count > 1;
            // 组织T-SQL语句
            foreach (var table in _DeleteWhere)
            {
                string wherestr = table.Value;
                foreach (var item in _allex)
                {
                    if (wherestr.Contains("[{0}]".ToFormat(item.SetName)))
                    {
                        wherestr = wherestr.Replace("[{0}]".ToFormat(item.SetName), item.FeildValue.ToString());
                    }
                    if (wherestr.Contains("[@{0}]".ToFormat(item.SetName)))
                    {
                        wherestr = wherestr.Replace("[@{0}]".ToFormat(item.SetName), "{0}{1}".ToFormat(Adapter.Prefix, item.SetName));
                        _params.AddParameter(item.SetName, item.FeildValue, ProcInPutEnum.InPut);
                    }
                }
                sqlText.AppendLine(Adapter.Delete(table.Key, wherestr));
            }
            return sqlText.ToString();
        }

        /// <summary>
        /// 执行查询T-SQL语句
        /// </summary>
        /// <param name="_selecttype">查询类型</param>
        /// <param name="_params">返回参数集合</param>
        /// <returns>返回查询T-SQL语句</returns>
        public virtual string Select(string _selecttype, out KdtParameterCollection _params)
        {
            if (this.Adapter == null) throw new Exception("未启用适配器信息！");
            if (_SelectSql == null || !_SelectSql.ContainsKey(_selecttype)) throw new Exception("不存在可以查询的表信息");

            _params = new KdtParameterCollection();
            var _allfeilds = GetAllKdtFields();
            List<KdtFeildEx> _allex = new List<KdtFeildEx>();
            foreach (var item in _allfeilds)
            {
                _allex.AddRange(item.Fields);
            }

            string sqltext = _SelectSql[_selecttype];
            foreach (var item in _allex)
            {
                if (sqltext.Contains("[{0}]".ToFormat(item.SetName)))
                {
                    sqltext = sqltext.Replace("[{0}]".ToFormat(item.SetName), item.FeildValue.ToString());
                }
                if (sqltext.Contains("[@{0}]".ToFormat(item.SetName)))
                {
                    sqltext = sqltext.Replace("[@{0}]".ToFormat(item.SetName), "{0}{1}".ToFormat(Adapter.Prefix, item.SetName));
                    _params.AddParameter(item.SetName, item.FeildValue, ProcInPutEnum.InPut);
                }
            }

            Dictionary<string, object> otherfields = GetFeilds();
            foreach (var key in otherfields)
            {
                if (sqltext.Contains("[{0}]".ToFormat(key.Key)))
                {
                    sqltext = sqltext.Replace("[{0}]".ToFormat(key.Key), key.Value.ToString());
                }
                if (sqltext.Contains("[@{0}]".ToFormat(key.Key)))
                {
                    sqltext = sqltext.Replace("[@{0}]".ToFormat(key.Key), "{0}{1}".ToFormat(Adapter.Prefix, key.Key));
                    _params.AddParameter(key.Key, key.Value, ProcInPutEnum.InPut);
                }
            }

            return sqltext;
        }

        /// <summary>
        /// 获取查询条件语句
        /// </summary>
        /// <returns></returns>
        public virtual string GetSelectWhere()
        {
            if (this.Adapter == null) throw new Exception("未启用适配器信息！");

            StringBuilder sqlText = new StringBuilder();


            return sqlText.ToString();
        }

        #endregion.

        #region 私有方法

        /// <summary>
        /// 创建插入语句
        /// </summary>
        /// <param name="_table">表字段集合</param>
        /// <param name="_incrstr">自增字段名称</param>
        /// <param name="_params">返回参数</param>
        /// <returns>执行插入SQL语句</returns>
        protected virtual string CreateAddSql(KdtTableFeildEx _table, string _incrstr, out KdtParameterCollection _params)
        {
            StringBuilder sqlText = new StringBuilder();
            _params = new KdtParameterCollection();
            StringBuilder insertFields, insertValues;

            if (_table.HasIncr)
            {
                string _declare = this.Adapter.Declare(_incrstr, DbDataType.INT);
                if (!_declare.IsNullOrEmpty())
                    sqlText.AppendLine(_declare);
                sqlText.AppendLine(this.Adapter.Funcation(_incrstr, _table.IncrFeild.FeildName, _table.TableName, DbFunName.MAX));
                sqlText.AppendLine(this.Adapter.Set(_incrstr, "{0} + 1".ToFormat(this.Adapter.ISNULL(_incrstr, "0", true))));

                insertFields = new StringBuilder("{0},".ToFormat(_table.IncrFeild.FeildName));
                insertValues = new StringBuilder("{0}{1},".ToFormat(this.Adapter.Prefix, _incrstr));
                if (_table.HasRelation)
                {
                    insertFields.AppendFormat("{0},".ToFormat(_table.RelFeild));
                    insertValues.AppendFormat("{0}{1},".ToFormat(this.Adapter.Prefix, "r0"));
                }
                foreach (var item in _table.Fields)
                {
                    if (item.IsIncr) continue;
                    insertFields.AppendFormat("{0},", item.FeildName);
                    insertValues.AppendFormat("{0}{1},", this.Adapter.Prefix, item.SetName);
                    _params.AddParameter(item.SetName, item.FeildValue, ProcInPutEnum.InPut);
                }
                sqlText.AppendLine(this.Adapter.Insert(_table.TableName,
                    insertFields.ToString().TrimEnd(','),
                    insertValues.ToString().TrimEnd(',')));
            }
            else
            {
                insertFields = new StringBuilder();
                insertValues = new StringBuilder();
                if (_table.HasRelation)
                {
                    insertFields.AppendFormat("{0},".ToFormat(_table.RelFeild));
                    insertValues.AppendFormat("{0}{1},".ToFormat(this.Adapter.Prefix, "r0"));
                }
                foreach (var item in _table.Fields)
                {
                    insertFields.AppendFormat("{0},", item.FeildName);
                    insertValues.AppendFormat("{0}{1},", this.Adapter.Prefix, item.SetName);
                    _params.AddParameter(item.SetName, item.FeildValue, ProcInPutEnum.InPut);
                }
                sqlText.AppendLine(this.Adapter.Insert(_table.TableName,
                   insertFields.ToString().TrimEnd(','),
                   insertValues.ToString().TrimEnd(',')));
            }

            return sqlText.ToString();
        }

        /// <summary>
        /// 创建插入或更新语句
        /// </summary>
        /// <param name="_table">表字段集合</param>
        /// <param name="_allex">字段集合</param>
        /// <param name="_incrstr">自增字段名称</param>
        /// <param name="_params">返回参数</param>
        /// <returns>执行插入SQL语句</returns>
        protected virtual string CreateAddOrUpdateSql(KdtTableFeildEx _table, List<KdtFeildEx> _allex, string _incrstr, out KdtParameterCollection _params)
        {
            StringBuilder sqlText = new StringBuilder();
            _params = new KdtParameterCollection();
            StringBuilder insertFields, insertValues;
            // 处理WHERE语句
            string wherestr = _AddOrUpdateWhere.ContainsKey(_table.TableName) ? _AddOrUpdateWhere[_table.TableName] : "";
            List<KdtFeildEx> whereFields = new List<KdtFeildEx>();
            foreach (var item in _allex)
            {
                if (wherestr.Contains("[{0}]".ToFormat(item.SetName)))
                {
                    wherestr = wherestr.Replace("[{0}]".ToFormat(item.SetName), item.FeildValue.ToString());
                    whereFields.Add(item);
                }
                if (wherestr.Contains("[@{0}]".ToFormat(item.SetName)))
                {
                    wherestr = wherestr.Replace("[@{0}]".ToFormat(item.SetName), "{0}{1}".ToFormat(Adapter.Prefix, item.SetName));
                    _params.AddParameter(item.SetName, item.FeildValue, ProcInPutEnum.InPut);
                    whereFields.Add(item);
                }
            }

            if (_table.HasIncr && _incrstr == "r0")
            {
                string _declare = this.Adapter.Declare(_incrstr, DbDataType.INT);
                if (!_declare.IsNullOrEmpty())
                    sqlText.AppendLine(_declare);
                sqlText.AppendLine(this.Adapter.Funcation(_incrstr, _table.IncrFeild.FeildName, _table.TableName, DbFunName.MAX));

            }
            sqlText.AppendLine("if not exists(select 1 from {0} {1})".ToFormat(_table.TableName, wherestr));
            sqlText.AppendLine("begin");
            if (_table.HasIncr)
            {
                if (_incrstr != "r0")
                {
                    string _declare = this.Adapter.Declare(_incrstr, DbDataType.INT);
                    if (!_declare.IsNullOrEmpty())
                        sqlText.AppendLine(_declare);
                    sqlText.AppendLine(this.Adapter.Funcation(_incrstr, _table.IncrFeild.FeildName, _table.TableName, DbFunName.MAX));
                    sqlText.AppendLine(this.Adapter.Set(_incrstr, "{0} + 1".ToFormat(this.Adapter.ISNULL(_incrstr, "0", true))));
                }
                else
                {
                    sqlText.AppendLine(this.Adapter.Set(_incrstr, "{0} + 1".ToFormat(this.Adapter.ISNULL(_incrstr, "0", true))));
                }

                insertFields = new StringBuilder("{0},".ToFormat(_table.IncrFeild.FeildName));
                insertValues = new StringBuilder("{0}{1},".ToFormat(this.Adapter.Prefix, _incrstr));
                if (_table.HasRelation)
                {
                    insertFields.AppendFormat("{0},".ToFormat(_table.RelFeild));
                    insertValues.AppendFormat("{0}{1},".ToFormat(this.Adapter.Prefix, "r0"));
                }
                foreach (var item in _table.Fields)
                {
                    if (item.IsIncr) continue;
                    insertFields.AppendFormat("{0},", item.FeildName);
                    insertValues.AppendFormat("{0}{1},", this.Adapter.Prefix, item.SetName);
                    _params.AddParameter(item.SetName, item.FeildValue, ProcInPutEnum.InPut);
                }

                sqlText.AppendLine(this.Adapter.Insert(_table.TableName,
                   insertFields.ToString().TrimEnd(','),
                   insertValues.ToString().TrimEnd(',')));

                sqlText.AppendLine(this.Adapter.Select(_incrstr, "p{0}".ToFormat(_table.TableName), "", true, true));
            }
            else
            {
                insertFields = new StringBuilder();
                insertValues = new StringBuilder();
                if (_table.HasRelation)
                {
                    insertFields.AppendFormat("{0},".ToFormat(_table.RelFeild));
                    insertValues.AppendFormat("{0}{1},".ToFormat(this.Adapter.Prefix, "r0"));
                }
                foreach (var item in _table.Fields)
                {
                    insertFields.AppendFormat("{0},", item.FeildName);
                    insertValues.AppendFormat("{0}{1},", this.Adapter.Prefix, item.SetName);
                    _params.AddParameter(item.SetName, item.FeildValue, ProcInPutEnum.InPut);
                }
                sqlText.AppendLine(this.Adapter.Insert(_table.TableName,
                   insertFields.ToString().TrimEnd(','),
                   insertValues.ToString().TrimEnd(',')));
            }
            sqlText.AppendLine("end else begin");
            sqlText.AppendFormat("update {0} set", _table.TableName);
            foreach (var item in whereFields)
            {
                _table.Fields.Remove(item);
            }
            foreach (var field in _table.Fields)
            {
                if (field.HasValue)
                {
                    sqlText.AppendFormat(" {0}={1}{2},", field.FeildName, this.Adapter.Prefix, field.SetName);
                    _params.AddParameter(field.SetName, field.FeildValue, ProcInPutEnum.InPut);
                }
            }
            if (sqlText.ToString().Contains("="))
            {
                sqlText = sqlText.Replace(",", " ", sqlText.Length - 1, 1);
                sqlText.AppendLine(wherestr);
            }

            sqlText.AppendLine("end");
            return sqlText.ToString();
        }

        #endregion.

        #region 转换方法 

        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            List<KeyValuePaire> vals = new List<KeyValuePaire>();
            var _allfeilds = GetAllKdtFields();
            if (_allfeilds != null && _allfeilds.Count > 0)
            {
                foreach (var item in _allfeilds)
                {
                    if (item.Fields != null && item.Fields.Count > 0)
                    {
                        foreach (var tf in item.Fields)
                        {
                            vals.Add(new KeyValuePaire() { key = tf.SetName, val = tf.FeildValue });
                        }
                    }
                }
            }
            return vals.ToJson(false);
        }

        /// <summary>
        /// 初始化类
        /// </summary>
        /// <param name="_entity"></param>
        public void Init(string _entity)
        {
            if (!_entity.IsNullOrEmpty())
            {
                List<KeyValuePaire> vals = _entity.ToEntity<List<KeyValuePaire>>(false);
                if (vals != null && vals.Count > 0)
                {
                    foreach (var val in vals)
                    {
                        this.Set(val.key, val.val);
                    }
                }
            }
        }

        #endregion.
    }
}
