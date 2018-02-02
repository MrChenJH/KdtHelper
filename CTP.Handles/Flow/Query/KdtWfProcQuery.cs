using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Flow
{
    public class KdtWfProcQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int Autono { get; set; }

        /// <summary>
        /// 实例编码
        /// </summary>
        [Field(null, "instance_id")]
        public string InstanceId { get; set; }


        /// <summary>
        /// 步骤编号
        /// </summary>
        [Field(null, "step_id")]
        public int StepId { get; set; }

        /// <summary>
        /// 执行编号
        /// </summary>
        [Field(null, "action_id")]
        public string ActionId { get; set; }


        /// <summary>
        /// 执行人
        /// </summary>
        [Field(null, "step_emp_id")]
        public string StepEmpid { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [Field(null, "action_time")]
        public string ActionTime { get; set; }

        /// <summary>
        /// 执行备注
        /// </summary>
        [Field(null, "step_note")]
        public string StepNote { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        [Field(null, "action_status")]
        public int ActionStatus { get; set; }


        /// <summary>
        ///创建人
        /// </summary>
        [Field(null, "creator")]
        public string Creator { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string CTime { get; set; }
    }
}
