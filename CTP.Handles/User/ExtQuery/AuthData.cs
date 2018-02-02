
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.User
{
    /// <summary>
    /// 权限项Id初始类
    /// </summary>
     public  class AuthData
    {

        /// <summary>
        /// 系统权限项ID集合
        /// </summary>
        public static Dictionary<string, string> Object
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                return dic;
            }
        }

        /// <summary>
        /// 权限项Id默认值
        /// </summary>
        public static List<AuthItemQuery> AuthIdList
        {
            get
            {
                List<AuthItemQuery> dic = new List<AuthItemQuery>();
                dic.Add(new AuthItemQuery() { nick = "添加", note = "添加权限" });
                dic.Add(new AuthItemQuery() { nick = "删除", note = "删除权限"});
                dic.Add(new AuthItemQuery() { nick = "修改", note = "修改权限" });
                dic.Add(new AuthItemQuery() { nick = "查询", note = "查询权限"});

                return dic;
            }
        }



    }
}
