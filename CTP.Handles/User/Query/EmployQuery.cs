using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.User
{
    public class EmployQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        [Field(null, "employ_id")]
        public string id { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Field(null, "employ_nick")]
        public string nick { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Field(null, "employ_sex")]
        public string sex { get; set; }

        /// <summary>
        /// OPEN_ID
        /// </summary>
        [Field(null, "employ_birthday")]
        public string birth { get; set; }

        /// <summary>
        /// 绑定来源
        /// </summary>
        [Field(null, "employ_native")]
        public string native { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Field(null, "birthday_place")]
        public string place { get; set; }

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
