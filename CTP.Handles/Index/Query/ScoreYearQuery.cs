using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Index
{
    public class ScoreYearQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Field(null, "user_id")]
        public string uid { get; set; }

        /// <summary>
        /// 年累计开放天数
        /// </summary>
        [Field(null, "year_day")]
        public int yday { get; set; }

        /// <summary>
        /// 基地类别
        /// </summary>
        [Field(null, "btype")]
        public int btype { get; set; }

        /// <summary>
        /// 节庆假日
        /// </summary>
        [Field(null, "holi_day")]
        public int hday { get; set; }

        /// <summary>
        /// 科普专职人员数量
        /// </summary>
        [Field(null, "peo_num")]
        public int pnum { get; set; }

        /// <summary>
        /// 展示面积
        /// </summary>
        [Field(null, "area")]
        public int area { get; set; }

        /// <summary>
        /// 年度更新展品展项
        /// </summary>
        [Field(null, "update_year")]
        public int uyear { get; set; }


        /// <summary>
        ///上海科技节
        /// </summary>
        [Field(null, "tech_holi")]
        public int tholi { get; set; }

        /// <summary>
        ///制度保障
        /// </summary>
        [Field(null, "sys_manager")]
        public int smanager { get; set; }

        /// <summary>
        ///制度保障
        /// </summary>
        [Field(null, "sys_safe")]
        public int ssafe { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string ctime { get; set; }

        /// <summary>
        ///基地地址
        /// </summary>
        [Field(null, "address")]
        public string address { get; set; }

        /// <summary>
        ///联系人
        /// </summary>
        [Field(null, "linkman")]
        public string linkman { get; set; }

        /// <summary>
        ///区域名称
        /// </summary>
        [Field(null, "aname")]
        public string aname { get; set; }
    }
}
