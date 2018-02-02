using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;
using KdtHelper.Common;

namespace CTP.Handles.User
{
    public class UserHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_user"; } }

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
                dic.Add("kdt_user", "where user_id=[@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_user", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_user", "where auto_no=[@autono]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                #region  SQLServer
                //分页查询用户信息（其中有两个userprop(X)字段写死）
                dic.Add("selectTotalPage", " select count(1)  from kdt_user ta left join kdt_user_extend tb on ta.user_id=tb.user_id left join  static_area tc on ta.user_classify = tc.area_type where ta.user_id like '%[id]%'  ");
                dic.Add("selectPage", "select * from(" + Adapter.RowNumber("kdt_user ", " ta.create_time ", true, "  ta  left join kdt_user_extend tb on ta.user_id=tb.user_id  left join  static_area tc on ta.user_classify = tc.area_type  where ta.user_id like '%[id]%'  ", " ta.user_id,ta.user_phone,tc.area_name, tb.userprop1, tb.userprop2 ")
                    + " )ta where rno between @start and @end");

                //根据用户Id查询用户信息
                dic.Add("GetById", "select * from kdt_user where user_id = [@id]");
                dic.Add("GetByName", "select top(10)* from kdt_user where user_id like '%[id]%' or user_nick like '%[id]%'");
                #endregion


                // 查询语句

                dic.Add("GetAll", "select * from kdt_user");
                dic.Add("CheckByName", " select count(1) from kdt_user where user_id = [@id] ");
                dic.Add("GetListByKey", "select * from kdt_user where user_id like '%[id]%' or user_nick like '%[id]%'");
                dic.Add("UpdateUser", Adapter.MultiSql("update kdt_user set user_id = [@id],user_nick=[@nick],user_pwd=[@pwd],user_phone = [@phone],user_classify=[@classify],user_email=[@email] where auto_no = [@autono]"
                    , "update kdt_user_extend set [creator] where user_id = [@id]"));
                dic.Add("AddUser", Adapter.MultiSql("insert into kdt_user (user_id,user_nick,user_pwd,open_id,open_source,user_phone,user_classify,user_email,creator,create_time ) values " +
                    "([@id],[@nick],[@pwd],[@openid],[@source],[@phone],[@classify],[@email],[@creator],[@ctime])", "insert into kdt_user_extend (user_id,user_type) values ([@id],[@classify])"));
                dic.Add("SelectUser", "select * from kdt_user a LEFT JOIN kdt_user_extend b on a.user_id=b.user_id [creator]");
                dic.Add("SelectAllUser", "select * from kdt_user a LEFT JOIN kdt_user_extend b on a.user_id=b.user_id ");
                dic.Add("SelectByUser", "select * from kdt_user where (user_id like '%[id]%' or user_nick like '%[id]%') and user_id not in (select user_id from kdt_user_auth)");
                dic.Add("SelectUserId", "select * from kdt_user a LEFT JOIN kdt_user_extend b on a.user_id=b.user_id  where a.user_id=[@id] and a.auto_no!=[@autono]");

                dic.Add("SelectLoginUser", "select * from kdt_user where user_id=[@id] and user_pwd = [@pwd]");

                //dic.Add("selectPage", "select * from ( " + Adapter.RowNumber("(select a.* from kdt_user a LEFT JOIN kdt_user_extend b on a.user_id = b.user_id where a.user_id like '%[id]%' or a.user_nick like '%[id]%')" +
                //    " as ta", "ta.auto_no", true, "", "ta.auto_no ,ta.user_id, ta.user_nick, ta.user_pwd, ta.open_id, ta.open_source, ta.user_phone, ta.user_classify, ta.user_email") + ")tb" +
                //    " where tb.rno between @start and @end");
                //dic.Add("selectTotalPage", "select Count(1) from kdt_user a LEFT JOIN kdt_user_extend b on a.user_id=b.user_id where a.user_id like '%[id]%' or a.user_nick like '%[id]%'");
               

                dic.Add("DeleteUser", Adapter.MultiSql("delete from kdt_user where user_id in ([id])"     //删除用户信息
                    , "delete from kdt_user_extend where user_id in ([id])"    //删除扩展表信息
                    , "delete from sys_menu_auth where map_id in ([id])"//删除用户菜单权限信息
                    , " delete from kdt_role_user where user_id in ([id]) "));   //删除角色用户信息
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_user", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _userid = new KdtFeildEx() { TableName = "kdt_user", FeildName = "user_id" };
        public KdtFeildEx id { get { return _userid; } set { _userid = value; } }

        private KdtFeildEx _nick = new KdtFeildEx() { TableName = "kdt_user", FeildName = "user_nick" };
        public KdtFeildEx nick { get { return _nick; } set { _nick = value; } }

        private KdtFeildEx _pwd = new KdtFeildEx() { TableName = "kdt_user", FeildName = "user_pwd" ,FeildValue = "123456".Convert("").ToMD5_16() };
        public KdtFeildEx pwd { get { return _pwd; } set { _pwd = value; } }

        private KdtFeildEx _openid = new KdtFeildEx() { TableName = "kdt_user", FeildName = "open_id" };
        public KdtFeildEx openid { get { return _openid; } set { _openid = value; } }

        private KdtFeildEx _source = new KdtFeildEx() { TableName = "kdt_user", FeildName = "open_source" };
        public KdtFeildEx source { get { return _source; } set { _source = value; } }

        private KdtFeildEx _phone = new KdtFeildEx() { TableName = "kdt_user", FeildName = "user_phone" };
        public KdtFeildEx phone { get { return _phone; } set { _phone = value; } }

        private KdtFeildEx _classify = new KdtFeildEx() { TableName = "kdt_user", FeildName = "user_classify" };
        public KdtFeildEx classify { get { return _classify; } set { _classify = value; } }

        private KdtFeildEx _email = new KdtFeildEx() { TableName = "kdt_user", FeildName = "user_email" };
        public KdtFeildEx email { get { return _email; } set { _email = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_user", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_user", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
