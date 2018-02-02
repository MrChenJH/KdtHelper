using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{
    public  class IndexEntity
    {

        /// <summary>
        /// ID
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 列码
        /// </summary>
        [Field(null, "col_code")]
        public string colCode { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [Field(null, "p_id")]
        public int Pid { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        [Field(null, "col_name")]
        public string colName { get; set; }


        /// <summary>
        /// 是否统计列
        /// </summary>
        [Field(null, "is_stat")]
        public int isStat { get; set; }

        /// <summary>
        /// 统计算法
        /// </summary>
        [Field(null, "stat_way")]
        public string statWay { get; set; }


        /// <summary>
        /// 自动编号列
        /// </summary>
        [Field(null, "is_auto_no")]
        public int isAutono { get; set; }

        /// <summary>
        /// 单位集合
        /// </summary>
        [Field(null, "unit_list")]
        public string uninList { get; set; }


        /// <summary>
        /// 行码
        /// </summary>
        [Field(null, "row_code")]
        public string rowCode { get; set; }

        /// <summary>
        /// 行名称
        /// </summary>
        [Field(null, "row_name")]
        public string rowName { get; set; }

        /// <summary>
        /// 统计列算法
        /// </summary>
        [Field(null, "stat_columns")]
        public string statColumns { get; set; }



    }
}
