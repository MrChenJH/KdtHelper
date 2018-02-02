using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Index
{
    public class StaticCheckTotalHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "static_check_total"; } }
        
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
                dic.Add("static_check_total", "where user_id = [@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_check_total", "where user_id = [@uid]  ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_check_total", "where user_id=[@uid]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                
                // 查询语句
                dic.Add("GetStaticTotal", "select a.*,c.area_name from static_check_total a " +
                                            " left join kdt_user b " +
                                            " on a.user_id = b.user_id " +
                                            " left join static_area c " +
                                            " on b.user_classify = c.area_type " +
                                            " where a.status in ([ctime]) and a.user_id like '%[uid]%' [atime]");
                //首页统计
                dic.Add("GetIndexTotal", "select a.status,count(1) auto_no from static_check_total a " +
                                        " left join kdt_user b  on a.user_id = b.user_id " +
                                        " left join static_area c  on b.user_classify = c.area_type [uid] " +
                                        " group by status");
                dic.Add("selectPage", "select * from ( " + Adapter.RowNumber("(select a.*, c.area_name from static_check_total a " +
                                            " left join kdt_user b " +
                                            " on a.user_id = b.user_id " +
                                            " left join static_area c " +
                                            " on b.user_classify = c.area_type " +
                                            " where a.status in ([ctime]) and a.user_id like '%[uid]%' [atime] ) " +
                                    " as ta", "ta.audit_time", true, "", "ta.*") + ")tb" +
                                    " where tb.rno between @start and @end");
                dic.Add("selectTotalPage", "select count(1) from static_check_total a " +
                                            " left join kdt_user b " +
                                            " on a.user_id = b.user_id " +
                                            " left join static_area c " +
                                            " on b.user_classify = c.area_type " +
                                            " where a.status in ([ctime]) and a.user_id like '%[uid]%' [atime]");
                dic.Add("IndexTotal", " update static_check_total set is_frozen =1 where user_id in " +
                                            " (select user_id from kdt_role_user where role_key = [status])");
                dic.Add("UpdatePassStatus", " update static_check_total set status =9 where status =5 ");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "static_check_total", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _user_id = new KdtFeildEx() { TableName = "static_check_total", FeildName = "user_id" };
        public KdtFeildEx uid  { get { return _user_id; } set { _user_id = value; } }
        //状态 1:录入人员保存 2：已提交  3：审核员审核  4：管理员及第三方审核  5：都审核通过  99：退回
        private KdtFeildEx _status = new KdtFeildEx() { TableName = "static_check_total", FeildName = "status" };
        public KdtFeildEx status { get { return _status; } set { _status = value; } }

        private KdtFeildEx _is_frozen = new KdtFeildEx() { TableName = "static_check_total", FeildName = "is_frozen" };
        public KdtFeildEx frozen { get { return _is_frozen; } set { _is_frozen = value; } }

        private KdtFeildEx _audit_time = new KdtFeildEx() { TableName = "static_check_total", FeildName = "audit_time",FeildValue=DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx atime { get { return _audit_time; } set { _audit_time = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "static_check_total", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "static_check_total", FeildName = "create_time" };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
