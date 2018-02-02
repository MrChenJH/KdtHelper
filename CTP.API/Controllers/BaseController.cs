using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KdtHelper.Common;
using KdtHelper.Common.Json;
using CTP.API.Util;
using KdtHelper.Core.ExecuterEx;
using System.Reflection;
using Microsoft.AspNetCore.Cors;

namespace CTP.API.Controllers
{
    /// <summary>
    /// 基础对外服务类
    /// </summary>
    public abstract class BaseController : Controller
    {
        #region 公共操作类

        /// <summary>
        /// 操作数据库基本方法
        /// </summary>
        /// <param name="handler">所属HANDLER</param>
        /// <param name="optype">操作类型<code>0:添加, 1 添加并返回自增编号，2 修改，3 AddOrUpdate, 8 删除 </code></param>
        /// <returns></returns>
        protected virtual string Op(string handler, int optype)
        {
            return JsonDirectInvork(() =>
            {
                // 初始化handler
                var baseHandler = CreateHandler(handler);
                if (baseHandler == null) throw new ArgumentNullException();

                // 执行操作
                using (var helper = CreateHelper())
                {
                    switch (optype)
                    {
                        case 0: helper.Add(baseHandler); break;
                        case 1: helper.Add(baseHandler); return baseHandler.NewId().ToString();
                        case 2: helper.Update(baseHandler); break;
                        case 3: helper.AddOrUpdate(baseHandler); break;
                        case 8: helper.Delete(baseHandler); break;
                        default: throw new NullParam("optype");
                    }
                }
                if (baseHandler.Affected > 0 || baseHandler.NewId() > 0)
                {
                    return new Text(true).ToJson();
                }

                throw new DbExecuteException();
            });
        }

        /// <summary>
        /// 条件执行方法
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="ext_method"></param>
        /// <returns></returns>
        protected virtual string Trans(string handler, string ext_method)
        {
            return JsonDirectInvork(() =>
            {
                // 初始化handler
                var baseHandler = CreateHandler(handler);
                if (baseHandler == null) throw new ArgumentNullException();

                if(ext_method.IsNullOrEmpty()) throw new NullParam("ext_method");

                // 执行操作
                using (var helper = CreateHelper())
                {
                    helper.TransExecute(baseHandler, ext_method);
                    if (baseHandler.Affected<0)
                    {
                        throw new DbExecuteException();
                    }
                }
                return new Text(true).ToJson();

            });
        }

        /// <summary>
        /// 查找单个字段值
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        protected virtual string GetField(string handler, string field)
        {
            return JsonDirectInvork(() =>
            {
                // 初始化handler
                var baseHandler = CreateHandler(handler);
                if (baseHandler == null) throw new ArgumentNullException();
                using (var helper = CreateHelper())
                {
                    return helper.SelectField<string>(baseHandler, field);
                }
            });
        }

        /// <summary>
        /// 读取单条数据集
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        protected virtual string GetById(string handler)
        {
            return SelectEntity(handler, "GetById");
        }

        /// <summary>
        /// 读取所有数据集合
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        protected virtual string GetAll(string handler)
        {
            return SelectList(handler, "GetAll");
        }

        /// <summary>
        /// 先判重再添加
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        protected virtual string AddCheckName(string handler)
        {
            return JsonDirectInvork(() =>
            {
                // 初始化handler
                var baseHandler = CreateHandler(handler);
                if (baseHandler == null) throw new ArgumentNullException();
                using (var helper = CreateHelper())
                {
                   var result =  helper.SelectField<string>(baseHandler, "CheckByName");
                    if(result != "0")
                    {
                        return new Text(false, "已存在同名文件").ToJson();
                    }
                    helper.Add(baseHandler);
                    if (baseHandler.Affected > 0 || baseHandler.NewId() > 0) return new Text(true, "添加成功").ToJson();
                    return new Text(false, "添加失败", 5002).ToJson();
                }
            });
        }

        /// <summary>
        /// 左侧菜单分层结构
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        protected virtual string GetCateList(string handler)
        {
            if (Request.Query["searchChild"].ToString() == "1")
                return SelectList(handler, "SelectChildInfo");
            else
                return SelectList(handler, "SelectGroupInfo");
        }

        /// <summary>
        /// 根据关键字模糊查找
        /// </summary>
        /// <returns></returns>
        protected string GetListByKey(string handler)
        {
            return SelectList(handler, "GetListByKey");
        }

        /// <summary>
        /// 获取类别信息
        /// </summary>
        /// <returns></returns>
        protected string GetCategory(string handler)
        {
            return SelectList(handler, "GetCategory");
        }

        /// <summary>
        /// 读取自定义方法数据集合
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        protected virtual string GetMethod(string handler,string method)
        {
            return SelectList(handler, method);
        }
        #endregion.

        #region 数据库操作类私有方法

        /// <summary>
        /// 创建Handler
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        protected virtual KdtFieldEntityEx CreateHandler(string handler)
        {
            if (handler.IsNullOrEmpty()) throw new NullParam("handler");
            switch (handler.Trim().ToLower())
            {
                #region  User(用户管理) 
                case "user": return SetHandler<Handles.User.UserHandler>();
                case "role": return SetHandler<Handles.User.RoleHandler>();
                case "ruser": return SetHandler<Handles.User.RoleUserHandler>();
                case "userdict": return SetHandler<Handles.User.UserDictHandler>();
                #endregion

                #region  Object(数据对象) 

                case "column": return SetHandler<Handles.Object.IndexColumnHandler>();
                case "row": return SetHandler<Handles.Object.IndexRowHandler>();
                case "objdict": return SetHandler<Handles.Object.SysObjDictHandler>();
                case "objlist": return SetHandler<Handles.Object.SysObjListHandler>();
                case "version": return SetHandler<Handles.Object.ObjVersionHandler>();

                #endregion

                #region  FileStore(文件存储) 
                case "format": return SetHandler<Handles.FileStore.FileFormatHandler>();
                case "store": return SetHandler<Handles.FileStore.FileStoreHandler>();
                case "file": return SetHandler<Handles.FileStore.StoreFileHandler>();
                case "node": return SetHandler<Handles.FileStore.StoreNodeHandler>();

                #endregion

                #region 菜单
                case "flowinfo": return SetHandler<Handles.Flow.KdtWfInfoHandler>();
                case "menuauthlist": return SetHandler<Handles.Menu.MenuAuthHandler>();
                case "menutoplist": return SetHandler<Handles.Menu.MenuTopHandler>();
                case "menulist": return SetHandler<Handles.Menu.MenuListHandler>();
                case "tpweb": return SetHandler<Handles.Template.KdtTpWebHandler>();
                #endregion

                #region 内容录入
                case "staticcheck": return SetHandler<Handles.Index.StaticCheckTotalHandler>();
                case "staticexceldata": return SetHandler<Handles.Index.StaticExcelDataHandler>();
                case "scoreexpert": return SetHandler<Handles.Index.ScoreExpertHandler>();
                case "scorecheck": return SetHandler<Handles.Index.ScoreCheckHandler>();
                case "staticoperate": return SetHandler<Handles.Index.StaticOperateHandler>();
                case "scoreyear": return SetHandler<Handles.Index.ScoreYearHandler>();
                case "statictotalscore": return SetHandler<Handles.Index.StaticTotalScoreHandler>();
                #endregion

                default: throw new NotExist(handler);
            }
        }

        /// <summary>
        /// 查询实体类返回值
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        protected virtual string SelectEntity(string handler, string method)
        {
            // 执行操作
            using (var helper = CreateHelper())
            {
                switch (handler.Trim().ToLower())
                {  
                    #region 用户管理

                    case "user":
                        return JsonEntityInvork<Handles.User.UserQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.User.UserQuery>(SetHandler<Handles.User.UserHandler>(), method);
                        });
                    case "userdict":
                        return JsonEntityInvork<Handles.User.UserDictQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.User.UserDictQuery>(SetHandler<Handles.User.UserDictHandler>(), method);
                        });

                    #endregion

                    #region  Object(数据对象) 
                    case "column":
                        return JsonEntityInvork<Handles.Object.IndexColumnQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.Object.IndexColumnQuery>(SetHandler<Handles.Object.IndexColumnHandler>(), method);
                        });
                    case "row":
                        return JsonEntityInvork<Handles.Object.IndexRowQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.Object.IndexRowQuery>(SetHandler<Handles.Object.IndexRowHandler>(), method);
                        });
                    case "objdict":
                        return JsonEntityInvork<Handles.Object.SysObjDictQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.Object.SysObjDictQuery>(SetHandler<Handles.Object.SysObjDictHandler>(), method);
                        });
                    case "objlist":
                        return JsonEntityInvork<Handles.Object.SysObjListQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.Object.SysObjListQuery>(SetHandler<Handles.Object.SysObjListHandler>(), method);
                        });
                    case "version":
                        return JsonEntityInvork<Handles.Object.ObjVersionQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.Object.ObjVersionQuery>(SetHandler<Handles.Object.ObjVersionHandler>(), method);
                        });
                    #endregion

                    #region  FileStore(文件存储) 
                    case "format":
                        return JsonEntityInvork<Handles.FileStore.FileFormatQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.FileStore.FileFormatQuery>(SetHandler<Handles.FileStore.FileFormatHandler>(), method);
                        });

                    #endregion

                    #region 模版
                    case "tpweb":
                        return JsonEntityInvork<Handles.Template.KdtTpWebQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.Template.KdtTpWebQuery>(SetHandler<Handles.Template.KdtTpWebHandler>(), method);
                        });
                    #endregion

                    #region Static
                    case "staticexceldata":
                        return JsonEntityInvork<Handles.Index.StaticExcelDataQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.Index.StaticExcelDataQuery>(SetHandler<Handles.Index.StaticExcelDataHandler>(), method);
                        });
                    case "scoreexpert":
                        return JsonEntityInvork<Handles.Index.ScoreExpertQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.Index.ScoreExpertQuery>(SetHandler<Handles.Index.ScoreExpertHandler>(), method);
                        });
                    case "scorecheck":
                        return JsonEntityInvork<Handles.Index.ScoreCheckQuery>(() =>
                        {
                            return helper.SelectEntity<Handles.Index.ScoreCheckQuery>(SetHandler<Handles.Index.ScoreCheckHandler>(), method);
                        });
                    #endregion

                    default: throw new NotExist(handler);
                }
            }
        }

        /// <summary>
        /// 查询集合类数据
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        protected virtual string SelectList(string handler, string method)
        {
            // 执行操作
            using (var helper = CreateHelper())
            {
                switch (handler.Trim().ToLower())
                {
                    #region  用户
                    case "user":
                        return JsonEntityInvork<List<Handles.User.UserQuery>>(() =>
                        {
                            return helper.SelectList<Handles.User.UserQuery>(SetHandler<Handles.User.UserHandler>(), method);
                        });
                    case "userdict":
                        return JsonEntityInvork<List<Handles.User.UserDictQuery>>(() =>
                        {
                            return helper.SelectList<Handles.User.UserDictQuery>(SetHandler<Handles.User.UserDictHandler>(), method);
                        });
                    case "ruser":
                        return JsonEntityInvork<List<Handles.User.RoleUserQuery>>(() =>
                        {
                            return helper.SelectList<Handles.User.RoleUserQuery>(SetHandler<Handles.User.RoleUserHandler>(), method);
                        });

                    #endregion

                    #region  角色
                    case "role":
                        return JsonEntityInvork<List<Handles.User.RoleQuery>>(() =>
                        {
                            return helper.SelectList<Handles.User.RoleQuery>(SetHandler<Handles.User.RoleHandler>(), method);
                        });
                    #endregion

                    #region 菜单
                    case "menuauthlist":
                        return JsonEntityInvork<List<Handles.Menu.MenuAuthQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Menu.MenuAuthQuery>(SetHandler<Handles.Menu.MenuAuthHandler>(), method);
                        });
                    case "menutoplist":
                        return JsonEntityInvork<List<Handles.Menu.MenuTopQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Menu.MenuTopQuery>(SetHandler<Handles.Menu.MenuTopHandler>(), method);
                        });
                    #endregion

                    #region 工作流
                    case "flowinstance":
                        return JsonEntityInvork<List<Handles.Flow.KdtWfQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Flow.KdtWfQuery>(SetHandler<Handles.Flow.KdtWfInstanceHandler>(), method);
                        });
                    #endregion

                    #region 模版
                    case "tptask":
                        return JsonEntityInvork<List<Handles.Template.TpTaskQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Template.TpTaskQuery>(SetHandler<Handles.Template.TpTaskHandler>(), method);
                        });
                    case "tpweb":
                        return JsonEntityInvork<List<Handles.Template.KdtTpWebQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Template.KdtTpWebQuery>(SetHandler<Handles.Template.KdtTpWebHandler>(), method);
                        });
                    case "tpscript":
                        return JsonEntityInvork<List<Handles.Template.KdtTpScriptQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Template.KdtTpScriptQuery>(SetHandler<Handles.Template.KdtTpScriptHandler>(), method);
                        });
                    #endregion

                    #region  Object(数据对象) 

                    case "column":
                        return JsonEntityInvork<List<Handles.Object.IndexColumnQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Object.IndexColumnQuery>(SetHandler<Handles.Object.IndexColumnHandler>(), method);
                        });
                    case "row":
                        return JsonEntityInvork<List<Handles.Object.IndexRowQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Object.IndexRowQuery>(SetHandler<Handles.Object.IndexRowHandler>(), method);
                        });
                    case "objdict":
                        return JsonEntityInvork<List<Handles.Object.SysObjDictQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Object.SysObjDictQuery>(SetHandler<Handles.Object.SysObjDictHandler>(), method);
                        });
                    case "objlist":
                        return JsonEntityInvork<List<Handles.Object.SysObjListQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Object.SysObjListQuery>(SetHandler<Handles.Object.SysObjListHandler>(), method);
                        });
                    case "version":
                        return JsonEntityInvork<List<Handles.Object.ObjVersionQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Object.ObjVersionQuery>(SetHandler<Handles.Object.ObjVersionHandler>(), method);
                        });
                    #endregion

                    #region 指标
                    case "staticinfo":
                        return JsonEntityInvork<List<Handles.Index.StaticInfoQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Index.StaticInfoQuery>(SetHandler<Handles.Index.StaticInfoHandler>(), method);
                        });
                    case "staticindex":
                        return JsonEntityInvork<List<Handles.Index.StaticIndexQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Index.StaticIndexQuery>(SetHandler<Handles.Index.StaticIndexHandler>(), method);
                        });
                    case "staticarea":
                        return JsonEntityInvork<List<Handles.Index.StaticAreaQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Index.StaticAreaQuery>(SetHandler<Handles.Index.StaticAreaHandler>(), method);
                        });
                    case "scoreexpert":
                        return JsonEntityInvork<List<Handles.Index.ScoreExpertQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Index.ScoreExpertQuery>(SetHandler<Handles.Index.ScoreExpertHandler>(), method);
                        });
                    case "staticexcel":
                        return JsonEntityInvork<List<Handles.Index.StaticExcelDataQuery>>(() =>
                        {
                            return helper.SelectList<Handles.Index.StaticExcelDataQuery>(SetHandler<Handles.Index.StaticExcelDataHandler>(), method);
                        });
                    #endregion

                    default: throw new NotExist(handler);
                }
            }
        }

        /// <summary>
        /// 执行分页查询类信息
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="_currentpage"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        protected virtual string SelectPage(string handler, int _currentpage, int pagesize)
        {
            KdtPageEx page = new KdtPageEx(_currentpage, pagesize);
            // 执行操作
            using (var helper = CreateHelper())
            {
                switch (handler.Trim().ToLower())
                {
                    case "user":
                        return JsonGridInvork<Handles.User.UserEntity>((out int total) =>
                        {
                            var list = helper.SelectPage<Handles.User.UserEntity>(SetHandler<Handles.User.UserHandler>(), page);
                            total = page.total;
                            return list;
                        });
                    case "format":
                        return JsonGridInvork<Handles.FileStore.FileFormatQuery>((out int total) =>
                        {
                            var list = helper.SelectPage<Handles.FileStore.FileFormatQuery>(SetHandler<Handles.FileStore.FileFormatHandler>(), page);
                            total = page.total;
                            return list;
                        });
                    case "version":
                        return JsonGridInvork<Handles.Object.ObjVersionQuery>((out int total) =>
                        {
                            var list = helper.SelectPage<Handles.Object.ObjVersionQuery>(SetHandler<Handles.Object.ObjVersionHandler>(), page);
                            total = page.total;
                            return list;
                        });
                    case "ruser":
                        return JsonGridInvork<Handles.User.RoleUserEntity>((out int total) =>
                        {
                            var list = helper.SelectPage<Handles.User.RoleUserEntity>(SetHandler<Handles.User.RoleUserHandler>(), page);
                            total = page.total;
                            return list;
                        });

                    default: throw new NotExist(handler);
                }
            }
        }

#endregion.

        #region Json 返回信息处理

                /// <summary>
                /// 代理带返回值参数回调
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="total"></param>
                /// <returns></returns>
                protected delegate List<T> QueryPagerData<T>(out int total);

                /// <summary>
                /// 直接返回执行JSON信息
                /// </summary>
                /// <param name="_func"></param>
                /// <returns></returns>
                protected virtual string JsonDirectInvork(Func<string> _func)
                {
                    return ReturnHook.Hook(() =>
                    {
                        if (_func == null) throw new NullCallback();

                        return _func();
                    });
                }

                /// <summary>
                /// 基础类JSON返回信息
                /// </summary>
                protected virtual string JsonTextInvork(Func<string> _func)
                {
                    return ReturnHook.Hook(delegate ()
                    {
                        if (_func == null) throw new NullCallback();

                        return new Text(true, _func()).ToJson();
                    });
                }

                /// <summary>
                /// 将目标集合转换成COMBO格式JSON字符串
                /// </summary>
                protected virtual string JsonComboInvork<T, TKey, TVal>(IEnumerable<T> _array, Func<T, ComboItem<TKey, TVal>> _func)
                    where T : class
                {
                    return ReturnHook.Hook(() =>
                    {
                        if (_array == null || _array.Count() <= 0) throw new NullParam();
                        if (_func == null) throw new NullCallback();

                        Combo<TKey, TVal> combo = new Combo<TKey, TVal>();
                        foreach (var item in _array)
                        {
                            ComboItem<TKey, TVal> result = _func(item);
                            combo.Add(result);
                        }

                        return combo.ToJson();
                    });
                }

                /// <summary>
                /// 将目标实体类转换成JSON字符串
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="_func"></param>
                /// <returns></returns>
                protected virtual string JsonEntityInvork<T>(Func<T> _func) where T : class, new()
                {
                    return ReturnHook.Hook(() =>
                    {
                        if (_func == null) throw new NullCallback();

                        Entity<T> entity = new Entity<T>(_func());
                        return entity.ToString();
                    });
                }

                /// <summary>
                /// 将目标集合转换成grid格式JSON字符串
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <param name="_func"></param>
                /// <returns></returns>
                protected virtual string JsonGridInvork<T>(QueryPagerData<T> _func) where T : class, new()
                {
                    return ReturnHook.Hook(delegate ()
                    {
                        if (_func == null) throw new NullCallback();

                        Grid<T> grid = new Grid<T>();
                        int total;
                        grid.AddRange(_func(out total));
                        grid.total = total;
                        return grid.ToString();
                    });
                }

        #endregion.

        #region 受保护私有方法

        /// <summary>
        /// 设置Handler信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ischange">字段是否装换</param>
        /// <returns></returns>
        protected virtual T SetHandler<T>( Boolean ischange = false) where T : class, new()
        {
            T val = typeof(T).Create<T>();
            // 获取Handler属性
            PropertyInfo[] properties = typeof(T).GetProperties();
            if (properties != null && properties.Length >= 0)
            {
                // 读取request内容，进行赋值
                foreach (PropertyInfo prop in properties)
                {
                    string p_name = prop.Name;
                    if (Request.Query.Keys.Count <= 0)
                    {
                        if (Request.Form.ContainsKey(p_name.ToLower()))
                        {
                            object pvalue = prop.GetValue(val, null);
                            if (pvalue is KdtFeildEx) // 自定义赋值
                            {
                                Type _filed = typeof(KdtFeildEx);
                                PropertyInfo _filedprop = _filed.GetProperty("FeildValue");

                                if(GetFeildName(p_name))
                                   _filedprop.SetValue(pvalue, Request.Form[p_name].ToString().ToMD5_16(), null); //给对应属性赋值
                                else
                                   _filedprop.SetValue(pvalue, Request.Form[p_name].ToString(), null); //给对应属性赋值
                            }
                            else
                            {
                                if (GetFeildName(p_name))
                                    prop.SetValue(val, Request.Form[p_name].ToString().ToMD5_16(), null); //给对应属性赋值
                                else
                                    prop.SetValue(val, Request.Form[p_name].ToString(), null); //给对应属性赋值
                            }
                        }
                    }
                    else
                    {
                        if (Request.Query.ContainsKey(p_name.ToLower()))
                        {
                            object pvalue = prop.GetValue(val, null);
                            if (pvalue is KdtFeildEx) // 自定义赋值
                            {
                                Type _filed = typeof(KdtFeildEx);
                                PropertyInfo _filedprop = _filed.GetProperty("FeildValue");
                                if (GetFeildName(p_name))
                                    _filedprop.SetValue(pvalue, Request.Query[p_name].ToString().ToMD5_16(), null); //给对应属性赋值
                                else
                                    _filedprop.SetValue(pvalue, Request.Query[p_name].ToString(), null); //给对应属性赋值
                            }
                            else
                            {
                                if (GetFeildName(p_name))
                                    prop.SetValue(val, Request.Query[p_name].ToString().ToMD5_16(), null); //给对应属性赋值
                                else
                                    prop.SetValue(val, Request.Query[p_name].ToString(), null); //给对应属性赋值
                            }
                        }
                    }
                }
            }
            return val;
        }

        /// <summary>
        /// 创建数据库执行器
        /// </summary>
        /// <param name="_dbconfig"></param>
        /// <returns></returns>
        protected virtual DbExecute CreateHelper(string _dbconfig = "sqlserverdb")
        {
            return new DbExecute(_dbconfig);
        }


        /// <summary>
        /// 特殊字段处理
        /// </summary>
        /// <param name="feild"></param>
        /// <returns></returns>
        private Boolean GetFeildName(string feild)
        {
            switch (feild)
            {
                case "code": return true;
                case "pwd": return true;
                default: return false;

            }
        }

        protected override void Dispose(bool disposing)
        {
            GC.SuppressFinalize(this);
        }
        #endregion.
    }
}