using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Flow
{
    public class KdtWfQuery
    {
        /// <summary>
        /// 实例编号
        /// </summary>
        [Field(null, "instance_id")]
        public string instanceid { get; set; }

        /// <summary>
        /// 实例名称
        /// </summary>
        [Field(null, "instance_name")]
        public string instancename { get; set; }

        /// <summary>
        /// 流程信息编码
        /// </summary>
        [Field(null, "flow_id")]
        public string flowid { get; set; }

        /// <summary>
        /// 流程信息名称
        /// </summary>
        [Field(null, "flow_name")]
        public string flowname { get; set; }

        /// <summary>
        /// 步骤编号
        /// </summary>
        [Field(null, "step_id")]
        public int stepid { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        [Field(null, "step_name")]
        public string stepname { get; set; }

        /// <summary>
        /// 执行编码
        /// </summary>
        [Field(null, "action_id")]
        public string actionid { get; set; }


        /// <summary>
        /// 执行名称
        /// </summary>
        [Field(null, "action_name")]
        public string actionname { get; set; }

        /// <summary>
        /// 执行人
        /// </summary>
        [Field(null, "step_emp_id")]
        public string empid { get; set; }


        /// <summary>
        /// 执行状态
        /// </summary>
        [Field(null, "action_status")]
        public string status { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [Field(null, "action_time")]
        public string time { get; set; }
        
    }
}
