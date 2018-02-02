using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Flow.Handler
{
    public class FlowLogHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "flow_log"; } }

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
                dic.Add("flow_log", "where auto_no=[@Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("flow_log", "where auto_no=[@Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("flow_log", "where auto_no=[@Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "flow_log", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx Autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _name = new KdtFeildEx() { TableName = "flow_log", FeildName = "flow_name" };
        public KdtFeildEx FlowName { get { return _name; } set { _name = value; } }

        private KdtFeildEx _moudle = new KdtFeildEx() { TableName = "flow_log", FeildName = "oper_moudle" };
        public KdtFeildEx OperMoudle { get { return _moudle; } set { _moudle = value; } }

        private KdtFeildEx _type = new KdtFeildEx() { TableName = "flow_log", FeildName = "oper_type" };
        public KdtFeildEx OperType { get { return _type; } set { _type = value; } }

        private KdtFeildEx _content = new KdtFeildEx() { TableName = "flow_log", FeildName = "oper_content" };
        public KdtFeildEx Content { get { return _content; } set { _content = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "flow_log", FeildName = "creator" };
        public KdtFeildEx Creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "flow_log", FeildName = "create_time" };
        public KdtFeildEx CTime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
