using KdtHelper.Core.ExecuterEx;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.User
{

    /// <summary>
    /// 角色用户权限表操作类
    /// </summary>
     public  class RoleUserAuthHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "role_user_auth"; } }

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
                dic.Add("role_user_auth", "where  role_key =[@key] and  aitem_id =[@id] and  user_id =[@uid] ");
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
                dic.Add("role_user_auth", "where role_key =[@key] and  aitem_id =[@id] and  user_id =[@uid] ");
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
                dic.Add("role_user_auth", "where role_key =[@key] and  aitem_id =[@id] and  user_id =[@uid] ");
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
                //查询某个用户的权限项
                dic.Add("GetUserAuthByUserId", " select role_key , '' as 'user_id' , aitem_id  from kdt_role_auth  where role_key in (select  role_key  from  kdt_role_user  where user_id = [@uid]) "
                                                 + " UNION  select  0  as 'role_key' , user_id , aitem_id  from kdt_user_auth  where  user_id = [@uid] "
                                                 + "  UNION select role_key ,user_id , aitem_id  from role_user_auth  where user_id = [@uid] ");
                //根据权限项Id查询角色用户权限信息
                //dic.Add("GetRoleUserAuthByAitemId", " select u.user_id ,u.user_nick , r.role_id , r.role_nick  from  role_user_auth  rua  left join  kdt_user  u  on rua.user_id = u.user_id "
                //                                   + "  left join  kdt_role_user  ru  on ru.user_id = u.user_id  left join  kdt_role  r on  r.auto_no = rua.role_key  "
                //                                   + "  where aitem_id = [@AitemId] ");

                // 根据权限项Id查询角色权限信息分页查询
                dic.Add("GetUserOfRoleAuthPageCount", " select COUNT(1) from  role_user_auth  rua  left join  kdt_user  u  on rua.user_id = u.user_id "
                                                   + "  left join  kdt_role_user  ru  on ru.user_id = u.user_id  left join  kdt_role  r on  r.auto_no = rua.role_key  "
                                                   + "  where aitem_id = [@id] and ( r.role_nick like '%[key]%'  or  u.user_nick like '%[uid]%') ");
                dic.Add("GetUserOfRoleAuthPage", "select * from(" + Adapter.RowNumber("(select r.* , u.user_id , u.user_nick  from  role_user_auth  rua  left join  kdt_user  u  on rua.user_id = u.user_id "
                                                                    + "  left join  kdt_role_user  ru  on ru.user_id = u.user_id  left join  kdt_role  r on  r.auto_no = rua.role_key  "
                                                                    + " where rua.aitem_id = [@id] and ( r.role_nick like '%[key]%'  or  u.user_nick like '%[uid]%' )   "
                                                                    + " ) as tb", "tb.auto_no", true, "", "tb.auto_no, tb.role_id , tb.role_nick , tb.user_id ,tb.user_nick") + ") ta where ta.rno BETWEEN @start and @end ");
                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _rolekey = new KdtFeildEx() { TableName = "role_user_auth", FeildName = "role_key", IsKey = true };
        public KdtFeildEx key { get { return _rolekey; } set { _rolekey = value; } }

        private KdtFeildEx _aitemid = new KdtFeildEx() { TableName = "role_user_auth", FeildName = "aitem_id", IsKey = true };
        public KdtFeildEx id { get { return _aitemid; } set { _aitemid = value; } }

        private KdtFeildEx _userid = new KdtFeildEx() { TableName = "role_user_auth", FeildName = "user_id", IsKey = true };
        public KdtFeildEx uid { get { return _userid; } set { _userid = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "role_user_auth", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "role_user_auth", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }


        #endregion

    }
}
