using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{

    /// <summary>
    /// 列指标基础类
    /// </summary>
    public  class IndexColumnQuery
    {

        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int id { get; set; }

        /// <summary>
        /// 列码
        /// </summary>
        [Field(null, "col_code")]
        public string code { get; set; }


        /// <summary>
        /// 父编号
        /// </summary>
        [Field(null, "p_id")]
        public int pid { get; set; }


        /// <summary>
        /// 对象名
        /// </summary>
        [Field(null, "obj_name")]
        public string objname { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Field(null, "col_name")]
        public string name { get; set; }

        /// <summary>
        /// 统计列
        /// </summary>
        [Field(null, "is_stat")]
        public int stat { get; set; }


        /// <summary>
        /// 统计算法
        /// </summary>
        [Field(null, "stat_way")]
        public string  way { get; set; }


        /// <summary>
        /// 自动编号列
        /// </summary>
        [Field(null, "is_auto_no")]
        public int isauto { get; set; }

        /// <summary>
        /// 单位集合
        /// </summary>
        [Field(null, "unit_list")]
        public string unit { get; set; }


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
