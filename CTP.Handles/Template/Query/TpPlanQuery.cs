using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Template
{
    public class TpPlanQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [Field(null, "task_name")]
        public string id { get; set; }

        /// <summary>
        /// 计划类型
        /// </summary>
        [Field(null, "plan_type")]
        public int type { get; set; }

        /// <summary>
        ///开始时间
        /// </summary>
        [Field(null, "start_time")]
        public string stime { get; set; }

        /// <summary>
        /// 是否重复
        /// </summary>
        [Field(null, "is_repeat")]
        public int isrepeat { get; set; }

        /// <summary>
        /// 重复间隔（分钟）
        /// </summary>
        [Field(null, "interval_munite")]
        public int repeatmuni { get; set; }

        /// <summary>
        /// 执行时间（分钟）
        /// </summary>
        [Field(null, "interval_total_munite")]
        public int totalmuni { get; set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        [Field(null, "has_expired")]
        public int isex { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [Field(null, "expired_time")]
        public string extime { get; set; }


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
