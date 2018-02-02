using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Template
{
    public class TpFtpQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// FTP名称
        /// </summary>
        [Field(null, "ftp_name")]
        public string id { get; set; }

        /// <summary>
        /// 根目录
        /// </summary>
        [Field(null, "ftp_root")]
        public string root { get; set; }

        /// <summary>
        ///FTP服务器
        /// </summary>
        [Field(null, "ftp_server")]
        public string server { get; set; }

        /// <summary>
        /// FTP端口
        /// </summary>
        [Field(null, "ftp_port")]
        public int port { get; set; }

        /// <summary>
        /// FTP用户名
        /// </summary>
        [Field(null, "ftp_user")]
        public string user { get; set; }

        /// <summary>
        /// FTP用户密码
        /// </summary>
        [Field(null, "ftp_pwd")]
        public string pwd { get; set; }

        /// <summary>
        /// FTP方式
        /// </summary>
        [Field(null, "ftp_passive")]
        public int type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Field(null, "ftp_note")]
        public string note { get; set; }


        /// <summary>
        ///创建人
        /// </summary>
        [Field(null, "creator")]
        public string ceator { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string ctime { get; set; }
    }
}
