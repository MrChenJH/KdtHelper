using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Flow
{
    public class KdtWfPositionHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_wf_position"; } }

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
                dic.Add("kdt_wf_position", " where auto_no=[AutoNo] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_position", " where auto_no=[AutoNo] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_wf_position", " where auto_no=[AutoNo]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("SelectAllInfo", "select * from kdt_wf_position ");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_wf_position", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx Autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _position_id = new KdtFeildEx() { TableName = "kdt_wf_position", FeildName = "position_id" };
        public KdtFeildEx PositionId  { get { return _position_id; } set { _position_id = value; } }

        private KdtFeildEx _position_name = new KdtFeildEx() { TableName = "kdt_wf_position", FeildName = "position_name" };
        public KdtFeildEx PositionName { get { return _position_name; } set { _position_name = value; } }

        private KdtFeildEx _is_sys = new KdtFeildEx() { TableName = "kdt_wf_position", FeildName = "is_sys" };
        public KdtFeildEx IsSys { get { return _is_sys; } set { _is_sys = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_wf_position", FeildName = "creator" };
        public KdtFeildEx Creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_wf_position", FeildName = "create_time",FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx CTime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
