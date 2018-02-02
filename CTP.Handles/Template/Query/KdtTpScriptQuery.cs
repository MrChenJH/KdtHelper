using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Template
{
    public class KdtTpScriptQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 脚本名称
        /// </summary>
        [Field(null, "script_name")]
        public string id { get; set; }


        /// <summary>
        /// 脚本类型
        /// </summary>
        [Field(null, "script_type")]
        public int type { get; set; }

        /// <summary>
        /// 脚本分类
        /// </summary>
        [Field(null, "script_node")]
        public string cate { get; set; }

        /// <summary>
        /// 数据API格式
        /// </summary>
        [Field(null, "data_format")]
        public string format { get; set; }

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

        /// <summary>
        /// 版本自增编号
        /// </summary>
        [Field(null, "ver_Auto")]
        public int verauto { get; set; }
    }
}
