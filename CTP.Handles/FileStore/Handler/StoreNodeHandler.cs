using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Common;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.FileStore
{

    /// <summary>
    /// 存储结构表操作类
    /// </summary>
    public class StoreNodeHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "sys_store_node"; } }

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
                dic.Add("sys_store_node", "where auto_no=[@autono] ");
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
                dic.Add("sys_store_node", "where auto_no=[@autono] ");
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
                dic.Add("sys_store_node", " where auto_no=[@autono] ");
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
                #region   公共SQl语句
                //通过唯一标识查询其栏目信息
                dic.Add("GetById", " select * from  sys_store_node  where node_id = [@id] ");
                #endregion 


                //通过父ID查询其父节点生成目录
                dic.Add("GetAllStoreNode", " select * from  sys_store_node  ");
                //判断栏目名称是否存在
                dic.Add("CheckIsExist", " select count(1) from  sys_store_node  where node_name = [@name] and  auto_no <> [@autono] ");
                //通过父ID查询其父节点生成目录
                dic.Add("GetPublishRootById", " select * from  sys_store_node  where node_id = [@pid] ");
                //查询栏目存储结构
                dic.Add("GetStoreNodeByPid", " select * from  sys_store_node  where node_pid = [@pid] ");
                //删除栏目存储结构
                dic.Add("DeleteNodeById", Adapter.MultiSql(" delete  from  sys_store_node  where  node_id   in  ([autono]) "
                                                     , " delete from  sys_store_file  where  node_id  in ([autono]) "));
                
                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _id = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _id; } set { _id = value; } }

        private KdtFeildEx _nodeid = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "node_id" };
        public KdtFeildEx id { get { return _nodeid; } set { _nodeid = value; } }

        private KdtFeildEx _nodepid = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "node_pid" };
        public KdtFeildEx pid { get { return _nodepid; } set { _nodepid = value; } }

        private KdtFeildEx _nodename = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "node_name" };
        public KdtFeildEx  name { get { return _nodename; } set { _nodename = value; } }

        private KdtFeildEx _nodetype = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "node_type" };
        public KdtFeildEx type { get { return _nodetype; } set { _nodetype = value; } }

        private KdtFeildEx _uploadtype = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "upload_type" };
        public KdtFeildEx utype { get { return _uploadtype; } set { _uploadtype = value; } }

        private KdtFeildEx _uploadconfig = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "upload_config" };
        public KdtFeildEx config { get { return _uploadconfig; } set { _uploadconfig = value; } }

        private KdtFeildEx _foldertype = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "folder_type" };
        public KdtFeildEx fdtype { get { return _foldertype; } set { _foldertype = value; } }

        private KdtFeildEx _publishroot = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "publish_root" };
        public KdtFeildEx  root { get { return _publishroot; } set { _publishroot = value; } }

        private KdtFeildEx _publishurl = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "publish_url" };
        public KdtFeildEx puburl { get { return _publishurl; } set { _publishurl = value; } }

        private KdtFeildEx _previewurl = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "preview_url" };
        public KdtFeildEx preurl { get { return _previewurl; } set { _previewurl = value; } }

        private KdtFeildEx _pullencrypt = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "pull_encrypt" };
        public KdtFeildEx encrypt { get { return _pullencrypt; } set { _pullencrypt = value; } }

        private KdtFeildEx _encryptmethod = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "encrypt_method" };
        public KdtFeildEx method { get { return _encryptmethod; } set { _encryptmethod = value; } }

        private KdtFeildEx _allowchange = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "allow_change" };
        public KdtFeildEx change { get { return _allowchange; } set { _allowchange = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "sys_store_node", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }

        #endregion


    }
}
