using KdtHelper.Common;
using KdtHelper.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTP.API.Util
{
    /// <summary>
    /// 配合ERRORCODE类，定义异常机制
    /// </summary>
    public static class ReturnHook
    {
        /// <summary>
        /// 错误码设定类型
        /// </summary>
        /// <param name="_fun"></param>
        /// <returns></returns>
        public static string Hook(Func<string> _fun)
        {
            try
            {
                return _fun();
            }
            #region 代码级异常错误(3000开头）
            catch (NullCallback ncb) { return new Text(false, ncb.Message.String2Unicode(), 3000).ToJson(); } // 回调函数为空异常
            catch (NullParam np) { return new Text(false, np.Message.String2Unicode(), 3001).ToJson(); } // 传入参数为空
            #endregion.

            #region 页面级异常处理(4000开头）

            #endregion.

            #region 系统运行级异常处理(5000开头）
            catch (OffLine of) { return new Text(false, of.Message.String2Unicode(), 5000).ToJson(); } //用户离线异常异常
            catch (NotExist ne) { return new Text(false, ne.Message.String2Unicode(), 5001).ToJson(); } //不存在的内容
            catch (DbExecuteException deex) { return new Text(false, deex.Message.String2Unicode(), 5002).ToJson(); } //数据库添加、更新、删除操作失败
            #endregion.
            catch (ArgumentNullException ane) { return new Text(false, ane.Message.String2Unicode(), 500).ToJson(); }
            catch (Exception ex) { return new Text(false, ex.Message.String2Unicode(), 501).ToJson(); } // 运行未知异常
        }
    }

    public class CommonObject
    {
        public string id_leaf { get; set; }
        public string tablename { get; set; }
        public string values { get; set; }
        public string creator { get; set; }
        public string handler { get; set; }
        public string other { get; set; }
        public string uid { get; set; }
        public string optype { get; set; }

        public string binfo { get; set; }

        public string work { get; set; }

        public string service { get; set; }

        public string inst { get; set; }

        public string value { get; set; }

    }
}
