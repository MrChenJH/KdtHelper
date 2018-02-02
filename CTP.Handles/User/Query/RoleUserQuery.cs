using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.User
{
    public class RoleUserQuery
    {

        /// <summary>
        /// 角色编号
        /// </summary>
        [Field(null, "role_key")]
        public int key { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Field(null, "user_id")]
        public string uid { get; set; }

        /// <summary>
        ///角色职位
        /// </summary>
        [Field(null, "user_position")]
        public string position { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [Field(null, "role_nick")]
        public string rnick { get; set; }

        /// <summary>
        ///用户昵称
        /// </summary>
        [Field(null, "user_nick")]
        public string unick { get; set; }


        /// <summary>
        ///创建人
        /// </summary>
        [Field(null, "creator")]
        public string Creator { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string ctime { get; set; }
    }
}
