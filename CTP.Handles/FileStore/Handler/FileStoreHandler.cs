using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Common;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.FileStore
{

    /// <summary>
    /// 文件存储表操作类
    /// </summary>
    public class FileStoreHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "sys_file_store"; } }

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
                dic.Add("sys_file_store", "where auto_no=[@id] ");
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
                dic.Add("sys_file_store", "where auto_no=[@id] ");
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
                dic.Add("sys_file_store", " where auto_no=[@id] ");
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
                //判断该文件类型是否存在
                dic.Add("CheckByName", " select * from  sys_file_store  where file_type  = [@type] and auto_no <> [@id] ");
                //查询所有文件存储信息
                dic.Add("GetAll", " select * from  sys_file_store  ");
                //通过自增编号查询文件存储信息
                dic.Add("GetById", " select * from  sys_file_store  where  auto_no  = [@id]");
                //通过文件类型查询文件存储信息
                dic.Add("GetFileStoreByType", " select * from  sys_file_store  where  file_type  = [@type]");
                //删除文件存储信息
                dic.Add("DelFileStore", Adapter.MultiSql(" delete  from  sys_file_store  where auto_no  in ([id]) ", "  "));
                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _id = new KdtFeildEx() { TableName = "sys_file_store", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx id { get { return _id; } set { _id = value; } }

        private KdtFeildEx _type = new KdtFeildEx() { TableName = "sys_file_store", FeildName = "file_type" };
        public KdtFeildEx type { get { return _type; } set { _type = value; } }

        private KdtFeildEx _storefolder = new KdtFeildEx() { TableName = "sys_file_store", FeildName = "store_folder" };
        public KdtFeildEx  folder { get { return _storefolder; } set { _storefolder = value; } }

        private KdtFeildEx _storeformat = new KdtFeildEx() { TableName = "sys_file_store", FeildName = "store_format" };
        public KdtFeildEx format { get { return _storeformat; } set { _storeformat = value; } }

        private KdtFeildEx _ischange = new KdtFeildEx() { TableName = "sys_file_store", FeildName = "is_change" };
        public KdtFeildEx change { get { return _ischange; } set { _ischange = value; } }

        private KdtFeildEx _targetsuffix = new KdtFeildEx() { TableName = "sys_file_store", FeildName = "target_suffix" };
        public KdtFeildEx suffix { get { return _targetsuffix; } set { _targetsuffix = value; } }

        private KdtFeildEx _multitarget = new KdtFeildEx() { TableName = "sys_file_store", FeildName = "multi_target" };
        public KdtFeildEx target { get { return _multitarget; } set { _multitarget = value; } }

        private KdtFeildEx _multitargetrole = new KdtFeildEx() { TableName = "sys_file_store", FeildName = "multi_target_role" };
        public KdtFeildEx role { get { return _multitargetrole; } set { _multitargetrole = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "sys_file_store", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "sys_file_store", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }


        #endregion

    }
}
