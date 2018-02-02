using KdtHelper;
using KdtHelper.Core.Adapter;
using KdtHelper.Core.ExecuterEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KdtHelper.Common;
using KdtHelper.Core;

namespace CTP.API.Util
{
    /// <summary>
    /// 连接数据库执行器
    /// </summary>
    public class DbExecute : KdtExecuterEx
    {
        #region 构造函数

        private string _DBCONFIG { get; set; }
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="_dbconfig"></param>
        /// <param name="_timeout"></param>
        public DbExecute(string _dbconfig, int _timeout = 30)
        {
            base.TimeOut = _timeout;
            this._DBCONFIG = _dbconfig;
        }

        private DriverType _DRIVERTYPE { get; set; }
        private string _DBSERVER { get; set; }
        /// <summary>
        /// 构造函数，初始化数据库连接
        /// </summary>
        /// <param name="_drivertype"></param>
        /// <param name="_server"></param>
        /// <param name="_timeout"></param>
        public DbExecute(string _drivertype, string _server, int _timeout = 30)
        {
            _DRIVERTYPE = _drivertype.Convert<DriverType>(DriverType.sqlserver);
            base.TimeOut = _timeout;
            this._DBSERVER = _server;
        }

        #endregion.

        /// <summary>
        /// 驱动器
        /// </summary>
        protected override DbDriverMember Driver
        {
            get
            {
                if (!_DBCONFIG.IsNullOrEmpty())
                    return base.CreateDriver(_DBCONFIG);
                else
                    return base.CreateDriver(_DRIVERTYPE, _DBSERVER);
            }
        }

        /// <summary>
        /// 执行类型
        /// </summary>
        protected override System.Data.CommandType CmdType
        {
            get { return System.Data.CommandType.Text; }
        }

        /// <summary>
        /// SQL语句适配器
        /// </summary>
        public AdapterSql AdapterSql
        {
            get
            {
                return new AdapterSql(this.Driver);
            }
        }
    }
}
