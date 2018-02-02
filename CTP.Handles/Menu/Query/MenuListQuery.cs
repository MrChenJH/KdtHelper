using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Menu
{
    public class MenuListQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 父编号
        /// </summary>
        [Field(null, "parent_id")]
        public int pid { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        [Field(null, "m_name")]
        public string name { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        [Field(null, "is_leaf")]
        public int leaf { get; set; }


        /// <summary>
        /// 菜单链接
        /// </summary>
        [Field(null, "m_link")]
        public string link { get; set; }


        /// <summary>
        /// 图标
        /// </summary>
        [Field(null, "m_icon")]
        public string icon { get; set; }



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
