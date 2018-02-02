using KdtHelper.Common;
using KdtHelper.Core.ExecuterEx;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{
    /// <summary>
    /// 指标列操作类
    /// </summary>
    public class IndexColumnHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "index_column"; } }

        #endregion

        #region 关系及条件

        /// <summary>
        /// 关联字段设置
        /// </summary>
        protected override Dictionary<string, string> _relationFields
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                return dic;
            }
        }

        /// <summary>
        /// 更新条件语法
        /// </summary>
        protected override Dictionary<string, string> _UpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("index_column", " where auto_no =[@id] ");
                return dic;
            }
        }

        /// <summary>
        /// 插入或更新方法条件语法
        /// </summary>
        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("index_column", " where auto_no =[@id]");
                return dic;
            }
        }

        /// <summary>
        /// 删除方法条件语法
        /// </summary>
        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("index_column", " where auto_no =[@id] ");
                return dic;
            }
        }

        /// <summary>
        /// T-SQL查询语句
        /// </summary>
        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();

                #region   公共SQL语句
                //通过Id查询所有数据信息
                dic.Add("GetById", "select * from  index_column  where  auto_no = [@id]");
                //查询所有数据信息
                dic.Add("GetAll", "select * from index_column");
                #endregion


                //判断某对象下是否存在该列指标名称
                dic.Add("CheckIsExist", "select *  from  index_column  where obj_name = [@objname]  and col_name = [@colname] and auto_no<>[@id] ");
                //通过数据对象名查询所有列信息
                dic.Add("GetColByName", "select *  from  index_column  where obj_name = [@objname] ");
                //通过父ID查询其指标信息
                dic.Add("GetColumnByPid", "select *  from  index_column  where p_id = [@id] ");
                //删除指标列
                dic.Add("DelColumnById", Adapter.MultiSql(" delete from index_column  where auto_no in ([id]) ",
                                                        "  delete from `" + objname.FeildValue.Convert("").ToMD5_16() + "`  where column_code  in ([colcode]) "));

                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _id = new KdtFeildEx() { TableName = "index_column", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx id { get { return _id; } set { _id = value; } }

        private KdtFeildEx _code = new KdtFeildEx() { TableName = "index_column", FeildName = "col_code" };
        public KdtFeildEx  code { get { return _code; } set { _code = value; } }

        private KdtFeildEx _pid = new KdtFeildEx() { TableName = "index_column", FeildName = "p_id" };
        public KdtFeildEx pid { get { return _pid; } set { _pid = value; } }

        private KdtFeildEx _objname = new KdtFeildEx() { TableName = "index_column", FeildName = "obj_name" };
        public KdtFeildEx objname { get { return _objname; } set { _objname = value; } }

        private KdtFeildEx _colname = new KdtFeildEx() { TableName = "index_column", FeildName = "col_name" };
        public KdtFeildEx  name { get { return _colname; } set { _colname = value; } }

        private KdtFeildEx _isstat = new KdtFeildEx() { TableName = "index_column", FeildName = "is_stat" };
        public KdtFeildEx stat { get { return _isstat; } set { _isstat = value; } }

        private KdtFeildEx _statway = new KdtFeildEx() { TableName = "index_column", FeildName = "stat_way" };
        public KdtFeildEx way { get { return _statway; } set { _statway = value; } }

        private KdtFeildEx _isautono = new KdtFeildEx() { TableName = "index_column", FeildName = "is_auto_no" };
        public KdtFeildEx isauto { get { return _isautono; } set { _isautono = value; } }

        private KdtFeildEx _unitlist = new KdtFeildEx() { TableName = "index_column", FeildName = "unit_list" };
        public KdtFeildEx unit { get { return _unitlist; } set { _unitlist = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "index_column", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "index_column", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }


        #endregion


    }
}
