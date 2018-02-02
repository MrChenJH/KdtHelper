using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Flow
{
    public class KdtWfHistoryHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_wf_history"; } }

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
                dic.Add("kdt_wf_history", "where auto_no=[Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_history", "where auto_no=[Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_history", "where auto_no=[Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("SelectAllInfo", "select * from kdt_wf_history");
                dic.Add("SelectHistoryById", "select * from kdt_wf_history where instance_id = [@InstanceId]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_wf_history", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx Autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _instance_id = new KdtFeildEx() { TableName = "kdt_wf_history", FeildName = "instance_id" };
        public KdtFeildEx InstanceId  { get { return _instance_id; } set { _instance_id = value; } }

        private KdtFeildEx _step_id = new KdtFeildEx() { TableName = "kdt_wf_history", FeildName = "step_id" };
        public KdtFeildEx StepId { get { return _step_id; } set { _step_id = value; } }

        private KdtFeildEx _action_id = new KdtFeildEx() { TableName = "kdt_wf_history", FeildName = "action_id" };
        public KdtFeildEx ActionId { get { return _action_id; } set { _action_id = value; } }

        private KdtFeildEx _step_emp_id = new KdtFeildEx() { TableName = "kdt_wf_history", FeildName = "step_emp_id" };
        public KdtFeildEx StepEmpid { get { return _step_emp_id; } set { _step_emp_id = value; } }

        private KdtFeildEx _action_time = new KdtFeildEx() { TableName = "kdt_wf_history", FeildName = "action_time",FeildValue=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ActionTime { get { return _action_time; } set { _action_time = value; } }

        private KdtFeildEx _step_note = new KdtFeildEx() { TableName = "kdt_wf_history", FeildName = "step_note" };
        public KdtFeildEx StepNote { get { return _step_note; } set { _step_note = value; } }

        private KdtFeildEx _action_status = new KdtFeildEx() { TableName = "kdt_wf_history", FeildName = "action_status" };
        public KdtFeildEx ActionStatus { get { return _action_status; } set { _action_status = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_wf_history", FeildName = "creator" };
        public KdtFeildEx Creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_wf_history", FeildName = "create_time",FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx CTime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
