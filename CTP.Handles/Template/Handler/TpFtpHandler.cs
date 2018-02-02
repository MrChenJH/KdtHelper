using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Template
{
    public class TpFtpHandler : KdtFieldEntityEx
    {
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_tp_ftp"; } }

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
                dic.Add("kdt_tp_ftp", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_ftp", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_ftp", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("GetById", "select * from kdt_tp_ftp where ftp_name =[@id]");
                dic.Add("GetAll", "select * from kdt_tp_ftp");
                dic.Add("CheckByName", "select count(1) from kdt_tp_ftp where ftp_name = [@id] ");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_tp_ftp", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _fname = new KdtFeildEx() { TableName = "kdt_tp_ftp", FeildName = "ftp_name" };
        public KdtFeildEx id { get { return _fname; } set { _fname = value; } }

        private KdtFeildEx _root = new KdtFeildEx() { TableName = "kdt_tp_ftp", FeildName = "ftp_root" };
        public KdtFeildEx root { get { return _root; } set { _root = value; } }

        private KdtFeildEx _server = new KdtFeildEx() { TableName = "kdt_tp_ftp", FeildName = "ftp_server" };
        public KdtFeildEx server { get { return _server; } set { _server = value; } }

        private KdtFeildEx _port = new KdtFeildEx() { TableName = "kdt_tp_ftp", FeildName = "ftp_port" };
        public KdtFeildEx port { get { return _port; } set { _port = value; } }

        private KdtFeildEx _user = new KdtFeildEx() { TableName = "kdt_tp_ftp", FeildName = "ftp_user" };
        public KdtFeildEx user { get { return _user; } set { _user = value; } }

        private KdtFeildEx _fpwd = new KdtFeildEx() { TableName = "kdt_tp_ftp", FeildName = "ftp_pwd" };
        public KdtFeildEx pwd { get { return _fpwd; } set { _fpwd = value; } }

        private KdtFeildEx _passive = new KdtFeildEx() { TableName = "kdt_tp_ftp", FeildName = "ftp_passive" };
        public KdtFeildEx type { get { return _passive; } set { _passive = value; } }

        private KdtFeildEx _note = new KdtFeildEx() { TableName = "kdt_tp_ftp", FeildName = "ftp_note" };
        public KdtFeildEx note { get { return _note; } set { _note = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_tp_ftp", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_tp_ftp", FeildName = "create_time" ,FeildValue=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")};
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
