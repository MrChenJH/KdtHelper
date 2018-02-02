using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core.ExecuterEx;
using KdtHelper.Common;

namespace CTP.Handles.Template
{
    public class KdtTpScriptHandler : KdtFieldEntityEx
    {

        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_tp_script"; } }

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
                dic.Add("kdt_tp_script", "  where script_name = [@id]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_script", "  where script_name = [@id]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_tp_script", "  where script_name = [@id] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                //查询条件
                int scripttype = type.FeildValue.Convert(0);     //模板类别
                string category = cate.FeildValue.Convert("");   //模板分类
                string typesql = "";
                string catsql = "";
                if (scripttype >= 0)
                {
                    typesql = "  and script_type = " + scripttype;
                }
                if (!category.IsNullOrEmpty())
                {
                    catsql = " and script_node = '" + category + "'";
                }

                //查询所有脚本模板列表信息
                dic.Add("GetScriptNameList", "select * from kdt_tp_script where  script_type = [@type] ");
                //通过脚本模板类名查询模板信息
                string strsql = typesql + catsql;
                dic.Add("GetScriptNameOfType", "select * from kdt_tp_script where  script_name like '%[id]%' " + strsql + " ");

                //通过脚本名查询脚本模板
                dic.Add("GetCategoryOrName", " select 0 as auto_no,  script_node as script_name from kdt_tp_script where (script_node != '' and script_node is not null ) " +
                                           " and  script_node like '%[id]%'  and  script_type = [@type] group by script_node " +
                                           " union all " +
                                           " select auto_no,  script_name from kdt_tp_script where (script_node = '' or  script_node is null)" +
                                           " and  script_name like '%[id]%'  and  script_type = [@type] ");

                //根据脚本模板分类查询脚本信息
                dic.Add("GetScriptByCategory", "select * from kdt_tp_script where  script_node =  [@cate]  and script_type = [@type] and  script_name like '%[id]%'  ");


                //根据脚本名称查询脚本信息
                dic.Add("GetByName", "select * from kdt_tp_script where script_name = [@id]");


                #region SqlServer版Sql语句

                //删除脚本模板
                dic.Add("DelScriptByName", "delete from kdt_tp_script where script_name =[id]");

                #endregion


                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_tp_script", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _script_name = new KdtFeildEx() { TableName = "kdt_tp_script", FeildName = "script_name" };
        public KdtFeildEx id { get { return _script_name; } set { _script_name = value; } }

        private KdtFeildEx _script_type = new KdtFeildEx() { TableName = "kdt_tp_script", FeildName = "script_type" };
        public KdtFeildEx type { get { return _script_type; } set { _script_type = value; } }

        private KdtFeildEx _script_node = new KdtFeildEx() { TableName = "kdt_tp_script", FeildName = "script_node" };
        public KdtFeildEx cate { get { return _script_node; } set { _script_node = value; } }

        private KdtFeildEx _data_format = new KdtFeildEx() { TableName = "kdt_tp_script", FeildName = "data_format" };
        public KdtFeildEx format { get { return _data_format; } set { _data_format = value; } }

        private KdtFeildEx _tp_html = new KdtFeildEx() { TableName = "kdt_tp_script", FeildName = "tp_html" };
        public KdtFeildEx tphtml { get { return _tp_html; } set { _tp_html = value; } }

        private KdtFeildEx _tp_script = new KdtFeildEx() { TableName = "kdt_tp_script", FeildName = "tp_script" };
        public KdtFeildEx tpscript { get { return _tp_script; } set { _tp_script = value; } }

        private KdtFeildEx _table_note = new KdtFeildEx() { TableName = "kdt_tp_script", FeildName = "table_note" };
        public KdtFeildEx note { get { return _table_note; } set { _table_note = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_tp_script", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_tp_script", FeildName = "create_time",FeildValue = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        private KdtFeildEx _ver_Auto = new KdtFeildEx() { TableName = "kdt_tp_script", FeildName = "ver_Auto" };
        public KdtFeildEx verauto { get { return _ver_Auto; } set { _ver_Auto = value; } }

        #endregion.
    }
}
