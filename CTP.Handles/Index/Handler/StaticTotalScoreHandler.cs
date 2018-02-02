using KdtHelper.Common;
using KdtHelper.Core.ExecuterEx;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Index
{
    public  class StaticTotalScoreHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "static_total_score"; } }

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
                dic.Add("static_total_score", "where user_id = [@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_total_score", "where user_id = [@uid]  ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_total_score", "where user_id=[@uid]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();

                ////分页查询语句
                //dic.Add("selectPage", "select * from(" + Adapter.RowNumber(" static_total_score ", "  a.create_time ", true, " a left join kdt_user b on a.user_id = b.user_id left join kdt_user_extend c on b.user_id = c.user_id " +
                //                                      " left join static_area d on b.user_classify = d.area_type  [basis] ", "  a.auto_no,a.user_id,a.basis_score,a.additional_score,a.yearly_score, d.area_name,c.user_type  ")
                //                                      + " )ta where rno between @start and @end");
                //dic.Add("selectTotalPage", "select count(1) from static_total_score a left join kdt_user b on a.user_id = b.user_id  " +
                //                             " left join kdt_user_extend c on b.user_id = c.user_id  left join static_area d on b.user_classify = d.area_type [basis] ");

                //获取专家平均分
                dic.Add("GetExpertAvgScore", "select cast(round(avg(cast(total as numeric(8,1))),1) as numeric(8,1)) from static_expert_score  where user_id = [@uid] ");
                //获取所有专家评分
                //dic.Add("GetExpertById", " select * from static_expert_score  where user_id = [@uid] ");
                dic.Add("GetExpertById", " SELECT [user_id], cast(round(avg(cast(total as numeric(8,1))),1) as numeric(8,1)) AS 'avgtotal' ," +
                                        " SUM(CASE expert WHEN '施强华' THEN total ELSE 0 END) AS 'expert1', " +
                                        " SUM(CASE expert WHEN '王建平' THEN total ELSE 0 END) AS 'expert2'," +
                                        " SUM(CASE expert WHEN '杨平' THEN total ELSE 0 END) AS 'expert3', " +
                                        " SUM(CASE expert WHEN '章文峻' THEN total ELSE 0 END) AS 'expert4', " +
                                        " SUM(CASE expert WHEN '吴瑞虎' THEN total ELSE 0 END) AS 'expert5', " +
                                        " SUM(CASE expert WHEN '刘文斌' THEN total ELSE 0 END) AS 'expert6', " +
                                        " SUM(CASE expert WHEN '彭芳' THEN total ELSE 0 END) AS 'expert7' " +
                                        " FROM static_expert_score where user_id = [@uid]  GROUP BY user_id");
                //分页查询语句
                var _isdesc = (isdesc.FeildValue.Convert("") == "1") ? true:false;
                dic.Add("selectPage", "select * from(" + Adapter.RowNumber(" ( select e.*, (cast(additional_score as numeric(8,1)) + basis + expert_avg) as total , (cast(additional_score as numeric(8,1)) + basis + expert_avg) as all_total from "
                                      + " (select d.*, cast(round(cast(basis_score as numeric(8, 1)) / bate, 2) as numeric(38, 2)) as basis "
                                      + "  from (select a.*, k.area_name , f.user_classify, h.expert1, h.expert2, h.expert3, h.expert4, h.expert5, h.expert6, h.expert7, b.user_type,"
                                      + "   CASE b.user_type WHEN 0 THEN 0.7 ELSE 1 END 'bate', CASE WHEN c.expert_avg is null THEN 0 ELSE c.expert_avg END 'expert_avg' from static_total_score a left join kdt_user_extend b on a.user_id = b.user_id   "
                                      + "  left join kdt_user f on a.user_id = f.user_id  left join static_area k on  f.user_classify = k.area_type  left join (  "
                                      + "  SELECT[user_id], SUM(CASE expert WHEN '施强华' THEN total ELSE 0 END) AS 'expert1', "
                                      + "      SUM(CASE expert WHEN '王建平' THEN total ELSE 0 END) AS 'expert2',  "
                                      + "    SUM(CASE expert WHEN '杨平' THEN total ELSE 0 END) AS 'expert3', "
                                      + "   SUM(CASE expert WHEN '章文峻' THEN total ELSE 0 END) AS 'expert4', "
                                      + "   SUM(CASE expert WHEN '吴瑞虎' THEN total ELSE 0 END) AS 'expert5', "
                                      + "   SUM(CASE expert WHEN '刘文斌' THEN total ELSE 0 END) AS 'expert6', "
                                      + "   SUM(CASE expert WHEN '彭芳' THEN total ELSE 0 END) AS 'expert7' "
                                      + "   FROM static_expert_score  GROUP BY user_id ) h on a.user_id = h.user_id left join  "
                                      + "  (select user_id, cast(round(avg(cast(total as numeric(8, 1))), 1) as numeric(8, 1)) "
                                      + "  as expert_avg from static_expert_score group by user_id) c on a.user_id = c.user_id) d) e [uid] ) as ta", " [creator] ", _isdesc, "  ", " * ")
                                                      + " )tb where tb.rno between @start and @end");

                dic.Add("selectTotalPage", " select count(1) from "
                                      + " (select d.*, cast(round(cast(basis_score as numeric(8, 1)) / bate, 2) as numeric(38, 2)) as basis "
                                      + "  from (select a.*,k.area_name ,f.user_classify, h.expert1, h.expert2, h.expert3, h.expert4, h.expert5, h.expert6, h.expert7, b.user_type,"
                                      + "   CASE b.user_type WHEN 0 THEN 0.7 ELSE 1 END 'bate', c.expert_avg from static_total_score a left join kdt_user_extend b on a.user_id = b.user_id  "
                                      + "  left join kdt_user f on a.user_id = f.user_id   left join static_area k on  f.user_classify = k.area_type   left join (  "
                                      + "  SELECT[user_id], SUM(CASE expert WHEN '施强华' THEN total ELSE 0 END) AS 'expert1', "
                                      + "      SUM(CASE expert WHEN '王建平' THEN total ELSE 0 END) AS 'expert2',  "
                                      + "    SUM(CASE expert WHEN '杨平' THEN total ELSE 0 END) AS 'expert3', "
                                      + "   SUM(CASE expert WHEN '章文峻' THEN total ELSE 0 END) AS 'expert4', "
                                      + "   SUM(CASE expert WHEN '吴瑞虎' THEN total ELSE 0 END) AS 'expert5', "
                                      + "   SUM(CASE expert WHEN '刘文斌' THEN total ELSE 0 END) AS 'expert6', "
                                      + "   SUM(CASE expert WHEN '彭芳' THEN total ELSE 0 END) AS 'expert7' "
                                      + "   FROM static_expert_score  GROUP BY user_id ) h on a.user_id = h.user_id left join  "
                                      + "  (select user_id, cast(round(avg(cast(total as numeric(8, 1))), 1) as numeric(8, 1)) "
                                      + "  as expert_avg from static_expert_score group by user_id) c on a.user_id = c.user_id) d) e  [uid] ");


                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "static_total_score", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _user_id = new KdtFeildEx() { TableName = "static_total_score", FeildName = "user_id" };
        public KdtFeildEx uid { get { return _user_id; } set { _user_id = value; } }

        private KdtFeildEx _basis = new KdtFeildEx() { TableName = "static_total_score", FeildName = "basis_score" };
        public KdtFeildEx basis { get { return _basis; } set { _basis = value; } }

        private KdtFeildEx _additional = new KdtFeildEx() { TableName = "static_total_score", FeildName = "additional_score" };
        public KdtFeildEx additional { get { return _additional; } set { _additional = value; } }

        private KdtFeildEx _yearly = new KdtFeildEx() { TableName = "static_total_score", FeildName = "yearly_score" };
        public KdtFeildEx yearly { get { return _yearly; } set { _yearly = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "static_total_score", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "static_total_score", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        private KdtFeildEx _prop = new KdtFeildEx() { TableName = "static_total_score", FeildName = "prop" };
        public KdtFeildEx prop { get { return _prop; } set { _prop = value; } }

        private KdtFeildEx _isdesc = new KdtFeildEx() { TableName = "static_total_score", FeildName = "isdesc" };
        public KdtFeildEx isdesc { get { return _isdesc; } set { _isdesc = value; } }

        #endregion.


    }
}
