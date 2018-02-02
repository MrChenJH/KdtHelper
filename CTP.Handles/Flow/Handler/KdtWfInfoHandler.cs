using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Flow
{
    public class KdtWfInfoHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_wf_info"; } }

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
                dic.Add("kdt_wf_info", "where flow_id=[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_info", "where flow_id=[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_info", "where flow_id=[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("GetById", "select * from kdt_wf_info where flow_id = '[id]'");
                dic.Add("CheckByName", "select count(1) from kdt_wf_info where flow_name = '[name]'");

                dic.Add("SelectChildInfo", "select * from kdt_wf_info where 1=1 [name]");
                dic.Add("SelectInfoCategory", "select flow_category from kdt_wf_info where (flow_category != '' and flow_category is not null ) group by flow_category ");
                dic.Add("SelectGroupInfo", "select 0 as auto_no, '0' as flow_id, flow_category as flow_name,0 as is_sys from kdt_wf_info where (flow_category != '' and flow_category is not null ) [note] group by flow_category " +
                                           " union all " +
                                           " select auto_no, flow_id, flow_name, is_sys from kdt_wf_info where (flow_category = '' || flow_category is null) [name] ");
                dic.Add("SelectByFlowName", "select * from kdt_wf_info where flow_category = '[name]' and is_sys = 0 ");
                dic.Add("DelFlowInfo", Adapter.MultiSql("delete from kdt_wf_info where flow_id = [@id]", "delete from kdt_wf_step where flow_id = [@id]"));
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_wf_info", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _flow_id = new KdtFeildEx() { TableName = "kdt_wf_info", FeildName = "flow_id" };
        public KdtFeildEx id { get { return _flow_id; } set { _flow_id = value; } }

        private KdtFeildEx _flow_name = new KdtFeildEx() { TableName = "kdt_wf_info", FeildName = "flow_name" };
        public KdtFeildEx name { get { return _flow_name; } set { _flow_name = value; } }

        private KdtFeildEx _flow_category = new KdtFeildEx() { TableName = "kdt_wf_info", FeildName = "flow_category" };
        public KdtFeildEx cate { get { return _flow_category; } set { _flow_category = value; } }

        private KdtFeildEx _flow_note = new KdtFeildEx() { TableName = "kdt_wf_info", FeildName = "flow_note" };
        public KdtFeildEx note { get { return _flow_note; } set { _flow_note = value; } }

        private KdtFeildEx _is_sys = new KdtFeildEx() { TableName = "kdt_wf_info", FeildName = "is_sys" };
        public KdtFeildEx sys { get { return _is_sys; } set { _is_sys = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_wf_info", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_wf_info", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
