using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Menu
{
    public class MenuTopQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Field(null, "m_icon")]
        public string icon { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        [Field(null, "m_name")]
        public string name { get; set; }



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
        ///排序字段
        /// </summary>
        [Field(null, "orderby")]
        public string order { get; set; }
    }
}
