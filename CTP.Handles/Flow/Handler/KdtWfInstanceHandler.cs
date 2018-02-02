using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Flow
{
    public class KdtWfInstanceHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_wf_instance"; } }

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
                dic.Add("kdt_wf_instance", "where instance_id = '[id]' ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_instance", "where instance_id = '[id]' ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_instance", "where instance_id = '[id]' ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("GetById", "select * from kdt_wf_instance where instance_id = '[id]'");
                dic.Add("CheckByName", "select count(1) from kdt_wf_instance where instance_name = '[name]'");
                //分页流程实例
                dic.Add("SelectAllInstanceCount", "select Count(1) from kdt_wf_instance where instance_status = [status] [name]");
                dic.Add("SelectAllInstance", "select * from ( " + Adapter.RowNumber(" kdt_wf_instance ", "create_time", true, " where instance_status = [status] [name]", "*") + ")tb" +
                   " where tb.rno between @start and @end");
                //查询实例执行进度
                dic.Add("GetInstanceTotalProc", "select a.*,b.step_emp_id,b.action_status,b.action_time from "
                          + "  (select a.instance_id, a.instance_name, b.flow_id, b.flow_name, c.step_id, c.step_name, c.action_id, d.action_name "
                          + "  from kdt_wf_instance a "
                          + "  inner join kdt_wf_info b on a.flow_id = b.flow_id "
                          + "  left join kdt_wf_step c on b.flow_id = c.flow_id "
                          + "  left join kdt_wf_action d on c.action_id = d.action_id "
                          + "  where a.instance_id = '[id]') a "
                          + "  left join "
                          + "  (select * from kdt_wf_proc where instance_id = '[id]') b "
                          + "  on  a.step_id = b.step_id and  a.instance_id = b.instance_id order by step_id ");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_wf_instance", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _instance_id = new KdtFeildEx() { TableName = "kdt_wf_instance", FeildName = "instance_id" };
        public KdtFeildEx id { get { return _instance_id; } set { _instance_id = value; } }

        private KdtFeildEx _instance_name = new KdtFeildEx() { TableName = "kdt_wf_instance", FeildName = "instance_name" };
        public KdtFeildEx name { get { return _instance_name; } set { _instance_name = value; } }

        private KdtFeildEx _instance_source = new KdtFeildEx() { TableName = "kdt_wf_instance", FeildName = "instance_source" };
        public KdtFeildEx source { get { return _instance_source; } set { _instance_source = value; } }

        private KdtFeildEx _map_id = new KdtFeildEx() { TableName = "kdt_wf_instance", FeildName = "map_id" };
        public KdtFeildEx mapid { get { return _map_id; } set { _map_id = value; } }

        private KdtFeildEx _flow_id = new KdtFeildEx() { TableName = "kdt_wf_instance", FeildName = "flow_id" };
        public KdtFeildEx flowid { get { return _flow_id; } set { _flow_id = value; } }

        private KdtFeildEx _step_id = new KdtFeildEx() { TableName = "kdt_wf_instance", FeildName = "step_id" };
        public KdtFeildEx stepid { get { return _step_id; } set { _step_id = value; } }

        private KdtFeildEx _instance_status = new KdtFeildEx() { TableName = "kdt_wf_instance", FeildName = "instance_status" };
        public KdtFeildEx status { get { return _instance_status; } set { _instance_status = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_wf_instance", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_wf_instance", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
