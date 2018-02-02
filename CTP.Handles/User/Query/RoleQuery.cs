using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.User
{
    public class RoleQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }


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
        /// 角色备注
        /// </summary>
        [Field(null, "role_type")]
        public int type { get; set; }
    }
}
