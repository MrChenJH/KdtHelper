using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.FileStore
{

    /// <summary>
    /// 存储文件基础类
    /// </summary>
    public  class StoreFileQuery
    {

        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [Field(null, "node_id")]
        public string id { get; set; }

        /// <summary>
        /// 文件标识
        /// </summary>
        [Field(null, "file_id")]
        public string fid { get; set; }


        /// <summary>
        ///文件名称
        /// </summary>
        [Field(null, "file_name")]
        public string name { get; set; }


        /// <summary>
        /// 文件类型
        /// </summary>
        [Field(null, "file_type")]
        public int type { get; set; }


        /// <summary>
        ///文件路径
        /// </summary>
        [Field(null, "file_path")]
        public string  path { get; set; }


        /// <summary>
        ///文件状态
        /// </summary>
        [Field(null, "file_status")]
        public int status { get; set; }


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
        ///转换时间
        /// </summary>
        [Field(null, "change_time")]
        public string changetime { get; set; }

    }
}
