using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Object
{
    public class ObjLogHandler: KdtFieldEntityEx
    {
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "obj_log"; } }

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
                dic.Add("obj_log", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("obj_log", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("obj_log", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "obj_log", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _objname = new KdtFeildEx() { TableName = "obj_log", FeildName = "obj_name" };
        public KdtFeildEx objname  { get { return _objname; } set { _objname = value; } }

        private KdtFeildEx _moudle = new KdtFeildEx() { TableName = "obj_log", FeildName = "oper_moudle" };
        public KdtFeildEx opermoudle { get { return _moudle; } set { _moudle = value; } }

        private KdtFeildEx _type = new KdtFeildEx() { TableName = "obj_log", FeildName = "oper_type" };
        public KdtFeildEx opertype { get { return _type; } set { _type = value; } }

        private KdtFeildEx _content = new KdtFeildEx() { TableName = "obj_log", FeildName = "oper_content" };
        public KdtFeildEx content { get { return _content; } set { _content = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "obj_log", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "obj_log", FeildName = "create_time" };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
