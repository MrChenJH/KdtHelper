using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Index
{
    public class StaticOperateHandler: KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "static_operate"; } }
        
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
                dic.Add("static_operate", "where user_id=[@uid] and operate_user = [@ouser]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_operate", "where  user_id=[@uid] and operate_user = [@ouser] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_operate", "where  user_id=[@uid] and operate_user = [@ouser]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                
                // 查询语句
                dic.Add("GetAll", "select * from static_operate ");
                //分页
                dic.Add("selectPage", "select * from ( " + Adapter.RowNumber("(select a.*,b.user_classify as btype,c.userprop1 as address,c.userprop2 as linkman, " +
                                            " d.area_name aname from static_operate a left " +
                                            " join kdt_user b on a.user_id = b.user_id " +
                                            " left join kdt_user_extend c on b.user_id = c.user_id " +
                                            " left join static_area d on b.user_classify = d.area_type [uid])" +
                                           " as ta", "ta.create_time", true, "", "ta.*") + ")tb" +
                                           " where tb.rno between @start and @end");
                dic.Add("selectTotalPage", "select count(1) from static_operate a left " +
                                             " join kdt_user b on a.user_id = b.user_id " +
                                             " left join kdt_user_extend c on b.user_id = c.user_id " +
                                             " left join static_area d on b.user_classify = d.area_type [uid]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "static_operate", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _user_id = new KdtFeildEx() { TableName = "static_operate", FeildName = "user_id" };
        public KdtFeildEx uid  { get { return _user_id; } set { _user_id = value; } }

        private KdtFeildEx _ouser = new KdtFeildEx() { TableName = "static_operate", FeildName = "operate_user" };
        public KdtFeildEx ouser { get { return _ouser; } set { _ouser = value; } }

        private KdtFeildEx _optype = new KdtFeildEx() { TableName = "static_operate", FeildName = "optype" };
        public KdtFeildEx type { get { return _optype; } set { _optype = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "static_operate", FeildName = "create_time",FeildValue=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
