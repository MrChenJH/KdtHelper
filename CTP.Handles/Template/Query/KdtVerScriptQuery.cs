using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Template
{
    public class KdtVerScriptQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 脚本自增编号
        /// </summary>
        [Field(null, "script_id")]
        public string id { get; set; }


        /// <summary>
        /// HTML段
        /// </summary>
        [Field(null, "tp_html")]
        public string tphtml { get; set; }

        /// <summary>
        /// 脚本段
        /// </summary>
        [Field(null, "tp_script")]
        public string tpscript { get; set; }

       
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
