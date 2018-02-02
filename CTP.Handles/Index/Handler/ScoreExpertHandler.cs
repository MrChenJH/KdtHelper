using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Index
{
    public class ScoreExpertHandler: KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "static_expert_score"; } }
        
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
                dic.Add("static_expert_score", " where user_id=[@uid] and expert = [@expert] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_expert_score", " where user_id=[@uid] and expert = [@expert] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_expert_score", " where user_id=[@uid] and expert = [@expert]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                
                // 查询语句
                dic.Add("GetAll", "select a.user_id,b.expert,b.work,b.brand,b.science,b.total,b.create_time,d.area_name aname from kdt_user a "+
                                    " left join(select * from static_expert_score where expert = '[expert]' ) b on a.user_id = b.user_id " +
                                    " left join kdt_user_extend c on a.user_id = c.user_id " +
                                    " left join static_area d on a.user_classify = d.area_type " +
                                    " where c.userprop8 = 1 " +
                                    " order by c.userprop9 desc");
                dic.Add("GetById", "select * from static_expert_score where user_id=[@uid] and expert = [@expert]");
                dic.Add("GetExpertTotal", "select a.user_id,b.expert,b.work,b.brand,b.science,b.total,b.create_time,d.area_name aname from static_check_total  a " +
                                            " left join "+
                                            " (select * from static_expert_score where expert = '[expert]' ) b "+
                                            " on a.user_id = b.user_id "+
                                            " left join kdt_user c on a.user_id = c.user_id "+
                                            " left join static_area d on c.user_classify = d.area_type [uid]");
                //分页查询
                dic.Add("selectPage", "select * from ( " + Adapter.RowNumber("(select a.user_id,b.expert,b.work,b.brand,b.science,b.total,b.create_time,d.area_name aname from static_check_total  a " +
                                            " left join " +
                                            " (select * from static_expert_score where expert = '[expert]' ) b " +
                                            " on a.user_id = b.user_id " +
                                            " left join kdt_user c on a.user_id = c.user_id " +
                                            " left join static_area d on c.user_classify = d.area_type [uid])" +
                                          " as ta", "ta.create_time", true, "", "ta.*") + ")tb" +
                                          " where tb.rno between @start and @end");
                dic.Add("selectTotalPage", "select count(1) from static_check_total  a " +
                                            " left join " +
                                            " (select * from static_expert_score where expert = '[expert]' ) b " +
                                            " on a.user_id = b.user_id " +
                                            " left join kdt_user c on a.user_id = c.user_id " +
                                            " left join static_area d on c.user_classify = d.area_type [uid]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "static_expert_score", FeildName = "auto_no", IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _user_id = new KdtFeildEx() { TableName = "static_expert_score", FeildName = "user_id", IsKey = true };
        public KdtFeildEx uid  { get { return _user_id; } set { _user_id = value; } }

        private KdtFeildEx _expert = new KdtFeildEx() { TableName = "static_expert_score", FeildName = "expert", IsKey = true };
        public KdtFeildEx expert { get { return _expert; } set { _expert = value; } }

        private KdtFeildEx _work = new KdtFeildEx() { TableName = "static_expert_score", FeildName = "work" };
        public KdtFeildEx work { get { return _work; } set { _work = value; } }

        private KdtFeildEx _brand = new KdtFeildEx() { TableName = "static_expert_score", FeildName = "brand" };
        public KdtFeildEx brand { get { return _brand; } set { _brand = value; } }

        private KdtFeildEx _science = new KdtFeildEx() { TableName = "static_expert_score", FeildName = "science" };
        public KdtFeildEx science { get { return _science; } set { _science = value; } }

        private KdtFeildEx _total = new KdtFeildEx() { TableName = "static_expert_score", FeildName = "total" };
        public KdtFeildEx total { get { return _total; } set { _total = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "static_expert_score", FeildName = "create_time" ,FeildValue=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")};
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
