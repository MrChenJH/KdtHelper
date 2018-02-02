using KdtHelper.Core.ExecuterEx;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.User
{
    /// <summary>
    /// 用户权限表操作类
    /// </summary>
    public  class UserAuthHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_user_auth"; } }

        #endregion

        #region 关系及条件

        /// <summary>
        /// 关联字段设置
        /// </summary>
        protected override Dictionary<string, string> _relationFields
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                return dic;
            }
        }

        /// <summary>
        /// 更新条件语法
        /// </summary>
        protected override Dictionary<string, string> _UpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_user_auth", " where  aitem_id =[@Id] and  user_id =[@uid] ");
                return dic;
            }
        }

        /// <summary>
        /// 插入或更新方法条件语法
        /// </summary>
        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_user_auth", " where  aitem_id =[@Id] and  user_id =[@uid] ");
                return dic;
            }
        }

        /// <summary>
        /// 删除方法条件语法
        /// </summary>
        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_user_auth", "where  aitem_id =[@Id] and  user_id =[@uid] ");
                return dic;
            }
        }

        /// <summary>
        /// T-SQL查询语句
        /// </summary>
        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                //根据权限项Id查询用户权限信息
                //dic.Add("GetUserAuthByAitemId", " select u.user_id ,u.user_nick  from kdt_user  u  left join  kdt_user_auth a on u.user_id = a.user_id  where  aitem_id  =[@AitemId] ");
                // 根据权限项Id查询用户权限信息分页查询
                dic.Add("GetUserAuthPageCount", "select COUNT(1)  from kdt_user  u  left join  kdt_user_auth a on u.user_id = a.user_id  where  aitem_id  =[@id] and ( u.user_nick like '%[uid]%' or u.user_id like '%[uid]%') ");
                dic.Add("GetUserAuthPage", "select * from(" + Adapter.RowNumber("(select u.*  from kdt_user  u  left join  kdt_user_auth ua on u.user_id = ua.user_id  where  ua.aitem_id  = [@id] and  ( u.user_nick like '%[uid]%' or u.user_id like '%[uid]%') ) as tb", "tb.auto_no", true, "", "tb.user_id , tb.user_nick") + ") ta where ta.rno BETWEEN @start and @end ");
                //删除用户权限
                dic.Add("DelUserAuth", Adapter.MultiSql(" delete  from  kdt_user_auth  where aitem_id =[@id] and  user_id in ([uid]) ", "  "));
                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _aitemid = new KdtFeildEx() { TableName = "kdt_user_auth", FeildName = "aitem_id", IsKey = true };
        public KdtFeildEx id { get { return _aitemid; } set { _aitemid = value; } }

        private KdtFeildEx _userid = new KdtFeildEx() { TableName = "kdt_user_auth", FeildName = "user_id" , IsKey = true };
        public KdtFeildEx uid { get { return _userid; } set { _userid = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_user_auth", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "kdt_user_auth", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }


        #endregion

    }
}
