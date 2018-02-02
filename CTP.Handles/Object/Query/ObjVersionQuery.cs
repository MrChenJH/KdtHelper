using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{
    public  class ObjVersionQuery
    {

        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 映射编号
        /// </summary>
        [Field(null, "map_id")]
        public string id { get; set; }


        /// <summary>
        /// 对象名
        /// </summary>
        [Field(null, "obj_name")]
        public string  name { get; set; }


        /// <summary>
        /// 版本内容
        /// </summary>
        [Field(null, "obj_context")]
        public string context { get; set; }

        
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
