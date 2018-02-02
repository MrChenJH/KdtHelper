using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Common;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.FileStore
{

    /// <summary>
    /// 文件格式表操作类
    /// </summary>
    public  class FileFormatHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "sys_file_format"; } }

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
                dic.Add("sys_file_format", "where auto_no=[@Id] ");
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
                dic.Add("sys_file_format", "where auto_no=[@Id] ");
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
                dic.Add("sys_file_format", " where auto_no=[@Id] ");
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
                #region  公共SQl语句
                //通过自增编号查询文件格式信息
                dic.Add("GetById", " select  * from  sys_file_format  where  auto_no  = [@autono] ");
                //判断该文件类型的后缀名是否存在
                dic.Add("CheckByName", "select *  from  sys_file_format  where file_suffix = [@suffix] and auto_no<>[@autono] ");

                int filetype = type.FeildValue.Convert(0);     //文件类型
                string sql = "";
                if (filetype >= 0)
                    sql = " and file_type = " + filetype;
                //分页查询所有的文件格式信息
                dic.Add("selectTotalPage", "select COUNT(1)  from sys_file_format  where  file_suffix  like '%[suffix]%' " + sql +" ");
                dic.Add("selectPage", "select * from(" + Adapter.RowNumber(" sys_file_format  tb ", "tb.auto_no", false, " where  file_suffix  like '%[suffix]%' " + sql + "  ", " tb.* ") + ") ta where ta.rno BETWEEN @start and @end ");

                #endregion 

                //判断该文件类型的后缀名是否存在
                dic.Add("CheckIsExist", "select *  from  sys_file_format  where file_suffix = [@suffix]  and file_type = [@type] and auto_no<>[@autono] ");
                //查询文件类型最大值
                dic.Add("GetMaxFileType", " select  MAX(file_type) as  'file_type' from sys_file_format  ");
                //通过自增编号查询文件格式信息
                dic.Add("GetFileFormatById", " select  * from  sys_file_format  where  auto_no  = [@autono] ");
                //删除文件格式
                dic.Add("DelFileFormat", Adapter.MultiSql(" delete  from  sys_file_format  where auto_no  in ([autono]) ", "  "));
                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _id = new KdtFeildEx() { TableName = "sys_file_format", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _id; } set { _id = value; } }

        private KdtFeildEx _filesuffix = new KdtFeildEx() { TableName = "sys_file_format", FeildName = "file_suffix" };
        public KdtFeildEx suffix { get { return _filesuffix; } set { _filesuffix = value; } }

        private KdtFeildEx _type = new KdtFeildEx() { TableName = "sys_file_format", FeildName = "file_type" };
        public KdtFeildEx type { get { return _type; } set { _type = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "sys_file_format", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "sys_file_format", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }


        #endregion

    }
}
