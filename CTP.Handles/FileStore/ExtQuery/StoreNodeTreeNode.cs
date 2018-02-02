using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.FileStore
{
   public  class StoreNodeTreeNode
    {

        public StoreNodeTreeNode()
        {
            children = new List<StoreNodeTreeNode>();
        }

        /// <summary>
        /// 自增编号
        /// </summary>
        public int Autono { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string NodeId { get; set; }


        /// <summary>
        /// 父栏目Id
        /// </summary>
        public string NodePid { get; set; }


        /// <summary>
        /// 栏目名称
        /// </summary>
        public string NodeName { get; set; }


        /// <summary>
        /// 子列表
        /// </summary>
        public List<StoreNodeTreeNode> children { get; set; }

    }
}
