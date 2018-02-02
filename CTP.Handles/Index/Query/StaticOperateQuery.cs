using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Index
{
    public class StaticOperateQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 基地名称
        /// </summary>
        [Field(null, "user_id")]
        public string uid { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Field(null, "operate_user")]
        public string ouser { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [Field(null, "optype")]
        public int type { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string ctime { get; set; }

        /// <summary>
        ///所属区
        /// </summary>
        [Field(null, "aname")]
        public string aname { get; set; }
    }
}
