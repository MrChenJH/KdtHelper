using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.User
{

    /// <summary>
    /// 权限项表基础类
    /// </summary>
    public class AuthItemQuery
    {
        [Field(null, "auto_no")]
        public int autono { get; set; }

        [Field(null, "aitem_sys")]
        public string sys { get; set; }

        [Field(null, "aitem_category")]
        public string  category { get; set; }

        [Field(null, "aitem_id")]
        public string id { get; set; }

        [Field(null, "aitem_nick")]
        public string  nick { get; set; }

        [Field(null, "aitem_note")]
        public string  note { get; set; }

        [Field(null, "creator")]
        public string creator { get; set; }

        [Field(null, "create_time")]
        public string ctime { get; set; }

    }
}
