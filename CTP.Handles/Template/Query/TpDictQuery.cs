using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Template
{
    public class TpDictQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 数据表名
        /// </summary>
        [Field(null, "table_name")]
        public string id { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        [Field(null, "field_name")]
        public string fname { get; set; }

        /// <summary>
        ///字段描述
        /// </summary>
        [Field(null, "field_note")]
        public string note { get; set; }

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
