using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{
    public  class IndexObjEntity
    {

        /// <summary>
        /// ID
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 基地名
        /// </summary>
        [Field(null, "id_leaf")]
        public string uid { get; set; }

        /// <summary>
        /// 行码
        /// </summary>
        [Field(null, "row_code")]
        public string rcode { get; set; }

        /// <summary>
        /// 列码
        /// </summary>
        [Field(null, "column_code")]
        public string ccode { get; set; }


        /// <summary>
        /// 单位
        /// </summary>
        [Field(null, "unit_val")]
        public string unit { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Field(null, "c_value")]
        public string value { get; set; }


        /// <summary>
        /// 自动编号列
        /// </summary>
        [Field(null, "d_status")]
        public int status { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Field(null, "creator")]
        public string creator { get; set; }


        /// <summary>
        /// 创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string ctime { get; set; }



    }
}
