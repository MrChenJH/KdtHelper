using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Flow
{
    public class KdtWfStepHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_wf_step"; } }

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
                dic.Add("kdt_wf_step", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_step", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_step", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("SelectStepByFlowId", "select a.*,b.action_name from kdt_wf_step a left join kdt_wf_action b " +
                                               " on a.action_id = b.action_id where a.flow_id = '[flowid]'" +
                                               " order by a.step_id asc");
                dic.Add("GetBackStepList", "select * from kdt_wf_step where flow_id = '[flowid]' and is_multi = 0");
                dic.Add("GetPreStepList", "select * from kdt_wf_step where flow_id = '[flowid]'");
                dic.Add("GetActionStepList", "select * from kdt_wf_action ");
                dic.Add("GetMaxStepId", "select max(step_id) from kdt_wf_step where flow_id = '[flowid]'");
                dic.Add("CheckByName", "select count(1) from kdt_wf_step where flow_id = '[flowid]' and step_name = '[name]'");
                dic.Add("GetStepByAutono", "select * from kdt_wf_step where auto_no = [Autono]");
                dic.Add("GetStepListByPre", "select * from kdt_wf_step where flow_id = '[flowid]' " +
                                    "and  step_pre = '[StepPre]' union all " +
                                    "select * from kdt_wf_step where flow_id = '[flowid]' " +
                                    "and  step_pre like('%[pre],%')");
                dic.Add("GetChartsData", "select * from ( " +
                                    " select 0 auto_no, 0 step_id, '0' flow_id, '开始' step_name, -1 step_back, -1 step_pre, 1 has_next, 0 is_multi, 0 step_type, '0' action_id, '' data_temp, '' creator, '' create_time from kdt_wf_step " +
                                    " where auto_no = 1 " +
                                    " union all " +
                                    " select * from kdt_wf_step where flow_id = '[FlowId]'" +
                                    " union all " +
                                    " select 100 auto_no, 100 step_id, '0' flow_id, '结束' step_name, -1 step_back, [StepId] step_pre, 0 has_next, 0 is_multi, 0 step_type, '0' action_id, '' data_temp, '' creator, '' create_time from kdt_wf_step " +
                                    " where auto_no = 1 ) t order by step_id ");
                dic.Add("GetStepListByBack", "select * from kdt_wf_step where flow_id = '[FlowId]' order by step_id");
                //查找下一步
                dic.Add("GetNextStepList", "select * from kdt_wf_step where flow_id = '[flowid]' and step_pre =[pre] order by step_id");
                dic.Add("GetNextMultiStepList", "select * from kdt_wf_step where flow_id = '[flowid]' and step_pre like '%[pre],%' order by step_id");
                dic.Add("GetByFlowAndStep", "select * from kdt_wf_step where flow_id = '[flowid]' and step_id =[stepid] ");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _step_id = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "step_id" };
        public KdtFeildEx stepid { get { return _step_id; } set { _step_id = value; } }

        private KdtFeildEx _flow_id = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "flow_id" };
        public KdtFeildEx flowid { get { return _flow_id; } set { _flow_id = value; } }

        private KdtFeildEx _step_name = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "step_name" };
        public KdtFeildEx name { get { return _step_name; } set { _step_name = value; } }

        private KdtFeildEx _step_back = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "step_back" };
        public KdtFeildEx back { get { return _step_back; } set { _step_back = value; } }

        private KdtFeildEx _step_pre = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "step_pre" };
        public KdtFeildEx pre { get { return _step_pre; } set { _step_pre = value; } }

        private KdtFeildEx _has_next = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "has_next" };
        public KdtFeildEx hasnext { get { return _has_next; } set { _has_next = value; } }

        private KdtFeildEx _is_multi = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "is_multi" };
        public KdtFeildEx multi { get { return _is_multi; } set { _is_multi = value; } }

        private KdtFeildEx _step_type = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "step_type" };
        public KdtFeildEx type { get { return _step_type; } set { _step_type = value; } }

        private KdtFeildEx _action_id = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "action_id" };
        public KdtFeildEx actionid { get { return _action_id; } set { _action_id = value; } }

        private KdtFeildEx _data_temp = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "data_temp" };
        public KdtFeildEx temp { get { return _data_temp; } set { _data_temp = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        private KdtFeildEx _action_time = new KdtFeildEx() { TableName = "kdt_wf_step", FeildName = "action_name" };
        public KdtFeildEx actionname { get { return _action_time; } set { _action_time = value; } }
        #endregion.
    }
}
