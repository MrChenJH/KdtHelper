using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.User
{

    /// <summary>
    /// 用户权限表基础类
    /// </summary>
    public   class UserAuthQuery
    {

        [Field(null, "aitem_id")]
        public string id { get; set; }

        [Field(null, "user_id")]
        public string  uid { get; set; }

        [Field(null, "creator")]
        public string creator { get; set; }

        [Field(null, "create_time")]
        public string ctime { get; set; }

    }
}
