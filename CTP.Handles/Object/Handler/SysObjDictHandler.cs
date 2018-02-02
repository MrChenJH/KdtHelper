using KdtHelper.Common;
using KdtHelper.Core.ExecuterEx;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{
    /// <summary>
    /// 数据对象字典操作类
    /// </summary>
    public class SysObjDictHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "sys_obj_dict"; } }

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
                dic.Add("sys_obj_dict", "where obj_feild =[@feild]  and  obj_name=[@name] ");
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
                dic.Add("sys_obj_dict", "where obj_feild =[@feild]  and  obj_name=[@name]");
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
                dic.Add("sys_obj_dict", "where obj_feild =[@feild]  and  obj_name=[@name] ");
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

                #region   公共SQL语句
                //通过Id查询该数据信息
                dic.Add("GetById", "select * from  sys_obj_dict  where  obj_name = [@name]");
                //查询所有数据信息
                dic.Add("GetAll", "select * from sys_obj_dict");
                #endregion

                //根据对象名查询对象字典列表信息
                dic.Add("GetObjDictByName", "select * from sys_obj_dict where obj_name=[@name]  and is_sys =[@sys]  ");

                //判断数据库表字段类型
                KdtHelper.DbDataType type = KdtHelper.DbDataType.NVARCHAR;
                string ctype = ftype.FeildValue.Convert("");
                if (!ctype.IsNullOrEmpty())
                {
                    switch (ctype.ToLower())
                    {
                        case "bool": type = KdtHelper.DbDataType.TINYINT; break;
                        case "int": type = KdtHelper.DbDataType.INT; break;
                        case "text": type = KdtHelper.DbDataType.TEXT; break;
                        case "ntext": type = KdtHelper.DbDataType.NTEXT; break;
                        case "bit": type = KdtHelper.DbDataType.TINYINT; break;
                        case "tinyint": type = KdtHelper.DbDataType.TINYINT; break;
                        default: type = KdtHelper.DbDataType.NVARCHAR; break;
                    }
                }

                #region SqlServer版Sql语句

                //添加数据库表字段
                dic.Add("Sql_AddField", type == KdtHelper.DbDataType.NVARCHAR ?
                   this.Adapter.AddField("[" + name.FeildValue.Convert("").ToMD5_16() + "]", "[feild]", type, "[len]") : this.Adapter.AddField("[" + name.FeildValue.Convert("").ToMD5_16() + "]", "[feild]", type));
                //修改数据库表字段
                dic.Add("Sql_ModifyField", type == KdtHelper.DbDataType.NVARCHAR ?
                    this.Adapter.ModifyField("[" + name.FeildValue.Convert("").ToMD5_16() + "]", "[feild]", type, "[len]") : this.Adapter.ModifyField("[" + name.FeildValue.Convert("").ToMD5_16() + "]", "[feild]", type));
                ////删除数据对象表及对象列表数据及对象字典数据
                //dic.Add("Sql_DelObjByName", Adapter.MultiSql("drop  table [" + name.FeildValue.Convert("").ToMD5_16() + "]"
                //        , "delete from sys_obj_list where obj_name =[@name]"
                //        , "delete from sys_obj_dict where obj_name =[@name]"));

                //删除数据对象字典数据及对象表字段
                dic.Add("Sql_DelObjDictByName", Adapter.MultiSql("alter table  [" + name.FeildValue.Convert("").ToMD5_16() + "]  drop column [feild]"
                    , " delete from sys_obj_dict  where obj_feild =[@feild]  and  obj_name=[@name]"));

                //查询某个数据对象下数据字段最大值
                dic.Add("Sql_GetMaxObjFeild", " select obj_feild  from sys_obj_dict  where  obj_name = [@name] and  obj_feild  like 'resourceprop%' order by  convert(int, substring(obj_feild, 13, len(obj_feild))) desc ");

                #endregion

                #region MySql版Sql语句
                //添加数据库表字段
                dic.Add("AddField", type == KdtHelper.DbDataType.NVARCHAR ?
                   this.Adapter.AddField("`" + name.FeildValue.Convert("").ToMD5_16() + "`", "[feild]", type, "[len]") : this.Adapter.AddField("`" + name.FeildValue.Convert("").ToMD5_16() + "`", "[feild]", type));
                //修改数据库表字段
                dic.Add("ModifyField", type == KdtHelper.DbDataType.NVARCHAR ?
                    this.Adapter.ModifyField("`" + name.FeildValue.Convert("").ToMD5_16() + "`", "[feild]", type, "[len]") : this.Adapter.ModifyField("`" + name.FeildValue.Convert("").ToMD5_16() + "`", "[feild]", type));

                ////删除数据对象表及对象列表数据及对象字典数据
                //dic.Add("DelObjByName", Adapter.MultiSql("drop  table `" + name.FeildValue.Convert("").ToMD5_16() + "`"
                //        , "delete from sys_obj_list where obj_name =[@name]"
                //        , "delete from sys_obj_dict where obj_name =[@name]"));

                //删除数据对象字典数据及对象表字段
                dic.Add("DelObjDictByName", Adapter.MultiSql("alter table  `" + name.FeildValue.Convert("").ToMD5_16() + "`  drop column [feild]"
                    , "delete from sys_obj_dict  where obj_feild =[@feild]  and  obj_name=[@name]"));

                //查询某个数据对象下数据字段最大值
                dic.Add("GetMaxObjFeild", " select obj_feild  from sys_obj_dict  where  obj_name = [@name] and  obj_feild  like 'resourceprop%' order by  cast(substring(obj_feild, 13, LENGTH(obj_feild)-12) as UNSIGNED)  desc  ");

                #endregion 

                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "sys_obj_dict", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _name = new KdtFeildEx() { TableName = "sys_obj_dict", FeildName = "obj_name" };
        public KdtFeildEx name { get { return _name; } set { _name = value; } }

        private KdtFeildEx _type = new KdtFeildEx() { TableName = "sys_obj_dict", FeildName = "obj_type" };
        public KdtFeildEx type { get { return _type; } set { _type = value; } }

        private KdtFeildEx _feild = new KdtFeildEx() { TableName = "sys_obj_dict", FeildName = "obj_feild" };
        public KdtFeildEx feild { get { return _feild; } set { _feild = value; } }

        private KdtFeildEx _description = new KdtFeildEx() { TableName = "sys_obj_dict", FeildName = "obj_description" };
        public KdtFeildEx des { get { return _description; } set { _description = value; } }

        private KdtFeildEx _feildtype = new KdtFeildEx() { TableName = "sys_obj_dict", FeildName = "obj_feild_type" };
        public KdtFeildEx  ftype { get { return _feildtype; } set { _feildtype = value; } }

        private KdtFeildEx _feildlen = new KdtFeildEx() { TableName = "sys_obj_dict", FeildName = "obj_feild_len" };
        public KdtFeildEx len { get { return _feildlen; } set { _feildlen = value; } }

        private KdtFeildEx _issys = new KdtFeildEx() { TableName = "sys_obj_dict", FeildName = "is_sys" };
        public KdtFeildEx  sys { get { return _issys; } set { _issys = value; } }

        private KdtFeildEx _feilddefault = new KdtFeildEx() { TableName = "sys_obj_dict", FeildName = "obj_feild_default" };
        public KdtFeildEx  fdefault { get { return _feilddefault; } set { _feilddefault = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "sys_obj_dict", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "sys_obj_dict", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }


        #endregion

    }
}
