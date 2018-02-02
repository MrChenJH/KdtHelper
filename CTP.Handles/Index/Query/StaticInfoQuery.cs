using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Index
{
    public class StaticInfoQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 父名称
        /// </summary>
        [Field(null, "parant_name")]
        public string pname { get; set; }

        /// <summary>
        /// 显示的指标名称
        /// </summary>
        [Field(null, "name")]
        public string name { get; set; }

        /// <summary>
        /// 关联的数据对象
        /// </summary>
        [Field(null, "name_code")]
        public string ncode { get; set; }

        /// <summary>
        /// 关联的数据对象名称
        /// </summary>
        [Field(null, "obj_name")]
        public string oname { get; set; }

        /// <summary>
        /// 数据对象类型
        /// </summary>
        [Field(null, "obj_type")]
        public int otype { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Field(null, "orderby")]
        public int order { get; set; }


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
