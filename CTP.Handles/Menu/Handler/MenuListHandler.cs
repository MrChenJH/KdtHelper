using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Menu
{
    public class MenuListHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "sys_menu_list"; } }

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
                dic.Add("sys_menu_list", "where auto_no=[@autono]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("sys_menu_list", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("sys_menu_list", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                #region 不带权限
                dic.Add("SelectList", "select * from sys_menu_list where parent_id = 0");
                dic.Add("SelectListByTabId", "select * from sys_menu_list where is_leaf = [leaf] and parent_id = 0");
                dic.Add("SelectListByPId", "select * from sys_menu_list where parent_id = [pid]");
                #endregion
                #region 带权限
                dic.Add("SelectAuthList", "select b.* from " +
                                        " (select * from sys_menu_auth  where map_id in ( select role_key from kdt_role_user where user_id = [@name]) and map_type = 0) a " +
                                        " join " +
                                        " (select * from sys_menu_list where parent_id = 0) b " +
                                        " on a.menu_id = b.auto_no");
                dic.Add("SelectAuthListByTabId", "select b.* from " +
                                    " (select * from sys_menu_auth  where map_id in ( select role_key from kdt_role_user where user_id = [@name]) and map_type = 0) a " +
                                    " join " +
                                    " (select * from sys_menu_list where is_leaf = [leaf] and  parent_id = 0) b " +
                                    " on a.menu_id = b.auto_no");
                dic.Add("SelectAuthListByPId", "select b.* from " +
                                       " (select * from sys_menu_auth  where map_id in ( select role_key from kdt_role_user where user_id = [@name]) and map_type = 0) a " +
                                       "  join " +
                                       " (select * from sys_menu_list where parent_id = [pid]) b " +
                                       " on a.menu_id = b.auto_no");
                #endregion
                dic.Add("SelectAllTab", "select b.*,a.* from  " +
                                       " (select s.is_leaf as is_leaf1 from sys_menu_list s  " +
                                         "inner join sys_menu_top t on s.is_leaf = t.auto_no  " +
                                          "group by is_leaf) a " +
                                          "left join(select * from sys_menu_list where parent_id = 0 ) b " +
                                          "on a.is_leaf1 = b.is_leaf");
                dic.Add("DeleteMenu", Adapter.MultiSql("delete from sys_menu_list where auto_no=[@autono]"
                    , "delete from sys_menu_auth where menu_id = [@autono]"));
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "sys_menu_list", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _pid = new KdtFeildEx() { TableName = "sys_menu_list", FeildName = "parent_id" };
        public KdtFeildEx pid { get { return _pid; } set { _pid = value; } }

        private KdtFeildEx _name = new KdtFeildEx() { TableName = "sys_menu_list", FeildName = "m_name" };
        public KdtFeildEx name { get { return _name; } set { _name = value; } }

        private KdtFeildEx _leaf = new KdtFeildEx() { TableName = "sys_menu_list", FeildName = "is_leaf" };
        public KdtFeildEx leaf { get { return _leaf; } set { _leaf = value; } }

        private KdtFeildEx _link = new KdtFeildEx() { TableName = "sys_menu_list", FeildName = "m_link" };
        public KdtFeildEx link { get { return _link; } set { _link = value; } }

        private KdtFeildEx _icon = new KdtFeildEx() { TableName = "sys_menu_list", FeildName = "m_icon" };
        public KdtFeildEx icon { get { return _icon; } set { _icon = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "sys_menu_list", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "sys_menu_list", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
