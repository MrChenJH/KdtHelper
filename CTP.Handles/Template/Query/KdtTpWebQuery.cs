using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Template
{
    public class KdtTpWebQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 网站名称
        /// </summary>
        [Field(null, "web_name")]
        public string id { get; set; }


        /// <summary>
        /// 根目录
        /// </summary>
        [Field(null, "web_root")]
        public string root { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Field(null, "table_note")]
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
