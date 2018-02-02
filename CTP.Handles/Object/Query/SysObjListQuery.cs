using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{

    /// <summary>
    /// 对象列表基础类
    /// </summary>
    public class SysObjListQuery
    {

        [Field(null, "auto_no")]
        public int autono { get; set; }

        [Field(null, "obj_name")]
        public string name { get; set; }

        [Field(null, "obj_table")]
        public string table { get; set; }

        [Field(null, "obj_type")]
        public int  type { get; set; }

        [Field(null, "obj_category")]
        public string category { get; set; }

        [Field(null, "is_dy_row")]
        public int  dyrow { get; set; }

        [Field(null, "ispublic")]
        public int ispublic { get; set; }

        [Field(null, "obj_head")]
        public string head { get; set; }

        [Field(null, "obj_foot")]
        public string foot { get; set; }

        [Field(null, "obj_title")]
        public string title { get; set; }

        [Field(null, "creator")]
        public string creator { get; set; }

        [Field(null, "create_time")]
        public string ctime { get; set; }

    }
}
