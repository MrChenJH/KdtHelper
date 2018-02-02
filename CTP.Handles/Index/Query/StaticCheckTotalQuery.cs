using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Index
{
    public class StaticCheckTotalQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Field(null, "user_id")]
        public string uid { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Field(null, "status")]
        public int status { get; set; }
        

        /// <summary>
        /// 是否冻结
        /// </summary>
        [Field(null, "is_frozen")]
        public string frozen { get; set; }
        


        /// <summary>
        /// 操作时间
        /// </summary>
        [Field(null, "audit_time")]
        public string audittime { get; set; }


        /// <summary>
        ///审核人
        /// </summary>
        [Field(null, "creator")]
        public string creator { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string ctime { get; set; }

        /// <summary>
        ///区域名称
        /// </summary>
        [Field(null, "area_name")]
        public string aname { get; set; }

        /// <summary>
        ///区域类型
        /// </summary>
        [Field(null, "area_type")]
        public string atype { get; set; }
    }
}
