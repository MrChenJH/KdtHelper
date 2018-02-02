using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.User
{
    public class UserQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        [Field(null, "user_id")]
        public string id { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Field(null, "user_nick")]
        public string nick { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Field(null, "user_pwd")]
        public string pwd { get; set; }

        /// <summary>
        /// OPEN_ID
        /// </summary>
        [Field(null, "open_id")]
        public string openid { get; set; }

        /// <summary>
        /// 绑定来源
        /// </summary>
        [Field(null, "open_source")]
        public string source { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Field(null, "user_phone")]
        public string phone { get; set; }

        /// <summary>
        /// 用户分类
        /// </summary>
        [Field(null, "user_classify")]
        public int classify { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Field(null, "user_email")]
        public string email { get; set; }

        /// <summary>
        ///创建人
        /// </summary>
        [Field(null, "creator")]
        public string creator { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string ctime { get; set; }

      
    }
}
