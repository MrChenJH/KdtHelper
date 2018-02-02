using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.User
{
    public class RoleHandler: KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_role"; } }

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
                dic.Add("kdt_role", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_role", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_role", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                #region  新
                  //通过角色昵称查询角色信息
                  dic.Add("GetAllRole", "select * from Kdt_role  where role_nick like '%[nick]%' ");
                  dic.Add("CheckByName", "select count(1) from Kdt_role  where role_nick = [@nick]  ");
                  dic.Add("GetById", "select * from Kdt_role  where auto_no = [@autono] ");
                #endregion 


                // 查询语句
                dic.Add("SelectMaxRoleId", "select role_id from kdt_role order by cast(SUBSTRING(role_id,5,LENGTH(role_id)-4) as UNSIGNED)desc");
                dic.Add("SelectDetailById", "select * from kdt_role where auto_no = [@autono]");
                dic.Add("SelectRoleByNick", "select count(1) from kdt_role a INNER JOIN kdt_role_net b on a.auto_no = b.role_key where role_nick = [@nick] and parent_key = [@creator]");
                //dic.Add("SelectRoleByNick", "select * from kdt_role where role_nick=[@nick]");
                dic.Add("SelectByRole", "select * from kdt_role where (role_nick like '%[nick]%') and auto_no not in (select role_key from kdt_role_auth )");
                
                dic.Add("GetDeptorRole", "select a.*,b.role_key,b.parent_key,b.role_path,b.role_type,b.role_order  from kdt_role a left join kdt_role_net b on a.auto_no= b.role_key [creator]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_role", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _nick = new KdtFeildEx() { TableName = "kdt_role", FeildName = "role_nick" };
        public KdtFeildEx nick { get { return _nick; } set { _nick = value; } }

        private KdtFeildEx _note = new KdtFeildEx() { TableName = "kdt_role", FeildName = "role_note" };
        public KdtFeildEx note { get { return _note; } set { _note = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_role", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_role", FeildName = "create_time" , FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
