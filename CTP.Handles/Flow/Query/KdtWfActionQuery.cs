using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Flow
{
    public class KdtWfActionQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 执行编号
        /// </summary>
        [Field(null, "action_id")]
        public string id { get; set; }

        /// <summary>
        /// 执行类型
        /// </summary>
        [Field(null, "action_type")]
        public int type { get; set; }

        /// <summary>
        /// 执行名称
        /// </summary>
        [Field(null, "action_name")]
        public string name { get; set; }

        /// <summary>
        /// 执行分类
        /// </summary>
        [Field(null, "action_category")]
        public string cate { get; set; }

        /// <summary>
        /// 审核人类型
        /// </summary>
        [Field(null, "audit_type")]
        public int audittype { get; set; }

        /// <summary>
        /// 审核人映射
        /// </summary>
        [Field(null, "audit_mapid")]
        public string auditmapid { get; set; }

        /// <summary>
        /// 审核人职位
        /// </summary>
        [Field(null, "audit_position_id")]
        public string auditposition{ get; set; }


        /// <summary>
        /// 存在代理
        /// </summary>
        [Field(null, "hasproxy")]
        public int hasproxy { get; set; }

        /// <summary>
        /// 代理人类型
        /// </summary>
        [Field(null, "proxy_type")]
        public int proxytype { get; set; }


        /// <summary>
        /// 代理人映射
        /// </summary>
        [Field(null, "proxy_mapid")]
        public string proxymapid { get; set; }

        /// <summary>
        /// 代理人职位
        /// </summary>
        [Field(null, "proxy_position_id")]
        public string proxyposition { get; set; }

        /// <summary>
        /// 存在抄送
        /// </summary>
        [Field(null, "hascopy")]
        public int hascopy { get; set; }

        /// <summary>
        /// 抄送人类型
        /// </summary>
        [Field(null, "copy_type")]
        public int copytype { get; set; }


        /// <summary>
        /// 抄送人映射
        /// </summary>
        [Field(null, "copy_mapid")]
        public string copymapid { get; set; }

        /// <summary>
        /// 抄送人职位
        /// </summary>
        [Field(null, "copy_position_id")]
        public string copyposition { get; set; }

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
