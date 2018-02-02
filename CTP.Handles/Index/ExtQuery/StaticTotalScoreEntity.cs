using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Index
{
     public  class StaticTotalScoreEntity
    {

        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        [Field(null, "user_id")]
        public string uid { get; set; }

        /// <summary>
        /// 所属区县
        /// </summary>
        [Field(null, "area_name")]
        public string name { get; set; }

        /// <summary>
        /// 基础分
        /// </summary>
       [Field(null, "basis")]
        public string basis { get; set; }

        /// <summary>
        /// 附加分
        /// </summary>
        [Field(null, "additional_score")]
        public string additional { get; set; }


        /// <summary>
        /// 年检评分
        /// </summary>
        [Field(null, "yearly_score")]
        public string yearly { get; set; }

        /// <summary>
        /// 专家平均分
        /// </summary>
        [Field(null, "expert_avg")]
        public string expertavg { get; set; }

        /// <summary>
        /// 考核总分（专家平均分+基础【考核】+附加）
        /// </summary>
        [Field(null, "total")]
        public string total { get; set; }

        /// <summary>
        /// 综合总分（基础+附加）
        /// </summary>
        [Field(null, "all_total")]
        public string alltotal { get; set; }

        /// <summary>
        /// 专家1评分
        /// </summary>
        [Field(null, "expert1")]
        public string expert1 { get; set; }

        /// <summary>
        ///  专家2评分
        /// </summary>
        [Field(null, "expert2")]
        public string expert2 { get; set; }

        /// <summary>
        ///  专家3评分
        /// </summary>
        [Field(null, "expert3")]
        public string expert3 { get; set; }

        /// <summary>
        ///  专家4评分
        /// </summary>
        [Field(null, "expert4")]
        public string expert4 { get; set; }

        /// <summary>
        ///  专家5评分
        /// </summary>
        [Field(null, "expert5")]
        public string expert5 { get; set; }

        /// <summary>
        ///  专家6评分
        /// </summary>
        [Field(null, "expert6")]
        public string expert6 { get; set; }

        /// <summary>
        ///  专家7评分
        /// </summary>
        [Field(null, "expert7")]
        public string expert7 { get; set; }


        /// <summary>
        /// 基地性质
        /// </summary>
        [Field(null, "user_type")]
        public int type { get; set; }


    }
}
