using KdtHelper.Common;
using KdtHelper.Core.ExecuterEx;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{
    /// <summary>
    /// 数据对象列表操作类
    /// </summary>
    public  class SysObjListHandler : KdtFieldEntityEx
    {
        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "sys_obj_list"; } }

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
                dic.Add("sys_obj_list", "where obj_name = [@name] ");
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
                dic.Add("sys_obj_list", "where obj_name = [@name] ");
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
                dic.Add("sys_obj_list", "where obj_name = [@name] ");
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
                dic.Add("GetById", "select * from  sys_obj_list  where  obj_name = [@name]");
                //查询所有数据信息
                dic.Add("GetAll", "select * from sys_obj_list");
                //查询数据对象类别
                dic.Add("GetCategory", " select obj_category  from  sys_obj_list  where  obj_type = [@type]  and obj_category is not null and obj_category!='' group by obj_category ");
                //判断数据对象名是否存在
                dic.Add("CheckByName", " select count(1) from sys_obj_list where obj_name = [@name] ");

                #endregion

                #region   私有SQl语句

                //创建数据对象表
                dic.Add("CreateTable", this.Adapter.CreateTable(name.FeildValue.Convert("").ToMD5_16()));

                #endregion 

                int objtype = type.FeildValue.Convert(0);     //对象类别
                string objcategory = category.FeildValue.Convert("");   //对象类名
                string typesql = "";
                string catsql = "";
                if (objtype >= 0)
                {
                    typesql = "  and obj_type = " + objtype;
                }
                if (!objcategory.IsNullOrEmpty())
                {
                    catsql = " and obj_category = '" + objcategory + "'";
                }

                //查询所有对象列表信息
                dic.Add("GetObjNameList", "select * from sys_obj_list where  obj_type = [@type] " + catsql + " ");
                //通过对象名查询数据对象菜单列表
                 dic.Add("GetCategoryOrName", "select 0 as auto_no,obj_category as obj_name,obj_type from sys_obj_list where obj_category is not null " 
                                            + "  and obj_category != ''  and  obj_category like '%[name]%'  and  obj_type = [@type]  group by obj_category ,obj_type  "
                                            + "  union all select auto_no, obj_name, obj_type from sys_obj_list "
                                            + "  where (obj_category is null or obj_category = '' ) and obj_name like '%[name]%' and  obj_type = [@type] ");
                //根据对象类名名查询对象信息
                dic.Add("GetObjByCategory", "select * from sys_obj_list where  obj_category =  [@category]  and obj_type = [@type] and obj_name like '%[name]%' ");

                //通过对象类名查询数据对象菜单列表
                string strsql = typesql + catsql;
                dic.Add("GetObjNameOfType", "select * from sys_obj_list where  obj_name like '%[name]%' " + strsql + " ");


                #region SqlServer版Sql语句

                //删除数据对象表及对象列表数据及对象字典数据
                dic.Add("Sql_DelObjByName", Adapter.MultiSql("drop  table [" + name.FeildValue.Convert("").ToMD5_16() + "]"
                        , "delete from sys_obj_list where obj_name =[@name]"
                        , "delete from sys_obj_dict where obj_name =[@name]"));
                
                #endregion

                #region MySql版Sql语句

                //删除数据对象表及对象列表数据及对象字典数据
                dic.Add("DelObjByName", Adapter.MultiSql("drop  table `" + name.FeildValue.Convert("").ToMD5_16() + "`"
                        , "delete from sys_obj_list where obj_name =[@name]"
                        , "delete from sys_obj_dict where obj_name =[@name]"));
                

                #endregion 


                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _name = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "obj_name" };
        public KdtFeildEx name { get { return _name; } set { _name = value; } }

        private KdtFeildEx _table = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "obj_table" };
        public KdtFeildEx table { get { return _table; } set { _table = value; } }

        private KdtFeildEx _type = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "obj_type" };
        public KdtFeildEx type { get { return _type; } set { _type = value; } }

        private KdtFeildEx _category = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "obj_category" };
        public KdtFeildEx category { get { return _category; } set { _category = value; } }

        private KdtFeildEx _dyrow = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "is_dy_row" , FeildValue = 0 };
        public KdtFeildEx  dyrow { get { return _dyrow; } set { _dyrow = value; } }

        private KdtFeildEx _ispublic = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "ispublic" };
        public KdtFeildEx ispublic { get { return _ispublic; } set { _ispublic = value; } }

        private KdtFeildEx _head = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "obj_head" };
        public KdtFeildEx head { get { return _head; } set { _head = value; } }

        private KdtFeildEx _foot = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "obj_foot" };
        public KdtFeildEx foot { get { return _foot; } set { _foot = value; } }

        private KdtFeildEx _title = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "obj_title" };
        public KdtFeildEx title { get { return _title; } set { _title = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "sys_obj_list", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }


        #endregion

    }
}
