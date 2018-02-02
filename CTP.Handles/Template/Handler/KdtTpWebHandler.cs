using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Template
{
    public class KdtTpWebHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_tp_web"; } }

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
                dic.Add("kdt_tp_web", " where web_name = [@id]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_web", " where web_name = [@id]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_web", " where web_name = [@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("GetById", "select * from kdt_tp_web where web_name = [@id]");
                dic.Add("CheckByName", "select count(1) from kdt_tp_web where web_name = [@id]");
                dic.Add("GetListByKey", "select * from kdt_tp_web where web_name like '%[id]%'");
                dic.Add("GetAll", "select * from kdt_tp_web ");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_tp_web", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _web_name = new KdtFeildEx() { TableName = "kdt_tp_web", FeildName = "web_name" };
        public KdtFeildEx id { get { return _web_name; } set { _web_name = value; } }

        private KdtFeildEx _web_root = new KdtFeildEx() { TableName = "kdt_tp_web", FeildName = "web_root" };
        public KdtFeildEx root { get { return _web_root; } set { _web_root = value; } }

        private KdtFeildEx _table_note = new KdtFeildEx() { TableName = "kdt_tp_web", FeildName = "table_note" };
        public KdtFeildEx note { get { return _table_note; } set { _table_note = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_tp_web", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_tp_web", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
