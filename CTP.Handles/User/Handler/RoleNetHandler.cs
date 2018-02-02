using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.User
{
    public class RoleNetHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_role_net"; } }

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
                dic.Add("kdt_role_net", "where role_key=[@key] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_role_net", "where role_key=[@key] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_role_net", "where role_key=[@key]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("SelectByPath", "select * from kdt_role a inner join kdt_role_net b on a.auto_no=b.role_key where parent_key=[@pid]");
                dic.Add("SelectThisType", "select * from kdt_role_net where role_key=[@key]");
                dic.Add("SelectIdByPid", "select * from kdt_role_net where parent_key=[@pid]");

                //删除部门节点以及角色对应的信息权限
                dic.Add("DelRoleById", Adapter.MultiSql("delete from kdt_role_net where role_key=[@key]"
                    , "delete from kdt_role where auto_no=[@key]"
                    , "delete from kdt_role_user where role_key=[@key]"
                    , "delete from kdt_role_auth where role_key=[@key]"
                    , "delete from role_user_auth where role_key=[@key]"));

                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _rolekey = new KdtFeildEx() { TableName = "kdt_role_net", FeildName = "role_key", IsKey = true, IsIncr = true };
        public KdtFeildEx key { get { return _rolekey; } set { _rolekey = value; } }

        private KdtFeildEx _pkey  = new KdtFeildEx() { TableName = "kdt_role_net", FeildName = "parent_key", IsKey = true };
        public KdtFeildEx pid  { get { return _pkey; } set { _pkey = value; } }

        private KdtFeildEx _path = new KdtFeildEx() { TableName = "kdt_role_net", FeildName = "role_path" };
        public KdtFeildEx path { get { return _path; } set { _path = value; } }

        private KdtFeildEx _type = new KdtFeildEx() { TableName = "kdt_role_net", FeildName = "role_type" };
        public KdtFeildEx type { get { return _type; } set { _type = value; } }

        private KdtFeildEx _order = new KdtFeildEx() { TableName = "kdt_role_net", FeildName = "role_order" };
        public KdtFeildEx order { get { return _order; } set { _order = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_role_net", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_role_net", FeildName = "create_time" };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
