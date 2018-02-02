using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.User
{
    /// <summary>
    /// 用户角色权限基础类
    /// </summary>
    public  class AuthEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Field(null, "user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Field(null, "user_nick")]
        public string UserNick { get; set; }

        /// <summary>
        /// 角色自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public string AutoNo { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        [Field(null, "role_id")]
        public string RoleId { get; set; }

        /// <summary>
        /// 角色昵称
        /// </summary>
        [Field(null, "role_nick")]
        public string RoleNick { get; set; }


    }
}
