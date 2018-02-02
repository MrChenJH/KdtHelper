using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;
using KdtHelper.Common;

namespace CTP.Handles.User
{
    public class RoleUserHandler: KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_role_user"; } }

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
                dic.Add("kdt_role_user", "where role_key=[@key] and user_id=[@uid]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_role_user", "where role_key=[@key] and user_id=[@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_role_user", "where role_key =[@key] and user_id=[@uid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();

                int classify = position.FeildValue.Convert(0);     //对象类别
                string sql = "";
                if (classify != 0)
                {
                    sql = " and  tb.user_classify = " + classify;
                }
                #region   SQlServer版
                //分页查询角色用户信息
                dic.Add("selectTotalPage", " select count(1)  from  kdt_role_user ta left join  kdt_user tb on  ta.user_id = tb.user_id  where ta.role_key = [@key]  and  tb.user_nick like '%[uid]%' " + sql +"  ");
                dic.Add("selectPage", "select * from(" + Adapter.RowNumber("kdt_role_user ", " ta.create_time ", true, "  ta  left join  kdt_user tb on  ta.user_id = tb.user_id  where ta.role_key = [@key]  and  tb.user_nick like '%[uid]%'  " + sql + "  ", " ta.role_key ,tb.user_id ,tb.user_nick ,tb.user_classify ")
                    + " )ta where rno between @start and @end");
                #endregion
                //判断该角色用户是否存在
                dic.Add("CheckByName", "select count(1) from kdt_role_user where role_key =[@key] and user_id=[@uid]");
                //删除角色用户
                dic.Add("DeleteRUser", Adapter.MultiSql(" delete from kdt_role_user where role_key =[@key] and  user_id in ([uid]) ", " "));     

                // 查询语句
                dic.Add("SelectUser", "select * from kdt_user where (user_id like '%[uid]%' or user_nick like '%[uid]%') and " +
                        "user_id not in (select user_id from kdt_role_user where role_key = [@key])");
                dic.Add("SelectRUser", "select * from kdt_role_user where role_key =[@key] and user_id=[@uid]");
                dic.Add("SelectUSerByRoleId", "select ta.*,tb.user_nick from ( select a.*,b.role_nick from kdt_role_user a left JOIN kdt_role b on a.role_key = b.auto_no )ta LEFT JOIN kdt_user tb on ta.user_id = tb.user_id where role_key =[@key]");
                //dic.Add("SelectByRUser", "");
                dic.Add("SelectByRUser", "select ta.*,tb.user_nick from ( select a.*,b.role_nick from kdt_role_user a left JOIN kdt_role b on a.role_key = b.auto_no )ta LEFT JOIN kdt_user tb on ta.user_id = tb.user_id" +
                                             " where(ta.user_id like '%[uid]%') or(tb.user_nick like '%[uid]%')");
               
                //dic.Add("GetUserPage", "select * from ( " + Adapter.RowNumber("(select ta.*,tb.user_nick from ( select a.*,b.role_nick from kdt_role_user a left JOIN kdt_role b on a.role_key = b.auto_no )ta LEFT JOIN kdt_user tb on ta.user_id = tb.user_id where role_key =[@key])" +
                //    " as tc", "tc.role_key", true, "", "tc.role_key,tc.user_id,tc.user_position,tc.role_nick,tc.user_nick") + ")td" +
                //    " where td.rno between @start and @end");
                //dic.Add("GetUserPageCount", "select Count(1) from kdt_role_user where role_key = [@key]");
                dic.Add("GetUserPage", "select * from ( " + Adapter.RowNumber("(select ta.*,tb.user_nick from ( select a.*,b.role_nick from kdt_role_user a left JOIN kdt_role b on a.role_key = b.auto_no )ta LEFT JOIN kdt_user tb on ta.user_id = tb.user_id where role_key =[@key] [uid])" +
                    " as tc", "tc.role_key", true, "", "tc.role_key,tc.user_id,tc.user_position,tc.role_nick,tc.user_nick") + ")td" +
                    " where td.rno between @start and @end");
                dic.Add("GetUserPageCount", "select Count(1) from (select ta.*,tb.user_nick from kdt_role_user ta LEFT JOIN kdt_user tb on ta.user_id= tb.user_id where ta.role_key = [@key] [uid]) a");

                dic.Add("UpdateFrozenBase", "update kdt_role_user set user_position = 1 where role_key = [key]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _rolekey = new KdtFeildEx() { TableName = "kdt_role_user", FeildName = "role_key", IsKey = true };
        public KdtFeildEx key { get { return _rolekey; } set { _rolekey = value; } }

        private KdtFeildEx _userid = new KdtFeildEx() { TableName = "kdt_role_user", FeildName = "user_id", IsKey = true };
        public KdtFeildEx uid { get { return _userid; } set { _userid = value; } }

        private KdtFeildEx _position = new KdtFeildEx() { TableName = "kdt_role_user", FeildName = "user_position",FeildValue = "0" };
        public KdtFeildEx position { get { return _position; } set { _position = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_role_user", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_role_user", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
