using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Flow
{
    public class KdtWfMsgQuery
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
        /// 接收人
        /// </summary>
        [Field(null, "step_rec_id")]
        public string StepRecid { get; set; }

        /// <summary>
        /// 开启发送
        /// </summary>
        [Field(null, "send_on")]
        public int SendOn { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [Field(null, "send_time")]
        public string SendTime { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        [Field(null, "send_status")]
        public int SendStatus { get; set; }

        /// <summary>
        /// 读取状态
        /// </summary>
        [Field(null, "read_status")]
        public int ReadStatus { get; set; }


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
