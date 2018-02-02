using System;
using System.Collections.Generic;
using System.Web;
using System.Text;

namespace KdtHelper.Core.ExecuterEx
{
    /// <summary>
    /// 查询条件类
    /// </summary>
    public class KdtPageEx
    {
        /// <summary>
        /// 
        /// </summary>
        public KdtPageEx() { }

        /// <summary>
        /// 初始化WEB页面信息
        /// </summary>
        /// <param name="_request"></param>
        public KdtPageEx(int _currentPage, int _pagesize)
        {
            //int _start = (_currentPage - 1) * _pagesize;
            int _start = _currentPage; //(_currentPage - 1) * _pagesize;
            this.start = _start <= 0 ? 1 : _start + 1;
            //this.end = _currentPage * _pagesize;
            this.end = _currentPage + _pagesize;
            this.selpage = "selectPage";
            this.selpagetotal = "selectTotalPage";
        }

        /// <summary>
        /// 查询分页方法名
        /// </summary>
        public string selpage { get; set; }

        /// <summary>
        /// 查询分页统计方法名
        /// </summary>
        public string selpagetotal { get; set; }

        /// <summary>
        /// 起始值
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// 结束值
        /// </summary>
        public int end { get; set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int total { get; internal set; }

        /// <summary>
        /// 条件1
        /// </summary>
        public string param1 { get; set; }

        /// <summary>
        /// 条件2
        /// </summary>
        public string param2 { get; set; }

        /// <summary>
        /// 条件3
        /// </summary>
        public string param3 { get; set; }
    }
}
