using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{

    /// <summary>
    /// 对象字典基础类
    /// </summary>
    public class SysObjDictQuery
    {

        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int  autono { get; set; }

        /// <summary>
        /// 对象名
        /// </summary>
        [Field(null, "obj_name")]
        public string name { get; set; }


        /// <summary>
        /// 数据类型
        /// </summary>
        [Field(null, "obj_type")]
        public int type { get; set; }


        /// <summary>
        /// 字段名
        /// </summary>
        [Field(null, "obj_feild")]
        public string feild { get; set; }

        /// <summary>
        /// 字段名
        /// </summary>
        [Field(null, "obj_description")]
        public string des { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Field(null, "obj_feild_type")]
        public string ftype { get; set; }


        /// <summary>
        /// 长度
        /// </summary>
        [Field(null, "obj_feild_len")]
        public int len { get; set; }


        /// <summary>
        /// 系统字段
        /// </summary>
        [Field(null, "is_sys")]
        public int sys { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [Field(null, "obj_feild_default")]
        public string fdefault { get; set; }


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
