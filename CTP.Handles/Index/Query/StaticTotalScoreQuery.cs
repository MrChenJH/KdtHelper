using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Index
{
    public class StaticTotalScoreQuery
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
        /// 基础分
        /// </summary>
        [Field(null, "basis_score")]
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
        ///创建人
        /// </summary>
        [Field(null, "area_name")]
        public string  name { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "user_type")]
        public int  type { get; set; }



    }
}
