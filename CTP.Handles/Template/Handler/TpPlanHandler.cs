using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Template
{ 
    public class TpPlanHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_tp_plan"; } }

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
                dic.Add("kdt_tp_plan", "where task_name=[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_plan", "where task_name=[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_plan", "where task_name=[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("GetById", "select * from kdt_tp_plan where task_name =[@id]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_tp_plan", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _tname = new KdtFeildEx() { TableName = "kdt_tp_plan", FeildName = "task_name" };
        public KdtFeildEx id { get { return _tname; } set { _tname = value; } }

        private KdtFeildEx _type = new KdtFeildEx() { TableName = "kdt_tp_plan", FeildName = "plan_type" };
        public KdtFeildEx type { get { return _type; } set { _type = value; } }

        private KdtFeildEx _stime = new KdtFeildEx() { TableName = "kdt_tp_plan", FeildName = "start_time" };
        public KdtFeildEx stime { get { return _stime; } set { _stime = value; } }

        private KdtFeildEx _repeat = new KdtFeildEx() { TableName = "kdt_tp_plan", FeildName = "is_repeat" };
        public KdtFeildEx isrepeat { get { return _repeat; } set { _repeat = value; } }

        private KdtFeildEx _imunite = new KdtFeildEx() { TableName = "kdt_tp_plan", FeildName = "interval_munite" };
        public KdtFeildEx repeatmuni { get { return _imunite; } set { _imunite = value; } }

        private KdtFeildEx _tmunite = new KdtFeildEx() { TableName = "kdt_tp_plan", FeildName = "interval_total_munite" };
        public KdtFeildEx totalmuni { get { return _tmunite; } set { _tmunite = value; } }

        private KdtFeildEx _expired = new KdtFeildEx() { TableName = "kdt_tp_plan", FeildName = "has_expired" };
        public KdtFeildEx isex { get { return _expired; } set { _expired = value; } }

        private KdtFeildEx _etime = new KdtFeildEx() { TableName = "kdt_tp_plan", FeildName = "expired_time" };
        public KdtFeildEx extime { get { return _etime; } set { _etime = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_tp_plan", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_tp_plan", FeildName = "create_time" ,FeildValue=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")};
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
