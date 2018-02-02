using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Template
{
    public class TpTaskHandler: KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_tp_task"; } }

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
                dic.Add("kdt_tp_task", "where task_name =[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_task", "where task_name =[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_task", "where task_name =[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("GetById", "select * from kdt_tp_task where task_name =[@id]");
                dic.Add("CheckByName", "select count(1) from kdt_tp_task where task_name =[@id]");
                dic.Add("GetAll", "select * from kdt_tp_task where task_status =[status] and task_name like '%[id]%'");

                dic.Add("SelectCTpTask", "select * from kdt_tp_task where 1=1 [Creator] ");
                dic.Add("SelectTpTask", "select 0 as auto_no,task_name,classify,creator,create_time from kdt_tp_task where classify <> '' [Creator] union " +
                    "select auto_no, task_name, classify, creator, create_time from kdt_tp_task where classify = ''[CTime]  ");
                dic.Add("DeleteByTaskName", Adapter.MultiSql("delete from kdt_tp_task where task_name in ([Task])"
                                            , "delete from kdt_tp_plan where task_name in ([Task])"));
                dic.Add("GetTaskPage", "select * from (" + Adapter.RowNumber("kdt_tp_task", "auto_no", true, "where 1=1 [Creator]", "*") + ")a where a.rno between @start and @end");
                dic.Add("GetTaskPageCount", "select count(1) from kdt_tp_task where 1=1 [Creator]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_tp_task", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _tname = new KdtFeildEx() { TableName = "kdt_tp_task", FeildName = "task_name" };
        public KdtFeildEx id { get { return _tname; } set { _tname = value; } }

        private KdtFeildEx _status = new KdtFeildEx() { TableName = "kdt_tp_task", FeildName = "task_status" };
        public KdtFeildEx status { get { return _status; } set { _status = value; } }

        private KdtFeildEx _type = new KdtFeildEx() { TableName = "kdt_tp_task", FeildName = "syn_type" };
        public KdtFeildEx type { get { return _type; } set { _type = value; } }

        private KdtFeildEx _script = new KdtFeildEx() { TableName = "kdt_tp_task", FeildName = "syn_script" };
        public KdtFeildEx script { get { return _script; } set { _script = value; } }

        private KdtFeildEx _cruntime = new KdtFeildEx() { TableName = "kdt_tp_task", FeildName = "current_runtime" };
        public KdtFeildEx ltime{ get { return _cruntime; } set { _cruntime = value; } }

        private KdtFeildEx _nruntime = new KdtFeildEx() { TableName = "kdt_tp_task", FeildName = "next_runtime" };
        public KdtFeildEx ntime { get { return _nruntime; } set { _nruntime = value; } }

        private KdtFeildEx _tnote = new KdtFeildEx() { TableName = "kdt_tp_task", FeildName = "table_note" };
        public KdtFeildEx note { get { return _tnote; } set { _tnote = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_tp_task", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_tp_task", FeildName = "create_time",FeildValue=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
