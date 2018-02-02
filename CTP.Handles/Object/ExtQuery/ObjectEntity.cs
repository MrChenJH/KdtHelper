using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{
    /// <summary>
    /// 指标数据对象基础类
    /// </summary>
    public  class ObjectEntity
    {
        public bool success { get; set; }

        public List<ObjTitleEntity> objTitle { get; set; }

        public string objHead { get; set; }

        public string objFoot { get; set; }

        public int isDyRow { get; set; }


        public List<TablesEntity> tables { get; set; }

    }

    public class TablesEntity
    {

        public List<IndexTreeNode> cols { get; set; }

        public List<IndexTreeNode>  rows { get; set; }
    }

     public class ObjTitleEntity
    {
        public  string name { get; set; }

        public string style { get; set; }
    }



}
