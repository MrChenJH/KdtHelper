using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Menu
{
    public class MenuAuthHandler : KdtFieldEntityEx
    {
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "sys_menu_auth"; } }

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
                dic.Add("sys_menu_auth", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("sys_menu_auth", "where map_id=[@mapid] and map_type=[maptype] and top_id = [topid] and menu_id =[menuid] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("sys_menu_auth", "where map_id=[@mapid] and map_type=[maptype] and top_id = [topid] and menu_id =[menuid]  ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                //获取顶部菜单权限
                dic.Add("GetTopAuthList", "select map_id, top_id from sys_menu_auth where map_id = [@mapid] and map_type = [maptype] group by top_id,map_id");
                dic.Add("GetMenuAuthList", "select map_id, menu_id from sys_menu_auth where map_id = [@mapid] and map_type = [maptype] ");
                //删除顶部菜单权限
                dic.Add("DelTopAuth", "delete from sys_menu_auth where top_id = [topid] ");
                //删除侧边菜单权限
                dic.Add("DelMenuAuth", "delete from sys_menu_auth where menu_id = [menuid] ");
                dic.Add("DelAuthByMapid", Adapter.MultiSql("delete from sys_menu_auth where map_id=[@mapid]", " "));
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "sys_menu_auth", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _map_id = new KdtFeildEx() { TableName = "sys_menu_auth", FeildName = "map_id" };
        public KdtFeildEx mapid { get { return _map_id; } set { _map_id = value; } }

        private KdtFeildEx _map_type = new KdtFeildEx() { TableName = "sys_menu_auth", FeildName = "map_type" };
        public KdtFeildEx maptype { get { return _map_type; } set { _map_type = value; } }

        private KdtFeildEx _top_id = new KdtFeildEx() { TableName = "sys_menu_auth", FeildName = "top_id" };
        public KdtFeildEx topid { get { return _top_id; } set { _top_id = value; } }

        private KdtFeildEx _menu_id = new KdtFeildEx() { TableName = "sys_menu_auth", FeildName = "menu_id" };
        public KdtFeildEx menuid { get { return _menu_id; } set { _menu_id = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "sys_menu_auth", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "sys_menu_auth", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
