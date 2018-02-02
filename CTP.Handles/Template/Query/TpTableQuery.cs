using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Template
{
    public class TpTableQuery
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
        /// 备注
        /// </summary>
        [Field(null, "table_note")]
        public string note { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [Field(null, "classify")]
        public string cate { get; set; }

        /// <summary>
        ///创建人
        /// </summary>
        [Field(null, "creator")]
        public string ceator { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string ctime { get; set; }
    }
}
