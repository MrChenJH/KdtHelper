using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Menu
{
    public class MenuAuthQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int Autono { get; set; }

        /// <summary>
        /// 映射编号
        /// </summary>
        [Field(null, "map_id")]
        public string MapId { get; set; }


        /// <summary>
        /// 映射类型
        /// </summary>
        [Field(null, "map_type")]
        public int MapType { get; set; }

        /// <summary>
        /// 顶部菜单ID
        /// </summary>
        [Field(null, "top_id")]
        public int TopId { get; set; }

        /// <summary>
        /// 侧边菜单ID
        /// </summary>
        [Field(null, "menu_id")]
        public int MenuId { get; set; }



        /// <summary>
        ///创建人
        /// </summary>
        [Field(null, "creator")]
        public string Creator { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string CTime { get; set; }

    }
}
