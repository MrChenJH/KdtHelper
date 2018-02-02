using KdtHelper.Common;
using KdtHelper.Core.ExecuterEx;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{
    /// <summary>
    /// 指标行操作类
    /// </summary>
    public  class IndexRowHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "index_row"; } }

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
                dic.Add("index_row", " where auto_no =[@id]");
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
                dic.Add("index_row", " where auto_no =[@id]");
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
                dic.Add("index_row", " where auto_no =[@id] ");
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
                dic.Add("GetById", "select * from  index_row  where  auto_no = [@id]");
                //查询所有数据信息
                dic.Add("GetAll", "select * from  index_row");
                #endregion

                //判断某对象下是否存在该行指标名称
                dic.Add("CheckIsExist", "select *  from  index_row  where obj_name = [@objname]  and row_name = [@rowname] and auto_no<>[@id] ");
                //通过数据对象名查询所有行信息
                dic.Add("GetAllRowByName", "select *  from  index_row  where obj_name = [@objname] ");
                //通过父ID查询其指标信息
                dic.Add("GetRowByPid", "select *  from  index_row  where p_id = [@id] ");
                //通过数据对象名查询该指标行列信息
                dic.Add("GetAllIndexByName", "select auto_no,p_id,row_code,NULL as 'col_code',row_name,NULL as 'col_name',is_stat,stat_columns ,NULL as 'stat_way', '0' as 'is_auto_no',NULL as 'unit_list'  from  index_row  where obj_name = [@objname]" +
                                             " UNION select auto_no,p_id,NULL as 'row_code',col_code,NULL as 'row_name',col_name,is_stat, NULL as 'stat_columns', stat_way,is_auto_no,unit_list from  index_column  where obj_name = [@objname] ");
                //删除指标行
                dic.Add("DelRowById", Adapter.MultiSql(" delete from index_row  where auto_no in ([id]) " 
                                                        , " delete from `" + objname.FeildValue.Convert("").ToMD5_16() + "`  where row_code in ([rowcode]) "));

                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _id = new KdtFeildEx() { TableName = "index_row", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx id { get { return _id; } set { _id = value; } }

        private KdtFeildEx _pid = new KdtFeildEx() { TableName = "index_row", FeildName = "p_id" };
        public KdtFeildEx pid { get { return _pid; } set { _pid = value; } }

        private KdtFeildEx _objname = new KdtFeildEx() { TableName = "index_row", FeildName = "obj_name" };
        public KdtFeildEx objname { get { return _objname; } set { _objname = value; } }

        private KdtFeildEx _rowname = new KdtFeildEx() { TableName = "index_row", FeildName = "row_name" };
        public KdtFeildEx name { get { return _rowname; } set { _rowname = value; } }

        private KdtFeildEx _rowcode = new KdtFeildEx() { TableName = "index_row", FeildName = "row_code" };
        public KdtFeildEx code { get { return _rowcode; } set { _rowcode = value; } }

        private KdtFeildEx _isstat = new KdtFeildEx() { TableName = "index_row", FeildName = "is_stat" };
        public KdtFeildEx stat { get { return _isstat; } set { _isstat = value; } }

        private KdtFeildEx _statcolumns = new KdtFeildEx() { TableName = "index_row", FeildName = "stat_columns" };
        public KdtFeildEx columns { get { return _statcolumns; } set { _statcolumns = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "index_row", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "index_row", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }


        #endregion

    }
}
