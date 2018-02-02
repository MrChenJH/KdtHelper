using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Index
{
    public class StaticIndexQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 数据对象编码
        /// </summary>
        [Field(null, "name_code")]
        public string ncode { get; set; }


        /// <summary>
        /// 指标名称
        /// </summary>
        [Field(null, "index_name")]
        public string iname { get; set; }

        /// <summary>
        /// 指标编号
        /// </summary>
        [Field(null, "index_code")]
        public string icode { get; set; }


        /// <summary>
        /// 指标类型
        /// </summary>
        [Field(null, "type")]
        public int type { get; set; }


        /// <summary>
        /// 指标内容
        /// </summary>
        [Field(null, "content")]
        public string content { get; set; }

        /// <summary>
        /// 是否验证
        /// </summary>
        [Field(null, "isValidate")]
        public int isvali { get; set; }

        /// <summary>
        /// 指标验证规则
        /// </summary>
        [Field(null, "vali_rule")]
        public string rule { get; set; }

        /// <summary>
        /// 指标得分
        /// </summary>
        [Field(null, "isbak")]
        public int isbak { get; set; }

        /// <summary>
        /// 指标得分规则
        /// </summary>
        [Field(null, "bak_content")]
        public string bak { get; set; }

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
        ///显示指标名称
        /// </summary>
        [Field(null, "name")]
        public string name { get; set; }

        /// <summary>
        ///指标在对象表中的字段
        /// </summary>
        [Field(null, "obj_feild")]
        public string feild { get; set; }
    }
}
