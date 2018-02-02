using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Index
{
    public class StaticExcelVesionHandler: KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "static_excel_Vesion"; } }
        
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
                dic.Add("static_excel_Vesion", " where user_id = [@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_excel_Vesion", " where user_id = [@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_excel_Vesion", " where user_id = [@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                
                // 查询语句
                dic.Add("GetById", "select * from static_excel_Vesion where user_id = [@uid]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "static_excel_Vesion", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _user_id = new KdtFeildEx() { TableName = "static_excel_Vesion", FeildName = "user_id" };
        public KdtFeildEx uid  { get { return _user_id; } set { _user_id = value; } }

        private KdtFeildEx _base_info = new KdtFeildEx() { TableName = "static_excel_Vesion", FeildName = "base_info" };
        public KdtFeildEx binfo { get { return _base_info; } set { _base_info = value; } }

        private KdtFeildEx _work_achive = new KdtFeildEx() { TableName = "static_excel_Vesion", FeildName = "work_achive" };
        public KdtFeildEx work { get { return _work_achive; } set { _work_achive = value; } }

        private KdtFeildEx _service_able = new KdtFeildEx() { TableName = "static_excel_Vesion", FeildName = "service_able" };
        public KdtFeildEx service { get { return _service_able; } set { _service_able = value; } }

        private KdtFeildEx _inst_garan = new KdtFeildEx() { TableName = "static_excel_Vesion", FeildName = "inst_garan" };
        public KdtFeildEx inst { get { return _inst_garan; } set { _inst_garan = value; } }

        private KdtFeildEx _other = new KdtFeildEx() { TableName = "static_excel_Vesion", FeildName = "other" };
        public KdtFeildEx other { get { return _other; } set { _other = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "static_excel_Vesion", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "static_excel_Vesion", FeildName = "create_time" };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        private KdtFeildEx _utime = new KdtFeildEx() { TableName = "static_excel_Vesion", FeildName = "update_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx utime { get { return _utime; } set { _utime = value; } }

        #endregion.
    }
}
