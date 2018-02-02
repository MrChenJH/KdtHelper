using KdtHelper.Core.ExecuterEx;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.User
{

    /// <summary>
    /// 角色权限表操作类
    /// </summary>
    public class RoleAuthHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_role_auth"; } }

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
                dic.Add("kdt_role_auth", "where role_key =[@key]  and  aitem_id =[@id]  ");
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
                dic.Add("kdt_role_auth", "where role_key = [@key] and  aitem_id =[@id] ");
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
                dic.Add("kdt_role_auth", "where role_key = [@key] and  aitem_id =[@id] ");
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
                //根据权限项Id查询角色权限信息
               // dic.Add("GetRoleAuthByAitemId", " select r.role_id , r.role_nick  from  kdt_role_auth a  left join  kdt_role r on a.role_key = r.auto_no  where aitem_id =[@AitemId] ");
                // 根据权限项Id查询角色权限信息分页查询
                dic.Add("GetRoleAuthPageCount", "select COUNT(1)  from  kdt_role r left join  kdt_role_auth a  on a.role_key = r.auto_no  where aitem_id =[@id] and  (r.role_id like '%[key]%' or r.role_nick like '%[key]%') ");
                dic.Add("GetRoleAuthPage", "select * from(" + Adapter.RowNumber("(select r.*  from kdt_role  r  left join  kdt_role_auth ra on ra.role_key = r.auto_no  where  ra.aitem_id  =[@id] and  (r.role_id like '%[key]%' or r.role_nick like '%[key]%') ) as tb", "tb.auto_no", true, "", "tb.auto_no, tb.role_id , tb.role_nick") + ") ta where ta.rno BETWEEN @start and @end ");
                //删除角色权限
                dic.Add("DelRoleAuth", Adapter.MultiSql(" delete  from  kdt_role_auth  where aitem_id =[@id] and  role_key in ([key]) ", "  "));
                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _rolekey = new KdtFeildEx() { TableName = "kdt_role_auth", FeildName = "role_key", IsKey = true};
        public KdtFeildEx key { get { return _rolekey; } set { _rolekey = value; } }

        private KdtFeildEx _aitemid = new KdtFeildEx() { TableName = "kdt_role_auth", FeildName = "aitem_id", IsKey = true };
        public KdtFeildEx id { get { return _aitemid; } set { _aitemid = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_role_auth", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "kdt_role_auth", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }


        #endregion


    }
}
