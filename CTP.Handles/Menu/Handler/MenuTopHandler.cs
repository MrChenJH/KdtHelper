using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Menu
{
    public class MenuTopHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "sys_menu_top"; } }

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
                dic.Add("sys_menu_top", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("sys_menu_top", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("sys_menu_top", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                dic.Add("SelectAllTab", "select * from sys_menu_top order by orderby");
                dic.Add("SelectAuthTab", "select b.* from  ( select * from sys_menu_auth  where map_id in ( select role_key from kdt_role_user  " +
                            " where user_id = [@name]) and map_type = 0 and menu_id = 0) a  " +
                            " left join sys_menu_top b on a.top_id = b.auto_no ");
                dic.Add("SelectOrderTab", "select * from sys_menu_top order by orderby desc");
                dic.Add("SelectMaxOrder", "select * from sys_menu_top order by orderby desc");
                dic.Add("DeleteTab", Adapter.MultiSql("delete from sys_menu_list where is_leaf=[@autono]"
                    , "delete from sys_menu_top where auto_no = [@autono]"
                    , "delete from sys_menu_auth where top_id = [@autono]"));
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "sys_menu_top", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _icon = new KdtFeildEx() { TableName = "sys_menu_top", FeildName = "m_icon" };
        public KdtFeildEx icon { get { return _icon; } set { _icon = value; } }

        private KdtFeildEx _name = new KdtFeildEx() { TableName = "sys_menu_top", FeildName = "m_name" };
        public KdtFeildEx name { get { return _name; } set { _name = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "sys_menu_top", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "sys_menu_top", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        private KdtFeildEx _orderby = new KdtFeildEx() { TableName = "sys_menu_top", FeildName = "orderby" };
        public KdtFeildEx order { get { return _orderby; } set { _orderby = value; } }
        #endregion.
    }
}
