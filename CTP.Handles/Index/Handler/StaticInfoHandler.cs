using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Index
{
    public class StaticInfoHandler: KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "static_info"; } }
        
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
                dic.Add("static_info", "where auto_no=[autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_info", "where auto_no=[autono]  ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_info", "where auto_no=[autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                
                // 查询语句
                dic.Add("GetAll", "select * from static_info ");
                dic.Add("GetCategory", " select parant_name,min(auto_no) autono from static_info group by parant_name order by autono");

                dic.Add("GetIndexCategory", "select * from static_info where parant_name = [@pname] order by orderby");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "static_info", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _parant_name = new KdtFeildEx() { TableName = "static_info", FeildName = "parant_name" };
        public KdtFeildEx pname  { get { return _parant_name; } set { _parant_name = value; } }

        private KdtFeildEx _name = new KdtFeildEx() { TableName = "static_info", FeildName = "name" };
        public KdtFeildEx name { get { return _name; } set { _name = value; } }

        private KdtFeildEx _name_coded = new KdtFeildEx() { TableName = "static_info", FeildName = "name_code" };
        public KdtFeildEx ncode { get { return _name_coded; } set { _name_coded = value; } }

        private KdtFeildEx _obj_name = new KdtFeildEx() { TableName = "static_info", FeildName = "obj_name" };
        public KdtFeildEx oname { get { return _obj_name; } set { _obj_name = value; } }

        private KdtFeildEx _obj_type = new KdtFeildEx() { TableName = "static_info", FeildName = "obj _type" };
        public KdtFeildEx otype { get { return _obj_type; } set { _obj_type = value; } }

        private KdtFeildEx _order = new KdtFeildEx() { TableName = "static_info", FeildName = "orderby" };
        public KdtFeildEx order { get { return _order; } set { _order = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "static_info", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "static_info", FeildName = "create_time",FeildValue=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
