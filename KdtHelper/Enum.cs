using System;

namespace KdtHelper
{
    /// <summary>
    /// 数据库关键字
    /// </summary>
    public enum DbDataType
    {
        INT,
        TEXT,
        NTEXT,
        BYTE,
        LONG,
        DOUBLE,
        FLOAT,
        CHAR,
        NCHAR,
        VARCHAR,
        NVARCHAR,
        BIT,
        TINYINT,
        DATETIME
    }

    /// <summary>
    /// 数据库类型函数
    /// </summary>
    public enum DbFunName
    {
        AVG, //求平均值
        COUNT, //统计数目
        MAX, //求最大值
        MIN, //求最小值
        SUM, //求和
        STDEV, //求所有数据的标准差
        STDEVP, //求总体标准差
        VAR, //求所有值的统计变异数
        VARP //求总体变异数
    }

    /// <summary>
    /// 字符串加密或转码枚举
    /// </summary>
    public enum SecurityCrypt
    {
        None,
        MD5,
        MD5_16Byte,
        MD5_32Byte,
        AES,
        DES,
        URI,
        URIComponent
    }

    /// <summary>
    /// 存储过程输入类型
    /// </summary>
    public enum ProcInPutEnum
    {
        InPut,
        OutPut,
        ReturnValue,
        InputOutPut
    }

    /// <summary>
    /// 数据库驱动类型
    /// </summary>
    public enum DriverType
    {
        sqlserver,
        oracle,
        mysql,
        sqlite,
        //sybase,
        db2,
        odbc
    }
}
