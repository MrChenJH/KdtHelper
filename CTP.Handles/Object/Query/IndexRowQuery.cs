using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{
    /// <summary>
    /// 指标行基础类
    /// </summary>
    public  class IndexRowQuery
    {

        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int id { get; set; }


        /// <summary>
        /// 父编号
        /// </summary>
        [Field(null, "p_id")]
        public int pid { get; set; }


        /// <summary>
        /// 行码
        /// </summary>
        [Field(null, "row_code")]
        public string code { get; set; }


        /// <summary>
        /// 对象名
        /// </summary>
        [Field(null, "obj_name")]
        public string objname { get; set; }


        /// <summary>
        /// 名称
        /// </summary>
        [Field(null, "row_name")]
        public string name { get; set; }

        /// <summary>
        /// 统计行
        /// </summary>
        [Field(null, "is_stat")]
        public int stat { get; set; }


        /// <summary>
        /// 统计列算法
        /// </summary>
        [Field(null, "stat_columns")]
        public string columns { get; set; }


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
