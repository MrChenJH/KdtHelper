using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{
    /// <summary>
    /// 指标列表基础类
    /// </summary>
    public  class IndexTreeNode
    {

        public IndexTreeNode()
        {
            children = new List<IndexTreeNode>();
        }


        /// <summary>
        /// ID
        /// </summary>
        public int autono { get; set; }

        /// <summary>
        /// 列码
        /// </summary>
        public string colCode { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int Pid { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string colName { get; set; }


        /// <summary>
        /// 是否统计列
        /// </summary>
        public int isStat { get; set; }

        /// <summary>
        /// 统计算法
        /// </summary>
        public string statWay { get; set; }


        /// <summary>
        /// 自动编号列
        /// </summary>
        public  int isAutono { get; set; }

        /// <summary>
        /// 单位集合
        /// </summary>
        public string uninList { get; set; }


        /// <summary>
        /// 行码
        /// </summary>
        public string rowCode { get; set; }

        /// <summary>
        /// 行名称
        /// </summary>
        public string rowName { get; set; }

        /// <summary>
        /// 统计列算法
        /// </summary>
        public string statColumns { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public string style { get; set; }


        /// <summary>
        /// 子列表
        /// </summary>
        public List<IndexTreeNode> children { get; set; }

    }
}
