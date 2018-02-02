using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Index
{
    public class ScoreCheckHandler: KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "static_check_score"; } }
        
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
                dic.Add("static_check_score", " where user_id=[@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_check_score", " where user_id=[@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_check_score", " where user_id=[@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("GetById", "select * from static_check_score where user_id=[@uid]");
                dic.Add("GetCheckTotal", "select a.*,b.user_classify as btype,c.userprop1 as address,c.userprop2 as linkman, "+
                                             " d.area_name aname from static_check_score a left "+
                                             " join kdt_user b on a.user_id = b.user_id "+
                                             " left join kdt_user_extend c on b.user_id = c.user_id "+
                                             " left join static_area d on b.user_classify = d.area_type [uid]");
                //分页查询语句
                //var _isdesc = (isdesc.FeildValue.ToString() == "1")?true:false;
                //dic.Add("selectPage", "select * from ( " + Adapter.RowNumber("(select a.*,b.user_classify as btype,c.userprop1 as address,c.userprop2 as linkman, " +
                //                             " d.area_name aname from static_check_score a left " +
                //                             " join kdt_user b on a.user_id = b.user_id " +
                //                             " left join kdt_user_extend c on b.user_id = c.user_id " +
                //                             " left join static_area d on b.user_classify = d.area_type [uid])" +
                //                            " as ta", "ta.[prop]", _isdesc, "", "ta.*") + ")tb" +
                //                            " where tb.rno between @start and @end");
                //dic.Add("selectTotalPage", "select count(1) from static_check_score a left " +
                //                             " join kdt_user b on a.user_id = b.user_id " +
                //                             " left join kdt_user_extend c on b.user_id = c.user_id " +
                //                             " left join static_area d on b.user_classify = d.area_type [uid]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "static_check_score", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _user_id = new KdtFeildEx() { TableName = "static_check_score", FeildName = "user_id" };
        public KdtFeildEx uid  { get { return _user_id; } set { _user_id = value; } }

        private KdtFeildEx _sub_year = new KdtFeildEx() { TableName = "static_check_score", FeildName = "sub_year" };
        public KdtFeildEx syear { get { return _sub_year; } set { _sub_year = value; } }

        private KdtFeildEx _sub_holi = new KdtFeildEx() { TableName = "static_check_score", FeildName = "sub_holi" };
        public KdtFeildEx sholi { get { return _sub_holi; } set { _sub_holi = value; } }

        private KdtFeildEx _nignt = new KdtFeildEx() { TableName = "static_check_score", FeildName = "nignt" };
        public KdtFeildEx night { get { return _nignt; } set { _nignt = value; } }

        private KdtFeildEx _peo_num = new KdtFeildEx() { TableName = "static_check_score", FeildName = "peo_num" };
        public KdtFeildEx pnum { get { return _peo_num; } set { _peo_num = value; } }

        private KdtFeildEx _match_score = new KdtFeildEx() { TableName = "static_check_score", FeildName = "match_score" };
        public KdtFeildEx mscore { get { return _match_score; } set { _match_score = value; } }

        private KdtFeildEx _new_area = new KdtFeildEx() { TableName = "static_check_score", FeildName = "new_area" };
        public KdtFeildEx narea { get { return _new_area; } set { _new_area = value; } }

        private KdtFeildEx _new_update = new KdtFeildEx() { TableName = "static_check_score", FeildName = "new_update" };
        public KdtFeildEx nupdate { get { return _new_update; } set { _new_update = value; } }

        private KdtFeildEx _sub_visit = new KdtFeildEx() { TableName = "static_check_score", FeildName = "sub_visit" };
        public KdtFeildEx svisit { get { return _sub_visit; } set { _sub_visit = value; } }

        private KdtFeildEx _rate_visit = new KdtFeildEx() { TableName = "static_check_score", FeildName = "rate_visit" };
        public KdtFeildEx rvisit { get { return _rate_visit; } set { _rate_visit = value; } }

        private KdtFeildEx _huimin_activity = new KdtFeildEx() { TableName = "static_check_score", FeildName = "huimin_activity" };
        public KdtFeildEx hmactiv { get { return _huimin_activity; } set { _huimin_activity = value; } }

        private KdtFeildEx _kepu_activity = new KdtFeildEx() { TableName = "static_check_score", FeildName = "kepu_activity" };
        public KdtFeildEx kpactiv { get { return _kepu_activity; } set { _kepu_activity = value; } }

        private KdtFeildEx _collect_reward = new KdtFeildEx() { TableName = "static_check_score", FeildName = "collect_reward" };
        public KdtFeildEx colreward { get { return _collect_reward; } set { _collect_reward = value; } }

        private KdtFeildEx _passport = new KdtFeildEx() { TableName = "static_check_score", FeildName = "passport" };
        public KdtFeildEx pass { get { return _passport; } set { _passport = value; } }

        private KdtFeildEx _pass_use = new KdtFeildEx() { TableName = "static_check_score", FeildName = "pass_use" };
        public KdtFeildEx upass { get { return _pass_use; } set { _pass_use = value; } }

        private KdtFeildEx _total_score = new KdtFeildEx() { TableName = "static_check_score", FeildName = "total_score" };
        public KdtFeildEx tscore { get { return _total_score; } set { _total_score = value; } }

        private KdtFeildEx _create_time = new KdtFeildEx() { TableName = "static_check_score", FeildName = "create_time",FeildValue=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _create_time; } set { _create_time = value; } }


        //private KdtFeildEx _prop = new KdtFeildEx() { TableName = "static_check_score", FeildName = "prop" };
        //public KdtFeildEx prop { get { return _prop; } set { _prop = value; } }

        //private KdtFeildEx _isdesc = new KdtFeildEx() { TableName = "static_check_score", FeildName = "isdesc" };
        //public KdtFeildEx isdesc { get { return _isdesc; } set { _isdesc = value; } }
        #endregion.
    }
}
