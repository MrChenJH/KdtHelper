using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Index
{
    public class StaticExcelVesionQuery
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
        /// 基地基本信息
        /// </summary>
        [Field(null, "base_info")]
        public string binfo { get; set; }

        /// <summary>
        /// 工作业绩
        /// </summary>
        [Field(null, "work_achive")]
        public string work { get; set; }

        /// <summary>
        /// 服务能力
        /// </summary>
        [Field(null, "service_able")]
        public string service { get; set; }

        /// <summary>
        /// 制度保障
        /// </summary>
        [Field(null, "inst_garan")]
        public string inst { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        [Field(null, "other")]
        public string other { get; set; }


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
        ///更新时间
        /// </summary>
        [Field(null, "update_time")]
        public string utime { get; set; }
    }
}
