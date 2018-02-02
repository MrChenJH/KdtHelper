using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.User
{
    public class UserDictQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        [Field(null, "user_ext_feild")]
        public string feild { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        [Field(null, "user_feild_type")]
        public string type { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        [Field(null, "user_feild_len")]
        public int len { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        [Field(null, "user_type")]
        public int utype { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [Field(null, "user_ext_display")]
        public string dis { get; set; }

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
