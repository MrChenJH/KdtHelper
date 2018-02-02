using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Index
{
    public class StaticExcelDataQuery
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
        /// 基地基本信息
        /// </summary>
        [Field(null, "base_info")]
        public string binfo { get; set; }

        /// <summary>
        /// 工作业绩
        /// </summary>
        [Field(null, "work_achive")]
        public string work { get; set; }

        /// <summary>
        /// 服务能力
        /// </summary>
        [Field(null, "service_able")]
        public string service { get; set; }

        /// <summary>
        /// 制度保障
        /// </summary>
        [Field(null, "inst_garan")]
        public string inst { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        [Field(null, "other")]
        public string other { get; set; }


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
        ///所属区
        /// </summary>
        [Field(null, "area_name")]
        public string aname { get; set; }

        /// <summary>
        /// 检查考核年份
        /// </summary>
        [Field(null, "userprop3")]
        public string userprop3 { get; set; }

        /// <summary>
        ///是否享受收税优惠
        /// </summary>
        [Field(null, "userprop4")]
        public string userprop4 { get; set; }

        /// <summary>
        ///命名年份
        /// </summary>
        [Field(null, "userprop5")]
        public string userprop5 { get; set; }

        /// <summary>
        ///进行专家评分
        /// </summary>
        [Field(null, "userprop8")]
        public string userprop8 { get; set; }

        /// <summary>
        ///基地类型
        /// </summary>
        [Field(null, "user_type")]
        public string utype { get; set; }


        /// <summary>
        ///去年接待人数
        /// </summary>
        [Field(null, "userprop6")]
        public string userprop6 { get; set; }

        /// <summary>
        ///科普护照排名
        /// </summary>
        [Field(null, "orderby")]
        public string orderby { get; set; }

        /// <summary>
        ///科普护照总数
        /// </summary>
        [Field(null, "value")]
        public string value { get; set; }
    }
}
