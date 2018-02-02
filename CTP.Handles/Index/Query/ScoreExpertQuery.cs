using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Index
{
    public class ScoreExpertQuery
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
        /// 专家名
        /// </summary>
        [Field(null, "expert")]
        public string expert { get; set; }

        /// <summary>
        /// 工作协同
        /// </summary>
        [Field(null, "work")]
        public string work { get; set; }

        /// <summary>
        /// 品牌打造
        /// </summary>
        [Field(null, "brand")]
        public string brand { get; set; }

        /// <summary>
        /// 展示面积
        /// </summary>
        [Field(null, "science")]
        public string science { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        [Field(null, "total")]
        public string total { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string ctime { get; set; }

        /// <summary>
        ///区域名称
        /// </summary>
        [Field(null, "aname")]
        public string aname { get; set; }
    }
}
