using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Flow
{
    public class KdtWfStepQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 步骤编号
        /// </summary>
        [Field(null, "step_id")]
        public int stepid { get; set; }

        /// <summary>
        /// 流程编号
        /// </summary>
        [Field(null, "flow_id")]
        public string flowid { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        [Field(null, "step_name")]
        public string name { get; set; }

        /// <summary>
        /// 退回步骤
        /// </summary>
        [Field(null, "step_back")]
        public int back { get; set; }

        /// <summary>
        /// 上一步
        /// </summary>
        [Field(null, "step_pre")]
        public string pre { get; set; }

        /// <summary>
        /// 存在下一步
        /// </summary>
        [Field(null, "has_next")]
        public int hasnext { get; set; }


        /// <summary>
        /// 是否多分支
        /// </summary>
        [Field(null, "is_multi")]
        public int multi { get; set; }

        /// <summary>
        /// 步骤类型
        /// </summary>
        [Field(null, "step_type")]
        public int type { get; set; }


        /// <summary>
        /// 执行编号
        /// </summary>
        [Field(null, "action_id")]
        public string actionid { get; set; }

        /// <summary>
        /// 数据模版
        /// </summary>
        [Field(null, "data_temp")]
        public string temp { get; set; }


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

        /// <summary>
        /// 
        /// </summary>
        [Field(null, "action_name")]
        public string actionname { get; set; }
    }
}
