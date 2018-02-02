using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.User
{

    /// <summary>
    /// 角色用户操作类
    /// </summary>
    public  class RoleUserEntity
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
        ///用户昵称
        /// </summary>
        [Field(null, "user_nick")]
        public string nick { get; set; }

        /// <summary>
        ///用户分类
        /// </summary>
        [Field(null, "user_classify")]
        public int  classify { get; set; }


    }
}
