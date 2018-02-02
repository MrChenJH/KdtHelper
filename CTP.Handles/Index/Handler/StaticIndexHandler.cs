using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.Index
{
    public class StaticIndexHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "static_index"; } }

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
                dic.Add("static_index", "where auto_no=[@autono]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_index", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("static_index", "where auto_no=[@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                // 查询语句
                #region 
                dic.Add("GetAll", "select * from static_index");
                dic.Add("GetById", "select * from static_index");

                dic.Add("GetIndexList", "select a.*,b.name from static_index a " +
                                " left join " +
                                " (select name, name_code from static_info [name] ) b " +
                                " on a.name_code = b.name_code where index_name like '%[iname]%'");
                //分页查询
                dic.Add("selectPage", "select * from ("+Adapter.RowNumber(" ( select a.*,b.name from static_index a " +
                                "  join " +
                                " (select name, name_code from static_info [name] ) b " +
                                " on a.name_code = b.name_code where index_name like '%[iname]%'","auto_no",false,"") +") ta ) tb where tb.rno between @start and @end ");
                dic.Add("selectTotalPage", "select count(1) from static_index a " +
                                "  join " +
                                " (select name, name_code from static_info [name] ) b " +
                                " on a.name_code = b.name_code where index_name like '%[iname]%'");

                dic.Add("GetByTableName", "select a.*, b.obj_feild from "+
                                " (select * from static_index where name_code = '[ncode]') a "+
                                " left join "+
                                " (select * from sys_obj_dict where obj_name = '[iname]' and obj_feild like 'resourceprop%') b "+
                                " on a.index_code = b.obj_description");
                dic.Add("GetIndexObjData", "select * from [[ncode]] where id_leaf = '[iname]'");
                #endregion
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "static_index", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _name_code = new KdtFeildEx() { TableName = "static_index", FeildName = "name_code" };
        public KdtFeildEx ncode { get { return _name_code; } set { _name_code = value; } }

        private KdtFeildEx _index_name = new KdtFeildEx() { TableName = "static_index", FeildName = "index_name" };
        public KdtFeildEx iname { get { return _index_name; } set { _index_name = value; } }

        private KdtFeildEx _index_code = new KdtFeildEx() { TableName = "static_index", FeildName = "index_code" };
        public KdtFeildEx icode { get { return _index_code; } set { _index_code = value; } }

        private KdtFeildEx _type = new KdtFeildEx() { TableName = "static_index", FeildName = "type" };
        public KdtFeildEx type { get { return _type; } set { _type = value; } }

        private KdtFeildEx _content = new KdtFeildEx() { TableName = "static_index", FeildName = "content" };
        public KdtFeildEx content { get { return _content; } set { _content = value; } }

        private KdtFeildEx _isvali = new KdtFeildEx() { TableName = "static_index", FeildName = "isValidate" };
        public KdtFeildEx isvali { get { return _isvali; } set { _isvali = value; } }

        private KdtFeildEx _rule = new KdtFeildEx() { TableName = "static_index", FeildName = "vali_rule" };
        public KdtFeildEx rule { get { return _rule; } set { _rule = value; } }

        private KdtFeildEx _isbak = new KdtFeildEx() { TableName = "static_index", FeildName = "isbak" };
        public KdtFeildEx isbak { get { return _isbak; } set { _isbak = value; } }

        private KdtFeildEx _bak_content = new KdtFeildEx() { TableName = "static_index", FeildName = "bak_content" };
        public KdtFeildEx bak { get { return _bak_content; } set { _bak_content = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "static_index", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "static_index", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }


        private KdtFeildEx _name = new KdtFeildEx() { TableName = "static_index", FeildName = "name" };
        public KdtFeildEx name { get { return _name; } set { _name = value; } }

        #endregion.
    }
}
