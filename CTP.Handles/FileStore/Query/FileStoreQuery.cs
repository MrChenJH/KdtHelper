using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.FileStore
{

    /// <summary>
    /// 文件存储基础类
    /// </summary>
    public   class FileStoreQuery
    {

        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int id { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        [Field(null, "file_type")]
        public int  type { get; set; }


        /// <summary>
        ///存储目录
        /// </summary>
        [Field(null, "store_folder")]
        public string  folder { get; set; }

        /// <summary>
        ///存储格式(存储格式：1:yyyy、2:yyyy-MM、3:yyyy-MM-dd)
        /// </summary>
        [Field(null, "store_format")]
        public int  format { get; set; }


        /// <summary>
        ///支持转换
        /// </summary>
        [Field(null, "is_change")]
        public int  change { get; set; }


        /// <summary>
        ///目标格式
        /// </summary>
        [Field(null, "target_suffix")]
        public string suffix { get; set; }


        /// <summary>
        ///多版本
        /// </summary>
        [Field(null, "multi_target")]
        public int  target { get; set; }


        /// <summary>
        ///版本规则
        /// </summary>
        [Field(null, "multi_target_role")]
        public string role { get; set; }


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
