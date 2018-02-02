using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Index
{
    public class ScoreYearHandler: KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "base_score_year"; } }
        
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
                dic.Add("base_score_year", " where user_id=[@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("base_score_year", " where user_id=[@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("base_score_year", " where user_id=[@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                
                // 查询语句
                dic.Add("GetAll", "select * from base_score_year ");

                dic.Add("GetYearTotal", "select a.*,b.user_classify as btype,c.userprop1 as address,c.userprop2 as linkman,d.area_name aname from base_score_year a " +
                                        " left join kdt_user b on a.user_id = b.user_id " +
                                        " left join kdt_user_extend c on  b.user_id = c.user_id " +
                                        " left join static_area d on b.user_classify = d.area_type [uid]");
                //分页
                dic.Add("selectPage", "select * from ( " + Adapter.RowNumber("(select a.*,b.user_classify as atype,c.user_type as btype,c.userprop1 as address,c.userprop2 as linkman,d.area_name aname from base_score_year a " +
                                        " left join kdt_user b on a.user_id = b.user_id " +
                                        " left join kdt_user_extend c on  b.user_id = c.user_id " +
                                        " left join static_area d on b.user_classify = d.area_type [uid])" +
                                          " as ta", "ta.create_time", true, "", "ta.*") + ")tb" +
                                          " where tb.rno between @start and @end");
                dic.Add("selectTotalPage", "select count(1) from base_score_year a " +
                                            " left join kdt_user b on a.user_id = b.user_id " +
                                            " left join kdt_user_extend c on  b.user_id = c.user_id " +
                                            " left join static_area d on b.user_classify = d.area_type [uid]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "base_score_year", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _user_id = new KdtFeildEx() { TableName = "base_score_year", FeildName = "user_id" };
        public KdtFeildEx uid  { get { return _user_id; } set { _user_id = value; } }

        private KdtFeildEx _year_day = new KdtFeildEx() { TableName = "base_score_year", FeildName = "year_day" };
        public KdtFeildEx yday { get { return _year_day; } set { _year_day = value; } }

        private KdtFeildEx _holi_day = new KdtFeildEx() { TableName = "base_score_year", FeildName = "holi_day" };
        public KdtFeildEx hday { get { return _holi_day; } set { _holi_day = value; } }

        private KdtFeildEx _peo_num = new KdtFeildEx() { TableName = "base_score_year", FeildName = "peo_num" };
        public KdtFeildEx pnum { get { return _peo_num; } set { _peo_num = value; } }

        private KdtFeildEx _area = new KdtFeildEx() { TableName = "base_score_year", FeildName = "area" };
        public KdtFeildEx area { get { return _area; } set { _area = value; } }

        private KdtFeildEx _update_year = new KdtFeildEx() { TableName = "base_score_year", FeildName = "update_year" };
        public KdtFeildEx uyear { get { return _update_year; } set { _update_year = value; } }

        private KdtFeildEx _tech_holi = new KdtFeildEx() { TableName = "base_score_year", FeildName = "tech_holi" };
        public KdtFeildEx tholi { get { return _tech_holi; } set { _tech_holi = value; } }

        private KdtFeildEx _sys_manager = new KdtFeildEx() { TableName = "base_score_year", FeildName = "sys_manager" };
        public KdtFeildEx smanager { get { return _sys_manager; } set { _sys_manager = value; } }

        private KdtFeildEx _sys_safe = new KdtFeildEx() { TableName = "base_score_year", FeildName = "sys_safe" };
        public KdtFeildEx ssafe { get { return _sys_safe; } set { _sys_safe = value; } }

        private KdtFeildEx _create_time = new KdtFeildEx() { TableName = "base_score_year", FeildName = "create_time" ,FeildValue =DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")};
        public KdtFeildEx ctime { get { return _create_time; } set { _create_time = value; } }

        #endregion.
    }
}
