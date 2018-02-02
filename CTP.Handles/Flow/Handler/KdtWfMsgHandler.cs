using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Flow
{
    public class KdtWfMsgHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_wf_msg"; } }

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
                dic.Add("kdt_wf_msg", "where auto_no=[Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_msg", "where auto_no=[Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_msg", "where auto_no=[Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("SelectAllInfo", "select * from kdt_wf_msg [FlowName]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_wf_msg", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx Autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _instance_id = new KdtFeildEx() { TableName = "kdt_wf_msg", FeildName = "instance_id" };
        public KdtFeildEx InstanceId  { get { return _instance_id; } set { _instance_id = value; } }

        private KdtFeildEx _step_id = new KdtFeildEx() { TableName = "kdt_wf_msg", FeildName = "step_id" };
        public KdtFeildEx StepId { get { return _step_id; } set { _step_id = value; } }

        private KdtFeildEx _action_id = new KdtFeildEx() { TableName = "kdt_wf_msg", FeildName = "action_id" };
        public KdtFeildEx ActionId { get { return _action_id; } set { _action_id = value; } }

        private KdtFeildEx _step_rec_id = new KdtFeildEx() { TableName = "kdt_wf_msg", FeildName = "step_rec_id" };
        public KdtFeildEx StepRecid { get { return _step_rec_id; } set { _step_rec_id = value; } }

        private KdtFeildEx _send_on = new KdtFeildEx() { TableName = "kdt_wf_msg", FeildName = "send_on" };
        public KdtFeildEx SendOn { get { return _send_on; } set { _send_on = value; } }

        private KdtFeildEx _send_time = new KdtFeildEx() { TableName = "kdt_wf_msg", FeildName = "send_time" };
        public KdtFeildEx SendTime { get { return _send_time; } set { _send_time = value; } }

        private KdtFeildEx _send_status = new KdtFeildEx() { TableName = "kdt_wf_msg", FeildName = "send_status" };
        public KdtFeildEx SendStatus { get { return _send_status; } set { _send_status = value; } }

        private KdtFeildEx _read_status = new KdtFeildEx() { TableName = "kdt_wf_msg", FeildName = "read_status" };
        public KdtFeildEx ReadStatus { get { return _read_status; } set { _read_status = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_wf_msg", FeildName = "creator" };
        public KdtFeildEx Creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_wf_msg", FeildName = "create_time",FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx CTime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
