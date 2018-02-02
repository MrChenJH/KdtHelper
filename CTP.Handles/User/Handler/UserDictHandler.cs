using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Common;
using KdtHelper.Core.ExecuterEx;

namespace CTP.Handles.User
{
    public class UserDictHandler : KdtFieldEntityEx
    {
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_user_dict"; } }

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
                dic.Add("kdt_user_dict", "where auto_no = [@autono]");
                return dic;
            }
        }

        protected override Dictionary<string, string> _AddOrUpdateWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_user_dict", "where  auto_no = [@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _DeleteWhere
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                dic.Add("kdt_user_dict", "where  auto_no = [@autono] ");
                return dic;
            }
        }

        protected override Dictionary<string, string> _SelectSql
        {
            get
            {
                var dic = new Dictionary<string, string>();
                //判断数据库表字段类型
                KdtHelper.DbDataType datatype = KdtHelper.DbDataType.NVARCHAR;
                string ctype = type.FeildValue.Convert("");
                if (!ctype.IsNullOrEmpty())
                {
                    switch (ctype.ToLower())
                    {
                        case "bool": datatype = KdtHelper.DbDataType.TINYINT; break;
                        case "int": datatype = KdtHelper.DbDataType.INT; break;
                        case "text": datatype = KdtHelper.DbDataType.TEXT; break;
                        case "ntext": datatype = KdtHelper.DbDataType.NTEXT; break;
                        case "bit": datatype = KdtHelper.DbDataType.TINYINT; break;
                        case "tinyint": datatype = KdtHelper.DbDataType.TINYINT; break;
                        default: datatype = KdtHelper.DbDataType.NVARCHAR; break;
                    }
                }
                //添加用户表字段
                dic.Add("AddUserField", datatype == KdtHelper.DbDataType.NVARCHAR ?
                   this.Adapter.AddField("kdt_user_extend", "[feild]", datatype, "[len]") : this.Adapter.AddField("kdt_user_extend", "[feild]", datatype));
                //添加职员表字段
                dic.Add("AddEmployField", datatype == KdtHelper.DbDataType.NVARCHAR ?
                   this.Adapter.AddField("kdt_employ_extend", "[feild]", datatype, "[len]") : this.Adapter.AddField("kdt_employ_extend", "[feild]", datatype));
                //修改用户表字段
                dic.Add("ModifyUserField", datatype == KdtHelper.DbDataType.NVARCHAR ?
                    this.Adapter.ModifyField("kdt_user_extend", "[feild]", datatype, "[len]") : this.Adapter.ModifyField("kdt_user_extend", "[feild]", datatype));
                //修改职员表字段
                dic.Add("ModifyEmployField", datatype == KdtHelper.DbDataType.NVARCHAR ?
                    this.Adapter.ModifyField("kdt_employ_extend", "[feild]", datatype, "[len]") : this.Adapter.ModifyField("kdt_employ_extend", "[feild]", datatype));
                //删除用户表字段
                dic.Add("DeleteUserFeild", Adapter.MultiSql("alter table  kdt_user_extend  [creator]"
                    , "delete from kdt_user_dict  where user_type=0 and   user_ext_feild in ([feild])"));
                dic.Add("SelectUserDict", "select * from kdt_user_dict");
                //查询最大字段名
                dic.Add("SelectMaxFeild", "select user_ext_feild  from kdt_user_dict  where user_type = [@utype] and  user_ext_feild  like 'userprop%' order by  cast(substring(user_ext_feild, 9, LENGTH(user_ext_feild)-8) as UNSIGNED)  desc");
                dic.Add("GetDictPage", "select * from (" + Adapter.RowNumber("kdt_user_dict", "auto_no", true, "[creator]", "*") + ") b where b.rno between @start and @end");
                dic.Add("GetDictPageCount", "select count(1) from kdt_user_dict [creator]");
                dic.Add("SelectUserDictById", "select * from kdt_user_dict where auto_no = [@autono]");

                //删除用户字段
                dic.Add("DeleteFeild", Adapter.MultiSql("alter table  kdt_user_extend DROP COLUMN [feild] "
                   , " delete from kdt_user_dict  where user_type=0 and   user_ext_feild =[@feild]"));
                dic.Add("GetAll", "select * from kdt_user_dict");
                dic.Add("GetById", "select * from kdt_user_dict where user_ext_feild = [@feild]");
                return dic;
            }
        }

        #endregion.

        #region 字段

        private KdtFeildEx _autono = new KdtFeildEx() { TableName = "kdt_user_dict", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _autono; } set { _autono = value; } }

        private KdtFeildEx _feild = new KdtFeildEx() { TableName = "kdt_user_dict", FeildName = "user_ext_feild" };
        public KdtFeildEx feild { get { return _feild; } set { _feild = value; } }

        private KdtFeildEx _ftype = new KdtFeildEx() { TableName = "kdt_user_dict", FeildName = "user_feild_type" };
        public KdtFeildEx type { get { return _ftype; } set { _ftype = value; } }

        private KdtFeildEx _flen = new KdtFeildEx() { TableName = "kdt_user_dict", FeildName = "user_feild_len" };
        public KdtFeildEx len { get { return _flen; } set { _flen = value; } }

        private KdtFeildEx _utype = new KdtFeildEx() { TableName = "kdt_user_dict", FeildName = "user_type", FeildValue = 0 };
        public KdtFeildEx utype { get { return _utype; } set { _utype = value; } }

        private KdtFeildEx _display = new KdtFeildEx() { TableName = "kdt_user_dict", FeildName = "user_ext_display" };
        public KdtFeildEx dis { get { return _display; } set { _display = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_user_dict", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _ctime = new KdtFeildEx() { TableName = "kdt_user_dict", FeildName = "create_time" };
        public KdtFeildEx ctime { get { return _ctime; } set { _ctime = value; } }

        #endregion.
    }
}
