using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.User
{

    /// <summary>
    /// 角色权限表基础类
    /// </summary>
    public  class RoleAuthQuery
    {

        [Field(null, "role_key")]
        public int key { get; set; }

        [Field(null, "aitem_id")]
        public string id { get; set; }

        [Field(null, "creator")]
        public string creator { get; set; }

        [Field(null, "create_time")]
        public string ctime { get; set; }

    }
}
