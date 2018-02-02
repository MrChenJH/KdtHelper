using KdtHelper.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.FileStore
{

    /// <summary>
    /// 存储结构基础类
    /// </summary>
    public  class StoreNodeQuery
    {

        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        [Field(null, "node_id")]
        public string id { get; set; }


        /// <summary>
        /// 父栏目Id
        /// </summary>
        [Field(null, "node_pid")]
        public string pid { get; set; }


        /// <summary>
        /// 栏目名称
        /// </summary>
        [Field(null, "node_name")]
        public string name { get; set; }


        /// <summary>
        ///栏目类型
        /// </summary>
        [Field(null, "node_type")]
        public int  type { get; set; }


        /// <summary>
        /// 上传方式
        /// </summary>
        [Field(null, "upload_type")]
        public int utype { get; set; }


        /// <summary>
        ///上传配置
        /// </summary>
        [Field(null, "upload_config")]
        public string config { get; set; }


        /// <summary>
        ///创建文件夹类型
        /// </summary>
        [Field(null, "folder_type")]
        public string fdtype { get; set; }


        /// <summary>
        ///生成目录
        /// </summary>
        [Field(null, "publish_root")]
        public string root { get; set; }


        /// <summary>
        ///发布url
        /// </summary>
        [Field(null, "publish_url")]
        public string puburl { get; set; }


        /// <summary>
        ///预览url
        /// </summary>
        [Field(null, "preview_url")]
        public string preurl { get; set; }


        /// <summary>
        ///加密推送
        /// </summary>
        [Field(null, "pull_encrypt")]
        public int  encrypt { get; set; }


        /// <summary>
        ///加密算法
        /// </summary>
        [Field(null, "encrypt_method")]
        public string method { get; set; }


        /// <summary>
        ///允许转换
        /// </summary>
        [Field(null, "allow_change")]
        public int  change { get; set; }


        /// <summary>
        ///创建人
        /// </summary>
        [Field(null, "creator")]
        public string creator { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string  ctime { get; set; }


    }
}
