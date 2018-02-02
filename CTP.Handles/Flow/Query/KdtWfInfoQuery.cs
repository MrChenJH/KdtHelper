using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Flow
{
    public class KdtWfInfoQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 流程编码
        /// </summary>
        [Field(null, "flow_id")]
        public string id { get; set; }


        /// <summary>
        /// 流程名称
        /// </summary>
        [Field(null, "flow_name")]
        public string name { get; set; }

        /// <summary>
        /// 流程描述
        /// </summary>
        [Field(null, "flow_note")]
        public string note { get; set; }

        /// <summary>
        /// 流程分类
        /// </summary>
        [Field(null, "flow_category")]
        public string cate { get; set; }


        /// <summary>
        /// 是否系统自带
        /// </summary>
        [Field(null, "is_sys")]
        public int sys { get; set; }


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
