using KdtHelper.Core.ExecuterEx;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.User
{

    /// <summary>
    /// 权限项表操作类
    /// </summary>
    public  class AuthItemHandler : KdtFieldEntityEx
    {

        #region 基础属性
        public override string CUser { get { return ""; } }

        protected override object _class { get { return this; } }

        protected override Type _classType { get { return this.GetType(); } }

        protected override string _mainTable { get { return "kdt_auth_item"; } }

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
                dic.Add("kdt_auth_item", "where auto_no = [@autono] ");
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
                dic.Add("kdt_auth_item", "where auto_no = [@autono] ");
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
                dic.Add("kdt_auth_item", "where auto_no = [@autono] ");
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
                //查询权限信息
                dic.Add("GetAllAuth", " select *  from  kdt_auth_item  GROUP BY aitem_sys  ");
                //查询权限信息
                dic.Add("GetAllAuthBySys", " select *  from  kdt_auth_item  where  aitem_sys = [@sys]  GROUP BY aitem_category  ");
                //查询权限信息
                dic.Add("GetAuthByCategory", " select *  from  kdt_auth_item  where  aitem_sys = [@sys]  and   aitem_category = [@category]   ");
                //查询权限信息
                dic.Add("GetAllAuthById", " select *  from  kdt_auth_item  where  aitem_id = [@id]   ");



                //判断该权限项类别是否存在
                dic.Add("GetCheckByCategory", " select count(1)  from  kdt_auth_item  where  aitem_sys = [@sys]  and  aitem_category = [@category] ");
                //判断该权限项是否存在
                //dic.Add("CheckIsExist", " select *  from  kdt_auth_item  where  aitem_id = [@id] ");
                //根据权限项ID删除权限信息
                dic.Add("DelAuthByAitemId", Adapter.MultiSql("delete from  kdt_auth_item  where  aitem_id = [@id]"
                        , "delete from kdt_role_auth where aitem_id = [@id] "
                        , "delete from kdt_user_auth where aitem_id = [@id] "
                        , "delete from role_user_auth where aitem_id = [@id] "));
                //根据系统名称或权限项类别删除权限信息
                dic.Add("DelAuthBySys", Adapter.MultiSql("delete from  kdt_auth_item  where  aitem_sys = [@sys] "
                        , " delete from  kdt_role_auth  where aitem_id  in (select aitem_id  from   kdt_auth_item  where aitem_sys = [@sys] ) "
                        , " delete from  kdt_user_auth  where aitem_id  in (select aitem_id  from   kdt_auth_item  where aitem_sys = [@sys] ) "
                        , " delete from  role_user_auth  where aitem_id  in (select aitem_id  from   kdt_auth_item  where aitem_sys = [@sys] ) "));

                //根据系统名称删除权限信息
                dic.Add("DelAuthBySysOrCategory", Adapter.MultiSql("delete from  kdt_auth_item  where  aitem_sys = [@sys]  and  aitem_category = [@category] "
                        , " delete from  kdt_role_auth  where aitem_id  in (select aitem_id  from   kdt_auth_item  where aitem_sys = [@sys] and  aitem_category = [@category] ) "
                        , " delete from  kdt_user_auth  where aitem_id  in (select aitem_id  from   kdt_auth_item  where aitem_sys = [@sys]   and  aitem_category = [@category] ) "
                        , " delete from  role_user_auth  where aitem_id  in (select aitem_id  from   kdt_auth_item  where aitem_sys = [@sys] and  aitem_category = [@category] ) "));

                //删除系统名称及类别
                dic.Add("DelAuthByCategory", Adapter.MultiSql(" delete from  kdt_role_auth  where aitem_sys = [@sys]  and  aitem_category = [@category]  ", " "));
                //获取权限项ID最大值
                dic.Add("GetMaxAuthId", "select *  from  kdt_auth_item  where  aitem_sys = [@sys]  and aitem_category = [@category]  "
                                           + " and aitem_id  like  '[AitemId]%' order by  cast(substring(aitem_id, 21, LENGTH(aitem_id)-20) as UNSIGNED)  desc");

                //查询空的权限项ID
                dic.Add("GetAuthIdByCategory", " select *  from  kdt_auth_item  where  aitem_sys = [@sys]  and  aitem_category = [@category]  and ( aitem_id = '' or  aitem_id is NULL ) ");

                //删除权限项
                //删除系统名称及类别
                dic.Add("DelAuthById", Adapter.MultiSql(" delete from  kdt_auth_item  where  aitem_sys = [@sys] and  aitem_category = [@category] and  aitem_id  like  '%[id]%'  ", " "));
                return dic;
            }
        }

        #endregion

        #region 字段

        private KdtFeildEx _id = new KdtFeildEx() { TableName = "kdt_auth_item", FeildName = "auto_no", IsKey = true, IsIncr = true };
        public KdtFeildEx autono { get { return _id; } set { _id = value; } }

        private KdtFeildEx _aitemsys = new KdtFeildEx() { TableName = "kdt_auth_item", FeildName = "aitem_sys" };
        public KdtFeildEx sys { get { return _aitemsys; } set { _aitemsys = value; } }

        private KdtFeildEx _category = new KdtFeildEx() { TableName = "kdt_auth_item", FeildName = "aitem_category" };
        public KdtFeildEx category { get { return _category; } set { _category = value; } }

        private KdtFeildEx _aitemid = new KdtFeildEx() { TableName = "kdt_auth_item", FeildName = "aitem_id" };
        public KdtFeildEx id { get { return _aitemid; } set { _aitemid = value; } }

        private KdtFeildEx _aitemnick = new KdtFeildEx() { TableName = "kdt_auth_item", FeildName = "aitem_nick" };
        public KdtFeildEx nick { get { return _aitemnick; } set { _aitemnick = value; } }

        private KdtFeildEx _aitemnote = new KdtFeildEx() { TableName = "kdt_auth_item", FeildName = "aitem_note" };
        public KdtFeildEx note { get { return _aitemnote; } set { _aitemnote = value; } }

        private KdtFeildEx _creator = new KdtFeildEx() { TableName = "kdt_auth_item", FeildName = "creator" };
        public KdtFeildEx creator { get { return _creator; } set { _creator = value; } }

        private KdtFeildEx _time = new KdtFeildEx() { TableName = "kdt_auth_item", FeildName = "create_time", FeildValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
        public KdtFeildEx ctime { get { return _time; } set { _time = value; } }


        #endregion

    }
}
