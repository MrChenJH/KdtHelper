using CTP.Handles.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles.Object
{

    /// <summary>
    ///  数据对象初始化数据
    /// </summary>
    public static  class ObjectData
    {

        /// <summary>
        /// 系统数据对象集合
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
        /// 常规数据对象默认值
        /// </summary>
        public static List<SysObjDictQuery> CgObj
        {
            get
            {
                List<SysObjDictQuery> dic = new List<SysObjDictQuery>();
                dic.Add(new SysObjDictQuery() { type = 0, feild = "auto_no", des = "自增编号", ftype = "int", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 0, feild = "id_leaf", des = "编号", ftype = "string", len = 50, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 0, feild = "d_name", des = "名称", ftype = "string", len = 200, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 0, feild = "d_tags", des = "标签集", ftype = "string", len = 60, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 0, feild = "d_content", des = "内容", ftype = "text", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 0, feild = "d_status", des = "状态", ftype = "tinyint", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 0, feild = "creator", des = "创建人", ftype = "string", len = 30, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 0, feild = "create_time", des = "创建时间", ftype = "string", len = 20, sys = 0 });

                return dic;
            }
        }

        /// <summary>
        /// 资源数据对象默认值
        /// </summary>
        public static List<SysObjDictQuery> ZyObj
        {
            get
            {
                List<SysObjDictQuery> dic = new List<SysObjDictQuery>();
                dic.Add(new SysObjDictQuery() { type = 1, feild = "auto_no", des = "自增编号", ftype = "int", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "id_leaf", des = "编号", ftype = "string", len = 16, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "d_name", des = "名称", ftype = "string", len = 200, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "d_tags", des = "标签集", ftype = "string", len = 60, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "d_type", des = "数据类型", ftype = "tinyint", len = 60, sys = 0 });
                //dic.Add(new SysObjDictQuery() { type = 1, feild = "d_pubtime", des = "发布时间", ftype = "string", len = 20, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "d_content", des = "内容", ftype = "text", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "d_status", des = "状态", ftype = "tinyint", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "is_share", des = "是否分享", ftype = "tinyint", len = 60, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "need_pwd", des = "是否需要密码", ftype = "tinyint", len = 60, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "is_comment", des = "是否评论", ftype = "tinyint", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "is_support", des = "是否点赞", ftype = "tinyint", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "creator", des = "创建人", ftype = "string", len = 30, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 1, feild = "create_time", des = "创建时间", ftype = "string", len = 20, sys = 0 });

                return dic;
            }
        }


        /// <summary>
        /// 检索数据对象默认值
        /// </summary>
        public static List<SysObjDictQuery> JsObj
        {
            get
            {
                List<SysObjDictQuery> dic = new List<SysObjDictQuery>();
                dic.Add(new SysObjDictQuery() { type = 2, feild = "auto_no", des = "自增编号", ftype = "int", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "id_leaf", des = "编号", ftype = "string", len = 16, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "d_name", des = "名称", ftype = "string", len = 200, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "d_tags", des = "标签集", ftype = "string", len = 200, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "is_share", des = "摘要", ftype = "string", len = 500, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "is_comment", des = "首图", ftype = "string", len = 200, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "is_support", des = "作者", ftype = "string", len = 50, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "d_content", des = "内容", ftype = "text", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "main_cls", des = "主类", ftype = "string", len = 50, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "sub_cls", des = "分类", ftype = "string", len = 50, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "d_source", des = "来源", ftype = "string", len = 100, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "d_status", des = "状态", ftype = "tinyint", len = 0, sys = 0 });
                //dic.Add(new SysObjDictQuery() { type = 2, feild = "d_pubtime", des = "发布时间", ftype = "string", len = 20, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "creator", des = "创建人", ftype = "string", len = 30, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 2, feild = "create_time", des = "创建时间", ftype = "string", len = 20, sys = 0 });

                return dic;
            }
        }


        /// <summary>
        /// 指标数据对象默认值
        /// </summary>
        public static List<SysObjDictQuery> ZbObj
        {
            get
            {
                List<SysObjDictQuery> dic = new List<SysObjDictQuery>();
                dic.Add(new SysObjDictQuery() { type = 3, feild = "auto_no", des = "自增编号", ftype = "int", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 3, feild = "id_leaf", des = "编号", ftype = "string", len = 50, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 3, feild = "map_id", des = "关联编号", ftype = "string", len = 50, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 3, feild = "is_dy_row", des = "是否动态行", ftype = "tinyint", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 3, feild = "row_code", des = "行码", ftype = "string", len = 30, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 3, feild = "column_code", des = "列码", ftype = "string", len = 30, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 3, feild = "unit_val", des = "单位", ftype = "string", len = 20, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 3, feild = "c_value", des = "值", ftype = "string", len = 30, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 3, feild = "d_status", des = "状态", ftype = "tinyint", len = 0, sys = 0 });
                //dic.Add(new SysObjDictQuery() { type = 3, feild = "d_pubdate", des = "发布日期", ftype = "string", len = 10, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 3, feild = "creator", des = "创建人", ftype = "string", len = 30, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 3, feild = "create_time", des = "创建时间", ftype = "string", len = 20, sys = 0 });

                return dic;
            }
        }


        /// <summary>
        /// 微信数据对象默认值
        /// </summary>
        public static List<SysObjDictQuery> WxObj
        {
            get
            {
                List<SysObjDictQuery> dic = new List<SysObjDictQuery>();
                dic.Add(new SysObjDictQuery() { type = 4, feild = "auto_no", des = "自增编号", ftype = "int", len = 0, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 4, feild = "id_leaf", des = "编号", ftype = "string", len = 16, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 4, feild = "d_name", des = "名称", ftype = "string", len = 200, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 4, feild = "d_tags", des = "标签集", ftype = "string", len = 60, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 4, feild = "load_img", des = "图片", ftype = "string", len = 100, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 4, feild = "d_content", des = "内容", ftype = "string", len = 500, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 4, feild = "d_status", des = "状态", ftype = "tinyint", len = 0, sys = 0 });
                //dic.Add(new SysObjDictQuery() { type = 4, feild = "d_pubtime", des = "发布时间", ftype = "string", len = 20, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 4, feild = "creator", des = "创建人", ftype = "string", len = 30, sys = 0 });
                dic.Add(new SysObjDictQuery() { type = 4, feild = "create_time", des = "创建时间", ftype = "string", len = 20, sys = 0 });

                return dic;
            }
        }

        /// <summary>
        /// 获取对象类别
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        public static List<SysObjDictQuery> ObjTypeData(int _type, int ispublic)
        {
            List<SysObjDictQuery> objdata = new List<SysObjDictQuery>();

            switch (_type)
            {
                case 0:
                    objdata = ObjectData.CgObj;
                    if (ispublic == 1)    //若为发布状态则新增发布时间
                        objdata.Add(new SysObjDictQuery() { type = 0, feild = "public_time", des = "发布时间", ftype = "string", len = 20, sys = 0 });
                    break;

                case 1:
                    objdata = ObjectData.ZyObj;
                    if (ispublic == 1)
                        objdata.Add(new SysObjDictQuery() { type = 1, feild = "public_time", des = "发布时间", ftype = "string", len = 20, sys = 0 });
                    break;

                case 2:
                    objdata = ObjectData.JsObj;
                    if (ispublic == 1)
                        objdata.Add(new SysObjDictQuery() { type = 2, feild = "public_time", des = "发布时间", ftype = "string", len = 20, sys = 0 });
                    break;

                case 3:
                    objdata = ObjectData.ZbObj;
                    if (ispublic == 1)
                        objdata.Add(new SysObjDictQuery() { type = 3, feild = "public_time", des = "发布时间", ftype = "string", len = 20, sys = 0 });
                    break;

                case 4:
                    objdata = ObjectData.WxObj;
                    if (ispublic == 1)
                        objdata.Add(new SysObjDictQuery() { type = 4, feild = "public_time", des = "发布时间", ftype = "string", len = 20, sys = 0 });
                    break;

                default:
                    objdata = ObjectData.CgObj;
                    if (ispublic == 1)
                        objdata.Add(new SysObjDictQuery() { type = 0, feild = "public_time", des = "发布时间", ftype = "string", len = 20, sys = 0 });
                    break;
            }

            return objdata;
        }




    }
}
