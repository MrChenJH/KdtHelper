using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Flow
{
    public class KdtWfActionHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_wf_action"; } }

        #region 关系及条件

        protected override Dictionary<string, string> _relationFields
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                return dic;
            }
        }

        protected override Dictionary<string, string> _UpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_action", "where action_id=[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_action", "where action_id=[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_action", "where action_id=[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句  
                dic.Add("SelectChildAction", "select * from kdt_wf_action where 1=1 [name] ");
                dic.Add("SelectGroupAction", "select 0 as auto_no, '0' as action_id, action_category as action_name from kdt_wf_action where (action_category != '' and action_category is not null ) [cate] group by action_category " +
                                              "  union all " +
                                              "  select auto_no, action_id, action_name from kdt_wf_action where (action_category = '' || action_category is null) [name] ");
                dic.Add("SelectActionCategory", "select action_category from kdt_wf_action where (action_category != '' and action_category is not null ) group by action_category ");
                dic.Add("SelectByActionName", "select * from kdt_wf_action where action_category = '[name]'");

                dic.Add("GetById", "select * from kdt_wf_action where action_id = [@id] ");
                dic.Add("CheckByName", "select count(1) from kdt_wf_action where action_name = [@name] ");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _action_id = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "action_id" };
        public KdtFeildEx id { get { return _action_id; } set { _action_id = value; } }

        private KdtFeildEx _action_type = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "action_type" };
        public KdtFeildEx type { get { return _action_type; } set { _action_type = value; } }

        private KdtFeildEx _action_name = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "action_name" };
        public KdtFeildEx name { get { return _action_name; } set { _action_name = value; } }

        private KdtFeildEx _action_category = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "action_category" };
        public KdtFeildEx cate { get { return _action_category; } set { _action_category = value; } }

        private KdtFeildEx _audit_type = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "audit_type" };
        public KdtFeildEx audittype { get { return _audit_type; } set { _audit_type = value; } }

        private KdtFeildEx _audit_mapid = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "audit_mapid" };
        public KdtFeildEx auditmapid { get { return _audit_mapid; } set { _audit_mapid = value; } }

        private KdtFeildEx _audit_position_id = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "audit_position_id" };
        public KdtFeildEx auditposition { get { return _audit_position_id; } set { _audit_position_id = value; } }

        private KdtFeildEx _hasproxy = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "hasproxy" };
        public KdtFeildEx hasproxy { get { return _hasproxy; } set { _hasproxy = value; } }

        private KdtFeildEx _proxy_type = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "proxy_type" };
        public KdtFeildEx proxytype { get { return _proxy_type; } set { _proxy_type = value; } }

        private KdtFeildEx _proxy_mapid = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "proxy_mapid" };
        public KdtFeildEx proxymapid { get { return _proxy_mapid; } set { _proxy_mapid = value; } }

        private KdtFeildEx _proxy_position_id = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "proxy_position_id" };
        public KdtFeildEx proxyposition { get { return _proxy_position_id; } set { _proxy_position_id = value; } }

        private KdtFeildEx _hascopy = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "hascopy" };
        public KdtFeildEx hascopy { get { return _hascopy; } set { _hascopy = value; } }

        private KdtFeildEx _copy_type = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "copy_type" };
        public KdtFeildEx copytype { get { return _copy_type; } set { _copy_type = value; } }

        private KdtFeildEx _copy_mapid = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "copy_mapid" };
        public KdtFeildEx copymapid { get { return _copy_mapid; } set { _copy_mapid = value; } }

        private KdtFeildEx _copy_position_id = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "copy_position_id" };
        public KdtFeildEx copyposition { get { return _copy_position_id; } set { _copy_position_id = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_wf_action", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
