using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.FileStore
{

    public class StoreNodeEntity
    {
        public int filetype { get; set; }
        public string filename { get; set; }

    }

    public static  class StoreNodeData
    {
        /// <summary>
        /// 栏目分类文件夹集合
        /// </summary>
        public static Dictionary<string, string> Object
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                return dic;
            }
        }

        /// <summary>
        /// 栏目分类文件夹默认值
        /// </summary>
        public static List<StoreNodeEntity> StoreNodeName
        {
            get
            {
                List<StoreNodeEntity> node = new List<StoreNodeEntity>();
                node.Add(new StoreNodeEntity() { filetype = 0 , filename = "图片" });
                node.Add(new StoreNodeEntity() { filetype = 1 , filename = "文档" });
                node.Add(new StoreNodeEntity() { filetype = 2 , filename = "视频" });
                node.Add(new StoreNodeEntity() { filetype = 3 , filename = "音频" });
                node.Add(new StoreNodeEntity() { filetype = 4 , filename = "脚本" });
                node.Add(new StoreNodeEntity() { filetype = 5 , filename = "CSS" });
                node.Add(new StoreNodeEntity() { filetype = 6 , filename = "可执行文件" });
                node.Add(new StoreNodeEntity() { filetype = 7 , filename = "压缩包" });
                node.Add(new StoreNodeEntity() { filetype = 8 , filename = "其他" });
                return node;
            }
        }

    }


}
