using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KdtHelper.Common;
using System.Reflection;
using KdtHelper.Common.Json;
using CTP.API.Util;
using Microsoft.AspNetCore.Cors;
using System.Linq;
using KdtHelper.Core;
using KdtHelper;
using System.Data;
using System.Net.Http.Headers;
using System.IO;
using KdtHelper.Core.ExecuterEx;

namespace CTP.API.Controllers
{
    /// <summary>
    /// V1版本接口类
    /// </summary>
    [EnableCors("any")]
    [Route("[controller]")]
    public class v1Controller : BaseController
    {

        #region 继承父类方法

        /// <summary>
        /// 增删改查
        /// </summary>
        /// <param name="handler">操作类名</param>
        /// <param name="optype">操作类型(0:添加，1:添加(带返回值),2:修改,3:添加或修改，8:删除)</param>
        /// <returns></returns>
        [HttpPost("Op")]
        public string OpExe(string handler, int optype)
        {
            return base.Op(handler, optype);
        }

        /// <summary>
        /// 自定义方法返回list数据集合
        /// </summary>
        /// <param name="handler">操作类名</param>
        /// <param name="method">执行方法名</param>
        /// <returns></returns>
        [HttpGet("GetMethod")]
        public string GetMethodExe(string handler, string method)
        {
            return base.GetMethod(handler, method);
        }

        /// <summary>
        /// 查询所有数据信息
        /// </summary>
        /// <param name="handler">操作类名</param>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public string GetAllExe(string handler)
        {
            return base.GetAll(handler);
        }

        /// <summary>
        /// 根据Id查询
        /// </summary>
        /// <param name="handler">操作类名</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public string GetByIdExe(string handler)
        {
            return base.GetById(handler);
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="handler">操作类名</param>
        /// <param name="start">起始页</param>
        /// <param name="size">每页条数</param>
        /// <returns></returns>
        [HttpGet("SelectPage")]
        public string SelectPageExe(string handler, int start, int size)
        {
            return base.SelectPage(handler, start, size);
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

        #endregion

        #region  Menu
        /// <summary>
        /// 获取全部子栏目
        /// </summary>
        /// <param name="isAuth">是否带权限</param>
        /// <param name="tabid"></param>
        /// <returns></returns>
        [HttpGet("GetMenuList")]
        public string GetMenuList(Boolean isAuth, int tabid = 0)
        {
            var mainMenuList = new List<Handles.Menu.MenuListQuery>();
            var result = new Text(true);
            return JsonDirectInvork(() =>
            {
                      // 初始化handler
                      var baseHandler = new Handles.Menu.MenuListHandler();
                using (var helper = CreateHelper())
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
        /// <returns></returns>
        [HttpGet("GetTabList")]
        public string GetTabList(Boolean isAuth)
        {
            using (var helper = CreateHelper())
            {
                return JsonEntityInvork<List<Handles.Menu.MenuTopQuery>>(() =>
                {
                    return helper.SelectList<Handles.Menu.MenuTopQuery>(SetHandler<Handles.Menu.MenuTopHandler>(), isAuth ? "SelectAllTab" : "SelectAuthTab");
                });
            }
        }

        #endregion

        #region Flow

        #region  流程实例
        /// <summary>
        /// 添加流程实例
        /// </summary>
        /// <param name="name">流程实例名称</param>
        /// <param name="source">流程实例来源</param>
        /// <param name="mapid">映射编号</param>
        /// <param name="flowid">关联的流程信息Id</param>
        /// <param name="stepid">关联的流程步骤</param>
        /// <param name="creator">创建人</param>
        /// <returns></returns>
        [HttpPost("FlowInstanceAdd")]
        public string FlowInstanceAdd(string name, string source, string mapid, string flowid, int stepid, string creator)
        {
            return JsonDirectInvork(() =>
            {
                      // 初始化handler
                      var baseHandler = new Handles.Flow.KdtWfInstanceHandler();
                baseHandler.name.FeildValue = name;
                using (var helper = CreateHelper())
                {
                    var result = helper.SelectField<string>(baseHandler, "CheckByName");
                    if (result != "0")
                    {
                        return new Text(false, "已存在同名流程实例").ToJson();
                    }
                    baseHandler.id.FeildValue = name.ToMD5_16();
                    baseHandler.source.FeildValue = source;
                    baseHandler.mapid.FeildValue = mapid;
                    baseHandler.flowid.FeildValue = flowid;
                    baseHandler.stepid.FeildValue = stepid;
                    baseHandler.status.FeildValue = 1;
                    baseHandler.creator.FeildValue = creator;
                    helper.Add(baseHandler);
                    if (baseHandler.NewId() > 0) return new Text(true, "添加成功").ToJson();
                    return new Text(false, "添加失败", 5002).ToJson();
                }
            });
        }
        #endregion

        #region  流程信息
        /// <summary>
        /// 添加流程信息
        /// </summary>
        /// <param name="name">流程信息名称</param>
        /// <param name="cate">流程信息类别</param>
        /// <param name="note">流程信息备注</param>
        /// <param name="sys">是否系统流程</param>
        /// <param name="creator">创建人</param>
        /// <returns></returns>
        [HttpPost("FlowInfoAdd")]
        public string FlowInfoAdd(string name, string cate, string note, int sys, string creator)
        {
            return JsonDirectInvork(() =>
            {
                      // 初始化handler
                      var baseHandler = new Handles.Flow.KdtWfInfoHandler();
                baseHandler.name.FeildValue = name;
                using (var helper = CreateHelper())
                {
                    var result = helper.SelectField<string>(baseHandler, "CheckByName");
                    if (result != "0")
                    {
                        return new Text(false, "已存在同名流程信息").ToJson();
                    }
                    baseHandler.id.FeildValue = name.ToMD5_16();
                    baseHandler.cate.FeildValue = cate;
                    baseHandler.note.FeildValue = note;
                    baseHandler.sys.FeildValue = sys;
                    baseHandler.creator.FeildValue = creator;
                    helper.Add(baseHandler);
                    if (baseHandler.NewId() > 0) return new Text(true, "添加成功").ToJson();
                    return new Text(false, "添加失败", 5002).ToJson();
                }
            });
        }
        #endregion

        #region  流程步骤

        /// <summary>
        /// 添加流程步骤
        /// </summary>
        /// <param name="flowid">流程信息编号</param>
        /// <param name="name">流程步骤名称</param>
        /// <param name="back">退回步骤</param>
        /// <param name="pre">上一步</param>
        /// <param name="hasnext">存在下一步</param>
        /// <param name="multi">多分枝</param>
        /// <param name="type">类型</param>
        /// <param name="actionid">执行编号</param>
        /// <param name="temp">数据模板</param>
        /// <param name="creator">创建人</param>
        /// <returns></returns>
        [HttpPost("FlowStepAdd")]
        public string FlowStepAdd(string flowid, string name, int back, string pre, int hasnext, int multi, int type, string actionid, string temp, string creator)
        {
            return JsonDirectInvork(() =>
            {
                      // 初始化handler
                      var baseHandler = new Handles.Flow.KdtWfStepHandler();
                baseHandler.flowid.FeildValue = flowid;
                baseHandler.name.FeildValue = name;
                using (var helper = CreateHelper())
                {
                    var result = helper.SelectField<string>(baseHandler, "CheckByName");
                    if (result != "0")
                    {
                        return new Text(false, "已存在同名流程信息").ToJson();
                    }
                    result = helper.SelectField<string>(baseHandler, "GetMaxStepId");
                    baseHandler.stepid.FeildValue = 1;
                    if (result != "")
                    {
                        baseHandler.stepid.FeildValue = Int32.Parse(result) + 1;
                    }
                    baseHandler.back.FeildValue = back;
                    baseHandler.pre.FeildValue = pre;
                    baseHandler.hasnext.FeildValue = hasnext;
                    baseHandler.multi.FeildValue = multi;
                    baseHandler.type.FeildValue = type;
                    baseHandler.actionid.FeildValue = actionid;
                    baseHandler.temp.FeildValue = temp;
                    baseHandler.creator.FeildValue = creator;
                    helper.Add(baseHandler);
                    if (baseHandler.NewId() > 0) return new Text(true, "添加成功").ToJson();
                    return new Text(false, "添加失败", 5002).ToJson();
                }
            });
        }
        #endregion

        #region 流程执行
        [HttpPost("flowActionAdd")]
        public string flowActionAdd(int type, string name, string cate, int audittype, string auditmapid, string auditposition, int hasproxy, int proxytype, string proxymapid, string proxyposition, int hascopy, int copytype, string copymapid, string copyposition, string creator)
        {
            return JsonDirectInvork(() =>
            {
                      // 初始化handler
                      var baseHandler = new Handles.Flow.KdtWfActionHandler();
                baseHandler.name.FeildValue = name;
                using (var helper = CreateHelper())
                {
                    var result = helper.SelectField<string>(baseHandler, "CheckByName");
                    if (result != "0")
                    {
                        return new Text(false, "已存在同名执行项").ToJson();
                    }
                    baseHandler.id.FeildValue = name.ToMD5_16();
                    baseHandler.cate.FeildValue = cate;
                    baseHandler.type.FeildValue = type;
                    baseHandler.audittype.FeildValue = audittype;
                    baseHandler.auditmapid.FeildValue = auditmapid;
                    baseHandler.auditposition.FeildValue = auditposition;
                    baseHandler.hasproxy.FeildValue = hasproxy;
                    baseHandler.proxytype.FeildValue = proxytype;
                    baseHandler.proxymapid.FeildValue = proxymapid;
                    baseHandler.proxyposition.FeildValue = proxyposition;
                    baseHandler.hascopy.FeildValue = hascopy;
                    baseHandler.copytype.FeildValue = copytype;
                    baseHandler.copymapid.FeildValue = copymapid;
                    baseHandler.copyposition.FeildValue = copyposition;
                    baseHandler.creator.FeildValue = creator;
                    helper.Add(baseHandler);
                    if (baseHandler.NewId() > 0) return new Text(true, "添加成功").ToJson();
                    return new Text(false, "添加失败", 5002).ToJson();
                }
            });
        }
        #endregion

        #region 流程图
        /// <summary>
        /// 获取流程图Data数据
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpGet("flowStepChartData")]
        public string flowStepChartData(string flowId, int level = 0)
        {
            var result = new Text(false);
            var step_id = 0;
            return JsonDirectInvork(() =>
            {
                      // 初始化handler
                      var baseHandler = new Handles.Flow.KdtWfStepHandler();
                baseHandler.pre.FeildValue = level.Convert("");
                baseHandler.flowid.FeildValue = flowId;
                using (var helper = CreateHelper())
                {
                    var step_result = helper.SelectField<String>(baseHandler, "GetMaxStepId");
                    step_id = (step_result == "") ? 1 : Int32.Parse(step_result) + 1;
                    baseHandler.stepid.FeildValue = step_id;
                    var stepList = helper.SelectList<Handles.Flow.KdtWfStepQuery>(baseHandler, "GetChartsData");
                    if (stepList.Count > 0)
                    {
                        var num = 0;
                        for (var i = 0; i < stepList.Count; i++)
                        {
                            num++;
                            result.msg += "{name:'" + stepList[i].name + "',isMulti:" + stepList[i].multi + ",level:" + num + ",value:" + stepList[i].autono + "},";
                            if (stepList[i].multi == 1 && stepList[i + 1].multi == 1 && (i + 1) < stepList.Count)
                            {
                                num--;
                            }
                        }
                        if (result.msg.Length > 1)
                        {
                            result.msg = result.msg.Substring(0, result.msg.Length - 1);
                        }
                        result.msg = '[' + result.msg + ']';
                        result.success = true;
                        return result.ToJson();
                    }
                    return result.ToJson();

                }
            });
        }

        private Text resultLink = new Text(true);
        /// <summary>
        /// 获取流程图Link数据
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="level"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("flowStepChartLink")]
        public string flowStepChartLink(string flowId, int level = 0, string name = "")
        {
            StepChartLink(flowId, level, name);
            resultLink.msg += StepBackLink(flowId);
            resultLink.msg = resultLink.msg.Substring(0, resultLink.msg.Length - 1);
            resultLink.msg = "[" + resultLink.msg + "]";
            return resultLink.ToJson();
        }


        /// <summary>
        /// 递归查找charts Link数据
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="level"></param>
        /// <param name="name"></param>
        private void StepChartLink(string flowId, int level = 0, string name = "")
        {
            using (var helper = CreateHelper())
            {
                var baseHandler = new Handles.Flow.KdtWfStepHandler();
                baseHandler.pre.FeildValue = level.Convert("");
                baseHandler.flowid.FeildValue = flowId;
                var preList = helper.SelectList<Handles.Flow.KdtWfStepQuery>(baseHandler, "GetStepListByPre");
                if (preList.Count > 0)
                {
                    foreach (var step in preList)
                    {
                        if (name != "")
                        {
                            resultLink.msg += "{source:'" + name + "',target:'" + step.name + "'},";
                        }
                        StepChartLink(flowId, step.stepid, step.name);
                    }
                }
            }
        }
        /// <summary>
        /// 退回Link数据
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        private string StepBackLink(string flowId)
        {
            string linkData = "";
            var baseHandler = new Handles.Flow.KdtWfStepHandler();
            baseHandler.flowid.FeildValue = flowId;
            using (var helper = CreateHelper())
            {
                var backList = helper.SelectList<Handles.Flow.KdtWfStepQuery>(baseHandler, "GetStepListByBack");
                if (backList.Count < 1)
                {
                    linkData += "{source:'开始',target:'结束'},";

                }
                else
                {
                    linkData += "{source:'开始',target:'" + backList[0].name + "'},";
                    linkData += "{source:'" + backList[backList.Count - 1].name + "',target:'结束'},";
                }
                if (backList.Count > 1)
                {
                    for (var num = 1; num < backList.Count; num++)
                    {
                        var entityList = backList.Where(p => p.stepid == backList[num].back).ToList();
                        if (entityList.Count == 1)
                        {
                            linkData += "{source:'" + backList[num].name + "',target:'" + entityList[0].name + "',lineStyle:{normal:{opacity: 0.9,width: 2,curveness: 0.3}}},";
                        }
                    }
                }
            }
            return linkData;
        }

        #endregion

        #endregion

        #region Template

        #endregion

        #region   Object(数据对象)

        #region  数据对象的添加、删除、查询等事件

        /// <summary>
        /// 获取数据对象菜单树
        /// </summary>
        /// <param name="ischeck">是否查询子集(0:否,1:是)</param>
        /// <returns></returns>
        [HttpGet("GetObjListByKey")]
        public string GetObjListByKey(int ischeck = 0)
        {
            return JsonGridInvork<Handles.Object.SysObjListQuery>((out int total) =>
            {
                var result = new List<Handles.Object.SysObjListQuery>();
                total = 0;
                string category = Request.Query["category"].Convert("");
                using (var helper = CreateHelper())
                {
                    if (ischeck == 1)  //查询子集
                          {
                        result = helper.SelectList<Handles.Object.SysObjListQuery>(SetHandler<Handles.Object.SysObjListHandler>(), "GetObjNameOfType");

                    }
                    else
                    {
                              //category为空时代表第二层，不为空时代表第三层
                              result = helper.SelectList<Handles.Object.SysObjListQuery>(SetHandler<Handles.Object.SysObjListHandler>(), category.IsNullOrEmpty() ? "GetCategoryOrName" : "GetObjByCategory");
                    }
                    if (result.Count > 0) return result;

                          //return new Text(false, "添加失败", 5002).ToJson();
                          throw new Exception("没有数据");
                }
            });
        }


        /// <summary>
        /// 添加数据对象 
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="type">对象类别(0:常规,1:资源,2:检索,3:指标,4:微信)</param>
        /// <param name="category">对象类名</param>
        /// <param name="ispublic">是否发布(0:不发布,1:发布)</param>
        /// <returns></returns>
        [HttpPost("AddObj")]
        public string AddObj(string name, int type, string category, int ispublic)
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var objentity = new Handles.Object.SysObjListHandler();
                    objentity.name.FeildValue = name.Convert("");
                    objentity.type.FeildValue = type.Convert(0);
                    objentity.category.FeildValue = category.Convert("");
                    objentity.ispublic.FeildValue = ispublic.Convert(0);
                          //objentity.creator.FeildValue = cn.GetLoginKey();
                          objentity.creator.FeildValue = "Administrator";

                          //第一步,判断是否存在该数据对象名
                          var result = helper.SelectField<string>(objentity, "CheckByName");
                    if (result != "0")
                    {
                        return new Text(false, "已存在同名执行项").ToJson();
                    }
                          //第二步，添加数据对象信息
                          helper.Add(objentity);
                    if (objentity.NewId() > 0)
                    {
                              //第三步,创建数据对象表
                              var create = helper.SelectField<string>(objentity, "CreateTable");
                        if (create != "0")     //创建成功
                              {
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
                                                                  //dictentity.Creator.FeildValue = cn.GetLoginKey();
                                      dictentity.creator.FeildValue = "Administrator";
                                helper.Add(dictentity);
                                if (dictentity.NewId() > 0)
                                {
                                    if (_dict.feild == "auto_no") continue;
                                          // 第五步，创建数据对象表字段
                                          create = helper.SelectField<string>(dictentity, "AddField");
                                    if (create != "0") continue;
                                }
                                      //回滚， 删除表，数据对象记录，及字典记录
                                      helper.TransExecute(dictentity, "DelObjByName");
                            }
                            return new Text(true, "添加成功").ToJson();
                        }
                        else
                        {
                                  //回滚，删除已添加的数据对象信息
                                  helper.Delete(objentity);
                        }
                    }
                    return new Text(false, "添加失败", 5002).ToJson();
                }
            });

        }

        /// <summary>
        /// 修改数据对象信息
        /// </summary>
        /// <param name="ischange">发布状态是否改变</param>
        /// <param name="id">自增编号</param>
        /// <param name="objname">对象名</param>
        /// <param name="objtype">对象类别(0:常规,1:资源,2:检索,3:指标,4:微信)</param>
        /// <param name="objcategory">对象类名</param>
        /// <param name="isdyrow">动态行</param>
        /// <param name="objhead">对象头</param>
        /// <param name="objfoot">对象尾</param>
        /// <param name="objtitle">对象标题</param>
        /// <param name="ispublic">是否发布(0:不发布,1:发布)</param>
        /// <returns></returns>
        [HttpPost("UpdateObj")]
        public string UpdateObj(bool ischange, int id, string objname, int objtype, string objcategory, int isdyrow, string objhead, string objfoot, string objtitle, int ispublic)
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
                              dictentity.name.FeildValue = objname.Convert("");
                        dictentity.type.FeildValue = objtype.Convert(0);
                        dictentity.feild.FeildValue = "public_time";
                        dictentity.des.FeildValue = "发布时间";
                        dictentity.ftype.FeildValue = "string";
                        dictentity.len.FeildValue = "20";
                        dictentity.sys.FeildValue = 0;    //0为系统，1为非系统
                                                          //dictentity.creator.FeildValue = cn.GetLoginKey();
                              dictentity.creator.FeildValue = "Administrator";
                        if (ispublic == 1)   //发布，之前为未发布状态（添加“发布时间”字段信息）
                              {
                                  //添加发布时间字段信息到数据字典表
                                  helper.Add(dictentity);
                            if (dictentity.NewId() > 0)
                            {
                                      //创建对象表字段
                                      var create = helper.SelectField<string>(dictentity, "AddField");
                                if (create == "0")  //创建成功
                                      {
                                          //回滚，删除对象字典表新增信息
                                          helper.Delete(dictentity);
                                }
                            }
                            else
                                return new Text(false, "修改失败", 5002).ToJson();
                        }
                        else    //不发布，之前为发布状态（删除字段信息）
                              {
                                  //删除字典信息及字段信息
                                  helper.TransExecute(dictentity, "DelObjDictByName");
                        }

                    }
                          //第二步修改数据对象表数据
                          var objentity = new Handles.Object.SysObjListHandler();
                    objentity.autono.FeildValue = id.Convert(0);
                    objentity.name.FeildValue = objname.Convert("");     //对象名及对象类别不能修改
                          objentity.type.FeildValue = objtype.Convert(0);
                    objentity.category.FeildValue = objcategory.Convert("");
                    objentity.dyrow.FeildValue = isdyrow.Convert(0);
                    objentity.head.FeildValue = objhead.Convert("");
                    objentity.foot.FeildValue = objfoot.Convert("");
                    objentity.title.FeildValue = objtitle.Convert("");
                    objentity.ispublic.FeildValue = ispublic.Convert(0);
                    helper.Update(objentity);
                    if (objentity.NewId() < 0)
                        helper.TransExecute(dictentity, "DelObjDictByName");      //回滚，删除对象字典表新增信息及对象表字段
                          return new Text(true, "修改成功").ToJson();
                }
            });
        }


        /// <summary>
        /// 删除数据对象
        /// </summary>
        /// <param name="objtype">对象类别</param>
        /// <param name="objcategory">对象类名</param>
        /// <param name="objname">对象名</param>
        /// <returns></returns>
        [HttpDelete("DeleteObj")]
        public string DeleteObj(int objtype, string objcategory = "", string objname = "")
        {

            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var entity = new Handles.Object.SysObjListHandler();
                    entity.type.FeildValue = objtype.Convert(0);
                    if (objname.IsNullOrEmpty())
                    {
                              //第一种，通过对象类别删除数据对象(objcategory为空)
                              //第二种，通过对象类别及对象类名删除数据对象
                              //第一步，查询该类别下的所有数据对象
                              var result = helper.SelectList<Handles.Object.SysObjListQuery>(entity, "GetObjNameList");
                        if (result.Count > 0)
                        {
                                  // List<Handles.Object.SysObjListQuery> objlist = result.ToEntity<List<Handles.Object.SysObjListQuery>>();
                                  //第二步，循环删掉其数据对象
                                  foreach (var _obj in result)
                            {
                                entity.name.FeildValue = _obj.name.Convert("");
                                helper.TransExecute(entity, "DelObjByName");
                                if (entity.NewId() > 0)
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
                              entity.name.FeildValue = objname.Convert("");
                        helper.TransExecute(entity, "DelObjByName");
                        if (entity.NewId() < 0) return new Text(false, "删除失败", 5002).ToJson();
                    }
                    return new Text(true, "删除成功").ToJson();
                }
            });
        }

        #endregion

        #region  对象字典的添加、删除、查询等事件

        /// <summary>
        /// 根据对象名及系统字段查询对象字典信息    GetMethodExe("objdict","SelectObjDictByName")
        /// </summary>
        /// <param name="objname">对象名</param>
        /// <param name="issys">是否系统字段(0:系统字段，1:非系统字段)</param>
        /// <returns></returns>
        //[HttpGet("GetObjDictList")]
        //public string GetObjDictList()
        //{

        //    using (var helper = CreateHelper())
        //    {
        //        return JsonEntityInvork<List<Handles.Object.SysObjDictQuery>>(() =>
        //        {
        //            return helper.SelectList<Handles.Object.SysObjDictQuery>(SetHandler<Handles.Menu.MenuTopHandler>(), "SelectObjDictByName");
        //        });
        //    }
        //}




        /// <summary>
        /// 添加对象字典信息
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="type">对象类别(0:常规,1:资源,2:检索,3:指标,4:微信)</param>
        /// <param name="des">字段描述</param>
        /// <param name="ftype">字段类型</param>
        /// <param name="len">字段长度</param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        [HttpPost("AddObjDict")]
        public string AddObjDict(string name, int type, string des, string ftype, int len, string def)
        {
            return JsonDirectInvork(() =>
            {

                using (var helper = CreateHelper())
                {
                    var dictentity = new Handles.Object.SysObjDictHandler();
                    dictentity.name.FeildValue = name.Convert("");
                    dictentity.type.FeildValue = type.Convert(0);
                    //获取字段名
                    var result = helper.SelectField<string>(dictentity, "GetMaxObjFeild");
                    if (result != "0")
                    {
                        dictentity.feild.FeildValue = "resourceprop" + (Int32.Parse(result.Substring(12, result.Length - 12)) + 1);
                    }
                    else dictentity.feild.FeildValue = "resourceprop1";
                    dictentity.des.FeildValue = des.Convert("");
                    dictentity.ftype.FeildValue = ftype.Convert("");
                    dictentity.len.FeildValue = len.Convert(0);
                    dictentity.fdefault.FeildValue = def.Convert("");
                    dictentity.sys.FeildValue = 1;    //0为系统，1为非系统
                                                      //dictentity.Creator.FeildValue = cn.GetLoginKey();
                          dictentity.creator.FeildValue = "Administrator";
                          //第一步，添加字典表数据
                          helper.Add(dictentity);
                    if (dictentity.NewId() > 0)
                    {
                              //第二步，添加表字段
                              result = helper.SelectField<string>(dictentity, "AddField");
                        if (result != "0") helper.Delete(dictentity);       //回滚，删除已添加的字典表数据
                          }
                    else return new Text(false, "删除失败", 5002).ToJson();

                    return new Text(true, "删除成功").ToJson();
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
                    if (dictentity.NewId() > 0)
                    {
                              //第二步，修改数据对象表字段（失败则无法回滚已修改的字典表信息）
                              var result = helper.SelectField<string>(dictentity, "ModifyField");
                        if (result == "0") return new Text(false, "删除失败", 5002).ToJson();
                    }
                    else return new Text(false, "删除失败", 5002).ToJson();

                    return new Text(true, "删除成功").ToJson();
                }
            });

        }


        #endregion

        #region  指标的添加、删除、查询等事件

        /// <summary>
        /// 添加或修改行指标
        /// </summary>
        /// <param name="id">自增编号</param>
        /// <param name="pid">父编号</param>
        /// <param name="objname">对象名</param>
        /// <param name="code">code码</param>
        /// <param name="name">行名称</param>
        /// <param name="stat">是否统计行</param>
        /// <param name="columns">统计列算法</param>
        /// <returns></returns>
        //[HttpPost("AddOrUpdateRow")]
        //public string AddOrUpdateRow(int id = 0, int pid = 0, string objname = "", string name = "", string code="", int stat = 0, string columns = "")
        //{
        //    return JsonDirectInvork(() =>
        //    {

        //        using (var helper = CreateHelper())
        //        {
        //            var rowentity = new Handles.Object.IndexRowHandler();
        //            rowentity.pid.FeildValue = pid.Convert(0);
        //            rowentity.name.FeildValue = name.Convert("");
        //            //rowentity.code.FeildValue = "row_" + rowname.ToMD5_16();
        //            rowentity.code.FeildValue = code.Convert("");
        //            rowentity.objname.FeildValue = objname.Convert("");
        //            rowentity.stat.FeildValue = stat.Convert(0);
        //            rowentity.columns.FeildValue = columns.Convert("");
        //            //rowentity.Creator.FeildValue = cn.GetLoginKey();
        //            rowentity.creator.FeildValue = "Administrator";
        //            //添加行指标
        //            helper.Add(rowentity);
        //            if(rowentity.NewId() > 0)
        //            {
        //                return new Text(true, "添加成功").ToJson();
        //            }
        //        }
        //        return new Text(false, "删除失败", 5002).ToJson();

        //    });

        //}



        /// <summary>
        /// 根据数据对象名查询该数据对象表数据
        /// </summary>
        /// <param name="objname">数据对象名</param>
        /// <returns></returns>
        [HttpGet("GetObjDataByName")]
        public string GetObjDataByName(string objname)
        {
            using (var helper = CreateHelper())
            {
                string strsql = string.Format("select *  from `" + objname.Convert("").ToMD5_16() + "` ");
                var result = helper.ExecuteQuery(strsql);
                return result.ToJson();
            }

        }


        /// <summary>
        /// 栏目树测试接口
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpGet("GetDeptTree")]
        public string GetDeptTree(string pid)
        {
            using (var helper = CreateHelper())
            {
                var handler = new Handles.User.RoleNetHandler();
                handler.pid.FeildValue = pid;
                return JsonEntityInvork<List<Handles.User.RoleNetQuery>>(() =>
                {
                    return helper.SelectList<Handles.User.RoleNetQuery>(SetHandler<Handles.User.RoleNetHandler>(), "SelectByPath");
                });
            }
        }

        /// <summary>
        /// 通过数据对象名获取指标信息
        /// </summary>
        /// <param name="objname">对象名</param>
        /// <returns></returns>
        [HttpGet("GetIndexByName")]
        public string GetIndexByName(string objname)
        {
            using (var helper = CreateHelper())
            {
                //获取数据对象信息
                var objentity = new Handles.Object.SysObjListHandler();
                objentity.name.FeildValue = objname.Convert("");
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
                    rowentity.name.FeildValue = objname.Convert("");
                    List<Handles.Object.IndexEntity> result = helper.SelectList<Handles.Object.IndexEntity>(rowentity, "GetAllIndexByName");
                    if (result.Count > 0)
                    {
                        List<Handles.Object.IndexEntity> list = result.ToString().ToEntity<List<Handles.Object.IndexEntity>>();
                        //指标行
                        List<Handles.Object.IndexEntity> rowlist = new List<Handles.Object.IndexEntity>();
                        rowlist = list.Where(n => !n.rowCode.IsNullOrEmpty()).ToList();
                        SetNode(0, null, rowlist);   //递归查询
                        table.rows = NodeList;

                        //指标列
                        NodeList = new List<Handles.Object.IndexTreeNode>();    //清空指标行信息
                        List<Handles.Object.IndexEntity> collist = new List<Handles.Object.IndexEntity>();
                        collist = list.Where(n => !n.colCode.IsNullOrEmpty()).ToList();
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
                        if (entity.NewId() > 0)
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
                        if (entity.NewId() > 0)
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
        /// <param name="id">自增编号</param>
        /// <param name="objname">数据对象名</param>
        /// <param name="values">对应字段值字符串</param>
        /// <param name="creator">用户名</param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateTableData")]
        public string AddOrUpdateTableData(int id = 0, string objname = "", string values = "", string creator = "")
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                          //（字段名 |##|字段值|&&|字段名|##|字段值）
                          //values = "row_code|##|row_AAA6AC01A455C623|&&|column_code|##|row_BBA6AC01A455C623|&&|unit_val|##|米|&&|c_value|##|4545|&&|d_status|##|0|&&|public_time|##|2017-09-09";
                          string idleaf = objname.ToMD5_16();
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
                    if (id == 0)     //添加
                          {
                              //获取Id最大值
                              strsql = string.Format("select MAX(auto_no) as  'maxid'  from `" + objname.Convert("").ToMD5_16() + "` ");
                              //helper.ExecuteQuery(strsql);
                              var result = helper.ExecuteQuery(strsql);
                        var maxidlist = result.ToJson().ToEntity<List<Handles.Object.MaxIdEntity>>();
                        if (maxidlist[0].maxid.Convert(0) != 0)
                        {
                            id = maxidlist[0].maxid.Convert(0) + 1;
                        }
                        else
                        {
                            id = 1;
                        }
                        p_params.AddParameter("Id", id, ProcInPutEnum.InPut);
                        p_params.AddParameter("Creator", creator, ProcInPutEnum.InPut);
                        p_params.AddParameter("CreateTime", createtime, ProcInPutEnum.InPut);
                        strsql = string.Format("insert into `{0}` (auto_no,id_leaf,{1} creator,create_time) values (@Id, @IdLeaf , {2} @Creator,@CreateTime)", objname.ToMD5_16(), filename, filenamevalue);
                        helper.ExecuteNonQuery(strsql, p_params);
                    }
                    else     //编辑
                          {
                        p_params.AddParameter("Id", id, ProcInPutEnum.InPut);
                        strsql = string.Format("update `{0}`  set {1}  id_leaf = @IDLeaf where auto_no = @Id", objname.ToMD5_16(), editfilenamevalue);
                        helper.ExecuteNonQuery(strsql, p_params);
                              //if (!result.Success)
                              //{
                              //    return new Text(false, "删除失败", 5002).ToJson();
                              //}
                          }
                    return new Text(true, "删除成功").ToJson();
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

        #region   FileStore(文件存储)

        #region  存储结构的添加、删除、查询等事件

        /// <summary>
        /// 查询栏目存储结构树
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStoreNodeTree")]
        public string GetStoreNodeTree()
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var entity = new Handles.FileStore.StoreNodeHandler();
                    var result = helper.SelectList<Handles.FileStore.StoreNodeQuery>(entity, "GetAllStoreNode");
                    if (result.Count > 0)
                    {
                        List<Handles.FileStore.StoreNodeQuery> list = result.ToString().ToEntity<List<Handles.FileStore.StoreNodeQuery>>();
                        StoreSetNode("0", null, list);   //递归查询
                          }
                    return StoreNodeList.ToJson();
                }
            });
        }

        #region   栏目存储结构树递归查询

        /// <summary>
        ///栏目最外层父节点集合
        /// </summary>
        private List<Handles.FileStore.StoreNodeTreeNode> StoreNodeList = new List<Handles.FileStore.StoreNodeTreeNode>();

        /// <summary>
        /// 递归查询栏目存储结构
        /// </summary>
        /// <param name="parentId">父ID</param>
        /// <param name="node">子节点集合</param>
        /// <param name="nodeList">栏目集合</param>
        private void StoreSetNode(string parentId, Handles.FileStore.StoreNodeTreeNode node, List<Handles.FileStore.StoreNodeQuery> nodeList)
        {
            for (int i = 0; i < nodeList.Count; i++)
            {
                if (nodeList[i].pid == parentId)
                {
                    var re = new Handles.FileStore.StoreNodeTreeNode();
                    re.Autono = nodeList[i].autono.Convert(0);
                    re.NodeId = nodeList[i].id.Convert("");
                    re.NodePid = nodeList[i].pid.Convert("");
                    re.NodeName = nodeList[i].name.Convert("");
                    StoreSetNode(nodeList[i].id.Convert(""), re, nodeList);
                    if (parentId == "0")
                        StoreNodeList.Add(re);    //最外层父节点
                    else
                        node.children.Add(re);      //获取子节点
                }
            }
        }

        #endregion


        /// <summary>
        /// 添加或修改栏目存储结构
        /// </summary>
        /// <param name="autono">自增编号（添加时可不传）</param>
        /// <param name="pid">父栏目（添加时最底层可不传值）</param>
        /// <param name="name">栏目名称</param>
        /// <param name="type">栏目类型</param>
        /// <param name="utype">上传方式（0:FTP上传，1:WebService上传，2:API上传）</param>
        /// <param name="config">上传配置（每个上传配置用"|"分开，例：192.168.1.141,Administrator,123456abcD|API上传配置值|）</param>
        /// <param name="fdtype">文件夹类型（用,隔开，例：0,1,2,3）</param>
        /// <param name="encrypt">是否加密推送（0：否，1：是）</param>
        /// <param name="method">加密算法</param>
        /// <param name="change">是否允许转换（0：否，1：是）</param>
        /// <param name="creator">用户名</param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateStoreNode")]
        public string AddOrUpdateStoreNode(int autono = 0, string pid = "", string name = "", int type = 0, int utype = 0, string config = "", string fdtype = "", int encrypt = 0, string method = "", int change = 0, string creator = "")
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var entity = new Handles.FileStore.StoreNodeHandler();
                    entity.autono.FeildValue = autono.Convert(0);
                    entity.id.FeildValue = name.Convert("").ToMD5_16();
                          //栏目生成目录(相对路径)
                          string targetpath = "";     //目标文件
                          if (pid.IsNullOrEmpty())
                    {
                        entity.pid.FeildValue = "0";     //最外层节点
                              targetpath = name;
                    }
                    else
                    {
                        entity.pid.FeildValue = pid;      //子节点
                                                          //通过唯一标识查询其父节点生成目录
                              Handles.FileStore.StoreNodeQuery fs = helper.SelectEntity<Handles.FileStore.StoreNodeQuery>(entity, "GetPublishRootById");
                        var publishroot = fs.root.Convert("");
                        targetpath = publishroot + "\\" + name;
                    }
                    entity.fdtype.FeildValue = fdtype.Convert("");
                    entity.root.FeildValue = targetpath.Convert("");
                    entity.name.FeildValue = name.Convert("");
                    entity.type.FeildValue = type.Convert(0);
                    entity.utype.FeildValue = utype.Convert(0);
                          //上传配置数据抽取
                          //string config = uploadconfig.Split('|')[uploadtype.Convert(0)];
                          entity.config.FeildValue = config;
                          //发布URL
                          JsonConfigurationHelper _configuration = new JsonConfigurationHelper();
                    string publishurl = _configuration.GetAppSetting("publishurl");
                    entity.puburl.FeildValue = publishurl + targetpath.Split('\\')[0];
                          //预览URl
                          string previewurl = _configuration.GetAppSetting("previewurl");
                    entity.preurl.FeildValue = previewurl + targetpath.Split('\\')[0];
                    entity.encrypt.FeildValue = encrypt.Convert(0);
                    entity.method.FeildValue = method.Convert("");
                    entity.change.FeildValue = change.Convert(0);
                    entity.creator.FeildValue = creator.Convert("");
                          //判断该栏目名称是否存在
                          var result = helper.SelectField<String>(entity, "CheckIsExist");
                    if (result == "0")
                    {
                              //添加或修改存储结构
                              if (autono == 0)
                            helper.Add(entity);
                        else
                            helper.Update(entity);
                        if (entity.NewId() > 0)
                        {
                                  //创建文件夹
                                  if (type == 0)     //Ftp上传
                                  {
                                try
                                {
                                    string ip = config.Split(',')[0].ToString();       //服务器名
                                          string username = config.Split(',')[1].ToString();    //服务器用户名
                                          string pwd = config.Split(',')[2].ToString();            //服务器用户密码
                                          string folderpath = targetpath.Convert("").Replace("\\", "/");   //FTP连接成功后的当前目录targetpath.Convert("").Replace("\\", "/")
                                          KdtFtpClient ftp = new KdtFtpClient(ip, username, pwd, folderpath);
                                    if (!ftp.DirectoryExist(name))    //如果不存在就创建file文件夹
                                          {                                          //创建栏目文件夹
                                              ftp.MakeDir(folderpath);
                                    }
                                          //创建栏目下文件夹类型
                                          string[] drectory = fdtype.Split(',');
                                    foreach (string str in drectory)
                                    {
                                              //循环添加默认栏目分类文件夹192.168.1.125,Administrator,123456abcD
                                              List<Handles.FileStore.StoreNodeEntity> nodelist = Handles.FileStore.StoreNodeData.StoreNodeName;      //所有默认栏目
                                                                                                                                                     //查询符合条件的默认栏目
                                              List<Handles.FileStore.StoreNodeEntity> list = new List<Handles.FileStore.StoreNodeEntity>();
                                        list = nodelist.Where(n => n.filetype == str.Convert(0)).ToList();
                                        var childfoldername = list[0].filename.Convert("");
                                              //判断是否存在文件夹
                                              if (!ftp.DirectoryExist(childfoldername))    //如果不存在就创建file文件夹
                                              {
                                            ftp.MakeDir(folderpath + "/" + childfoldername);     //当前目录下创建子文件夹
                                              }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw new DataException(ex.Message);
                                }
                            }
                            if (utype == 1)     //API上传TODo
                                  {

                            }
                            if (utype == 2)     //WebService上传TODo
                                  {

                            }
                            return new Text(true, "添加成功").ToJson();
                        }
                    }
                    else
                        return new Text(false, "已存在同名流程实例").ToJson();
                    return new Text(true, "保存失败", 5002).ToJson();

                }
            });
        }


        /// <summary>
        /// 删除栏目存储结构
        /// </summary>
        /// <param name="nodeid">栏目唯一标识</param>
        /// <returns></returns>
        [HttpDelete("DeleteStoreNode")]
        public string DeleteStoreNode(string nodeid = "")
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    nodeidlist += nodeid.Convert("") + ",";   //唯一标识集合
                          namelist += "'" + namelist + "' ,";  //栏目名称的集合
                                                               //第一步查询要删除的项
                          GetDeleteNode(nodeid);
                    var entity = new Handles.FileStore.StoreNodeHandler();
                    entity.id.FeildValue = nodeid.Convert("");
                    entity.autono.FeildValue = idlist.Convert("").TrimEnd(',');
                    entity.name.FeildValue = namelist.Convert("").TrimEnd(',');
                          //第一步查询要删除的栏目信息
                          Handles.FileStore.StoreNodeQuery sn = helper.SelectEntity<Handles.FileStore.StoreNodeQuery>(entity, "GetNodeById");
                          //第三步删除行及指标数据
                          helper.TransExecute(entity, "DeleteNodeById");
                    string path = sn.root.Convert("");
                    int uploadtype = sn.utype.Convert(0);
                          //第四步删除服务器上的栏目信息
                          if (uploadtype == 0)   //ftp删除
                          {
                        string ip = sn.config.Split(',')[0].ToString();       //服务器名
                              string username = sn.config.Split(',')[1].ToString();    //服务器用户名
                              string pwd = sn.config.Split(',')[2].ToString();            //服务器用户密码
                              string folderpath = sn.config.Convert("").Replace("\\", "/");   //FTP连接成功后的当前目录targetpath.Convert("").Replace("\\", "/")
                              KdtFtpClient ftp = new KdtFtpClient(ip, username, pwd, folderpath);
                        if (ftp.DirectoryExist(sn.name.Convert("")))    //如果存在就删除
                              {
                            ftp.RemoveDirectory(folderpath);
                        }
                    }
                    else if (uploadtype == 1)
                    {

                    }
                    else if (uploadtype == 2)
                    {

                    }
                    return new Text(true, "删除成功").ToJson();
                }
            });
        }

        /// <summary>
        /// 要删除栏目的唯一标识集合
        /// </summary>
        private string nodeidlist = "";

        /// <summary>
        /// 要删除栏目名称的集合
        /// </summary>
        private string namelist = "";


        /// <summary>
        /// 查询要删除的栏目信息
        /// </summary>
        private void GetDeleteNode(string nodeid = "")
        {
            using (var helper = CreateHelper())
            {
                //第一步根据父Id查询其要删除的子节点Id
                var entity = new Handles.FileStore.StoreNodeHandler();
                entity.pid.FeildValue = nodeid.Convert(0);
                var result = helper.SelectList<Handles.FileStore.StoreNodeQuery>(entity, "GetStoreNodeByPid");
                if (result.Count > 0)
                {
                    List<Handles.FileStore.StoreNodeQuery> list = result.ToJson().ToEntity<List<Handles.FileStore.StoreNodeQuery>>();
                    foreach (var item in list)
                    {
                        //第二步循环赋值
                        idlist = idlist + "'" + item.id + "' ,";
                        namelist = namelist + "'" + item.name + "' ,";
                        GetDeleteNode(item.id);
                    }
                }
            }
        }


        #endregion


        #region  存储文件的添加、删除、查询等事件

        /// <summary>
        /// 添加或编辑存储文件
        /// </summary>
        /// <param name="autono">自增编号</param>
        /// <param name="id">所属栏目</param>
        /// <param name="name">文件名称</param>
        /// <param name="type">文件类型</param>
        /// <param name="status">文件状态</param>
        /// <param name="creator">用户名</param>
        /// <returns></returns>
        [HttpPost("AddStoreFile")]
        public string AddStoreFile(int autono = 0, string id = "", string name = "", int type = 0, int status = 0, string creator = "")
        {
            //Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                          //第一步，上传存储文件
                          var files = HttpContext.Request.Form.Files;
                          //查询接口上传方式及配置
                          int uploadtype = 0;      //上传类型
                          string uploadconfig = "";     //上传配置
                          string targetpath = "";    //上传目录
                          string folderpath = "";   //FTP连接成功后上传目录
                          var filenames = "";
                    if (files.Count > 0)
                    {
                              //可以写遍历files
                              var file = files[0];
                        filenames = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');   //文件名
                              var snentity = new Handles.FileStore.StoreNodeHandler();
                        snentity.id.FeildValue = id.Convert("");
                        Handles.FileStore.StoreNodeQuery sn = helper.SelectEntity<Handles.FileStore.StoreNodeQuery>(snentity, "GetById");
                        uploadtype = sn.utype.Convert(0);
                        uploadconfig = sn.config.Convert("");
                        targetpath = sn.root.Convert("");
                        if (uploadtype == 0)     //Ftp上传
                              {
                            string ip = uploadconfig.Split(',')[0].ToString();       //服务器名
                                  string username = uploadconfig.Split(',')[1].ToString();    //服务器用户名
                                  string pwd = uploadconfig.Split(',')[2].ToString();            //服务器用户密码
                                                                                                 //通过文件类型查询存储格式:
                                  int storeformat = 0;     //存储格式（0：yyyy、1：yyyy-MM、2：yyyy-MM-dd）
                                  var fsentity = new Handles.FileStore.FileStoreHandler();
                            fsentity.type.FeildValue = type.Convert(0);
                            Handles.FileStore.FileStoreQuery fileentity = helper.SelectEntity<Handles.FileStore.FileStoreQuery>(fsentity, "GetFileStoreByType");
                            if (!fileentity.folder.IsNullOrEmpty())
                            {
                                folderpath = targetpath.Replace("\\", "/") + "/" + GetFileTypeName(type) + "/";   //FTP连接成功后上传目录
                                      storeformat = fileentity.format.Convert(0);
                                if (storeformat == 0)    //存储格式（0：yyyy、1：yyyy-MM、2：yyyy-MM-dd）
                                      {
                                    folderpath = folderpath + DateTime.Now.ToString("yyyy");
                                }
                                else if (storeformat == 1)
                                {
                                    folderpath = folderpath + DateTime.Now.ToString("yyyy-MM");
                                }
                                else if (storeformat == 2)
                                {
                                    folderpath = folderpath + DateTime.Now.ToString("yyyy-MM-dd");
                                }
                                try
                                {
                                    KdtFtpClient ftp = new KdtFtpClient(ip, username, pwd, folderpath);
                                    Stream memory = file.OpenReadStream();
                                    ftp.UpLoadFile(memory, folderpath, filenames);      //上传文件

                                      }
                                catch (Exception ex)
                                {
                                    throw new DataException(ex.Message);
                                }
                            }

                        }
                        if (uploadtype == 1)     //API上传TODo.
                              {

                        }
                        if (uploadtype == 2)     //WebService上传TODo.
                              {

                        }
                    }
                          //第二步，添加存储数据库数据
                          var entity = new Handles.FileStore.StoreFileHandler();
                    entity.autono.FeildValue = autono.Convert(0);
                    entity.id.FeildValue = id.Convert("");
                          //文件存储类型
                          string filetypename = GetFileTypeName(type).Convert("");
                    entity.fid.FeildValue = name.Convert("").ToMD5_16();
                    entity.name.FeildValue = name.Convert("");
                    entity.type.FeildValue = type.Convert(0);
                          //需要通过栏目查询ToDo.
                          entity.path.FeildValue = folderpath + "\\" + filenames;
                    entity.status.FeildValue = status.Convert(0);
                    entity.creator.FeildValue = creator.Convert("");
                    helper.Add(entity);
                          //添加或修改文件存储
                          if (entity.NewId() > 0)
                        return new Text(true, "添加成功").ToJson();
                    return new Text(true, "添加失败", 5002).ToJson();
                }
            });
        }



        /// <summary>
        /// 删除存储文件信息
        /// </summary>
        /// <param name="id">栏目Id</param>
        /// <param name="idlist">自增编号拼接字符串（以","分隔，例:1,2,3）</param>
        /// <param name="path">文件路径拼接字符串（以","分隔）</param>
        /// <returns></returns>
        [HttpDelete("DeleteStoreFile")]
        public string DeleteStoreFile(string id = "", string idlist = "", string path = "")
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var entity = new Handles.FileStore.StoreFileHandler();
                    entity.autono.FeildValue = idlist.Convert("");
                    helper.TransExecute(entity, "DelStoreFile");

                          //查询栏目上传类型
                          var snentity = new Handles.FileStore.StoreNodeHandler();
                    snentity.id.FeildValue = id.Convert("");
                    Handles.FileStore.StoreNodeQuery sn = helper.SelectEntity<Handles.FileStore.StoreNodeQuery>(snentity, "GetById");
                    int uploadtype = sn.utype.Convert(0);
                    if (uploadtype == 0)     //Ftp上传
                          {
                        string ip = sn.config.Split(',')[0].ToString();       //服务器名
                              string username = sn.config.Split(',')[1].ToString();    //服务器用户名
                              string pwd = sn.config.Split(',')[2].ToString();            //服务器用户密码
                              try
                        {
                            KdtFtpClient ftp = new KdtFtpClient(ip, username, pwd);
                                  //删除服务器上的文件
                                  string[] filepath = path.Split(',');
                            foreach (var item in filepath)
                            {
                                if (ftp.FileExist(item.Convert("")))    //判断是否存在该文件
                                      {
                                    ftp.DeleteFile(item.Convert(""));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new DataException(ex.Message);
                        }

                    }
                    if (uploadtype == 1)     //API上传TODo.
                          {

                    }
                    if (uploadtype == 2)     //WebService上传TODo.
                          {

                    }


                    return new Text(true, "删除成功").ToJson();
                }
            });
        }

        #endregion

        #region 文件类型转换

        /// <summary>
        /// 文件类型转换
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetFileTypeName(int type)
        {
            switch (type)
            {
                case 0: return "图片";
                case 1: return "文档";
                case 2: return "视频";
                case 3: return "音频";
                case 4: return "脚本";
                case 5: return "CSS";
                case 6: return "可执行文件";
                case 7: return "压缩包";
                case 8: return "其他";
                default: return "";

            }
        }

        #endregion

        #endregion

        #region   Atuh(权限项)

        #region  权限项的添加、删除、查询等事件


        /// <summary>
        /// 查询权限信息
        /// </summary>
        /// <param name="aitemsys">系统名称</param>
        /// <param name="aitemcategory">权限项类别</param>
        /// <param name="aitemid">权限项Id</param>
        /// <returns></returns>
        [HttpGet("GetAuth")]
        public string GetAuth(string aitemsys = "", string aitemcategory = "", string aitemid = "")
        {

            using (var helper = CreateHelper())
            {
                var result = new List<Handles.User.AuthItemQuery>();
                return JsonEntityInvork<List<Handles.User.AuthItemQuery>>(() =>
                {
                    if (aitemsys.IsNullOrEmpty() && aitemcategory.IsNullOrEmpty() && aitemid.IsNullOrEmpty())
                    {
                        result = helper.SelectList<Handles.User.AuthItemQuery>(SetHandler<Handles.User.AuthItemHandler>(), "GetAllAuth");
                    }
                    else if (!aitemsys.IsNullOrEmpty() && aitemcategory.IsNullOrEmpty())
                    {
                        result = helper.SelectList<Handles.User.AuthItemQuery>(SetHandler<Handles.User.AuthItemHandler>(), "GetAllAuthBySys");
                    }
                    else if (!aitemsys.IsNullOrEmpty() && !aitemcategory.IsNullOrEmpty())
                    {
                        result = helper.SelectList<Handles.User.AuthItemQuery>(SetHandler<Handles.User.AuthItemHandler>(), "GetAuthByCategory");
                    }
                    else if (!aitemid.IsNullOrEmpty())
                    {
                        result = helper.SelectList<Handles.User.AuthItemQuery>(SetHandler<Handles.User.AuthItemHandler>(), "GetAllAuthById");
                    }
                    return result;

                });
            }

        }


        /// <summary>
        /// 查询角色权限或用户权限或角色用户权限
        /// </summary>
        /// <param name="type">查询类型（0:用户，1:角色，2:角色用户）</param>
        /// <param name="aitemid">权限项ID</param>
        /// <param name="key">检索关键字</param>
        /// <param name="start">起始页(初始值为0)</param>
        /// <param name="size">每页多少条</param>
        /// <returns></returns>
        //[HttpGet("GetRoleORUserOfAuth")]
        //public string GetRoleORUserOfAuth(int type = 0, string aitemid = "", string key = "", int start = 0, int size = 10)
        //{
        //    using (Invoke cn = new Invoke())
        //    {
        //        ResponseData result = new ResponseData();
        //        if (type == 0)    //用户权限
        //        {
        //            var entity = new UserAuthHandler();
        //            entity.AitemId.FeildValue = aitemid.Convert("");
        //            entity.UserId.FeildValue = key.Convert("");
        //            KdtPageEx page = new KdtPageEx()
        //            {
        //                selpage = "GetUserAuthPage",
        //                selpagetotal = "GetUserAuthPageCount",
        //                start = start + 1,
        //                end = start + size
        //            };
        //            result = cn.SelectPage<AuthEntity>(entity, page);
        //            if (result.Success)
        //            {
        //                return ReturnBase<AuthEntity>.returnToList(result).ToJson();
        //            }

        //        }
        //        else if (type == 1)    //角色权限
        //        {
        //            var entity = new RoleAuthHandler();
        //            entity.AitemId.FeildValue = aitemid.Convert("");
        //            entity.RoleKey.FeildValue = key.Convert("");
        //            KdtPageEx page = new KdtPageEx()
        //            {
        //                selpage = "GetRoleAuthPage",
        //                selpagetotal = "GetRoleAuthPageCount",
        //                start = start + 1,
        //                end = start + size
        //            };
        //            result = cn.SelectPage<AuthEntity>(entity, page);
        //            if (result.Success)
        //            {
        //                return ReturnBase<AuthEntity>.returnToList(result).ToJson();
        //            }

        //        }
        //        else if (type == 2)    //角色用户权限
        //        {
        //            var entity = new RoleUserAuthHandler();
        //            entity.AitemId.FeildValue = aitemid.Convert("");
        //            entity.RoleKey.FeildValue = key.Convert("");
        //            entity.UserId.FeildValue = key.Convert("");
        //            KdtPageEx page = new KdtPageEx()
        //            {
        //                selpage = "GetUserOfRoleAuthPage",
        //                selpagetotal = "GetUserOfRoleAuthPageCount",
        //                start = start + 1,
        //                end = start + size
        //            };
        //            result = cn.SelectPage<AuthEntity>(entity, page);
        //            if (result.Success)
        //            {
        //                return ReturnBase<AuthEntity>.returnToList(result).ToJson();
        //            }
        //        }

        //        result.Msg = "查询失败";
        //        return result.ToJson();
        //    }

        //}


        /// <summary>
        /// 添加或修改权限项
        /// </summary>
        /// <param name="id">自增编号(添加时不用传值)</param>
        /// <param name="checkedvalue">是否添加系统权限项（0:不添加，1:添加）</param>
        /// <param name="aitemsys">系统名称</param>
        /// <param name="aitemcategory">权限项类别</param>
        /// <param name="aitemnick">权限项昵称</param>
        /// <param name="aitemnote">权限项描述</param>
        /// <param name="creator">用户名</param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateAuth")]
        public string AddOrUpdateAuth(int id = 0, int checkedvalue = 0, string aitemsys = "", string aitemcategory = "", string aitemnick = "", string aitemnote = "", string creator = "")
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var authentity = new Handles.User.AuthItemHandler();
                    authentity.autono.FeildValue = id.Convert(0);
                    authentity.sys.FeildValue = aitemsys.Convert("");
                    authentity.category.FeildValue = aitemcategory.Convert("");
                    authentity.nick.FeildValue = aitemnick.Convert("");
                    authentity.note.FeildValue = aitemnote.Convert("");
                    authentity.creator.FeildValue = creator.Convert("");
                    if (id == 0)    //添加
                          {
                        if (aitemnick.IsNullOrEmpty())    //添加系统名称及权限项类别
                              {
                                  //第一步，判断该权限项类别是否存在
                                  var result = helper.SelectField<String>(authentity, "GetCheckByCategory");
                            if (result == "0")
                            {
                                      //第二步，添加系统名称及权限项类别
                                      if (checkedvalue == 1)      //添加系统权限（系统权限：增删改查）【系统名称+全新啊项类别.md5+sys001】
                                      {
                                          //先删除系统字段再添加
                                          authentity.id.FeildValue = (aitemsys + aitemcategory).ToMD5_16() + "sys00";
                                    helper.TransExecute(authentity, "DelAuthById");
                                          //添加
                                          List<Handles.User.AuthItemQuery> authdata = Handles.User.AuthData.AuthIdList;
                                    for (int i = 0; i < authdata.Count; i++)
                                    {
                                        authentity.id.FeildValue = (aitemsys + aitemcategory).ToMD5_16() + "sys00" + (i + 1);
                                        authentity.nick.FeildValue = authdata[i].nick.Convert("");
                                        authentity.note.FeildValue = authdata[i].note.Convert("");
                                        helper.Add(authentity);
                                        if (authentity.NewId() < 0)
                                        {
                                                  //回滚删除
                                                  helper.TransExecute(authentity, "DelAuthByCategory");
                                            return new Text(false, "添加失败", 5002).ToJson();
                                        }
                                    }
                                }
                                else     //不添加系统权限
                                      {
                                    authentity.id.FeildValue = "";
                                    helper.Add(authentity);
                                    if (authentity.NewId() < 0)
                                    {
                                              //回滚删除
                                              helper.TransExecute(authentity, "DelAuthByCategory");
                                        return new Text(false, "添加失败", 5002).ToJson();
                                    }
                                }
                            }
                            else
                            {
                                return new Text(false, "已存在同名权限项类别").ToJson();
                            }
                        }
                        else       //添加权限项ID        
                              {

                                  //第一步查询权限项Id字段最大值
                                  authentity.id.FeildValue = (aitemsys + aitemcategory).ToMD5_16() + "u00";
                            Handles.User.AuthItemQuery auth = helper.SelectEntity<Handles.User.AuthItemQuery>(authentity, "GetMaxAuthId");
                            var aitemid = auth.id.Convert("");
                            if (!aitemid.IsNullOrEmpty())
                            {
                                authentity.id.FeildValue = (aitemsys + aitemcategory).ToMD5_16() + "u00" + (Int32.Parse(aitemid.Substring(20, aitemid.Length - 20)) + 1);
                            }
                            else
                            {
                                authentity.id.FeildValue = (aitemsys + aitemcategory).ToMD5_16() + "u001";
                            }
                                  //第二步根据系统名称及权限项类别查询是否存在空的权限项ID（如果存在则编辑，否则添加）
                                  auth = helper.SelectEntity<Handles.User.AuthItemQuery>(authentity, "GetAuthIdByCategory");
                            if (auth.id.IsNullOrEmpty() && !auth.sys.IsNullOrEmpty() && !auth.category.IsNullOrEmpty())
                            {
                                authentity.autono.FeildValue = auth.id.Convert(0);
                                authentity.sys.FeildValue = auth.sys.Convert("");
                                authentity.category.FeildValue = auth.category.Convert("");
                                      //第三步修改权限项Id
                                      helper.Update(authentity);
                            }
                            else
                            {
                                      //第三步添加权限项Id
                                      helper.Add(authentity);
                                if (authentity.NewId() < 0)
                                {
                                    return new Text(false, "添加失败", 5002).ToJson();
                                }
                            }

                        }
                    }
                    else        //修改
                          {
                        helper.Update(authentity);
                        if (authentity.Affected < 0)
                            return new Text(false, "修改失败", 5002).ToJson();
                    }
                    return new Text(false, "添加失败").ToJson();
                }

            });

        }


        /// <summary>
        ///  删除权限项(删除系统名称/删除权限项类别/删除权限项ID)
        /// </summary>
        /// <param name="aitemsys">系统名称</param>
        /// <param name="aitemcategory">权限项类别</param>
        /// <param name="aitemid">权限项ID</param>
        /// <returns></returns>
        [HttpDelete("DeleteAuth")]
        public string DeleteAuth(string aitemsys = "", string aitemcategory = "", string aitemid = "")
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var authentity = new Handles.User.AuthItemHandler();
                    authentity.sys.FeildValue = aitemsys.Convert("");
                    authentity.category.FeildValue = aitemcategory.Convert("");
                    authentity.id.FeildValue = aitemid.Convert("");
                          //删除权限项(同时删除其用户权限表，角色权限表，角色用户权限表)
                          if (aitemid.IsNullOrEmpty())
                    {
                              //第一种，删除系统名称(aitemcategory为空)
                              if (!aitemcategory.IsNullOrEmpty())
                        {
                                  //第二种，删除权限项类别
                                  helper.TransExecute(authentity, "DelAuthBySysOrCategory");
                        }
                        helper.TransExecute(authentity, "DelAuthBySys");
                    }
                    else
                    {
                              //第三种，删除权限项ID
                              helper.TransExecute(authentity, "DelAuthByAitemId");
                    }
                    return new Text(false, "删除失败").ToJson();
                }
            });
        }

        #endregion

        #endregion

        #region   User(用户)

        #region 用户的添加、删除、查询等事件

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddUser")]
        public string AddUser()
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var handler = SetHandler<Handles.User.UserHandler>(true);
                    var result = helper.SelectField<string>(handler, "CheckById");
                    if (result != "0")
                        return new Text(false, "已存在同名用户Id").ToJson();
                    helper.TransExecute(handler, "AddUser");
                    if (handler.NewId() > 0)
                        return new Text(true, "添加成功").ToJson();
                    else
                        return new Text(false, "添加失败", 5002).ToJson();
                }
            });
        }


        /// <summary>
        ///  更新用户
        /// </summary>
        /// <param name="autono"></param>
        /// <param name="id">用户名</param>
        /// <param name="nick">用户昵称</param>
        /// <param name="pwd">用户密码</param>
        /// <param name="openid">OPEN_ID</param>
        /// <param name="source">绑定来源</param>
        /// <param name="phone">用户手机</param>
        /// <param name="classify">用户分类</param>
        /// <param name="email">用户邮箱</param>
        /// <param name="userprop">自定义字段</param>
        /// <returns></returns>
        [HttpPost("UpdateUser")]
        public string UpdateUser(int autono, string id, string nick, string pwd, string openid, string source, string phone, int classify, string email, string userprop)
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var handler = new Handles.User.UserHandler();
                    handler.autono.FeildValue = autono.Convert(0);
                    handler.id.FeildValue = id.Convert("");
                    var result = helper.SelectField<string>(handler, "SelectUserId");
                    if (result != "0")
                    {
                        return new Text(false, "已存在同名用户Id").ToJson();
                    }
                    handler.nick.FeildValue = nick.Convert("");
                    handler.pwd.FeildValue = pwd.ToMD5_16();
                    handler.openid.FeildValue = openid;
                    handler.source.FeildValue = source.Convert("");
                    handler.phone.FeildValue = phone.Convert("");
                    handler.classify.FeildValue = classify.Convert("");
                    handler.email.FeildValue = email.Convert("");
                    handler.creator.FeildValue = userprop.Convert("");
                    handler.ctime.FeildValue = DateTime.Now.ToString();
                    if (userprop.IsNullOrEmpty())
                        helper.Update(handler);
                    else
                        helper.TransExecute(handler, "UpdateUser");

                    if (handler.Affected > 0)
                        return new Text(true, "修改成功").ToJson();
                    else
                        return new Text(false, "修改失败", 5002).ToJson();
                }
            });
        }



        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="type">用户分类</param>
        /// <param name="start">开始页</param>
        /// <param name="size">每页条数</param>
        /// <returns></returns>
        [HttpGet("GetUser")]
        public string GetUserPage(string key, int type, int start = 0, int size = 10)
        {
            return JsonEntityInvork<List<Handles.User.UserQuery>>(() =>
            {
                using (var helper = CreateHelper())
                {
                    var handler = new Handles.User.UserHandler();
                    string sql = " where 1 = 1 ";
                    if (!key.IsNullOrEmpty())
                        sql += " and ( (a.user_id like '%" + key + "%') or (a.user_nick like '%" + key + "%'))";
                    if (type == 0)
                        sql += " and a.user_classify = 0";
                    else if (type == 1)
                        sql += " and a.user_classify=1";
                    else if (type == 2)
                        sql += " and a.user_classify=2";

                    handler.creator.FeildValue = sql;
                    KdtPageEx page = new KdtPageEx()
                    {
                        selpage = "GetUserPage",
                        selpagetotal = "GetUserPageCount",
                        start = start + 1,
                        end = start + size
                    };
                    return helper.SelectPage<Handles.User.UserQuery>(handler, page);
                }
            });
        }



        /// <summary>
        /// 根据用户名查询用户
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <returns></returns>          
        [HttpGet("GetUserByName")]
        public string GetUserByName(string id)
        {
            using (var helper = CreateHelper())
            {
                string strsql = "select * from kdt_user a LEFT JOIN kdt_user_extend b on a.user_id=b.user_id  where a.user_id= '" + id + "'";
                var result = helper.ExecuteQuery(strsql);
                return result.ToJson();
            }

        }


        #endregion


        #region 职工操作

        /// <summary>
        /// 添加职工
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddEmploy")]
        public string AddEmploy()
        {
            return JsonDirectInvork(() =>
            {
                using (var helper = CreateHelper())
                {
                    var handler = SetHandler<Handles.User.EmployHandler>();
                    var result = helper.SelectField<string>(handler, "SelectByEmployId");
                    if (result != "0")
                        return new Text(false, "已存在同名职员Id").ToJson();
                    helper.TransExecute(handler, "AddEmploy");
                    if (handler.NewId() > 0)
                        return new Text(true, "添加成功").ToJson();
                    else
                        return new Text(false, "添加失败", 5002).ToJson();
                }
            });
        }


        ///// <summary>
        ///// 更新职员
        ///// </summary>
        ///// <param name="autono"></param>
        ///// <param name="employid"></param>
        ///// <param name="enick"></param>
        ///// <param name="sex"></param>
        ///// <param name="birthday"></param>
        ///// <param name="native"></param>
        ///// <param name="bplace"></param>
        ///// <param name="userprop"></param>
        ///// <returns></returns>
        //[HttpPost("UpdateEmploy")]
        //public string UpdateEmploy(int autono, string employid, string enick, string sex, string birthday, string native, string bplace, string userprop)
        //{
        //    return JsonDirectInvork(() =>
        //    {
        //        using (var helper = CreateHelper())
        //        {
        //            var handler = new Handles.User.EmployHandler();
        //            handler.Autono.FeildValue = id;
        //            handler.EmployId.FeildValue = employid;
        //            ResponseData result = helper.SelectEntity<EmployQuery>(handler, "SelectEmployId");
        //            if (result.Success)
        //            {
        //                var employ = result.Msg.ToEntity<EmployQuery>();
        //                if (!employ.EmployId.IsNullOrEmpty())
        //                {
        //                    result.Msg = "该用户已经存在";
        //                    return result.ToJson();
        //                }
        //                handler.ENick.FeildValue = enick;
        //                handler.ESex.FeildValue = sex;
        //                handler.Birthday.FeildValue = birthday;
        //                handler.Native.FeildValue = native;
        //                handler.Place.FeildValue = bplace;
        //                handler.Creator.FeildValue = "admin";
        //                handler.CTime.FeildValue = DateTime.Now.ToString();
        //                if (userprop.IsNullOrEmpty())
        //                {
        //                    result = helper.Update(handler);
        //                }
        //                else
        //                {
        //                    handler.Creator.FeildValue = userprop;
        //                    result = helper.TransExecute(handler, "UpdateEmploy");
        //                }
        //                if (result.Success)
        //                {
        //                    WriteLog(new UserIdLog(employid, "更新职工", String.Format("更新了名为{0}的职工！", employid)));
        //                    result.Msg = "更新成功";
        //                    return result.ToJson();
        //                }
        //                result.Msg = "更新失败";
        //                return result.ToJson();
        //            }
        //            result.Msg = "更新失败";
        //            return result.ToJson();

        //        }
        //    });
        //}




        ///// <summary>
        ///// 获取会员信息
        ///// </summary>
        ///// <param name="employ"></param>
        ///// <param name="start"></param>
        ///// <param name="size"></param>
        ///// <returns></returns>
        //[HttpGet("GetEmploy")]
        //public string GetEmploy(string employ, int start = 0, int size = 10)
        //{
        //    using (Invoke helper = new Invoke())
        //    {
        //        string sql = "where 1=1";
        //        var handler = new EmployHandler();
        //        if (!employ.IsNullOrEmpty())
        //        {
        //            sql += " and employ_id like '%" + employ + "%' or employ_nick like '%" + employ + "%'";
        //        }
        //        handler.Creator.FeildValue = sql;
        //        KdtPageEx page = new KdtPageEx()
        //        {
        //            selpage = "GetEmployPage",
        //            selpagetotal = "GetEmployPageCount",
        //            start = start + 1,
        //            end = start + size
        //        };
        //        ResponseData result = helper.SelectPage<EmployQuery>(handler, page);
        //        if (result.Success)
        //        {
        //            return ReturnBase<EmployQuery>.returnToList(result).ToJson();
        //        }
        //        else
        //        {
        //            return result.ToJson();

        //        }

        //    }
        //}




        ///// <summary>
        ///// 根据职员名查询职员详情
        ///// </summary>
        ///// <param name="employid">职员名</param>
        ///// <returns></returns>
        //[HttpGet("GetEmployByName")]
        //public string GetEmployByName(string employid)
        //{
        //    using (Invoke helper = new Invoke())
        //    {
        //        string strsql = "select * from kdt_employ a LEFT JOIN kdt_employ_extend b on a.employ_id=b.user_id  where a.employ_id= '" + employid + "'";
        //        ResponseData result = helper.ExecuteQuery(strsql);
        //        if (result.Success)
        //        {
        //            return result.ToJson();
        //        }
        //        result.Msg = "查询失败";
        //        return result.ToJson();
        //    }

        //}




        ///// <summary>
        ///// 删除用户
        ///// </summary>
        ///// <param name="employ"></param>
        ///// <returns></returns>
        //[HttpDelete("DeleteEmploy")]
        //public string DeleteEmploy(string employ)
        //{
        //    using (Invoke helper = new Invoke())
        //    {
        //        var handler = new EmployHandler();
        //        handler.ENick.FeildValue = employ.Convert("");
        //        ResponseData result = helper.SelectField<string>(handler, "DeleteEmploy");
        //        if (result.Success)
        //        {
        //            WriteLog(new UserIdLog(employ, "删除职工", String.Format("删除了名为{0}的职工！", employ)));
        //            result.Msg = "删除成功";
        //            return result.ToJson();
        //        }
        //        result.Msg = "删除失败";
        //        return result.ToJson();
        //    }
        //}

        //#endregion


        //#region 用户字典操作
        ///// <summary>
        ///// 用户添加新字段
        ///// </summary>
        ///// <param name="feildtype"></param>
        ///// <param name="feildlen"></param>
        ///// <param name="usertype">0为用户，1为职员</param>
        ///// <param name="display"></param>
        ///// <returns></returns>
        //[HttpPost("AddUserDict")]
        //public string AddUserDict(string feildtype, int feildlen, int usertype, string display)
        //{
        //    using (Invoke helper = new Invoke())
        //    {
        //        var handler = new UserDictHandler();
        //        handler.UType.FeildValue = usertype.Convert("");
        //        ResponseData result = helper.SelectEntity<UserDictQuery>(handler, "SelectMaxFeild");
        //        if (result.Success)
        //        {
        //            var feild = result.Msg.ToEntity<UserDictQuery>();
        //            var userfeild = feild.UserFeild.Convert("");
        //            if (userfeild.IsNullOrEmpty())
        //            {
        //                handler.ExFeild.FeildValue = "userprop1";
        //            }
        //            else
        //            {
        //                handler.ExFeild.FeildValue = "userprop" + (Int32.Parse(userfeild.Substring(8, userfeild.Length - 8)) + 1);
        //            }
        //            handler.FType.FeildValue = feildtype.Convert("");
        //            handler.FLen.FeildValue = feildlen.Convert(0);
        //            handler.Display.FeildValue = display.Convert("");
        //            handler.Creator.FeildValue = "admin";
        //            handler.CTime.FeildValue = DateTime.Now.ToString();
        //            result = helper.Add(handler);
        //            if (result.Success && !result.Msg.IsNullOrEmpty())
        //            {
        //                if (usertype == 0)
        //                {
        //                    result = helper.SelectField<UserDictQuery>(handler, "AddUserField");
        //                }
        //                else if (usertype == 1)
        //                {
        //                    result = helper.SelectField<UserDictQuery>(handler, "AddEmployField");
        //                }
        //                else
        //                {
        //                    result.Msg = "该类型用户不存在";
        //                    return result.ToJson();
        //                }

        //                if (!result.Success)
        //                {
        //                    result = helper.Delete(handler);
        //                    if (result.Success)
        //                    {
        //                        result.Msg = "添加失败";
        //                        return result.ToJson();
        //                    }
        //                    else
        //                    {
        //                        result.Msg = "回滚删除失败";
        //                        return result.ToJson();
        //                    }
        //                }
        //                WriteLog(new UserIdLog(userfeild, "新增字段", String.Format("为用户新增了字段！", userfeild)));
        //                result.Msg = "添加成功";
        //                return result.ToJson();
        //            }
        //            else
        //            {
        //                result.Msg = "添加失败";
        //                return result.ToJson();
        //            }

        //        }
        //        result.Msg = "添加失败";
        //        return result.ToJson();
        //    }
        //}

        ///// <summary>
        ///// 更新用户字段
        ///// </summary>
        ///// <param name="autono"></param>
        ///// <param name="feild"></param>
        ///// <param name="feildtype"></param>
        ///// <param name="feildlen"></param>
        ///// <param name="usertype">0为用户，1为职员</param>
        ///// <param name="display"></param>
        ///// <returns></returns>
        //[HttpPost("UpdateUserDict")]
        //public string UpdateUserDict(int autono, string feild, string feildtype, int feildlen, int usertype, string display)
        //{
        //    using (Invoke helper = new Invoke())
        //    {
        //        var handler = new UserDictHandler();
        //        handler.Autono.FeildValue = autono;//userid和feild不可以做修改
        //        handler.ExFeild.FeildValue = feild;
        //        handler.FType.FeildValue = feildtype;
        //        handler.FLen.FeildValue = feildlen;
        //        handler.UType.FeildValue = usertype;
        //        handler.Display.FeildValue = display;
        //        handler.Creator.FeildValue = "admin";
        //        handler.CTime.FeildValue = DateTime.Now.ToString();
        //        ResponseData result = helper.Update(handler);
        //        if (result.Success)
        //        {
        //            if (usertype == 0)
        //            {
        //                result = helper.SelectField<UserDictQuery>(handler, "ModifyUserField");
        //            }
        //            else if (usertype == 1)
        //            {
        //                result = helper.SelectField<UserDictQuery>(handler, "ModifyEmployField");
        //            }
        //            else
        //            {
        //                result.Msg = "该类型用户不存在";
        //                return result.ToJson();
        //            }
        //            if (result.Success)
        //            {
        //                WriteLog(new UserIdLog(feild, "更新字段", String.Format("为用户更新了{0}字段！", feild)));
        //                result.Msg = "修改成功";
        //                return result.ToJson();
        //            }
        //            else
        //            {
        //                result.Msg = "修改失败";
        //                return result.ToJson();
        //            }
        //        }
        //        result.Msg = "修改失败";
        //        return result.ToJson();
        //    }
        //}

        ///// <summary>
        ///// 删除用户字段
        ///// </summary>
        ///// <param name="usertype">0为用户，1为职员</param>
        ///// <param name="feild"></param>
        ///// <returns></returns>
        //[HttpDelete("DeleteUserDict")]
        //public string DeleteUserDict(int usertype, string feild)
        //{
        //    using (Invoke helper = new Invoke())
        //    {
        //        string[] data = feild.Split(",");
        //        string sql = "";
        //        foreach (var item in data)
        //        {
        //            var cdata = item.Split("'");
        //            string onefeild = cdata[1];
        //            sql += " drop " + onefeild + ",";
        //        }
        //        sql = sql.Substring(0, sql.Length - 1);
        //        var handler = new UserDictHandler();
        //        handler.ExFeild.FeildValue = feild;
        //        handler.Creator.FeildValue = sql;
        //        ResponseData result = new ResponseData();
        //        if (usertype == 0)
        //        {
        //            result = helper.TransExecute(handler, "DeleteUserFeild");
        //        }
        //        else if (usertype == 1)
        //        {
        //            result = helper.TransExecute(handler, "DeleteEmployFeild");
        //        }
        //        if (result.Success)
        //        {
        //            WriteLog(new UserIdLog(feild, "删除字段", String.Format("为用户删除了{0}字段！", feild)));
        //            result.Msg = "删除成功";
        //            return result.ToJson();
        //        }
        //        result.Msg = "删除失败";
        //        return result.ToJson();
        //    }
        //}

        ///// <summary>
        ///// 查询用户字典
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("GetUserDict")]
        //public string GetUserDict(string usertype, string display, int start, int size)
        //{
        //    using (Invoke helper = new Invoke())
        //    {
        //        string sql = "where 1=1";
        //        if (usertype == "0")
        //        {
        //            sql += " and user_type = 0";
        //        }
        //        else if (usertype == "1")
        //        {
        //            sql += " and user_type = 1";
        //        }
        //        if (!display.IsNullOrEmpty())
        //        {
        //            sql += " and user_ext_display like '%" + display + "%'";
        //        }
        //        var handler = new UserDictHandler();
        //        handler.Creator.FeildValue = sql;
        //        KdtPageEx page = new KdtPageEx()
        //        {
        //            selpage = "GetDictPage",
        //            selpagetotal = "GetDictPageCount",
        //            start = start + 1,
        //            end = start + size
        //        };
        //        ResponseData result = helper.SelectPage<UserDictQuery>(handler, page);
        //        if (result.Success)
        //        {
        //            return ReturnBase<UserDictQuery>.returnToList(result).ToJson();
        //        }
        //        result.Msg = "查询失败";
        //        return result.ToJson();
        //    }

        //}



        ///// <summary>
        ///// 查询用户字典
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("GetUserDictById")]
        //public string GetUserDictById(int autono)
        //{
        //    using (Invoke helper = new Invoke())
        //    {
        //        var handler = new UserDictHandler();
        //        handler.Autono.FeildValue = autono.Convert(0);
        //        ResponseData result = helper.SelectEntity<UserDictQuery>(handler, "SelectUserDictById");
        //        if (result.Success)
        //        {
        //            return ReturnBase<UserDictQuery>.ReturnToEntity(result).ToJson();
        //        }
        //        result.Msg = "查询失败";
        //        return result.ToJson();
        //    }

        //}





        //#endregion


        //#region 登录判断用户是否存在
        ///// <summary>
        ///// 登录用户判断
        ///// </summary>
        ///// <param name="userid"></param>
        ///// <param name="password"></param>
        ///// <returns></returns>
        //[HttpGet("Login")]
        //public string Login(string userid, string password)
        //{
        //    using (Invoke helper = new Invoke())
        //    {
        //        var handler = new UserHandler();
        //        handler.UserId.FeildValue = userid.Convert("");
        //        handler.UPwd.FeildValue = password.ToMD5_16().Convert("");
        //        ResponseData result = helper.SelectEntity<UserQuery>(handler, "SelectLoginUser");
        //        if (result.Success)
        //        {
        //            var user = result.Msg.ToEntity<UserQuery>();
        //            if (!user.UserId.IsNullOrEmpty())
        //            {
        //                result.Msg = "该用户存在";
        //                return result.ToJson();
        //            }
        //            result.Msg = "该用户不存在或密码错误";
        //            return result.ToJson();
        //        }
        //        result.Msg = "查询失败";
        //        return result.ToJson();
        //    }
        //}


        //#endregion


        //#region  用户，角色，角色用户模糊查询
        ///// <summary>
        ///// 用户模糊查询（为角色添加用户）
        ///// </summary>
        ///// <param name="roleid"></param>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //[HttpGet("GetRUser")]
        //public string GetRUser(int roleid, string user)
        //{
        //    using (Invoke helper = new Invoke())
        //    {
        //        var handler = new RoleUserHandler();
        //        handler.RoleKey.FeildValue = roleid.Convert(0);
        //        handler.UserId.FeildValue = user.Convert("");
        //        ResponseData result = helper.SelectList<RoleUserQuery>(handler, "SelectUser");
        //        if (result.Success)
        //        {
        //            return ReturnBase<RoleUserQuery>.returnToList(result).ToJson();
        //        }
        //        else
        //        {
        //            result.Msg = "查询失败";
        //            return result.ToJson();
        //        }
        //    }
        //}






        ///// <summary>
        ///// 用户，角色，角色用户的模糊查询
        ///// </summary>
        ///// <param name="tab"></param>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //[HttpGet("GetRoleOrUser")]
        //public string GetRoleOrUser(int tab, string name)
        //{
        //    using (Invoke helper = new Invoke())
        //    {
        //        ResponseData result = new ResponseData();
        //        if (tab != 0)
        //        {
        //            if (tab == 1)
        //            {
        //                var handler = new UserHandler();
        //                handler.UserId.FeildValue = name.Convert("");
        //                result = helper.SelectList<UserQuery>(handler, "SelectByUser");
        //                if (result.Success)
        //                {
        //                    return ReturnBase<UserQuery>.returnToList(result).ToJson();
        //                }
        //                else
        //                {
        //                    result.Msg = "查询失败";
        //                    return result.ToJson();
        //                }


        //            }
        //            else if (tab == 2)
        //            {
        //                var handler = new RoleHandler();
        //                handler.RNick.FeildValue = name.Convert("");
        //                result = helper.SelectList<RoleQuery>(handler, "SelectByRole");
        //                if (result.Success)
        //                {
        //                    return ReturnBase<RoleQuery>.returnToList(result).ToJson();
        //                }
        //                else
        //                {
        //                    result.Msg = "查询失败";
        //                    return result.ToJson();
        //                }
        //            }
        //            else if (tab == 3)
        //            {
        //                var handler = new RoleUserHandler();
        //                handler.UserId.FeildValue = name.Convert("");
        //                result = helper.SelectList<RoleUserQuery>(handler, "SelectByRUser");
        //                if (result.Success)
        //                {
        //                    return ReturnBase<RoleUserQuery>.returnToList(result).ToJson();
        //                }
        //                else
        //                {
        //                    result.Msg = "查询失败";
        //                    return result.ToJson();
        //                }
        //            }
        //        }
        //        result.Msg = "输入的用户类型有错误";
        //        return result.ToJson();

        //    }
        //}






        #endregion

        #endregion

    }

}
