using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Template
{
    public class TpTaskQuery
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
        /// 任务状态
        /// </summary>
        [Field(null, "task_status")]
        public int status { get; set; }

        /// <summary>
        /// 同步类型
        /// </summary>
        [Field(null, "syn_type")]
        public int type { get; set; }

        /// <summary>
        /// 任务脚本
        /// </summary>
        [Field(null, "syn_script")]
        public string script { get; set; }

        /// <summary>
        /// 上次执行时间
        /// </summary>
        [Field(null, "current_runtime")]
        public string ltime { get; set; }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        [Field(null, "next_runtime")]
        public string ntime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Field(null, "table_note")]
        public string note { get; set; }

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
