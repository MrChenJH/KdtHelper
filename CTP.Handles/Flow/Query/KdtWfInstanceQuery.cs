using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Flow
{
    public class KdtWfInstanceQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 实例编号
        /// </summary>
        [Field(null, "instance_id")]
        public string id { get; set; }


        /// <summary>
        /// 实例名称
        /// </summary>
        [Field(null, "instance_name")]
        public string name { get; set; }

        /// <summary>
        /// 实例来源
        /// </summary>
        [Field(null, "instance_source")]
        public string source { get; set; }


        /// <summary>
        /// 映射编号
        /// </summary>
        [Field(null, "map_id")]
        public string mapid { get; set; }

        /// <summary>
        /// 流程编码
        /// </summary>
        [Field(null, "flow_id")]
        public string flowid { get; set; }

        /// <summary>
        /// 步骤编号
        /// </summary>
        [Field(null, "step_id")]
        public int stepid { get; set; }

        /// <summary>
        /// 流程状态
        /// </summary>
        [Field(null, "instance_status")]
        public int status { get; set; }


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
