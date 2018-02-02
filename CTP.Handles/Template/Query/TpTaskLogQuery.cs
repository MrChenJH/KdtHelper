using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Template
{
    public class TpTaskLogQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        ///任务名称
        /// </summary>
        [Field(null, "task_name")]
        public string name { get; set; }

        /// <summary>
        ///执行时间
        /// </summary>
        [Field(null, "task_runtime")]
        public string time { get; set; }

        /// <summary>
        ///执行状态
        /// </summary>
        [Field(null, "run_status")]
        public int status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Field(null, "table_note")]
        public int note { get; set; }

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
