using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.User
{
    public class EmployHandler: KdtFieldEntityEx
    {
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_employ"; } }

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
                dic.Add("kdt_employ", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_employ", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_employ", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("UpdateEmploy", Adapter.MultiSql("update kdt_employ set employ_id = [@id],employ_nick=[@nick],employ_sex=[@sex],employ_birthday = [@birth],employ_native=[@native],birthday_place=[@place] where auto_no = [@autono]"
                    , "update kdt_employ_extend set [creator] where user_id = [@id]"));
                dic.Add("AddEmploy", Adapter.MultiSql("insert into kdt_employ (employ_id,employ_nick,employ_sex,employ_birthday,employ_native,birthday_place,creator,create_time ) values " +
                    "([@id],[@nick],[@sex],[@birth],[@native],[@place],[@creator],[@ctime])", "insert into kdt_employ_extend (user_id,user_type) values ([@id],1)"));
                dic.Add("DeleteEmploy", "delete from kdt_employ where employ_id in ([nick])");
                dic.Add("SelectByEmployId", "select count(1) from kdt_employ a LEFT JOIN kdt_employ_extend b on a.employ_id=b.user_id  where a.employ_id=[@id]");
                dic.Add("SelectAllEmploy", "select * from kdt_employ a LEFT JOIN kdt_employ_extend b on a.employ_id=b.user_id ");
                dic.Add("SelectByEmploy", "select * from kdt_employ a LEFT JOIN kdt_employ_extend b on a.employ_id=b.user_id where employ_id like '%[id]%' or employ_nick like '%[id]%'");
                dic.Add("SelectEmployId", "select * from kdt_employ a LEFT JOIN kdt_employ_extend b on a.employ_id=b.user_id  where a.employ_id=[@id] and a.auto_no!=[@autono]");
                dic.Add("GetEmployPageCount", "select Count(1) from kdt_employ a LEFT JOIN kdt_employ_extend b on a.employ_id=b.user_id [creator]");
                dic.Add("GetEmployPage", "select * from( "+  Adapter.RowNumber("(select a.* from kdt_employ a LEFT JOIN kdt_employ_extend b on a.employ_id = b.user_id  [creator]) as ta",
                    "ta.auto_no",true,"","ta.auto_no, ta.employ_id, ta.employ_nick, ta.employ_sex, ta.employ_birthday, ta.employ_native, ta.birthday_place")+")tb where tb.rno between @start and @end");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_employ", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _eid = new KdtFeildEx() { TableName = "kdt_employ", FeildName = "employ_id" };
        public KdtFeildEx id  { get { return _eid; } set { _eid = value; } }

        private KdtFeildEx _nick = new KdtFeildEx() { TableName = "kdt_employ", FeildName = "employ_nick" };
        public KdtFeildEx nick { get { return _nick; } set { _nick = value; } }

        private KdtFeildEx _sex = new KdtFeildEx() { TableName = "kdt_employ", FeildName = "employ_sex" };
        public KdtFeildEx sex { get { return _sex; } set { _sex = value; } }

        private KdtFeildEx _birthday = new KdtFeildEx() { TableName = "kdt_employ", FeildName = "employ_birthday" };
        public KdtFeildEx birth { get { return _birthday; } set { _birthday = value; } }

        private KdtFeildEx _native = new KdtFeildEx() { TableName = "kdt_employ", FeildName = "employ_native" };
        public KdtFeildEx native { get { return _native; } set { _native = value; } }

        private KdtFeildEx _place = new KdtFeildEx() { TableName = "kdt_employ", FeildName = "birthday_place" };
        public KdtFeildEx place { get { return _place; } set { _place = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_employ", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_employ", FeildName = "create_time" };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
