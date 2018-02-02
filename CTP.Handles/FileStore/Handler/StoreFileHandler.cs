using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Common;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.FileStore
{

    /// <summary>
    /// 存储文件表操作类
    /// </summary>
    public  class StoreFileHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "sys_store_file"; } }

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
                dic.Add("sys_store_file", "where auto_no=[@autono] ");
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
                dic.Add("sys_store_file", "where auto_no=[@autono] ");
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
                dic.Add("sys_store_file", " where auto_no=[@autono] ");
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
                //分页查询所有的存储文件信息
                int filetype = type.FeildValue.Convert(0);     //文件类型
                string sql = "";
                if (filetype >= 0)
                    sql = " and  file_type = " + filetype;
                dic.Add("selectTotalPage", "select COUNT(1)  from sys_store_file  where  file_name  like '%[name]%' and  node_id = [@id] "+ sql + " ");
                dic.Add("selectPage", "select * from(" + Adapter.RowNumber(" sys_store_file  tb ", "tb.auto_no", false, " where  file_name  like '%[name]%' and  node_id = [@id] " + sql + "  ", " tb.* ") + ") ta where ta.rno BETWEEN @start and @end ");
                //删除存储文件信息
                dic.Add("DelStoreFile", Adapter.MultiSql(" delete  from  sys_store_file  where auto_no  in ([autono]) ", "  "));
                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _id = new KdtFeildEx() { TableName = "sys_store_file", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _id; } set { _id = value; } }

        private KdtFeildEx _nodeid = new KdtFeildEx() { TableName = "sys_store_file", FeildName = "node_id" };
        public KdtFeildEx id { get { return _nodeid; } set { _nodeid = value; } }

        private KdtFeildEx _fileid = new KdtFeildEx() { TableName = "sys_store_file", FeildName = "file_id" };
        public KdtFeildEx fid { get { return _fileid; } set { _fileid = value; } }

        private KdtFeildEx _filename = new KdtFeildEx() { TableName = "sys_store_file", FeildName = "file_name" };
        public KdtFeildEx name { get { return _filename; } set { _filename = value; } }

        private KdtFeildEx _filetype = new KdtFeildEx() { TableName = "sys_store_file", FeildName = "file_type" };
        public KdtFeildEx  type { get { return _filetype; } set { _filetype = value; } }

        private KdtFeildEx _filepath = new KdtFeildEx() { TableName = "sys_store_file", FeildName = "file_path" };
        public KdtFeildEx path { get { return _filepath; } set { _filepath = value; } }

        private KdtFeildEx _filestatus = new KdtFeildEx() { TableName = "sys_store_file", FeildName = "file_status" };
        public KdtFeildEx status { get { return _filestatus; } set { _filestatus = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "sys_store_file", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "sys_store_file", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }

        private KdtFeildEx _changetime = new KdtFeildEx() { TableName = "sys_store_file", FeildName = "change_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx changetime { get { return _changetime; } set { _changetime = value; } }


        #endregion

    }
}
