using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Index
{
    public class StaticExcelDataHandler: KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "static_excel_data"; } }
        
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
                dic.Add("static_excel_data", " where user_id = [@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_excel_data", " where user_id = [@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_excel_data", " where user_id = [@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();

                // 查询语句
                dic.Add("GetAll", "select a.user_id,b.base_info,b.work_achive,b.service_able,b.inst_garan,b.other,b.create_time, " +
                                   " d.area_name, c.userprop3, c.userprop4, c.userprop5, c.userprop8, c.user_type from kdt_user a "+
                                   " left join static_excel_data b on a.user_id = b.user_id "+
                                   " left join kdt_user_extend c on a.user_id = c.user_id "+
                                   " left join static_area d on a.user_classify = d.area_type "+
                                   " where a.user_id not like '%审核员' and a.user_id not like '评估中心%' "+
                                   " and a.user_id not like '专家%' and a.user_id != 'admin' and a.user_id != '市级管理员'");
                dic.Add("GetById", "select * from static_excel_data where user_id = [@uid]");

                dic.Add("GetCheckScoreTotal", "select a.*,b.orderby,b.value from "+
                                            " (select b.user_id, b.base_info, b.work_achive, b.service_able, b.inst_garan, b.other, b.create_time, " +
                                            " d.area_name, c.userprop3, c.userprop4, c.userprop5, c.userprop6, c.userprop8, c.user_type from static_excel_data b " +
                                            " left join kdt_user a on a.user_id = b.user_id " +
                                            "  left join kdt_user_extend c on a.user_id = c.user_id " +
                                            "  left join static_area d on a.user_classify = d.area_type " +
                                            " where a.user_id not like '%审核员' and a.user_id not like '评估中心%' " +
                                            " and a.user_id not like '专家%' and a.user_id != 'admin' and a.user_id != '市级管理员') a " +
                                            " left join (select t.user_id, c.orderby, c.value from kdt_user t " +
                                            " left join (select ROW_NUMBER() OVER(ORDER BY b.value desc) AS orderby, b.* from( " +
                                            " select id_leaf, sum(cast(a.c_value as int)) value from " +
                                            " (select * from[eb54337fb2589139] where auto_no in  " +
                                            " (select(auto_no + 1) as auto_no from[eb54337fb2589139] where (c_value = '2016市级' or c_value = '2017市级')  and id_leaf in (select id_leaf from [FDC52455623851E5] where resourceprop5 = 0 ) )) a " +
                                            " group by a.id_leaf)b)c on t.user_id = c.id_leaf )b " +
                                            " on a.user_id = b.user_id  order by b.value desc");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "static_excel_data", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _user_id = new KdtFeildEx() { TableName = "static_excel_data", FeildName = "user_id" };
        public KdtFeildEx uid  { get { return _user_id; } set { _user_id = value; } }

        private KdtFeildEx _base_info = new KdtFeildEx() { TableName = "static_excel_data", FeildName = "base_info" };
        public KdtFeildEx binfo { get { return _base_info; } set { _base_info = value; } }

        private KdtFeildEx _work_achive = new KdtFeildEx() { TableName = "static_excel_data", FeildName = "work_achive" };
        public KdtFeildEx work { get { return _work_achive; } set { _work_achive = value; } }

        private KdtFeildEx _service_able = new KdtFeildEx() { TableName = "static_excel_data", FeildName = "service_able" };
        public KdtFeildEx service { get { return _service_able; } set { _service_able = value; } }

        private KdtFeildEx _inst_garan = new KdtFeildEx() { TableName = "static_excel_data", FeildName = "inst_garan" };
        public KdtFeildEx inst { get { return _inst_garan; } set { _inst_garan = value; } }

        private KdtFeildEx _other = new KdtFeildEx() { TableName = "static_excel_data", FeildName = "other" };
        public KdtFeildEx other { get { return _other; } set { _other = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "static_excel_data", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "static_excel_data", FeildName = "create_time",FeildValue=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
