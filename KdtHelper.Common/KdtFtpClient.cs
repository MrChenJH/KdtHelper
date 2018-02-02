using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace KdtHelper.Common
{
    /// <summary>
    /// FTP应用
    /// </summary>
    public sealed class KdtFtpClient : KdtDisposable
    {

        #region 构造函数

        /// <summary>
        /// 构造FTP基本数据
        /// </summary>
        /// <param name="_ip">ip地址</param>
        /// <param name="_uid">ftp用户名</param>
        /// <param name="_pwd">ftp用户密码</param>
        /// <param name="folderpath">指定FTP连接成功后的当前目录, 如果不指定即默认为根目录</param>
        /// <param name="_root">ftp根目录</param>
        /// <param name="_port">ftp端口号</param>
        /// <param name="_passive">ftp连接模式</param>
        public KdtFtpClient(string _ip, string _uid, string _pwd, string _folderpath = "", string _root = "/", string _port = "21", bool _passive = true)
        {
            this.FtpIp = _ip;
            this.FtpPort = _port;
            this.FtpPwd = _pwd;
            this.FolderPath = _folderpath;
            this.FtpRoot = _root;
            this.FtpUid = _uid;
            this.IsPassive = _passive;
            this.FtpUrl = "ftp://" + _ip;
        }

        /// <summary>
        /// ftp字符串
        /// </summary>
        /// <param name="ftpstr">格式：(列子)ip:127.0.0.1;uid:ftpuser;pwd:ftppwd;root:ftproot;port:ftpport;ps:ftppassive;</param>
        public KdtFtpClient(string ftpstr)
        {
            Regex reg = new Regex(@"\s*ip\s*[:：]\s*(\S*?)\s*[;；]\s*uid\s*[:：]\s*(\S*?)\s*[;；]\s*pwd\s*[:：]\s*(\S*?)\s*[;；]\s*" +
                @"root\s*[:：]\s*(\S*?)\s*[;；]\s*port\s*[:：]\s*(\S*?)\s*[;；]\s*ps\s*[:：]\s*(\S*?)\s*[;；]",
                RegexOptions.Multiline | RegexOptions.IgnoreCase);
            if (reg.IsMatch(ftpstr))
            {
                Match mc = reg.Match(ftpstr);
                this.FtpIp = mc.Groups[1].Value;
                this.FtpUid = mc.Groups[2].Value;
                this.FtpPwd = mc.Groups[3].Value;
                this.FtpRoot = string.IsNullOrEmpty(mc.Groups[4].Value) ? "/" : mc.Groups[4].Value;
                this.FtpPort = string.IsNullOrEmpty(mc.Groups[5].Value) ? "21" : mc.Groups[5].Value;
                this.IsPassive = string.IsNullOrEmpty(mc.Groups[6].Value) ? false : bool.Parse(this.FtpPort = mc.Groups[6].Value);
            }
        }

        /// <summary>
        /// 构造FTP基本数据
        /// </summary>
        /// <param name="_ip">ip地址</param>
        /// <param name="_uid">ftp用户名</param>
        /// <param name="_pwd">ftp用户密码</param>
        /// <param name="_root">ftp根目录</param>
        /// <param name="_port">ftp端口号</param>
        /// <param name="_passive">ftp连接模式</param>
        public static KdtFtpClient Create(string _ip, string _uid, string _pwd, string _folderpath = "", string _root = "/", string _port = "21", bool _passive = true)
        {
            return new KdtFtpClient(_ip, _uid, _pwd, _folderpath, _root, _port, _passive);
        }

        /// <summary>
        /// ftp字符串
        /// </summary>
        /// <param name="ftpstr">格式：(列子)ip:127.0.0.1;uid:ftpuser;pwd:ftppwd;root:ftproot;port:ftpport;ps:ftppassive;</param>
        public static KdtFtpClient Create(string ftpstr)
        {
            return new KdtFtpClient(ftpstr);
        }

        #endregion.

        #region 基本属性string ftpURI;

        public string FtpIp { get; set; }
        public string FtpUid { get; set; }
        public string FtpPwd { get; set; }
        public string FolderPath { get; set; }
        public string FtpUrl { get; set; }
        public string FtpRoot { get; set; }
        public string FtpPort { get; set; }
        public bool IsPassive { get; set; }

        #endregion.

        #region 异步调用委托

        /// <summary>
        /// FTP异步上传（不修改FTP上文件名称
        /// </summary>
        /// <param name="uploadfile">上传文件名</param>
        /// <param name="saveFolder">保存文件路径</param>
        public delegate void AsyncUpLoadFile(string _uploadfile, string _saveFolder);

        /// <summary>
        /// FTP异步上传（修改FTP上文件名称
        /// </summary>
        /// <param name="fileStream">上传文件流</param>
        /// <param name="saveFolder">保存文件路径</param>
        /// <param name="saveName">保存名称</param>
        public delegate void AsyncUpLoadFile_ModifyName(Stream _fileStream, string _saveFolder, string _saveName);

        /// <summary>
        /// FTP异步上传(上传栏目）
        /// </summary>
        /// <param name="clientFolder">需上传的文件夹路径</param>
        /// <param name="saveFolder">上传的FTP保存路径</param>
        /// <param name="recursion">是否递归上传文件夹的子文件夹及文件</param>
        /// <param name="cover">是否强行覆盖FTP上文件</param>
        public delegate void AsyncUpLoadFolder(string _clientFolder, string _saveFolder, bool _recursion, bool _cover);

        #endregion.

        #region 上传方法

        /// <summary>
        /// 上传文件（保持客户端与FTP端文件名一致）
        /// </summary>
        /// <param name="uploadfile">上传文件路径(本地文件路径）</param>
        /// <param name="saveFolder">上传至FTP文件目录</param>
        public void UpLoadFile(string uploadfile, string saveFolder)
        {
            try
            {
                if (!File.Exists(uploadfile)) { throw new Exception("不存在的上传文件"); }

                FileInfo fileInf = new FileInfo(uploadfile);
                string url = MakeFtpUrl(new StringBuilder(saveFolder));
                CreateFolder(saveFolder);
                url = MakeFtpUrl(url, new StringBuilder(fileInf.Name));

                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = this.IsPassive;
                reqFTP.ContentLength = fileInf.Length;

                int buffLength = 8192;
                byte[] buff = new byte[buffLength];
                int contentLen;

                using (FileStream fs = fileInf.OpenRead())
                {
                    using (Stream strm = reqFTP.GetRequestStream())
                    {
                        contentLen = fs.Read(buff, 0, buffLength);
                        while (contentLen != 0)
                        {
                            strm.Write(buff, 0, contentLen);
                            contentLen = fs.Read(buff, 0, buffLength);
                        }
                        strm.Close();
                    }
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("上传文件:{0} 失败,原因为：{1}".ToFormat(uploadfile, ex.Message));
            }
        }

        /// <summary>
        /// 上传流文件到FTP服务器
        /// </summary>
        /// <param name="fileStream">流文件</param>
        /// <param name="saveFolder">保存路径</param>
        /// <param name="saveName">保存名</param>
        public void UpLoadFile(Stream fileStream, string saveFolder, string saveName)
        {
            try
            {
                CreateFolder(saveFolder);

                string url = MakeFtpUrl(new StringBuilder(saveFolder));
                url = MakeFtpUrl(url, new StringBuilder(saveName));

                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = this.IsPassive;
                reqFTP.ContentLength = fileStream.Length;

                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;

                using (fileStream)
                {
                    using (Stream strm = reqFTP.GetRequestStream())
                    {
                        contentLen = fileStream.Read(buff, 0, buffLength);
                        while (contentLen != 0)
                        {
                            strm.Write(buff, 0, contentLen);
                            contentLen = fileStream.Read(buff, 0, buffLength);
                        }
                        strm.Close();
                    }
                    fileStream.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("上传文件:{0} 失败,原因为：{1}".ToFormat(saveName, ex.Message));
            }
        }

        /// <summary>
        /// 传文件（保持客户端与FTP端文件名一致）
        /// </summary>
        /// <param name="clientFolder">客户端文件夹</param>
        /// <param name="saveFolder">FTP端保存目录</param>
        /// <param name="recursion">是否执行递归至子栏目</param>
        /// <param name="cover">是否强行覆盖文件</param>
        public void UpLoadFolder(string clientFolder, string saveFolder, bool recursion, bool cover)
        {
            List<string> ftpFiles = GetFileList(saveFolder);
            List<string> cfolders;
            List<string> cfiles;
            LoadClientFolderFiles(clientFolder, out cfolders, out cfiles);
            // 处理文件
            foreach (string file in cfiles)
            {
                if (cover)
                    this.UpLoadFile(file, saveFolder);
                else if (!ftpFiles.Contains(GetLastFileName(file), StringComparer.OrdinalIgnoreCase))
                    this.UpLoadFile(file, saveFolder);
                else
                {
                    // 跳过重复的文件
                }
            }
            foreach (string folder in cfolders) // 处理栏目
            {
                string lastName = GetLastFileName(folder);
                if (!ftpFiles.Contains(lastName, StringComparer.OrdinalIgnoreCase))
                    CreateFolder(MakeFtpUrl(saveFolder, new StringBuilder(lastName)));
                if (recursion)
                    UpLoadFolder(folder, MakeFtpUrl(saveFolder, new StringBuilder(lastName)), recursion, cover);
            }
        }

        #endregion.

        #region 下载方法

        /// <summary>
        /// 从ftp上下载文件到本地（执行端）
        /// </summary>
        /// <param name="saveFolder">本来保存路径</param>
        /// <param name="saveName">本地保存名</param>
        /// <param name="fileName">Ftp上文件名称</param>
        public void DownLoadFile(string saveFolder, string saveName, string fileName)
        {
            CreateClientFolder(saveFolder);
            using (FileStream OutputStream = new FileStream(saveFolder + "\\" + saveName, FileMode.Create))
            {
                try
                {
                    FtpWebRequest ReqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(MakeFtpUrl(new StringBuilder(fileName))));
                    ReqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                    ReqFTP.UseBinary = true;
                    ReqFTP.UsePassive = this.IsPassive;
                    ReqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);

                    using (FtpWebResponse response = (FtpWebResponse)ReqFTP.GetResponse())
                    {
                        using (Stream FtpStream = response.GetResponseStream())
                        {
                            long Cl = response.ContentLength;
                            int bufferSize = 2048;
                            int readCount;
                            byte[] buffer = new byte[bufferSize];
                            readCount = FtpStream.Read(buffer, 0, bufferSize);
                            while (readCount > 0)
                            {
                                OutputStream.Write(buffer, 0, readCount);
                                readCount = FtpStream.Read(buffer, 0, bufferSize);
                            }
                            FtpStream.Close();
                        }
                        response.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("下载文件:{0} 失败,原因为：{1}".ToFormat(fileName, ex.Message));
                }
            }
        }

        /// <summary>
        /// 下载FTP文件
        /// </summary>
        /// <param name="ftpFileName">文件相对FTP路径</param>
        /// <param name="_callback">回调保存文件方法</param>
        public void DownLoadFile(string ftpFileName, Action<byte[], int> _callback)
        {
            try
            {
                if (_callback == null) throw new Exception("未设置接收FTP文件方法！");

                FtpWebRequest ReqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(MakeFtpUrl(new StringBuilder(ftpFileName))));
                ReqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                ReqFTP.UseBinary = true;
                ReqFTP.UsePassive = this.IsPassive;
                ReqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);

                using (FtpWebResponse response = (FtpWebResponse)ReqFTP.GetResponse())
                {
                    using (Stream FtpStream = response.GetResponseStream())
                    {
                        long Cl = response.ContentLength;
                        int bufferSize = 2048;
                        int readCount;
                        byte[] buffer = new byte[bufferSize];
                        readCount = FtpStream.Read(buffer, 0, bufferSize);
                        while (readCount > 0)
                        {
                            _callback(buffer, readCount);
                            readCount = FtpStream.Read(buffer, 0, bufferSize);
                        }
                        FtpStream.Close();
                    }
                    response.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("下载文件:{0} 失败,原因为：{1}".ToFormat(ftpFileName, ex.Message));
            }
        }

        #endregion.

        #region 其他公共方法

        /// <summary>
        /// 测试FTP
        /// </summary>
        /// <returns>测试FTP是否可用</returns>
        public bool TestFtp()
        {
            bool check = false;
            try
            {
                string url = GetFtpCn();
                if (url.LastIndexOf("/") == url.Length - 1) url = url + "test.txt";
                else url = url + "/test.txt";
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UsePassive = this.IsPassive;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
                check = true;
            }
            catch (Exception ex)
            {
                throw new Exception("测试连接FTP服务失败:{0}".ToFormat(ex.Message));
            }
            return check;
        }

        /// <summary>
        /// 删除FTP上的文件
        /// </summary>
        /// <param name="files">文件集合</param>
        public void DeleteFile(List<string> files)
        {
            foreach (string file in files)
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(MakeFtpUrl(new StringBuilder(file))));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                reqFTP.UsePassive = this.IsPassive;
                try
                {
                    using (FtpWebResponse Response = (FtpWebResponse)reqFTP.GetResponse())
                    {
                        long size = Response.ContentLength;
                        using (Stream datastream = Response.GetResponseStream())
                        {
                            using (StreamReader sr = new StreamReader(datastream))
                            {
                                sr.ReadToEnd();
                                sr.Close();
                            }
                            datastream.Close();
                        }
                        Response.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("删除文件:{0} 失败,原因为：{1}".ToFormat(file, ex.Message));
                }
            }
        }

        /// <summary>
        /// 删除FTP上的文件
        /// </summary>
        /// <param name="file">文件名称</param>
        public void DeleteFile(string file)
        {
            FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(MakeFtpUrl(new StringBuilder(file))));
            reqFTP.UseBinary = true;
            reqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);
            reqFTP.KeepAlive = false;
            reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
            reqFTP.UsePassive = this.IsPassive;
            try
            {
                using (FtpWebResponse Response = (FtpWebResponse)reqFTP.GetResponse())
                {
                    long size = Response.ContentLength;
                    using (Stream datastream = Response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(datastream))
                        {
                            sr.ReadToEnd();
                            sr.Close();
                        }
                        datastream.Close();
                    }
                    Response.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("删除文件:{0} 失败,原因为：{1}".ToFormat(file, ex.Message));
            }
        }

        /// <summary>
        /// 获取栏目文件列表
        /// </summary>
        /// <param name="folderName">文件夹名</param>
        /// <returns>文件列表</returns>
        public List<string> GetFileList(string folderName)
        {
            try
            {
                List<string> downloadFiles = new List<string>();
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(MakeFtpUrl(new StringBuilder(folderName))));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.UsePassive = this.IsPassive;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    downloadFiles.Add(line);
                    line = reader.ReadLine();
                }
                reader.Close();
                response.Close();
                return downloadFiles;
            }
            catch
            {
                return null;
            }
        }

        #endregion.

        #region 私有方法

        /// <summary>
        /// 获取FTP连接字符串
        /// </summary>
        /// <returns>获取ftp连接字符串</returns>
        private string GetFtpCn()
        {
            StringBuilder ftpText = new StringBuilder("ftp://");
            ftpText.Append(this.FtpIp);
            if (!string.IsNullOrEmpty(this.FtpPort))
            {
                ftpText.Append(":");
                ftpText.Append(this.FtpPort);
            }
            ftpText.Append(this.FtpRoot);
            return ftpText.ToString();
        }

        /// <summary>
        /// 创建客户端（执行端）栏目
        /// </summary>
        /// <param name="folderPath">栏目路径</param>
        private void CreateClientFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        /// <summary>
        /// 拼接FTP连接路径
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>完整的FTP路径</returns>
        private string MakeFtpUrl(StringBuilder fileName)
        {
            if (fileName.Length > 0)
            {
                fileName = fileName.Replace("\\", "/");
                fileName = fileName.Replace("/", "", 0, 1);
            }
            return this.GetFtpCn() + fileName;
        }

        /// <summary>
        /// 拼接文件路径
        /// </summary>
        /// <param name="preStr">前字符串</param>
        /// <param name="sufStr">后字符串</param>
        /// <returns>完整的路径</returns>
        private string MakeFtpUrl(string preStr, StringBuilder sufStr)
        {
            preStr = preStr.Replace("\\", "/");
            sufStr = sufStr.Replace("\\", "/");
            if (sufStr.Length > 0)
                sufStr = sufStr.Replace("/", "", 0, 1);
            if (preStr.LastIndexOf("/") == preStr.Length - 1)
            {
                return preStr + sufStr;
            }
            else
                return preStr + "/" + sufStr;
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="fullName">全名</param>
        /// <returns>文件名</returns>
        private string GetLastFileName(string fullName)
        {
            fullName = fullName.Replace("\\", "/");
            return fullName.Substring(fullName.LastIndexOf("/"));
        }

        /// <summary>
        /// 创建FTP文件目录
        /// </summary>
        /// <param name="folderPath">文件目录</param>
        private void CreateFolder(string folderPath)
        {
            folderPath = folderPath.Replace("\\", "/");
            if (folderPath.Contains("/"))
            {
                CreateFolder(folderPath.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries), 0, this.GetFtpCn());
            }
            else
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(MakeFtpUrl(new StringBuilder(folderPath))));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UsePassive = this.IsPassive;
                try
                {
                    FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                    response.Close();
                }
                catch { }
            }
        }

        /// <summary>
        /// 创建FTP文件夹
        /// </summary>
        private void CreateFolder(string[] paths, int index, string url)
        {
            if (index < paths.Length)
            {
                if(index==0)
                    url = MakeFtpUrl(url, new StringBuilder());
                //else
                   // url = MakeFtpUrl(url, new StringBuilder(paths[index-1]));
                if (!CheckFtpFolder(url, paths[index]))
                {
                    url = MakeFtpUrl(url, new StringBuilder(paths[index]));
                    FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                    reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                    reqFTP.UseBinary = true;
                    reqFTP.UsePassive = this.IsPassive;
                    reqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);
                    reqFTP.KeepAlive = false;


                    try
                    {
                        FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                        Stream ftpStream = response.GetResponseStream();

                        ftpStream.Close();
                        response.Close();
                    }
                    catch { }
                }
                else
                {
                    url = MakeFtpUrl(url, new StringBuilder(paths[index]));
                }
                   
                CreateFolder(paths, index + 1, url);
            }
        }

        /// <summary>
        /// 检测FTP栏目是否存在
        /// </summary>
        private bool CheckFtpFolder(string url, string floder)
        {
            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                reqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);
                Stream responseStream = reqFTP.GetResponse().GetResponseStream();
                MemoryStream stream = new MemoryStream();
                responseStream.CopyTo(stream);
                string content = ASCIIEncoding.UTF8.GetString(stream.ToArray());
                responseStream.Close();
                return content.Contains(floder);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 解析读取本地文件夹文件
        /// </summary>
        /// <param name="folderPath">本地文件夹路径</param>
        /// <param name="folders">子栏目列表</param>
        /// <param name="files">栏目下文件</param>
        private void LoadClientFolderFiles(string folderPath, out List<string> folders, out List<string> files)
        {
            folders = new List<string>();
            files = new List<string>();
            if (Directory.Exists(folderPath))
            {
                folders = Directory.GetDirectories(folderPath).ToList();
                files = Directory.GetFiles(folderPath).ToList();
            }
        }

        #endregion.

        #region 判断文件是否存在

        /// <summary>
        /// 判断当前目录下指定的子目录是否存在
        /// </summary>
        /// <param name="RemoteDirectoryName">指定的目录名</param>
        public bool DirectoryExist(string RemoteDirectoryName)
        {
            string[] dirList = GetDirectoryList();
            foreach (string str in dirList)
            {
                if (str.Trim() == RemoteDirectoryName.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 判断当前目录下指定的文件是否存在
        /// </summary>
        /// <param name="RemoteFileName">远程文件名</param>
        public bool FileExist(string RemoteFileName)
        {
            string[] fileList = GetFilesList("*.*");
            foreach (string str in fileList)
            {
                if (str.Trim() == RemoteFileName.Trim())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取当前目录下所有的文件夹列表(仅文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] GetDirectoryList()
        {
            string[] drectory = GetFilesDetailList();
            var m = "";

            foreach (string str in drectory)
            {
                int dirPos = str.IndexOf("<DIR>");
                if (dirPos > 0)
                {
                    /*判断 Windows 风格*/
                    m += str.Substring(dirPos + 5).Trim() + "\n";
                }
                else if (!str.Trim().IsNullOrEmpty())
                {
                    if (str.Trim().Substring(0, 1).ToUpper() == "D")
                    {
                        /*判断 Unix 风格*/
                        string dir = str.Substring(54).Trim();
                        if (dir != "." && dir != "..")
                        {
                            m += dir + "\n";
                        }
                    }
                }
            }


            char[] n = new char[] { '\n' };
            return m.Trim('\n').Split(n);
        }

        /// <summary>
        /// 获取当前目录下明细(包含文件和文件夹)
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesDetailList()
        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpUrl + this.FolderPath));
                ftp.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                ftp.UsePassive = false;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);

                //while (reader.Read() > 0)
                //{
                //

                string line = reader.ReadLine();
                //line = reader.ReadLine();
                //line = reader.ReadLine();

                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                if (!result.ToString().IsNullOrEmpty())
                {
                    result.Remove(result.ToString().LastIndexOf("\n"), 1);
                }
                reader.Close();
                response.Close();

                return result.ToString().Split('\n');

            }
            catch (Exception ex)
            {
                downloadFiles = null;
                throw new Exception("FtpHelper  Error --> " + ex.Message);
            }
        }

        /// <summary>
        /// 获取当前目录下文件列表(仅文件)
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesList(string mask)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpUrl + "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.FtpUid, this.FtpPwd);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.UsePassive = false;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);

                string line = reader.ReadLine();
                while (line != null)
                {
                    if (mask.Trim() != string.Empty && mask.Trim() != "*.*")
                    {

                        string mask_ = mask.Substring(0, mask.IndexOf("*"));
                        if (line.Substring(0, mask_.Length) == mask_)
                        {
                            result.Append(line);
                            result.Append("\n");
                        }
                    }
                    else
                    {
                        result.Append(line);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                if (ex.Message.Trim() != "远程服务器返回错误: (550) 文件不可用(例如，未找到文件，无法访问文件)。")
                {
                    throw new Exception("FtpHelper GetFileList Error --> " + ex.Message.ToString());
                }
                return downloadFiles;
            }
        }



        #endregion

        #region  创建文件夹

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dirName"></param>
        public void MakeDir(string dirName)
        {
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.FtpUrl + dirName));
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                reqFTP.UseBinary = true;
                reqFTP.UsePassive = false;
                reqFTP.Credentials = new NetworkCredential(FtpUid, FtpPwd);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("FtpHelper MakeDir Error --> " + ex.Message);
            }
        }


        #endregion

        #region  删除文件夹

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="folderName"></param>
        public void RemoveDirectory(string folderName)
        {
            try
            {
                string uri = this.FtpUrl + folderName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

                reqFTP.Credentials = new NetworkCredential(FtpUid, FtpPwd);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;

                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

                //Insert_Standard_ErrorLog.Insert("FtpWeb", "Delete Error --> " + ex.Message + "  文件名:" + folderName);
            }
        }

        #endregion
    }
}
