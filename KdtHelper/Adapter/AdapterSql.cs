using KdtHelper.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace KdtHelper.Core.Adapter
{
    public class AdapterSql
    {
        /// <summary>
        /// 驱动器类型
        /// </summary>
        private DriverType _DriverType { get; set; }

        /// <summary>
        /// 语法定义格式
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_driver"></param>
        public AdapterSql(DbDriverMember _driver)
        {
            this._DriverType = _driver.Driver.Convert(DriverType.sqlserver);
            if (_driver.Prefix.IsNullOrEmpty())
            {
                switch (_DriverType)
                {
                    case DriverType.oracle: Prefix = ":"; break;
                    default: Prefix = "@"; break;
                }
            }
            else
                Prefix = _driver.Prefix;
        }

        /// <summary>
        /// 获取数据库类型关键字
        /// </summary>
        /// <param name="_type">自定义类型</param>
        /// <returns>实际使用类型</returns>
        public string DataType(DbDataType _type)
        {
            string datatype = "";
            switch (_type)
            {
                case DbDataType.INT:
                    if (_DriverType == DriverType.oracle)
                        datatype = "NUMBER";
                    else
                        datatype = "int";
                    break;
                case DbDataType.TEXT:
                    if (_DriverType == DriverType.oracle)
                        datatype = "BLOB";
                    else
                        datatype = "text";
                    break;
                case DbDataType.NTEXT:
                    if (_DriverType == DriverType.oracle)
                        datatype = "BLOB";
                    else
                        datatype = "ntext";
                    break;
                case DbDataType.LONG:
                    if (_DriverType == DriverType.oracle)
                        datatype = "NUMBER";
                    else
                        datatype = "bigint";
                    break;
                case DbDataType.DOUBLE:
                    if (_DriverType == DriverType.oracle)
                        datatype = "BINARY_DOUBLE";
                    else
                        datatype = "double";
                    break;
                case DbDataType.FLOAT:
                    if (_DriverType == DriverType.oracle)
                        datatype = "BINARY_FLOAT";
                    else
                        datatype = "double";
                    break;
                case DbDataType.CHAR: datatype = "char"; break;
                case DbDataType.NCHAR: datatype = "nchar"; break;
                case DbDataType.VARCHAR:
                    if (_DriverType == DriverType.oracle)
                        datatype = "varchar2";
                    else
                        datatype = "varchar";
                    break;
                case DbDataType.NVARCHAR:
                    if (_DriverType == DriverType.oracle)
                        datatype = "nvarchar2";
                    else
                        datatype = "nvarchar";
                    break;
                case DbDataType.BIT:
                    break;
                case DbDataType.TINYINT:
                    if (_DriverType == DriverType.oracle)
                        datatype = "NUMBER";
                    else if (_DriverType == DriverType.mysql)
                        datatype = "SMALLINT";
                    else
                        datatype = "tinyint";
                    break;
                case DbDataType.DATETIME:
                    if (_DriverType == DriverType.oracle)
                        datatype = "DATE";
                    else
                        datatype = "datetime";
                    break;
            }

            return datatype;
        }

        /// <summary>
        /// SQL SERVER下 IsNull函数
        /// </summary>
        /// <param name="check_expression">检查表达式</param>
        /// <param name="replacement_value">替换表达式</param>
        /// <returns>SQL语句</returns>
        public string ISNULL(string check_expression, string replacement_value, bool isval = false)
        {
            switch (_DriverType)
            {
                case DriverType.oracle: return "NVL({0},{1})".ToFormat(check_expression, replacement_value);
                case DriverType.mysql: return "IFNULL({0},{1})".ToFormat(isval ? "{0}{1}".ToFormat(Prefix, check_expression) : check_expression, replacement_value);
                default: return "ISNULL({0},{1})".ToFormat(isval ? "{0}{1}".ToFormat(Prefix, check_expression) : check_expression, replacement_value);
            }
        }

        /// <summary>
        /// 声明变量函数
        /// </summary>
        /// <param name="_var">变量名称</param>
        /// <param name="_vartype">变量类型</param>
        /// <returns>返回定义T-SQL语句</returns>
        public string Declare(string _var, DbDataType _vartype)
        {
            switch (_DriverType)
            {
                case DriverType.oracle:
                    return "declare {0} {1};".ToFormat(_var, DataType(_vartype));
                case DriverType.mysql: return "";
                default:
                    return "declare {0}{1} {2} ".ToFormat(Prefix, _var, DataType(_vartype));
            }
        }

        /// <summary>
        /// SQL语句使用一个参数字段函数，如MAX,COUNT等
        /// </summary>
        /// <param name="_var">变量名称</param>
        /// <param name="_field">读取字段</param>
        /// <param name="_table">表名</param>
        /// <param name="_fun">函数类型</param>
        /// <returns>函数SQL语句</returns>
        public string Funcation(string _var, string _field, string _table, DbFunName _fun)
        {
            switch (_DriverType)
            {
                case DriverType.oracle:
                    return "select {3}({0}) into {1} from {2};".ToFormat(_field, _var, _table, _fun.ToString());
                case DriverType.mysql:
                    return "select {4}({2}) into {0}{1} from {3};".ToFormat(Prefix, _var, _field, _table, _fun.ToString());
                default:
                    return "select {0}{1}={4}({2}) from {3}".ToFormat(Prefix, _var, _field, _table, _fun.ToString());
            }
        }

        /// <summary>
        /// 赋值操作（T-SQL里面 set用法）
        /// </summary>
        /// <param name="_var">变量名称</param>
        /// <param name="_express">赋值表达式</param>
        /// <returns>SET函数SQL语句</returns>
        public string Set(string _var, string _express)
        {
            switch (_DriverType)
            {
                case DriverType.oracle: return "{0}{1}={2};".ToFormat(_var, Prefix, _express);
                case DriverType.mysql: return "set {0}{1}={2};".ToFormat(Prefix, _var, _express);
                default:
                    return "set {0}{1}={2}".ToFormat(Prefix, _var, _express);
            }
        }

        /// <summary>
        /// 查询语法
        /// </summary>
        /// <param name="fields_expression">查询字段表达式</param>
        /// <param name="_table">表名</param>
        /// <param name="where_expression">条件表达式</param>
        /// <param name="_onlyval">仅有参数</param>
        /// <returns>查询T-SQL语句</returns>
        public string Select(string fields_expression, string _table, string where_expression, bool isval = false, bool _onlyval = false)
        {
            if (_onlyval)
            {
                switch (_DriverType)
                {
                    case DriverType.oracle:
                        return "select {0} {1};".ToFormat(fields_expression, string.IsNullOrEmpty(_table) ? "" : "as {0}".ToFormat(_table));
                    case DriverType.mysql:
                        return "select {0}{1} {2};".ToFormat(Prefix, fields_expression, string.IsNullOrEmpty(_table) ? "" : "as {0}".ToFormat(_table));
                    default:
                        return "select {0}{1} {2}".ToFormat(Prefix, fields_expression, string.IsNullOrEmpty(_table) ? "" : "as {0}".ToFormat(_table));
                }
            }
            switch (_DriverType)
            {
                case DriverType.oracle:
                    return "select {0} from {1} {2};".ToFormat(fields_expression, _table,
                    string.IsNullOrEmpty(where_expression) ? "" : "where {0}".ToFormat(where_expression));
                case DriverType.mysql:
                    return "select {0} from {1} {2};".ToFormat(
                        isval ? "{0}{1}".ToFormat(Prefix, fields_expression) : fields_expression, _table,
                    string.IsNullOrEmpty(where_expression) ? "" : "where {0}".ToFormat(where_expression));
                default:
                    return "select {0} from {1} {2}".ToFormat(
                        isval ? "{0}{1}".ToFormat(Prefix, fields_expression) : fields_expression, _table,
                    string.IsNullOrEmpty(where_expression) ? "" : "where {0}".ToFormat(where_expression));
            }
        }

        /// <summary>
        /// 插入表达式
        /// </summary>
        /// <param name="_table">插入表名</param>
        /// <param name="insert_expression">插入字段表达式</param>
        /// <param name="value_expression">值表达式</param>
        /// <returns>插入SQL语句</returns>
        public string Insert(string _table, string insert_expression, string value_expression)
        {
            switch (_DriverType)
            {
                case DriverType.oracle:
                case DriverType.mysql:
                    return "insert into {0}({1})values({2});".ToFormat(_table, insert_expression, value_expression);
                default:
                    return "insert into {0}({1})values({2})".ToFormat(_table, insert_expression, value_expression);
            }
        }

        /// <summary>
        /// 删除SQL语句
        /// </summary>
        /// <param name="_table">表名</param>
        /// <param name="where_expression">查询表达式</param>
        /// <returns>删除语句</returns>
        public string Delete(string _table, string where_expression)
        {
            switch (_DriverType)
            {
                case DriverType.oracle:
                case DriverType.mysql:
                    return "delete from {0} {1};".ToFormat(_table, where_expression);
                default:
                    return "delete from {0} {1}".ToFormat(_table, where_expression);
            }
        }

        /// <summary>
        /// 分页查询自增排序读取
        /// </summary>
        /// <param name="table"></param>
        /// <param name="orderfield"></param>
        /// <param name="isdesc"></param>
        /// <param name="wherestr"></param>
        /// <returns></returns>
        public string RowNumber(string table, string orderfield, bool isdesc, string wherestr, string selfields = "*")
        {
            switch (_DriverType)
            {
                case DriverType.mysql:
                    return "select (@row_number:=@row_number + 1) as rno, {4} from {0},(SELECT @row_number:=0) AS t {1} ORDER BY {2} {3}".ToFormat(
                        table, wherestr, orderfield, isdesc ? "desc" : "asc", selfields == "*" ? "{0}.*".ToFormat(table) : selfields);
                default:
                    return "select row_number() over(order by {0} {1}) as rno, {4} from {2} {3}".ToFormat(orderfield, isdesc ? "desc" : "asc", table, wherestr, selfields);
            }
        }

        /// <summary>
        /// 创建表语法
        /// </summary>
        /// <param name="tablename">创建表名</param>
        /// <returns></returns>
        public string CreateTable(string tablename)
        {
            switch (_DriverType)
            {
                case DriverType.oracle:
                    return "create table {0}(auto_no number not null, primary key(auto_no));".ToFormat(tablename);
                case DriverType.mysql:
                    return "create table `{0}`(auto_no int not null, primary key(auto_no)) ENGINE=MyISAM  DEFAULT CHARSET=utf8;".ToFormat(tablename);
                default:
                    return "create table [{0}](auto_no int not null primary key)".ToFormat(tablename);
            }
        }

        /// <summary>
        /// 添加字段方法
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="fieldname">字段名</param>
        /// <param name="_vartype">字段类型</param>
        /// <param name="len">字段长度</param>
        /// <returns></returns>
        public string AddField(string tablename, string fieldname, DbDataType _vartype, string len)
        {
            string lenstr = "";
            if (_vartype != DbDataType.INT) lenstr = "({0})".ToFormat(len);
            switch (_DriverType)
            {
                case DriverType.mysql:
                case DriverType.oracle:
                    return "alter table {0} add {1} {2}{3};".ToFormat(tablename, fieldname, DataType(_vartype), lenstr);
                default:
                    return "alter table {0} add {1} {2}{3}".ToFormat(tablename, fieldname, DataType(_vartype), lenstr);
            }
        }

        /// <summary>
        /// 添加字段方法
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="fieldname">字段名</param>
        /// <param name="_vartype">字段类型</param>
        /// <param name="len">字段长度</param>
        /// <returns></returns>
        public string AddField(string tablename, string fieldname, DbDataType _vartype)
        {
            switch (_DriverType)
            {
                case DriverType.mysql:
                case DriverType.oracle:
                    return "alter table {0} add {1} {2};".ToFormat(tablename, fieldname, DataType(_vartype));
                default:
                    return "alter table {0} add {1} {2}".ToFormat(tablename, fieldname, DataType(_vartype));
            }
        }

        /// <summary>
        /// 修改字段类型
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="fieldname">字段名</param>
        /// <param name="_vartype">字段类型</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public string ModifyField(string tablename, string fieldname, DbDataType _vartype, string len)
        {
            string lenstr = "";
            if (_vartype != DbDataType.INT) lenstr = "({0})".ToFormat(len);
            switch (_DriverType)
            {
                case DriverType.mysql:
                case DriverType.oracle:
                    return "alter table {0} modify column {1} {2}{3};".ToFormat(tablename, fieldname, DataType(_vartype), lenstr);
                default:
                    return "alter table {0} alter column {1} {2}{3}".ToFormat(tablename, fieldname, DataType(_vartype), lenstr);
            }
        }

        /// <summary>
        /// 修改字段类型
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="fieldname">字段名</param>
        /// <param name="_vartype">字段类型</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public string ModifyField(string tablename, string fieldname, DbDataType _vartype)
        {
            switch (_DriverType)
            {
                case DriverType.mysql:
                case DriverType.oracle:
                    return "alter table {0} modify column {1} {2};".ToFormat(tablename, fieldname, DataType(_vartype));
                default:
                    return "alter table {0} alter column {1} {2}".ToFormat(tablename, fieldname, DataType(_vartype));
            }
        }

        /// <summary>
        /// 修改字段名
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="fieldname">字段名称</param>
        /// <returns></returns>
        public string DelField(string tablename, string fieldname)
        {
            switch (_DriverType)
            {
                case DriverType.mysql:
                case DriverType.oracle:
                    return "alter table {0} drop {1};".ToFormat(tablename, fieldname);
                default:
                    return "alter table {0} drop {1}".ToFormat(tablename, fieldname);
            }
        }

        /// <summary>
        /// 多条SQL语句组合
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string MultiSql(params string[] sql)
        {
            StringBuilder sqlText = new StringBuilder();

            foreach (var item in sql)
            {
                switch (_DriverType)
                {
                    case DriverType.mysql:
                    case DriverType.oracle:
                        sqlText.AppendFormat("{0};{1}", item, Environment.NewLine);
                        break;
                    default:
                        sqlText.AppendFormat("{0}{1}", item, Environment.NewLine);
                        break;
                }
            }
            return sqlText.ToString();
        }

        /// <summary>
        /// 创建表索引
        /// </summary>
        /// <param name="tablename"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public string CreateIndex(string tablename, string fields)
        {
            switch (_DriverType)
            {
                case DriverType.mysql:
                case DriverType.oracle:
                    return "CREATE INDEX IX_{0} ON {0}()".ToFormat(tablename, fields);
                default:
                    return "CREATE NONCLUSTERED INDEX IX_{0} ON dbo.{0}({1}) WITH(FILLFACTOR = 70)".ToFormat(tablename, fields);
            }
        }
    }
}
