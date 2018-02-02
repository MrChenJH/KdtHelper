using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Flow
{
    public class KdtWfPositionQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int Autono { get; set; }

        /// <summary>
        /// 职位编码
        /// </summary>
        [Field(null, "position_id")]
        public string PositionId { get; set; }


        /// <summary>
        /// 职位名称
        /// </summary>
        [Field(null, "position_name")]
        public string PositionName { get; set; }


        /// <summary>
        /// 是否系统自带
        /// </summary>
        [Field(null, "is_sys")]
        public int IsSys { get; set; }


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
