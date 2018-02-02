using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KdtHelper.Common;
using System.Reflection;
using KdtHelper.Common.Json;
using CTP.API.Util;
using Microsoft.AspNetCore.Cors;
using System.Linq;
using KdtHelper.Core.ExecuterEx;
using KdtHelper;
using KdtHelper.Core;

namespace CTP.API.Controllers
{
    /// <summary>
    /// V1版本接口类
    /// </summary>
    [EnableCors("any")]
    [Route("[controller]")]
    public class kpyController : BaseController
    {
        #region 继承父类方法

        private object lockOp = new object();
        /// <summary>
        /// 共有操作
        /// </summary>
        /// <param name="handler">所属HANDLER</param>
        /// <param name="optype">操作类型<code>0:添加, 1 添加并返回自增编号，2 修改，3 AddOrUpdate, 8 删除</code></param>
        /// <param name="islock">是否加锁</param>
        /// <returns></returns>
        [HttpPost("Op")]
        public string OpExe(string handler, int optype, bool islock = false)
        {
            if (islock)
            {
                lock (lockOp)
                {
                    return base.Op(handler, optype);
                }
            }
            else
                return base.Op(handler, optype);
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        [HttpGet("GetMethod")]
        public string GetMethodExe(string handler, string method)
        {
            return base.GetMethod(handler, method);
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="start"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("SelectPage")]
        public string SelectPageExe(string handler, int start, int limit)
        {
            return base.SelectPage(handler, start, limit);
        }

        /// <summary>
        /// 分类列表查询
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpGet("GetCateList")]
        public string GetCateListExe(string handler)
        {
            return base.GetCateList(handler);
        }
        /// <summary>
        /// 查找单个字段值
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        [HttpGet("GetField")]
        public string GetFieldExe(string handler, string field)
        {
            return base.GetField(handler, field);
        }

        /// <summary>
        /// 读取所有数据集合
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public string GetAllExe(string handler)
        {
            return base.GetAll(handler);
        }

        /// <summary>
        /// 读取单条数据
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public string GetByIdExe(string handler)
        {
            return base.GetById(handler);
        }
        /// <summary>
        /// 添加时判重
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpPost("AddCheckName")]
        public string AddCheckNameExe(string handler)
        {
            return base.AddCheckName(handler);
        }
        /// <summary>
        /// 根据关键字模糊查找
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetListByKey")]
        public string GetListByKeyExe(string handler)
        {
            return base.GetListByKey(handler);
        }
        /// <summary>
        /// 获取类别信息
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        [HttpGet("GetCategory")]
        public string GetCategoryExe(string handler)
        {
            return base.GetCategory(handler);
        }
        /// <summary>
        /// 事务执行方法
        /// </summary>
        /// <param name="handler">操作类名</param>
        /// <param name="method">执行方法名</param>
        /// <returns></returns>
        [HttpPost("Trans")]
        public string TransExe(string handler, string method)
        {
            return base.Trans(handler, method);
        }
        /// <summary>
        /// 执行具体sql的方法
        /// </summary>
        /// <param name="method">字典Key值</param>
        /// <returns></returns>
        [HttpGet("GetSelectByKey")]
        public string GetSelectByKey(string method)
        {
            using (var helper = CreateHelper())
            {
                object[] args = new object[Request.Query.Keys.Count - 1];
                int num = 0;
                foreach (object key in Request.Query.Keys)
                {
                    if (key.ToString() != "method")
                    {
                        if (key.ToString() == "pwd")
                        {
                            args[num] = Request.Query[key.ToString()].ToString().ToMD5_16();
                        }
                        else
                        {
                            args[num] = Request.Query[key.ToString()].ToString();
                        }
                        num++;
                    }
                }
                string strsql = String.Format(new Handles.ExcuteSql().GetDictByKey(method), args);
                var result = helper.ExecuteQuery(strsql);
                return result.ToJson();
            }
        }

        #endregion

        #region  Menu
        /// <summary>
        /// 获取全部子栏目
        /// </summary>
        /// <param name="isAuth">是否带权限</param>
        /// <param name="tabid"></param>
        /// <param name="userid">用户名</param>
        /// <returns></returns>
        [HttpGet("GetMenuList")]
        public string GetMenuList(Boolean isAuth, int tabid = 0, string userid = "")
        {
            var mainMenuList = new List<Handles.Menu.MenuListQuery>();
            var result = new Text(true);
            return JsonDirectInvork(() =>
            {
                // 初始化handler
                var baseHandler = new Handles.Menu.MenuListHandler();
                baseHandler.name.FeildValue = userid.Convert("");
                using (var helper = CreateHelper("sqlserverdb"))
                {

                    if (tabid == 0)
                    {

                        mainMenuList = helper.SelectList<Handles.Menu.MenuListQuery>(baseHandler, isAuth ? "SelectAuthList" : "SelectList");
                    }
                    else
                    {
                        baseHandler.leaf.FeildValue = tabid;
                        mainMenuList = helper.SelectList<Handles.Menu.MenuListQuery>(baseHandler, isAuth ? "SelectAuthListByTabId" : "SelectListByTabId");
                    }
                    if (mainMenuList.Count > 0)
                    {
                        for (var num = 0; num < mainMenuList.Count; num++)
                        {
                            result.msg += "{\"TabId\":\"" + mainMenuList[num].leaf + "\",\"Id\":\"" + mainMenuList[num].autono + "\",\"name\":\"" + mainMenuList[num].name + "\",\"icon\":\"" + mainMenuList[num].icon + "\",\"url\":\"" + mainMenuList[num].link + "\",\"Child\":[";
                            baseHandler.pid.FeildValue = mainMenuList[num].autono;
                            var resultChild = helper.SelectList<Handles.Menu.MenuListQuery>(baseHandler, isAuth ? "SelectAuthListByPId" : "SelectListByPId");
                            if (resultChild.Count > 0)
                            {
                                for (var childNum = 0; childNum < resultChild.Count; childNum++)
                                {
                                    result.msg += "{\"TabId\":\"" + resultChild[childNum].leaf + "\",\"Id\":\"" + resultChild[childNum].autono + "\",\"name\":\"" + resultChild[childNum].name + "\",\"icon\":\"" + resultChild[childNum].icon + "\",\"url\":\"" + resultChild[childNum].link + "\",\"Child\":[]},";
                                }
                                result.msg = result.msg.Substring(0, result.msg.Length - 1);
                            }
                            result.msg += "]},";
                        }
                        result.msg = "[" + result.msg.Substring(0, result.msg.Length - 1) + "]";
                    }
                    return result.ToJson();

                }
            });
        }


        /// <summary>
        /// 获得主菜单列表
        /// </summary>
        /// <param name="isAuth">带权限查询</param>
        /// <param name="userid">用户名</param>
        /// <returns></returns>
        [HttpGet("GetTabList")]
        public string GetTabList(Boolean isAuth, string userid = "")
        {
            using (var helper = CreateHelper("sqlserverdb"))
            {
                var handler = new Handles.Menu.MenuTopHandler();
                handler.name.FeildValue = userid.Convert("");
                return JsonEntityInvork<List<Handles.Menu.MenuTopQuery>>(() =>
                {
                    return helper.SelectList<Handles.Menu.MenuTopQuery>(handler, isAuth ? "SelectAuthTab" : "SelectAllTab");
                });
            }
        }

        /// <summary>
        /// 获取指标查询列表
        /// </summary>
        /// <param name="pname">第一级菜单</param>
        /// <param name="name">第二级菜单</param>
        /// <param name="iname">指标名称</param>
        /// <param name="start">开始</param>
        /// <param name="limit">分页数</param>
        /// <returns></returns>
        [HttpGet("GetIndexPageList")]
        public string GetIndexPageList(string pname, string name, string iname, int start = 0, int limit = 10)
        {
            string str_where = " where 1 = 1 ";
            if (!String.IsNullOrEmpty(pname))
            {
                str_where += "and parant_name = '" + pname + "'";
            }
            if (!String.IsNullOrEmpty(name))
            {
                str_where += " and name_code = '" + name + "'";
            }
            KdtPageEx page = new KdtPageEx(start, limit);
            using (var helper = CreateHelper("sqlserverdb"))
            {
                var baseHandler = new Handles.Index.StaticIndexHandler();
                baseHandler.name.FeildValue = str_where;
                baseHandler.iname.FeildValue = iname.Convert("");
                return JsonGridInvork<Handles.Index.StaticIndexQuery>((out int total) =>
                {
                    var list = helper.SelectPage<Handles.Index.StaticIndexQuery>(baseHandler, page);
                    total = page.total;
                    return list;
                });
            }
        }

        #endregion

        #region  内容录入
        /// <summary>
        /// 获取内容录入表单项
        /// </summary>
        /// <param name="pname">左侧菜单名称</param>
        /// <returns></returns>
        [HttpGet("GetIndexListByPname")]
        public string GetIndexListByPname(string pname)
        {
            var indexMenuList = new List<Handles.Index.StaticInfoQuery>();
            var result = new Text(true);
            return JsonDirectInvork(() =>
            {
                // 初始化handler
                var baseHandler = new Handles.Index.StaticInfoHandler();
                var indexHandler = new Handles.Index.StaticIndexHandler();
                using (var helper = CreateHelper("sqlserverdb"))
                {
                    baseHandler.pname.FeildValue = pname.Convert("");
                    indexMenuList = helper.SelectList<Handles.Index.StaticInfoQuery>(baseHandler, "GetIndexCategory");
                    if (indexMenuList.Count > 0)
                    {
                        for (var num = 0; num < indexMenuList.Count; num++)
                        {
                            var tableName = indexMenuList[num].ncode;
                            var objName = indexMenuList[num].oname;
                            result.msg += "{title:\"" + indexMenuList[num].name + "\",objtype:\"" + indexMenuList[num].otype + "\",tableName:\"" + tableName + "\",objName:\"" + indexMenuList[num].oname + "\",Children:[";
                            indexHandler.ncode.FeildValue = tableName;
                            indexHandler.iname.FeildValue = objName;
                            var resultChild = helper.SelectList<Handles.Index.StaticIndexQuery>(indexHandler, "GetByTableName");
                            if (resultChild.Count > 0)
                            {
                                for (var childNum = 0; childNum < resultChild.Count; childNum++)
                                {
                                    result.msg += "{\"text\":\"" + resultChild[childNum].iname + "\",\"type\":\"" + resultChild[childNum].type + "\",\"icode\":\"" + resultChild[childNum].icode + "\",\"content\":\"" + resultChild[childNum].content + "\",\"isvali\":\"" + resultChild[childNum].isvali + "\",\"vrule\":\"" + resultChild[childNum].rule + "\",\"isbak\":\"" + resultChild[childNum].isbak + "\",\"bak\":\"" + resultChild[childNum].bak + "\",\"feild\":\"" + resultChild[childNum].feild + "\"},";
                                }
                                result.msg = result.msg.Substring(0, result.msg.Length - 1);
                            }
                            result.msg += "]},";
                        }
                        result.msg = "[" + result.msg.Substring(0, result.msg.Length - 1) + "]";
                    }
                    return result.ToJson();

                }
            });
        }
        /// <summary>
        /// 内容审核  获取全部的指标对象
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllIndexList")]
        public string GetAllIndexList()
        {
            var indexMenuList = new List<Handles.Index.StaticInfoQuery>();
            var result = new Text(true);
            return JsonDirectInvork(() =>
            {
                // 初始化handler
                var baseHandler = new Handles.Index.StaticInfoHandler();
                var indexHandler = new Handles.Index.StaticIndexHandler();
                using (var helper = CreateHelper("sqlserverdb"))
                {
                    var parentMenuList = helper.SelectList<Handles.Index.StaticInfoQuery>(baseHandler, "GetCategory");
                    if (parentMenuList.Count > 0)
                    {
                        for (var parent = 0; parent < parentMenuList.Count; parent++)
                        {
                            baseHandler.pname.FeildValue = parentMenuList[parent].pname.Convert("");
                            result.msg += "{title:\"" + parentMenuList[parent].pname + "\",id:\"_id" + parent + "\",Children:[";
                            indexMenuList = helper.SelectList<Handles.Index.StaticInfoQuery>(baseHandler, "GetIndexCategory");
                            if (indexMenuList.Count > 0)
                            {
                                for (var num = 0; num < indexMenuList.Count; num++)
                                {
                                    var tableName = indexMenuList[num].ncode;
                                    result.msg += "{title:\"" + indexMenuList[num].name + "\",objtype:\"" + indexMenuList[num].otype + "\",tableName:\"" + tableName + "\",objName:\"" + indexMenuList[num].oname + "\",Children:[";
                                    indexHandler.ncode.FeildValue = tableName;
                                    indexHandler.iname.FeildValue = indexMenuList[num].oname.Convert("");
                                    var resultChild = helper.SelectList<Handles.Index.StaticIndexQuery>(indexHandler, "GetByTableName");
                                    if (resultChild.Count > 0)
                                    {
                                        for (var childNum = 0; childNum < resultChild.Count; childNum++)
                                        {
                                            result.msg += "{\"text\":\"" + resultChild[childNum].iname + "\",\"type\":\"" + resultChild[childNum].type + "\",\"icode\":\"" + resultChild[childNum].icode + "\",\"content\":\"" + resultChild[childNum].content + "\",\"isvali\":\"" + resultChild[childNum].isvali + "\",\"vrule\":\"" + resultChild[childNum].rule + "\",\"isbak\":\"" + resultChild[childNum].isbak + "\",\"bak\":\"" + resultChild[childNum].bak + "\",\"feild\":\"" + resultChild[childNum].feild + "\"},";
                                        }
                                        result.msg = result.msg.Substring(0, result.msg.Length - 1);
                                    }
                                    result.msg += "]},";
                                }
                                result.msg = result.msg.Substring(0, result.msg.Length - 1);
                            }
                            result.msg += "]},";
                        }
                        result.msg = "[" + result.msg.Substring(0, result.msg.Length - 1) + "]";
                    }
                    return result.ToJson();

                }
            });
        }

        /// <summary>
        /// 根据用户名统计表的数据
        /// </summary>
        /// <param name="pname"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("GetIndexValueByName")]
        public string GetIndexValueByName(string pname, string userid)
        {
            var indexMenuList = new List<Handles.Index.StaticInfoQuery>();
            var result = new Text(true);
            return JsonDirectInvork(() =>
            {
                // 初始化handler
                var baseHandler = new Handles.Index.StaticInfoHandler();
                var indexHandler = new Handles.Index.StaticIndexHandler();
                using (var helper = CreateHelper("sqlserverdb"))
                {
                    baseHandler.pname.FeildValue = pname.Convert("");
                    indexMenuList = helper.SelectList<Handles.Index.StaticInfoQuery>(baseHandler, "GetIndexCategory");
                    if (indexMenuList.Count > 0)
                    {
                        for (var num = 0; num < indexMenuList.Count; num++)
                        {
                            var tableName = indexMenuList[num].ncode;
                            var objName = indexMenuList[num].oname;
                            indexHandler.ncode.FeildValue = tableName;
                            indexHandler.iname.FeildValue = objName;
                            if (indexMenuList[num].otype.Convert("") == "0")
                            {
                                var resultChild = helper.ExecuteQuery("select * from [" + tableName + "] where id_leaf = '" + userid + "'");
                                result.msg += "\"" + tableName + "\":\"{";
                                if (resultChild.Count > 0)
                                {
                                    result.msg += resultChild.ToJson().Replace("[\"{", "").Replace("}\"]", "").ToString();
                                }
                                result.msg += "}\",";
                            }
                            else if (indexMenuList[num].otype.Convert("") == "3")
                            {
                                result.msg += "\"" + tableName + "\":[";
                                indexHandler.iname.FeildValue = userid;
                                var indexList = helper.SelectList<Handles.Object.IndexObjEntity>(indexHandler, "GetIndexObjData");
                                //var resultChild = helper.ExecuteQuery("select * from [" + tableName + "] where id_leaf = '" + userid + "'");
                                //var ss = resultChild.ToJson();
                                //var indexList = ss.ToEntity<List<Handles.Object.IndexObjEntity>>();
                                var rowlist = indexList.GroupBy(t => t.rcode).Select(g => (new { rcode = g.Key })).ToList();
                                var strIndex = "";
                                foreach (var row in rowlist)
                                {
                                    strIndex += "{\"rows\":\"" + row.rcode + "\",\"cols\":[";
                                    var colList = indexList.Where(g => g.rcode == row.rcode).ToList();
                                    foreach (var col in colList)
                                    {
                                        strIndex += "{\"value\":\"" + col.value + "\",\"ccode\":\"" + col.ccode + "\"},";
                                    }
                                    strIndex = strIndex.Substring(0, strIndex.Length - 1);
                                    strIndex += "]},";
                                }
                                if (strIndex.Length > 0)
                                {
                                    strIndex = strIndex.Substring(0, strIndex.Length - 1);
                                }
                                result.msg += (strIndex + "],");
                            }
                        }
                        result.msg = "{" + result.msg.Substring(0, result.msg.Length - 1) + "}";
                    }
                    return result.msg;

                }
            });
        }

        /// <summary>
        /// 获取科普护照指标对象内容
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("GetPassportIndex")]
        public string GetPassportIndex(string userid)
        {
            var indexMenuList = new List<Handles.Index.StaticInfoQuery>();
            var result = new Text(true);
            return JsonDirectInvork(() =>
            {
                // 初始化handler
                var baseHandler = new Handles.Index.StaticInfoHandler();
                var indexHandler = new Handles.Index.StaticIndexHandler();
                using (var helper = CreateHelper("sqlserverdb"))
                {
                    result.msg += "\"EB54337FB2589139\":[";
                    indexHandler.iname.FeildValue = userid;
                    indexHandler.ncode.FeildValue = "EB54337FB2589139";
                    var indexList = helper.SelectList<Handles.Object.IndexObjEntity>(indexHandler, "GetIndexObjData");
                    var rowlist = indexList.GroupBy(t => t.rcode).Select(g => (new { rcode = g.Key })).ToList();
                    var strIndex = "";
                    foreach (var row in rowlist)
                    {
                        strIndex += "{\"rows\":\"" + row.rcode + "\",\"cols\":[";
                        var colList = indexList.Where(g => g.rcode == row.rcode).ToList();
                        foreach (var col in colList)
                        {
                            strIndex += "{\"value\":\"" + col.value + "\",\"ccode\":\"" + col.ccode + "\"},";
                        }
                        strIndex = strIndex.Substring(0, strIndex.Length - 1);
                        strIndex += "]},";
                    }
                    if (strIndex.Length > 0)
                    {
                        strIndex = strIndex.Substring(0, strIndex.Length - 1);
                    }
                    result.msg += (strIndex + "],");
                    result.msg = "{" + result.msg.Substring(0, result.msg.Length - 1) + "}";
                    return result.msg;

                }
            });
        }
        #endregion

        #region 内容审核

        /// <summary>
        /// 获取工作台列表
        /// </summary>
        /// <param name="uid">基地名称</param>
        /// <param name="atype">所属区类型</param>
        /// <param name="status">状态</param>
        /// <param name="start">开始条数</param>
        /// <param name="limit">每页条数</param>
        /// <returns></returns>
        [HttpGet("GetStaticTotal")]
        public string GetStaticTotal(string uid, int atype = 0, string status = "", int start = 0, int limit = 10)
        {
            string str_where = "";
            if (String.IsNullOrEmpty(uid))
            {
                uid = "";
            }
            if (atype != 0)
            {
                str_where = " and  c.area_type in (" + atype + ")";
            }
            KdtPageEx page = new KdtPageEx(start, limit);
            using (var helper = CreateHelper("sqlserverdb"))
            {
                var baseHandler = new Handles.Index.StaticCheckTotalHandler();
                baseHandler.uid.FeildValue = uid;
                baseHandler.ctime.FeildValue = status;
                baseHandler.atime.FeildValue = str_where.Convert("");
                return JsonGridInvork<Handles.Index.StaticCheckTotalQuery>((out int total) =>
                {

                    var list = helper.SelectPage<Handles.Index.StaticCheckTotalQuery>(baseHandler, page);
                    total = page.total;
                    return list;
                });
            }
        }
        #endregion

        #region 评分
        /// <summary>
        /// 教育基地年检查询列表
        /// </summary>
        /// <param name="uid">基地名称模糊查找</param>
        /// <param name="classify">用户所在区</param>
        /// <param name="start">开始数</param>
        /// <param name="limit">页面总条数</param>
        /// <returns></returns>
        [HttpGet("GetYearTotalList")]
        public string GetYearTotalList(string uid, int classify = 0, int start = 0, int limit = 10)
        {
            if (String.IsNullOrEmpty(uid))
            {
                uid = "";
            }
            string str_where = " where a.user_id like '%" + uid + "%' ";
            if (classify != 0)
            {
                str_where += " and b.user_classify = " + classify;
            }
            KdtPageEx page = new KdtPageEx(start, limit);
            using (var helper = CreateHelper("sqlserverdb"))
            {
                var baseHandler = new Handles.Index.ScoreYearHandler();
                baseHandler.uid.FeildValue = str_where;
                return JsonGridInvork<Handles.Index.ScoreYearQuery>((out int total) =>
                {
                    var list = helper.SelectPage<Handles.Index.ScoreYearQuery>(baseHandler, page);
                    total = page.total;
                    return list;
                });
            }
        }

        /// <summary>
        /// 专家评分列表查询
        /// </summary>
        /// <param name="uid">基地名称模糊查找</param>
        /// <param name="expert">专家名</param>
        /// <param name="classify">用户所在区</param>
        /// <returns></returns>
        [HttpGet("GetExpertTotalList")]
        public string GetExpertTotalList(string uid, string expert, int classify = 0)
        {
            if (String.IsNullOrEmpty(uid))
            {
                uid = "";
            }
            string str_where = " where a.user_id like '%" + uid + "%' ";
            if (classify != 0)
            {
                str_where += " and c.user_classify = " + classify;
            }
            using (var helper = CreateHelper("sqlserverdb"))
            {
                var baseHandler = new Handles.Index.ScoreExpertHandler();
                baseHandler.uid.FeildValue = str_where;
                baseHandler.expert.FeildValue = expert;
                return JsonEntityInvork<List<Handles.Index.ScoreExpertQuery>>(() =>
                {
                    var list = helper.SelectList<Handles.Index.ScoreExpertQuery>(baseHandler, "GetExpertTotal");
                    return list;
                });
            }
        }

        /// <summary>
        /// 教育基地考核评分列表
        /// </summary>
        /// <param name="uid">基地名称模糊查找</param>
        /// <param name="classify">用户所在区</param>
        /// <returns></returns>
        //[HttpGet("GetCheckTotalList")]
        //public string GetCheckTotalList(string uid, int classify = 0, int start = 0, int limit = 10,string prop= "total_score",int desc = 1)
        //{
        //    if (String.IsNullOrEmpty(uid))
        //    {
        //        uid = "";
        //    }
        //    string str_where = " where a.user_id like '%" + uid + "%' ";
        //    if (classify != 0)
        //    {
        //        str_where += " and b.user_classify = " + classify;
        //    }
        //    KdtPageEx page = new KdtPageEx(start, limit);
        //    using (var helper = CreateHelper("sqlserverdb"))
        //    {
        //        var baseHandler = new Handles.Index.ScoreCheckHandler();
        //        baseHandler.uid.FeildValue = str_where;
        //        baseHandler.prop.FeildValue = prop;
        //        baseHandler.isdesc.FeildValue = desc;
        //        return JsonGridInvork<Handles.Index.ScoreCheckQuery>((out int total) =>
        //        {
        //            var list = helper.SelectPage<Handles.Index.ScoreCheckQuery>(baseHandler, page);
        //            total = page.total;
        //            return list;
        //        });
        //    }
        //}


        /// <summary>
        /// 操作日志记录
        /// </summary>
        /// <param name="uid">基地名称模糊查找</param>
        /// <param name="classify">用户所在区</param>
        /// <param name="start">用户所在区</param>
        /// <param name="limit">用户所在区</param>
        /// <returns></returns>
        [HttpGet("GetOperateList")]
        public string GetOperateList(string uid, int classify = 0, int start = 0, int limit = 10)
        {
            if (String.IsNullOrEmpty(uid))
            {
                uid = "";
            }
            string str_where = " where a.user_id like '%" + uid + "%' ";
            if (classify != 0)
            {
                str_where += " and b.user_classify = " + classify;
            }
            KdtPageEx page = new KdtPageEx(start, limit);
            using (var helper = CreateHelper("sqlserverdb"))
            {
                var baseHandler = new Handles.Index.StaticOperateHandler();
                baseHandler.uid.FeildValue = str_where;
                return JsonGridInvork<Handles.Index.StaticOperateQuery>((out int total) =>
                {
                    var list = helper.SelectPage<Handles.Index.StaticOperateQuery>(baseHandler, page);
                    total = page.total;
                    return list;
                });
            }
        }

        /// <summary>
        /// 首页统计
        /// </summary>
        /// <param name="classify">用户所在区</param>
        /// <returns></returns>
        [HttpGet("GetIndexCountList")]
        public string GetIndexCountList(int classify = 0)
        {
            string str_where = "";
            if (classify != 0)
            {
                str_where += " where b.user_classify = " + classify;
            }
            using (var helper = CreateHelper("sqlserverdb"))
            {
                var baseHandler = new Handles.Index.StaticCheckTotalHandler();
                baseHandler.uid.FeildValue = str_where;
                return JsonEntityInvork<List<Handles.Index.StaticCheckTotalQuery>>(() =>
                {
                    var list = helper.SelectList<Handles.Index.StaticCheckTotalQuery>(baseHandler, "GetIndexTotal");
                    return list;
                });
            }
        }


        /// <summary>
        /// 考核评分列表/综合评分列表
        /// </summary>
        /// <param name="uid">用户名</param>
        /// <param name="classify">所属区县</param>
        /// <param name="type">基地性质</param>
        /// <param name="start">起始页</param>
        /// <param name="limit">每页条数</param>
        /// <param name="feild">排序字段</param>
        /// <param name="isdesc">排序规则（1：desc,0：asc）</param>
        ///  <param name="checktype">（0：考核,1：综合）</param>
        /// <returns></returns>
        [HttpGet("GetScoreList")]
        public string GetScoreList(string uid, int classify = 0, string  type = "-1", int start = 0, int limit = 10, string feild = "create_time", int isdesc = 1,int checktype=0)
        {

            if (String.IsNullOrEmpty(uid))
            {
                uid = "";
            }
            string str_where = " where user_id like '%" + uid + "%' ";
            if (classify != 0)
            {
                str_where += " and user_classify = " + classify;
            }
            if (type != "-1")
            {
               str_where += " and  user_type in ("+ type.Convert("") + ") " ;
            }
            if(checktype == 0)
            {
                str_where += "  and  basis != 0";
            }

            KdtPageEx page = new KdtPageEx(start, limit);
            using (var helper = CreateHelper())
            {
                var baseHandler = new Handles.Index.StaticTotalScoreHandler();
                baseHandler.uid.FeildValue = str_where ;
                baseHandler.isdesc.FeildValue = isdesc.Convert(0);
                baseHandler.creator.FeildValue = feild.Convert("");
                return JsonGridInvork<Handles.Index.StaticTotalScoreEntity>((out int total) =>
                {
                    var list = helper.SelectPage<Handles.Index.StaticTotalScoreEntity>(baseHandler, page);
                    total = page.total;
                    return list;

                });
            }
        }


        ///// <summary>
        /////获取实体类属性值
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <param name="property"></param>
        ///// <returns></returns>
        //private object GetPropertyValue(object obj, string property)
        //{
        //    System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
        //    return propertyInfo.GetValue(obj, null);
        //}


        #endregion

        #region   User(用户)

        /// <summary>
        /// 用户扩展信息添加/修改
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddOrUpdateExtend")]
        public string AddOrUpdateExtend(string userid, string objdatas)
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    //（字段名 |##|字段值|&&|字段名|##|字段值）
                    string strsql = "";      //执行SQL语句
                    KdtParameterCollection p_params = new KdtParameterCollection();
                    p_params.AddParameter("UserId", userid, ProcInPutEnum.InPut);
                    //将行中的数据根据行的列名保存到数据库中
                    string filename = string.Empty;       //字段
                    string filenamevalue = string.Empty;    //字段值
                    if (!objdatas.IsNullOrEmpty())   //编辑
                    {
                        string[] filedatas = objdatas.Split(new string[] { "|&&|" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string str in filedatas)
                        {
                            string[] fileKeyVlaue = str.Split(new string[] { "|##|" }, StringSplitOptions.RemoveEmptyEntries);
                            if (fileKeyVlaue.Length > 0)
                            {
                                filenamevalue += String.Format("{0}=@{0},", fileKeyVlaue[0]);

                                if (fileKeyVlaue.Length == 2)
                                    p_params.AddParameter(fileKeyVlaue[0], fileKeyVlaue[1], ProcInPutEnum.InPut);
                                else
                                    p_params.AddParameter(fileKeyVlaue[0], "", ProcInPutEnum.InPut);
                            }
                        }
                        strsql = string.Format("update kdt_user_extend  set {0}  where user_id = @UserId", filenamevalue.Substring(0, filenamevalue.Length - 1));
                        helper.ExecuteNonQuery(strsql, p_params);

                    }
                    else   //添加
                    {
                        //获取最大autono
                        strsql = string.Format("select MAX(auto_no) as  maxid  from kdt_user_extend  ");
                        var result = helper.ExecuteQuery(strsql);
                        var maxidlist = result[0].ToString().Split(":")[1].Replace("}", "").Replace("\"", "").Convert("");
                        var autono = 0;
                        if (maxidlist != "0")
                            autono = maxidlist.Convert(0) + 1;
                        else
                            autono = 1;
                        p_params.AddParameter("auto_no", autono, ProcInPutEnum.InPut);
                        strsql = string.Format("insert into kdt_user_extend (auto_no,user_id,user_type) values (@auto_no,@UserId,0)");
                        helper.ExecuteNonQuery(strsql, p_params);
                    }
                    return new Text(true).ToJson();
                }
            });

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapid"></param>
        /// <param name="objdatas"></param>
        /// <returns></returns>
        [HttpPost("AddMenuAuth")]
        public string AddMenuAuth(string mapid = "", string objdatas = "")
        {
            using (var helper = CreateHelper())
            {
                string strDelsql = string.Format("delete from sys_menu_auth where map_id = '" + mapid + "'");   //删除该角色权限
                var delResult = helper.ExecuteQuery(strDelsql);
                //获取最大自增编号
                string maxIdsql = string.Format("select MAX(auto_no) as  maxid  from  sys_menu_auth ");
                var maxIdResult = helper.ExecuteQuery(maxIdsql);
                var maxid = maxIdResult[0].ToString().Split(":")[1].Replace("}", "").Replace("\"", "").Convert("");
                var autono = 0;
                if (maxid != "0")
                    autono = maxid.Convert(0) + 1;
                else
                    autono = 1;
                string insertSql = "";    //添加SQL
                if (!objdatas.IsNullOrEmpty())
                {
                    string[] filedatas = objdatas.Split(new string[] { "|&&|" }, StringSplitOptions.RemoveEmptyEntries);     //|&&|分隔条数
                    int id;    //自增编号
                    int maptype;    //权限类型
                    int topid;    //顶部菜单
                    int menuid;    //左侧菜单
                    string creator = "";    //创建人
                    string createtime = "";   //创建时间
                    for (var num = 0; num < filedatas.Length; num++)
                    {
                        id = (num + autono).Convert(0);
                        string[] fileKeyVlaue = filedatas[num].Split(new string[] { "|##|" }, StringSplitOptions.RemoveEmptyEntries);  //|&&|分隔字段值
                        createtime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                        if (fileKeyVlaue.Length > 0)
                        {
                            maptype = fileKeyVlaue[0].Convert(0);
                            topid = fileKeyVlaue[1].Convert(0);
                            menuid = fileKeyVlaue[2].Convert(0);
                            creator = fileKeyVlaue[3].Convert("");
                            insertSql += string.Format("insert into sys_menu_auth  values (" + id + ", '" + mapid + "' ," + maptype + " ," + topid + ", " + menuid + ", '" + creator + "','" + createtime + "') ;");
                        }
                    }
                    if (insertSql.Length > 0)
                        return helper.TransExecuteSql(insertSql).ToString();
                }
                else
                    return "99";    //仅做删除不需添加
                return "";
            }
        }

        #region 用户字典的添加、删除、查询等事件

        /// <summary>
        /// 用户添加新字段
        /// </summary>
        /// <param name="type">字段类型</param>
        /// <param name="len">字段长度</param>
        /// <param name="dis">字段描述</param>
        /// <param name="creator">创建人</param>
        /// <returns></returns>
        [HttpPost("AddUserDict")]
        public string AddUserDict(string type, int len, string dis, string creator)
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var handler = new Handles.User.UserDictHandler();
                    handler.utype.FeildValue = 0.Convert(0);
                    var result = helper.SelectField<string>(handler, "SelectMaxFeild");
                    if (result != "0")
                        handler.feild.FeildValue = "userprop" + (Int32.Parse(result.Substring(8, result.Length - 8)) + 1);
                    else
                        handler.feild.FeildValue = "userprop1";

                    handler.type.FeildValue = type.Convert("");
                    handler.len.FeildValue = len.Convert(0);
                    handler.dis.FeildValue = dis.Convert("");
                    handler.creator.FeildValue = creator.Convert("");
                    helper.Add(handler);
                    if (handler.Affected > 0)
                    {
                        result = helper.SelectField<string>(handler, "AddUserField");    //添加字段
                        if (result != "0")
                            return new Text(true, "添加成功").ToJson();
                        else
                            helper.Delete(handler);    //回滚删除
                    }
                    return new Text(false, "添加失败", 5002).ToJson();
                }
            });
        }


        /// <summary>
        /// 更新用户字段
        /// </summary>
        /// <param name="autono">自增编号</param>
        /// <param name="feild">字段名</param>
        /// <param name="type">字段类型</param>
        /// <param name="len">长度</param>
        /// <param name="dis">字段描述</param>
        /// <returns></returns>
        [HttpPost("UpdateUserDict")]
        public string UpdateUserDict(int autono, string feild, string type, int len, string dis)
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var handler = new Handles.User.UserDictHandler();
                    handler.autono.FeildValue = autono.Convert(0);  //userid和feild不可以做修改
                    handler.feild.FeildValue = feild.Convert("");
                    handler.type.FeildValue = type.Convert(0);
                    handler.len.FeildValue = len.Convert(0);
                    handler.dis.FeildValue = dis.Convert(0);
                    helper.Update(handler);
                    if (handler.Affected > 0)
                    {
                        var result = helper.SelectField<string>(handler, "ModifyUserField");
                        if (result != "0")
                            return new Text(true, "修改成功").ToJson();
                    }
                    return new Text(false, "修改失败", 5002).ToJson();
                }
            });
        }

        /// <summary>
        /// 删除用户字段
        /// </summary>
        /// <param name="feild">删除字段</param>
        /// <returns></returns>
        [HttpDelete("DeleteUserDict")]
        public string DeleteUserDict(string feild)
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    string[] data = feild.Split(",");
                    string sql = "";
                    foreach (var item in data)
                    {
                        var cdata = item.Split("'");
                        string onefeild = cdata[1];
                        sql += " drop " + onefeild + ",";
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    var handler = new Handles.User.UserDictHandler();
                    handler.feild.FeildValue = feild;
                    handler.creator.FeildValue = sql;
                    helper.TransExecute(handler, "DeleteUserFeild");
                    if (handler.Affected > 0)
                        return new Text(true, "删除成功").ToJson();
                    else
                        return new Text(false, "删除成功", 5002).ToJson();
                }
            });
        }

        /// <summary>
        /// 查询用户字典
        /// </summary>
        /// <param name="key">检索关键字</param>
        /// <param name="start">起始页</param>
        /// <param name="size">每页条数</param>
        /// <returns></returns>
        [HttpGet("GetUserDict")]
        public string GetUserDict(string key, int start, int size)
        {
            return JsonEntityInvork<List<Handles.User.UserQuery>>(() =>
            {
                using (var helper = CreateHelper())
                {
                    string sql = "where 1=1";
                    if (!key.IsNullOrEmpty())
                    {
                        sql += " and user_ext_display like '%" + key + "%'";
                    }
                    var handler = new Handles.User.UserDictHandler();
                    handler.creator.FeildValue = sql;
                    KdtPageEx page = new KdtPageEx()
                    {
                        selpage = "GetDictPage",
                        selpagetotal = "GetDictPageCount",
                        start = start + 1,
                        end = start + size
                    };
                    return helper.SelectPage<Handles.User.UserQuery>(handler, page);
                }
            });
        }


        #endregion

        #region 登录判断用户是否存在
        /// <summary>
        /// 登录用户判断
        /// </summary>
        /// <param name="id">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [HttpGet("Login")]
        public string Login(string id, string pwd)
        {
            using (var helper = CreateHelper())
            {
                var handler = new Handles.User.UserHandler();
                handler.id.FeildValue = id.Convert("");
                handler.pwd.FeildValue = pwd.ToMD5_16().Convert("");
                return JsonEntityInvork<List<Handles.User.UserQuery>>(() =>
                {
                    return helper.SelectList<Handles.User.UserQuery>(SetHandler<Handles.User.UserHandler>(), "SelectLoginUser");
                });
            }
        }


        #endregion



        #endregion

        #region   Object(数据对象)

        #region  数据对象的添加、删除、查询等事件


        /// <summary>
        /// 获取数据对象栏目树
        /// </summary>
        /// <param name="ischeck">是否查询子集(0:否,1:是)</param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet("GetObjListByKey")]
        public string GetObjListByKey(int ischeck = 0, string name = "", int type = 0, string category = "")
        {
            return JsonGridInvork<Handles.Object.SysObjListQuery>((out int total) =>
            {
                var result = new List<Handles.Object.SysObjListQuery>();
                total = 0;
                //string category = Request.Query["category"].Convert("");
                var handler = new Handles.Object.SysObjListHandler();
                handler.name.FeildValue = name.Convert("");
                handler.type.FeildValue = type.Convert(0);
                handler.category.FeildValue = category.Convert("");
                using (var helper = CreateHelper())
                {
                    if (ischeck == 1)  //查询子集
                        return result = helper.SelectList<Handles.Object.SysObjListQuery>(handler, "GetObjNameOfType");

                    else
                        //category为空时代表第二层，不为空时代表第三层
                        return result = helper.SelectList<Handles.Object.SysObjListQuery>(handler, category.IsNullOrEmpty() ? "GetCategoryOrName" : "GetObjByCategory");

                }
            });
        }


        /// <summary>
        /// 添加数据对象 
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="type">对象类别(0:常规,1:资源,2:检索,3:指标,4:微信)</param>
        /// <param name="category">对象类名</param>
        /// <param name="ispublic">是否发布(0:否,1:是)</param>
        /// <param name="creator">用户名</param>
        /// <returns></returns>
        [HttpPost("AddObj")]
        public string AddObj(string name, int type, string category, int ispublic, string creator)
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var a = name.Convert("").ToMD5_16();
                    var objentity = new Handles.Object.SysObjListHandler();
                    objentity.name.FeildValue = name.Convert("");
                    objentity.table.FeildValue = name.Convert("").ToMD5_16();
                    objentity.type.FeildValue = type.Convert(0);
                    objentity.category.FeildValue = category.Convert("");
                    objentity.ispublic.FeildValue = ispublic.Convert(0);
                    objentity.creator.FeildValue = creator.Convert("");
                    //第一步,判断是否存在该数据对象名
                    var result = helper.SelectField<string>(objentity, "CheckByName");
                    if (result != "0")
                        return new Text(false, "已存在同名执行项").ToJson();
                    //第二步，添加数据对象信息
                    helper.Add(objentity);
                    if (objentity.Affected > 0)
                    {
                        try
                        {
                            //第三步,创建数据对象表
                            helper.SelectField<string>(objentity, "CreateTable");
                        }
                        catch (Exception ex)
                        {
                            //回滚，删除已添加的数据对象信息
                            helper.Delete(objentity);
                            return new Text(false, "添加失败", 5002).ToJson();
                        }
                        //第四步，添加数据字典系统字段
                        var dictentity = new Handles.Object.SysObjDictHandler();
                        //判断对象类型
                        List<Handles.Object.SysObjDictQuery> objdata = Handles.Object.ObjectData.ObjTypeData(type.Convert(0), ispublic);
                        foreach (var _dict in objdata)
                        {
                            dictentity.name.FeildValue = name.Convert("");
                            dictentity.type.FeildValue = _dict.type.Convert(0);
                            dictentity.feild.FeildValue = _dict.feild.Convert("");
                            dictentity.des.FeildValue = _dict.des.Convert("");
                            dictentity.ftype.FeildValue = _dict.ftype.Convert("");
                            dictentity.len.FeildValue = _dict.len.Convert(0);
                            dictentity.sys.FeildValue = 0;    //0为系统，1为非系统
                            dictentity.creator.FeildValue = creator;
                            helper.Add(dictentity);
                            if (dictentity.Affected > 0)
                            {
                                if (_dict.feild == "auto_no") continue;
                                // 第五步，创建数据对象表字段
                                try
                                {
                                    helper.SelectField<string>(dictentity, "Sql_AddField");
                                    continue;
                                }
                                catch (Exception ex)
                                {
                                    //回滚， 删除表，数据对象记录，及字典记录
                                    helper.TransExecute(dictentity, "Sql_DelObjByName");
                                    return new Text(false, "添加失败", 5002).ToJson();
                                }
                            }
                            else
                            {
                                //回滚， 删除表，数据对象记录，及字典记录
                                helper.TransExecute(dictentity, "Sql_DelObjByName");
                                return new Text(false, "添加失败", 5002).ToJson();
                            }
                        }
                    }
                    else
                        return new Text(false, "添加失败", 5002).ToJson();

                    return new Text(true, "添加成功").ToJson();
                }
            });

        }


        /// <summary>
        /// 修改数据对象信息
        /// </summary>
        /// <param name="ischange">发布状态是否改变</param>
        /// <param name="id">自增编号</param>
        /// <param name="name">对象名</param>
        /// <param name="type">对象类别(0:常规,1:资源,2:检索,3:指标,4:微信)</param>
        /// <param name="category">对象类名</param>
        /// <param name="isrow">动态行(0:否,1:是)</param>
        /// <param name="head">对象头</param>
        /// <param name="foot">对象尾</param>
        /// <param name="title">对象标题</param>
        /// <param name="ispublic">是否发布(0:否,1:是)</param>
        /// <param name="creator">用户名</param>
        /// <returns></returns>
        [HttpPost("UpdateObj")]
        public string UpdateObj(bool ischange, int id, string name, int type, string category, int isrow, string head, string foot, string title, int ispublic, string creator)
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    //第一步，判断发布状态是否已修改（添加/删除对象字段信息）
                    var dictentity = new Handles.Object.SysObjDictHandler();
                    if (ischange)
                    {
                        //判断对象类型
                        dictentity.name.FeildValue = name.Convert("");
                        dictentity.type.FeildValue = type.Convert(0);
                        dictentity.feild.FeildValue = "public_time";
                        dictentity.des.FeildValue = "发布时间";
                        dictentity.ftype.FeildValue = "string";
                        dictentity.len.FeildValue = "20";
                        dictentity.sys.FeildValue = 0;    //0为系统，1为非系统
                        dictentity.creator.FeildValue = creator.Convert("");
                        if (ispublic == 1)   //发布，之前为未发布状态（添加“发布时间”字段信息）
                        {
                            //添加发布时间字段信息到数据字典表
                            helper.Add(dictentity);
                            if (dictentity.Affected > 0)
                            {
                                //创建对象表字段
                                try
                                {
                                    helper.SelectField<string>(dictentity, "Sql_AddField");
                                }
                                catch (Exception ex)
                                {
                                    //回滚，删除对象字典表新增信息
                                    helper.Delete(dictentity);
                                    return new Text(false, "修改失败", 5002).ToJson();
                                }
                            }
                            else
                                return new Text(false, "修改失败", 5002).ToJson();
                        }
                        else    //不发布，之前为发布状态（删除字段信息）
                        {
                            //删除字典信息及字段信息
                            helper.TransExecute(dictentity, "Sql_DelObjDictByName");
                        }

                    }
                    //第二步修改数据对象表数据
                    var objentity = new Handles.Object.SysObjListHandler();
                    objentity.autono.FeildValue = id.Convert(0);
                    objentity.name.FeildValue = name.Convert("");     //对象名及对象类别不能修改
                    objentity.type.FeildValue = type.Convert(0);
                    objentity.category.FeildValue = category.Convert("");
                    objentity.dyrow.FeildValue = isrow.Convert(0);
                    objentity.head.FeildValue = head.Convert("");
                    objentity.foot.FeildValue = foot.Convert("");
                    objentity.title.FeildValue = title.Convert("");
                    objentity.ispublic.FeildValue = ispublic.Convert(0);
                    helper.Update(objentity);
                    if (objentity.Affected < 0)
                        helper.TransExecute(dictentity, "DelObjDictByName");      //回滚，删除对象字典表新增信息及对象表字段
                    return new Text(true, "修改成功").ToJson();
                }
            });
        }


        /// <summary>
        /// 删除数据对象
        /// </summary>
        /// <param name="type">对象类别(0:常规,1:资源,2:检索,3:指标,4:微信)</param>
        /// <param name="category">对象类名</param>
        /// <param name="name">对象名</param>
        /// <returns></returns>
        [HttpDelete("DeleteObj")]
        public string DeleteObj(int type, string category = "", string name = "")
        {

            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var entity = new Handles.Object.SysObjListHandler();
                    entity.type.FeildValue = type.Convert(0);
                    entity.category.FeildValue = category.Convert("");
                    if (name.IsNullOrEmpty())
                    {
                        //第一种，通过对象类别删除数据对象(objcategory为空)
                        //第二种，通过对象类别及对象类名删除数据对象
                        //第一步，查询该类别下的所有数据对象
                        var result = helper.SelectList<Handles.Object.SysObjListQuery>(entity, "GetObjNameList");
                        if (result.Count > 0)
                        {
                            //第二步，循环删掉其数据对象
                            foreach (var _obj in result)
                            {
                                entity.name.FeildValue = _obj.name.Convert("");
                                helper.TransExecute(entity, "Sql_DelObjByName");
                                if (entity.Affected > 0)
                                    continue;
                                else
                                    return new Text(false, "删除失败", 5002).ToJson();
                            }
                        }
                        else
                            return new Text(false, "删除失败", 5002).ToJson();
                    }
                    else
                    {
                        //第三种，删除单个对象
                        entity.name.FeildValue = name.Convert("");
                        helper.TransExecute(entity, "Sql_DelObjByName");
                        if (entity.Affected < 0) return new Text(false, "删除失败", 5002).ToJson();
                    }
                    return new Text(true, "删除成功").ToJson();
                }
            });
        }

        #endregion

        #region  对象字典的添加、删除、查询等事件


        /// <summary>
        /// 添加对象字典信息
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="type">对象类别(0:常规,1:资源,2:检索,3:指标,4:微信)</param>
        /// <param name="des">字段描述</param>
        /// <param name="ftype">字段类型(string,int,text)</param>
        /// <param name="len">字段长度</param>
        /// <param name="def">默认值</param>
        /// <param name="creator">用户名</param>
        /// <returns></returns>
        [HttpPost("AddObjDict")]
        public string AddObjDict(string name, int type, string des, string ftype, int len, string def, string creator)
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var dictentity = new Handles.Object.SysObjDictHandler();
                    dictentity.name.FeildValue = name.Convert("");
                    dictentity.type.FeildValue = type.Convert(0);
                    //获取字段名
                    var result = helper.SelectField<string>(dictentity, "Sql_GetMaxObjFeild");
                    if (!result.IsNullOrEmpty())
                        dictentity.feild.FeildValue = "resourceprop" + (Int32.Parse(result.Substring(12, result.Length - 12)) + 1);
                    else dictentity.feild.FeildValue = "resourceprop1";
                    dictentity.des.FeildValue = des.Convert("");
                    dictentity.ftype.FeildValue = ftype.Convert("");
                    dictentity.len.FeildValue = len.Convert(0);
                    dictentity.fdefault.FeildValue = def.Convert("");
                    dictentity.sys.FeildValue = 1;    //0为系统，1为非系统
                    dictentity.creator.FeildValue = creator.Convert("");
                    //第一步，添加字典表数据
                    helper.Add(dictentity);
                    if (dictentity.Affected > 0)
                    {
                        //第二步，添加表字段
                        try
                        {
                            helper.SelectField<string>(dictentity, "Sql_AddField");
                        }
                        catch (Exception ex)
                        {
                            helper.Delete(dictentity);       //回滚，删除已添加的字典表数据
                            return new Text(false, "添加失败", 5002).ToJson();

                        }
                    }
                    else return new Text(false, "添加除失败", 5002).ToJson();

                    return new Text(true, "添加成功").ToJson();
                }

            });

        }


        /// <summary>
        /// 修改对象字典信息
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="feild">字段名</param>
        /// <param name="des">字段描述</param>
        /// <param name="ftype">字段类型</param>
        /// <param name="len">字段长度</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        [HttpPost("UpdateObjDict")]
        public string UpdateObjDict(string name, string feild, string des, string ftype, int len, string def)
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var dictentity = new Handles.Object.SysObjDictHandler();
                    dictentity.name.FeildValue = name.Convert("");
                    dictentity.feild.FeildValue = feild.Convert("");
                    dictentity.des.FeildValue = des.Convert("");
                    dictentity.ftype.FeildValue = ftype.Convert("");    //string类型不能改为其他类型，长度不能改小
                    dictentity.len.FeildValue = len.Convert(0);
                    dictentity.des.FeildValue = des.Convert("");
                    //第一步，修改数据字典表信息
                    helper.Update(dictentity);
                    if (dictentity.Affected > 0)
                    {
                        //第二步，修改数据对象表字段（失败则无法回滚已修改的字典表信息）
                        try
                        {
                            helper.SelectField<string>(dictentity, "Sql_ModifyField");
                        }
                        catch (Exception ex)
                        {
                            return new Text(false, "修改失败", 5002).ToJson();
                        }
                    }
                    else return new Text(false, "修改失败", 5002).ToJson();

                    return new Text(true, "修改成功").ToJson();
                }
            });

        }





        #endregion

        #region  指标的添加、删除、查询等事件

        /// <summary>
        /// 根据数据对象名查询该数据对象表数据（指标）
        /// </summary>
        /// <param name="name">数据对象名</param>
        /// <param name="userid">用户名</param>
        /// <returns></returns>
        [HttpGet("GetIndexObjByName")]
        public string GetObjDataByName(string name, string userid)
        {
            using (var helper = CreateHelper())
            {
                string strsql = string.Format("select *  from [" + name.Convert("").ToMD5_16() + "] where  map_id  = " + userid + " ");
                var result = helper.ExecuteQuery(strsql);
                return result.ToJson();
            }

        }


        /// <summary>
        /// 通过数据对象名获取指标信息  
        /// </summary>
        /// <param name="name">对象名</param>
        /// <returns></returns>
        [HttpGet("GetIndexByName")]
        public string GetIndexByName(string name)
        {
            using (var helper = CreateHelper())
            {
                //获取数据对象信息
                var objentity = new Handles.Object.SysObjListHandler();
                objentity.name.FeildValue = name.Convert("");
                Handles.Object.SysObjListQuery objlist = helper.SelectEntity<Handles.Object.SysObjListQuery>(objentity, "GetById");
                if (!objlist.name.IsNullOrEmpty())
                {
                    Handles.Object.ObjectEntity obj = new Handles.Object.ObjectEntity();
                    obj.success = true;
                    Handles.Object.ObjTitleEntity title = new Handles.Object.ObjTitleEntity();
                    List<Handles.Object.ObjTitleEntity> titlelist = new List<Handles.Object.ObjTitleEntity>();
                    title.name = objlist.name;
                    title.style = "";
                    titlelist.Add(title);
                    obj.objTitle = titlelist;
                    obj.objHead = objlist.head;
                    obj.objFoot = objlist.foot;
                    obj.isDyRow = objlist.dyrow;
                    Handles.Object.TablesEntity table = new Handles.Object.TablesEntity();
                    List<Handles.Object.TablesEntity> tablelist = new List<Handles.Object.TablesEntity>();
                    //获取所有指标列与行信息
                    var rowentity = new Handles.Object.IndexRowHandler();
                    rowentity.name.FeildValue = name.Convert("");
                    List<Handles.Object.IndexEntity> result = helper.SelectList<Handles.Object.IndexEntity>(rowentity, "GetAllIndexByName");
                    if (result.Count > 0)
                    {
                        // List<Handles.Object.IndexEntity> list = result.ToString().ToEntity<List<Handles.Object.IndexEntity>>();
                        //指标行
                        List<Handles.Object.IndexEntity> rowlist = new List<Handles.Object.IndexEntity>();
                        rowlist = result.Where(n => !n.rowCode.IsNullOrEmpty()).ToList();
                        SetNode(0, null, rowlist);   //递归查询
                        table.rows = NodeList;

                        //指标列
                        NodeList = new List<Handles.Object.IndexTreeNode>();    //清空指标行信息
                        List<Handles.Object.IndexEntity> collist = new List<Handles.Object.IndexEntity>();
                        collist = result.Where(n => !n.colCode.IsNullOrEmpty()).ToList();
                        SetNode(0, null, collist);    //递归查询
                        table.cols = NodeList;
                        tablelist.Add(table);
                        obj.tables = tablelist;
                        return obj.ToJson();
                    }

                }
                return "查询失败";
            }

        }


        /// <summary>
        /// 删除指标行或指标列  
        /// </summary>
        /// <param name="id">自增编号</param>
        /// <param name="objname">数据对象名</param>
        /// <param name="rowcode">行码</param>
        /// <param name="colcode">列码</param>
        /// <returns></returns>
        [HttpDelete("DeleteRowOrColumn")]
        public string DeleteRowOrColumn(int id = 0, string objname = "", string rowcode = "", string colcode = "")
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    idlist += id.Convert("") + ",";   //Id集合
                    rowcodelist += "'" + rowcode + "' ,";  //行码集合
                    colcodelist += "'" + colcode + "' ,";   //列码集合
                    if (!rowcode.IsNullOrEmpty())    //删除行
                    {
                        //第一步查询要删除的项
                        DeleteIndex(id, rowcode, "");
                        var entity = new Handles.Object.IndexRowHandler();
                        entity.id.FeildValue = idlist.Convert("").TrimEnd(',');
                        entity.objname.FeildValue = objname.Convert("");
                        entity.code.FeildValue = rowcodelist.Convert("").TrimEnd(',');
                        //第二步删除行及指标数据
                        helper.TransExecute(entity, "DelRowById");
                        if (entity.Affected > 0)
                        {
                            return new Text(true, "删除成功").ToJson();
                        }
                    }
                    else if (!colcode.IsNullOrEmpty())     //删除列
                    {
                        //第一步查询要删除的项
                        DeleteIndex(id, "", colcode);
                        var entity = new Handles.Object.IndexColumnHandler();
                        entity.id.FeildValue = idlist.Convert("").TrimEnd(',');
                        entity.objname.FeildValue = objname.Convert("");
                        entity.code.FeildValue = colcodelist.Convert("").TrimEnd(',');
                        //第二步删除列及指标数据
                        helper.TransExecute(entity, "DelColumnById");
                        if (entity.Affected > 0)
                        {
                            return new Text(true, "删除成功").ToJson();
                        }
                    }
                    return new Text(false, "删除失败", 5002).ToJson();
                }
            });
        }
        /// <summary>
        /// 添加或修改对象表数据
        /// </summary>
        /// <param name="id_leaf">自增编号</param>
        /// <param name="tablename">表名</param>
        /// <param name="values">对应字段值字符串</param>
        /// <param name="creator">用户名</param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateTableData")]
        public string AddOrUpdateTableData([FromBody]CommonObject common )
        {
            string id_leaf = common.id_leaf;
            string tablename = common.tablename;
            string values = common.values;
            string creator = common.creator;
            string strsql = string.Format("select count(1) as id  from [" + tablename.Convert("") + "] where id_leaf = '" + id_leaf + "'");
            using (var helper = CreateHelper())
            {
                var result = helper.ExecuteQuery(strsql);
                var result1 = result[0].ToString().Split(":")[1].Replace("}", "").Replace("\"", "").Convert("");
                return AddOrUpdateTable(result1.Convert(0), id_leaf, tablename, values, creator);
                //if (result1 == "0")
                //{
                //   return AddOrUpdateTable(0, id_leaf, tablename, values, creator);
                //}
                //else
                //{
                //   return AddOrUpdateTable(1, id_leaf, tablename, values, creator);
                //}
            }
        }

        /// <summary>
        /// 添加或修改对象表数据
        /// </summary>
        /// <param name="id_leaf">自增编号</param>
        /// <param name="tablename">表名</param>
        /// <param name="values">对应字段值字符串</param>
        /// <param name="creator">用户名</param>
        /// <returns></returns>
        [HttpPost("OpSend")]
        public string OpSend([FromBody]CommonObject common)
        {
            string uid = common.uid;
            string creator = common.creator;
            string other = common.other;
            Handles.Index.StaticExcelDataHandler baseHander = new Handles.Index.StaticExcelDataHandler();
            baseHander.work.FeildValue = common.work;
            baseHander.other.FeildValue = other;
            baseHander.creator.FeildValue = creator;
            baseHander.uid.FeildValue = uid;
            using (var helper = CreateHelper())
            {
                helper.Update(baseHander);
                if (baseHander.Affected > 0 || baseHander.NewId() > 0)
                {
                    return new Text(true).ToJson();
                }
                throw new DbExecuteException();
            }
        }

        private object lockObjTable = new object();
        /// <summary>
        ///  指标对象添加用更新数据
        /// </summary>
        /// <param name="id_leaf">用户名</param>
        /// <param name="tablename">表名</param>
        /// <param name="row_code">行码</param>
        /// <param name="col_code">列码</param>
        /// <param name="value">值</param>
        /// <param name="creator">创建人</param>
        /// <returns></returns>
        private string AddOrUpdateObjTableData_Bak(string id_leaf = "", string tablename = "", string row_code = "", string col_code = "", string value = "", string creator = "")
        {
            using (var helper = CreateHelper())
            {
                KdtParameterCollection p_params = new KdtParameterCollection();
                p_params.AddParameter("IdLeaf", id_leaf, ProcInPutEnum.InPut);
                p_params.AddParameter("Rcode", row_code, ProcInPutEnum.InPut);
                p_params.AddParameter("Ccode", col_code, ProcInPutEnum.InPut);
                p_params.AddParameter("Value", value, ProcInPutEnum.InPut);
                p_params.AddParameter("Creator", creator, ProcInPutEnum.InPut);
                p_params.AddParameter("CreateTime", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), ProcInPutEnum.InPut);
                lock (lockObjTable)
                {
                    string strsql = string.Format("select count(1) id from [" + tablename.Convert("") + "] where id_leaf = '" + id_leaf + "' and row_code = '" + row_code + "' and column_code = '" + col_code + "'");
                    var typeResult = helper.ExecuteQuery(strsql);
                    var result1 = typeResult[0].ToString().Split(":")[1].Replace("}", "").Replace("\"", "").Convert("");
                    if (result1 == "0")
                    {
                        strsql = string.Format("select MAX(auto_no) as  maxid  from [" + tablename.Convert("") + "] ");
                        var result = helper.ExecuteQuery(strsql);
                        var maxidlist = result[0].ToString().Split(":")[1].Replace("}", "").Replace("\"", "").Convert("");
                        //var maxidlist = result.ToJson().ToEntity<List<Handles.Object.MaxIdEntity>>();
                        var autono = 0;
                        if (maxidlist != "0")
                        {
                            autono = maxidlist.Convert(0) + 1;
                        }
                        else
                        {
                            autono = 1;
                        }
                        p_params.AddParameter("Id", autono, ProcInPutEnum.InPut);

                        strsql = string.Format("insert into [{0}] (auto_no,id_leaf,map_id,is_dy_row,row_code,column_code,unit_val,c_value,d_status,creator,create_time) values (@Id, @IdLeaf ,@IdLeaf ,1,@Rcode,@Ccode,'',@Value,1,@Creator,@CreateTime)", tablename);
                        helper.ExecuteNonQuery(strsql, p_params);
                    }
                    else
                    {
                        strsql = string.Format("update [{0}] set c_value=@Value,creator=@Creator,create_time=@CreateTime where id_leaf = @IDLeaf and row_code=@Rcode and column_code=@Ccode ", tablename);
                        helper.ExecuteNonQuery(strsql, p_params);
                    }
                    return new Text(true).ToJson();
                }
            }
        }
        /// <summary>
        /// 更新指标对象数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddOrUpdateObjTableData")]
        public string AddOrUpdateObjTableData([FromBody]CommonObject common)
        {
            string id_leaf = common.id_leaf;
            string tablename = common.tablename;
            string value = common.value;
            string creator = common.creator;

            using (var helper = CreateHelper())
            {
                string strDelsql = string.Format("delete from [" + tablename.Convert("") + "] where id_leaf = '" + id_leaf + "'");
                var delResult = helper.ExecuteQuery(strDelsql);
                if (String.IsNullOrEmpty(value)) { return ""; }
                string maxIdsql = string.Format("select MAX(auto_no) as  maxid  from [" + tablename.Convert("") + "] ");
                var maxIdResult = helper.ExecuteQuery(maxIdsql);
                var maxid = maxIdResult[0].ToString().Split(":")[1].Replace("}", "").Replace("\"", "").Convert("");
                var autono = 0;
                if (maxid != "0")
                {
                    autono = maxid.Convert(0) + 1;
                }
                else
                {
                    autono = 1;
                }
                string insertSql = "";
                string[] filedatas = value.Split(new string[] { "|&&|" }, StringSplitOptions.RemoveEmptyEntries);
                for (var num = 0; num < filedatas.Length; num++)
                {
                    string[] fileKeyVlaue = filedatas[num].Split(new string[] { "|##|" }, StringSplitOptions.RemoveEmptyEntries);
                    KdtParameterCollection p_params = new KdtParameterCollection();
                    if (fileKeyVlaue.Length > 0)
                    {
                        p_params.AddParameter("Id", (num + autono), ProcInPutEnum.InPut);
                        p_params.AddParameter("IdLeaf", id_leaf, ProcInPutEnum.InPut);
                        p_params.AddParameter("Rcode", fileKeyVlaue[0], ProcInPutEnum.InPut);
                        p_params.AddParameter("Ccode", fileKeyVlaue[1], ProcInPutEnum.InPut);
                        p_params.AddParameter("Value", fileKeyVlaue[2], ProcInPutEnum.InPut);
                        p_params.AddParameter("Creator", creator, ProcInPutEnum.InPut);
                        p_params.AddParameter("CreateTime", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), ProcInPutEnum.InPut);
                        //insertSql += string.Format("insert into [{0}] (auto_no,id_leaf,map_id,is_dy_row,row_code,column_code,unit_val,c_value,d_status,creator,create_time) values (@Id, @IdLeaf ,@IdLeaf ,1,@Rcode,@Ccode,'',@Value,1,@Creator,@CreateTime)", tablename);
                        insertSql += string.Format("insert into [{0}] values (" + (num + autono) + ", '" + id_leaf + "' ,'" + id_leaf + "' ,1,'" + fileKeyVlaue[0] + "','" + fileKeyVlaue[1] + "','','" + fileKeyVlaue[2] + "',1,'" + creator + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')", tablename);
                    }
                }
                if (insertSql.Length > 0)
                    return helper.TransExecuteSql(insertSql).ToString();
                return "";
            }
        }

        private string AddOrUpdateTable(int id = 0, string id_leaf = "", string tablename = "", string values = "", string creator = "")
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    //（字段名 |##|字段值|&&|字段名|##|字段值）
                    //values = "row_code|##|row_AAA6AC01A455C623|&&|column_code|##|row_BBA6AC01A455C623|&&|unit_val|##|米|&&|c_value|##|4545|&&|d_status|##|0|&&|public_time|##|2017-09-09";
                    string idleaf = id_leaf;
                    string strsql = "";      //执行SQL语句
                    KdtParameterCollection p_params = new KdtParameterCollection();
                    //将行中的数据根据行的列名保存到数据库中
                    string filename = string.Empty;       //字段
                    string filenamevalue = string.Empty;    //字段值
                    string editfilenamevalue = string.Empty;  //编辑字段对应值
                    string[] filedatas = values.Split(new string[] { "|&&|" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string str in filedatas)
                    {
                        string[] fileKeyVlaue = str.Split(new string[] { "|##|" }, StringSplitOptions.RemoveEmptyEntries);
                        if (fileKeyVlaue.Length > 0)
                        {
                            if (id == 0)
                            {
                                filename += fileKeyVlaue[0] + ",";
                                filenamevalue += String.Format("@{0},", fileKeyVlaue[0]);
                            }
                            else
                            {
                                editfilenamevalue += String.Format("{0}=@{0},", fileKeyVlaue[0]);
                            }
                            if (fileKeyVlaue.Length == 2)
                                p_params.AddParameter(fileKeyVlaue[0], fileKeyVlaue[1], ProcInPutEnum.InPut);
                            else
                                p_params.AddParameter(fileKeyVlaue[0], "", ProcInPutEnum.InPut);
                        }
                    }
                    string createtime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    p_params.AddParameter("IdLeaf", idleaf, ProcInPutEnum.InPut);
                    p_params.AddParameter("DName", idleaf, ProcInPutEnum.InPut);
                    if (id == 0)     //添加
                    {
                        //获取Id最大值
                        strsql = string.Format("select MAX(auto_no) as  maxid  from [" + tablename.Convert("") + "] ");
                        //helper.ExecuteQuery(strsql);
                        var result = helper.ExecuteQuery(strsql);
                        var maxidlist = result[0].ToString().Split(":")[1].Replace("}", "").Replace("\"", "").Convert("");
                        //var maxidlist = result.ToJson().ToEntity<List<Handles.Object.MaxIdEntity>>();
                        var autono = 0;
                        if (maxidlist != "0")
                        {
                            autono = maxidlist.Convert(0) + 1;
                        }
                        else
                        {
                            autono = 1;
                        }
                        p_params.AddParameter("Id", autono, ProcInPutEnum.InPut);
                        p_params.AddParameter("Creator", creator, ProcInPutEnum.InPut);
                        p_params.AddParameter("CreateTime", createtime, ProcInPutEnum.InPut);
                        strsql = string.Format("insert into [{0}] (auto_no,id_leaf,d_name,{1} creator,create_time) values (@Id, @IdLeaf ,@DName, {2} @Creator,@CreateTime)", tablename, filename, filenamevalue);
                        helper.ExecuteNonQuery(strsql, p_params);
                    }
                    else     //编辑
                    {
                        p_params.AddParameter("Id", id, ProcInPutEnum.InPut);
                        strsql = string.Format("update [{0}]  set {1}  where id_leaf = @IDLeaf", tablename, editfilenamevalue.Substring(0, editfilenamevalue.Length - 1));
                        helper.ExecuteNonQuery(strsql, p_params);
                    }
                    return new Text(true).ToJson();
                }
            });
        }


        #region  递归查询指标信息

        /// <summary>
        /// 指标行或列最外层父节点集合
        /// </summary>
        private List<Handles.Object.IndexTreeNode> NodeList = new List<Handles.Object.IndexTreeNode>();

        /// <summary>
        /// 要删除指标的Id集合
        /// </summary>
        private string idlist = "";

        /// <summary>
        /// 要删除指标行码的集合
        /// </summary>
        private string rowcodelist = "";

        /// <summary>
        /// 要删除指标列码的集合
        /// </summary>
        private string colcodelist = "";


        /// <summary>
        /// 递归查询指标行或列节点
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <param name="node">子节点集合</param>
        /// <param name="indexList">指标集合</param>
        private void SetNode(int parentId, Handles.Object.IndexTreeNode node, List<Handles.Object.IndexEntity> indexList)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                if (indexList[i].Pid == parentId)
                {
                    var re = new Handles.Object.IndexTreeNode();
                    re.autono = indexList[i].autono.Convert(0);
                    re.colCode = indexList[i].colCode.Convert("");
                    re.Pid = indexList[i].Pid.Convert(0);
                    re.colName = indexList[i].colName.Convert("");
                    re.isStat = indexList[i].isStat.Convert(0);
                    re.statWay = indexList[i].statWay.Convert("");
                    re.isAutono = indexList[i].isAutono.Convert(0);
                    re.uninList = indexList[i].uninList.Convert("");
                    re.rowCode = indexList[i].rowCode.Convert("");
                    re.rowName = indexList[i].rowName.Convert("");
                    re.statColumns = indexList[i].statColumns.Convert("");
                    SetNode(indexList[i].autono.Convert(0), re, indexList);
                    if (parentId == 0)
                    {
                        NodeList.Add(re);    //最外层父节点
                    }
                    else
                    {
                        node.children.Add(re);      //获取子节点
                    }
                }
            }
        }


        /// <summary>
        /// 查询要删除的指标信息
        /// </summary>
        private void DeleteIndex(int id, string rowcode, string colcode)
        {
            using (var helper = CreateHelper())
            {
                if (!rowcode.IsNullOrEmpty())    //删除行
                {
                    //第一步根据父Id查询其要删除的子节点Id
                    var entity = new Handles.Object.IndexRowHandler();
                    entity.id.FeildValue = id.Convert(0);
                    List<Handles.Object.IndexRowQuery> result = helper.SelectList<Handles.Object.IndexRowQuery>(entity, "GetRowByPid");
                    if (result.Count > 0)
                    {
                        List<Handles.Object.IndexRowQuery> list = result.ToString().ToEntity<List<Handles.Object.IndexRowQuery>>();
                        foreach (var item in list)
                        {
                            //第二步循环赋值
                            idlist += item.id + ",";
                            rowcodelist = rowcodelist + "'" + item.code + "' ,";
                            DeleteIndex(item.id, item.code, "");
                        }
                    }
                }
                else if (!colcode.IsNullOrEmpty())     //删除列
                {
                    //第一步根据父Id查询其要删除的子节点Id
                    var entity = new Handles.Object.IndexColumnHandler();
                    entity.id.FeildValue = id.Convert(0);
                    List<Handles.Object.IndexColumnQuery> result = helper.SelectList<Handles.Object.IndexColumnQuery>(entity, "GetColumnByPid");
                    if (result.Count > 0)
                    {
                        List<Handles.Object.IndexColumnQuery> list = result.ToString().ToEntity<List<Handles.Object.IndexColumnQuery>>();
                        foreach (var item in list)
                        {
                            //第二步循环赋值
                            idlist += item.id + ",";
                            colcodelist = colcodelist + "'" + item.code + "' ,";
                            DeleteIndex(item.id, "", item.code);
                        }
                    }

                }
            }
        }


        #endregion


        #endregion

        #endregion

        #region 填充静态表格
        /// <summary>
        /// 获取Excel填充数据
        /// </summary>
        /// <param name="uid">用户名</param>
        /// <returns></returns>
        [HttpGet("GetExcelData")]
        public string GetExcelData(string uid)
        {
            using (var helper = CreateHelper("sqlserverdb"))
            {
                var baseHandler = new Handles.Index.StaticExcelDataHandler();
                baseHandler.uid.FeildValue = uid;
                var excelEntity = helper.SelectEntity<Handles.Index.StaticExcelDataQuery>(baseHandler, "GetById");
                return excelEntity.ToJson(); ;
            }
        }



        /// <summary>
        /// 获取Excel填充数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllExcelData")]
        public string GetAllExcelData()
        {
            using (var helper = CreateHelper("sqlserverdb"))
            {
                var baseHandler = new Handles.Index.StaticExcelDataHandler();
                var excelEntity = helper.SelectList<Handles.Index.StaticExcelDataQuery>(baseHandler, "GetAll");
                return excelEntity.ToJson(); ;
            }
        }


        /// <summary>
        /// 获取Excel填充数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllCheckScoreData")]
        public string GetAllCheckScoreData()
        {
            using (var helper = CreateHelper("sqlserverdb"))
            {
                var baseHandler = new Handles.Index.StaticExcelDataHandler();
                var excelEntity = helper.SelectList<Handles.Index.StaticExcelDataQuery>(baseHandler, "GetCheckScoreTotal");
                return excelEntity.ToJson(); ;
            }
        }

        /// <summary>
        /// 更新基地信息版本
        /// </summary>
        /// <param name="common"></param>
        /// <returns></returns>
        [HttpPost("UpdateExcelVesion")]
        public string UpdateExcelVesion([FromBody]CommonObject common)
        {

            string uid = common.uid; string binfo = common.binfo; string work = common.work; string service = common.service;
            string inst = common.inst; string other = common.other; string creator = common.creator;
            using (var helper = CreateHelper("sqlserverdb"))
            {
                var excelHandler = new Handles.Index.StaticExcelDataHandler();
                var vesionHandler = new Handles.Index.StaticExcelVesionHandler();
                excelHandler.uid.FeildValue = uid;
                excelHandler.binfo.FeildValue = binfo;
                excelHandler.work.FeildValue = work;
                excelHandler.service.FeildValue = service;
                excelHandler.inst.FeildValue = inst;
                excelHandler.other.FeildValue = other;
                excelHandler.creator.FeildValue = creator;
                var excelEntity = helper.SelectEntity<Handles.Index.StaticExcelDataQuery>(excelHandler, "GetById");
                vesionHandler.uid.FeildValue = uid;
                vesionHandler.binfo.FeildValue = excelEntity.binfo;
                vesionHandler.work.FeildValue = excelEntity.work;
                vesionHandler.service.FeildValue = excelEntity.service;
                vesionHandler.inst.FeildValue = excelEntity.inst;
                vesionHandler.other.FeildValue = excelEntity.other;
                vesionHandler.creator.FeildValue = excelEntity.creator;
                vesionHandler.ctime.FeildValue = excelEntity.ctime;
                helper.Add(vesionHandler);
                helper.AddOrUpdate(excelHandler);
                if (excelHandler.Affected > 0 || excelHandler.NewId() > 0)
                {
                    return new Text(true).ToJson();
                }
                throw new DbExecuteException();
            }
        }

        #endregion

        #region 登录判断
        /// <summary>
        /// 登录信息
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost("LoginInfoDecide")] 
        public string LoginInfoDecide(string uid,string pwd) 
        {
            var userList = new List<Handles.User.UserQuery>();
            var result = new Text(false);
            return JsonDirectInvork(() =>
            {
                // 初始化handler
                var userHandler = new Handles.User.UserHandler();
                using (var helper = CreateHelper("sqlserverdb"))
                {
                    userHandler.id.FeildValue = uid;
                    var strResult = helper.SelectField<String>(userHandler, "CheckByName");
                    if(strResult == "0")
                    {
                        result.msg = "用户名错误";
                    }
                    else
                    {
                        string strsql = String.Format(new Handles.ExcuteSql().GetDictByKey("GetLoginUserInfo"),uid,pwd.ToMD5_16());
                        result.msg = (helper.ExecuteQuery(strsql)).ToJson();
                        if (result.msg.Length > 2)
                        {
                            result.success = true;
                            return result.msg;
                        }
                        result.msg = "密码错误";
                    }
                    return result.msg;
                }
            });
        }

        #endregion
		
		 #region   脚本模板管理


        /// <summary>
        /// 获取脚本模板左侧栏目树
        /// </summary>
        /// <param name="ischeck">是否查询子集(0:否,1:是)</param>
        /// <param name="name">检索关键字</param>
        /// <param name="type">脚本类型</param>
        /// <param name="category">脚本分类</param>
        /// <returns></returns>
        [HttpGet("GetScriptListByKey")]
        public string GetScriptListByKey(int ischeck = 0, string name = "", int type = 0, string category = "")
        {
            return JsonGridInvork<Handles.Template.KdtTpScriptQuery>((out int total) =>
            {
                var result = new List<Handles.Template.KdtTpScriptQuery>();
                total = 0;
                var handler = new Handles.Template.KdtTpScriptHandler();
                handler.id.FeildValue = name.Convert("");
                handler.type.FeildValue = type.Convert(0);
                handler.cate.FeildValue = category.Convert("");
                using (var helper = CreateHelper())
                {
                    if (ischeck == 1)  //查询子集
                        return result = helper.SelectList<Handles.Template.KdtTpScriptQuery>(handler, "GetScriptNameOfType");

                    else
                        //category为空时代表第二层，不为空时代表第三层
                        return result = helper.SelectList<Handles.Template.KdtTpScriptQuery>(handler, category.IsNullOrEmpty() ? "GetCategoryOrName" : "GetScriptByCategory");
                }
            });
        }

        /// <summary>
        /// 删除脚本模板
        /// </summary>
        /// <param name="type">脚本类型</param>
        /// <param name="category">对象类名</param>
        /// <param name="name">对象名</param>
        /// <returns></returns>
        [HttpDelete("DeleteScript")]
        public string DeleteScript(int type, string category = "", string name = "")
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var entity = new Handles.Template.KdtTpScriptHandler();
                    entity.type.FeildValue = type.Convert(0);
                    entity.cate.FeildValue = category.Convert("");
                    string scriptname = "";   //要删除的脚本模板名称
                    if (name.IsNullOrEmpty())
                    {
                        //第一种，通过脚本类型删除脚本模板(script_node为空)
                        //第二种，通过脚本类型及脚本分类删除脚本模板
                        //第一步，查询该类型下的所有脚本模板
                        var result = helper.SelectList<Handles.Template.KdtTpScriptQuery>(entity, "GetScriptNameList");
                        if (result.Count > 0)
                        {
                            //第二步，循环删掉其脚本模板
                            foreach (var _obj in result)
                            {
                                scriptname += "'" + _obj.id.Convert("") + "' ,";
                            }
                        }
                    }
                    else
                        //第三种，删除单个脚本
                        scriptname = "'" + name.Convert("") + "' ,";
                    entity.id.FeildValue = scriptname.Trim(',');
                    helper.TransExecute(entity, "DelScriptByName");
                    if (entity.Affected < 0)
                        return new Text(false, "删除失败", 5002).ToJson();
                    return new Text(true, "删除成功").ToJson();
                }
            });
        }
        #endregion

    }
}
