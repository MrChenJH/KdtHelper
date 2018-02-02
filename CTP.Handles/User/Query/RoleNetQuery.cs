using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.User
{
    public class RoleNetQuery
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        [Field(null, "role_id")]
        public string id { get; set; }

        /// <summary>
        ///角色昵称
        /// </summary>
        [Field(null, "role_nick")]
        public string nick { get; set; }

        /// <summary>
        /// 角色备注
        /// </summary>
        [Field(null, "role_note")]
        public string note { get; set; }

        /// <summary>
        /// 角色自增键
        /// </summary>
        [Field(null, "role_key")]
        public int key { get; set; }

        /// <summary>
        /// 父自增键
        /// </summary>
        [Field(null, "parent_key")]
        public int pid { get; set; }

        /// <summary>
        /// 角色路径
        /// </summary>
        [Field(null, "role_path")]
        public string path { get; set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        [Field(null, "role_type")]
        public int type { get; set; }


        /// <summary>
        /// 角色排序
        /// </summary>
        [Field(null, "role_order")]
        public int order { get; set; }

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
