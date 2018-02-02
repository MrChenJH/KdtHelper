using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.User
{
    public class UserEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Field(null, "user_id")]
        public string id { get; set; }

        /// <summary>
        /// 所属区县
        /// </summary>
        [Field(null, "area_name")]
        public string aname { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Field(null, "user_phone")]
        public string phone { get; set; }


        /// <summary>
        /// 自定义字段
        /// </summary>
        [Field(null, "userprop1")]
        public string usp1 { get; set; }

        /// <summary>
        ///自定义字段
        /// </summary>
        [Field(null, "userprop2")]
        public string usp2 { get; set; }

    }
}
