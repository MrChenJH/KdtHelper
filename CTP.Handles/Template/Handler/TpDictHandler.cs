using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Template
{
    public class TpDictHandler : KdtFieldEntityEx
    {
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_tp_dict"; } }

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
                dic.Add("kdt_tp_dict", "where auto_no=[@Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_dict", "where auto_no=[@Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_dict", "where auto_no=[@Autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("GetById", "select * from kdt_tp_dict where table_name =[@id]");

                dic.Add("CheckTpFtp", "select * from kdt_tp_dict where ftp_name =[@fname]");
                dic.Add("CheckUTpFtp", "select * from kdt_tp_dict where ftp_name = [@fname] and auto_no<>[@autono]");
                dic.Add("SelectAllTpDict", "select * from kdt_tp_dict");
                dic.Add("SelectTpDictById", "select * from kdt_tp_dict where auto_no =[@autono]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_tp_dict", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _tname = new KdtFeildEx() { TableName = "kdt_tp_dict", FeildName = "table_name" };
        public KdtFeildEx id { get { return _tname; } set { _tname = value; } }

        private KdtFeildEx _field = new KdtFeildEx() { TableName = "kdt_tp_dict", FeildName = "field_name" };
        public KdtFeildEx fname { get { return _field; } set { _field = value; } }

        private KdtFeildEx _fnote = new KdtFeildEx() { TableName = "kdt_tp_dict", FeildName = "field_note" };
        public KdtFeildEx note { get { return _fnote; } set { _fnote = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_tp_dict", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_tp_dict", FeildName = "create_time",FeildValue=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
