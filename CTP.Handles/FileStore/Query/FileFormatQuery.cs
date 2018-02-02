using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.FileStore
{

    /// <summary>
    /// 文件格式基础类
    /// </summary>
     public  class FileFormatQuery
    {

        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 文件后缀
        /// </summary>
        [Field(null, "file_suffix")]
        public string suffix { get; set; }


        /// <summary>
        ///文件类型
        /// </summary>
        [Field(null, "file_type")]
        public int type { get; set; }


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
