using KdtHelper.Core.ExecuterEx;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{
    /// <summary>
    /// 对象版本表操作类
    /// </summary>
    public   class ObjVersionHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "sys_obj_version"; } }

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
                dic.Add("sys_obj_version", "where auto_no = [@autono] ");
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
                dic.Add("sys_obj_version", "where auto_no = [@autono] ");
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
                dic.Add("sys_obj_version", "where auto_no = [@autono] ");
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
                dic.Add("GetById", "select * from  sys_obj_version  where  auto_no = [@autono]");

                #endregion

                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "sys_obj_version", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _id = new KdtFeildEx() { TableName = "sys_obj_version", FeildName = "map_id" };
        public KdtFeildEx id { get { return _id; } set { _id = value; } }

        private KdtFeildEx _name = new KdtFeildEx() { TableName = "sys_obj_version", FeildName = "obj_name" };
        public KdtFeildEx name { get { return _name; } set { _name = value; } }

        private KdtFeildEx _context = new KdtFeildEx() { TableName = "sys_obj_version", FeildName = "obj_context" };
        public KdtFeildEx  context { get { return _context; } set { _context = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "sys_obj_version", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "sys_obj_version", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }


        #endregion

    }
}
