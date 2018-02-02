using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace KdtHelper.Common
{
    public static class KdtUtility
    {
        #region 字符串加密算法集合

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="value">传入的字符串</param>
        /// <param name="format">X为大写，x为小写</param>
        /// <returns></returns>
        public static string ToMd5(this string value, string format = "X2")
        {
            byte[] bytes;
            using (var md5 = MD5.Create())
            {
                bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            }
            var result = new StringBuilder();
            foreach (byte t in bytes)
            {
                result.Append(t.ToString(format));
            }
            return result.ToString();
        }

        /// <summary>
        /// 返回16位MD5加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMD5_16(this string value)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(value)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }


        /// <summary>
        /// AES加密处理
        /// </summary>
        /// <param name="value">传入的字符串</param>
        /// <param name="key">设置AES密码</param>
        /// <returns>加密后字符串</returns>
        public static string ToAES(this string value, string key = "aes12345")
        {
            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 128;
                aes.BlockSize = 128;

                byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key);
                byte[] keyBytes = new byte[16];
                int len = pwdBytes.Length;
                if (len > keyBytes.Length) len = keyBytes.Length;
                System.Array.Copy(pwdBytes, keyBytes, len);
                aes.Key = keyBytes;


                byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(key);
                aes.IV = ivBytes;
                ICryptoTransform transform = aes.CreateEncryptor();

                byte[] plainText = Encoding.UTF8.GetBytes(value);
                byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);
                return System.Convert.ToBase64String(cipherBytes);
            } 
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="value">传入的字符串</param>
        /// <param name="key">AES密码</param>
        /// <returns>解密后字符串</returns>
        public static string DeAES(this string value, string key = "aes12345")
        {
            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.KeySize = 128;
                aes.BlockSize = 128;

                byte[] encryptedData = System.Convert.FromBase64String(value);
                byte[] pwdBytes = System.Text.Encoding.UTF8.GetBytes(key);
                byte[] keyBytes = new byte[16];

                int len = pwdBytes.Length;
                if (len > keyBytes.Length) len = keyBytes.Length;
                System.Array.Copy(pwdBytes, keyBytes, len);
                aes.Key = keyBytes;
                byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes(key);
                aes.IV = ivBytes;

                ICryptoTransform transform = aes.CreateDecryptor();
                byte[] plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                return Encoding.UTF8.GetString(plainText);
            }
        }

        #endregion

        #region 常规字符串处理

        /// <summary>
        /// <summary>
        /// 字符串转Unicode
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unicode编码后的字符串</returns>
        public static string String2Unicode(this string source)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(source);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Unicode转字符串
        /// </summary>
        /// <param name="source">经过Unicode编码的字符串</param>
        /// <returns>正常字符串</returns>
        public static string Unicode2String(this string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                source, x => string.Empty + System.Convert.ToChar(System.Convert.ToUInt16(x.Result("$1"), 16)));
        }

        /// <summary>
        /// 比较两个值
        /// </summary>
        /// <param name="value1">待比较值</param>
        /// <param name="value2">比较值</param>
        /// <returns></returns>
        public static bool CompareTo(this object value1, object value2)
        {
            bool isSystemType1 = Type.GetTypeCode(value1.GetType()) != TypeCode.Object;
            bool isSystemType2 = Type.GetTypeCode(value2.GetType()) != TypeCode.Object;

            bool success = false;
            if (isSystemType1)
            {
                //如果value1是基础类型.value2不是基础类型.但已实现IConvertible接口.则将value2转为value1类型
                if (!isSystemType2 && value2 is IConvertible)
                {
                    value2 = ((IConvertible)value2).ToType(value1.GetType(), null);
                }
                else if (value1.GetType() != value2.GetType() && value2 is IConvertible)
                {
                    value2 = ((IConvertible)value2).ToType(value1.GetType(), null);
                }
            }
            else if (isSystemType2)
            {
                //如果value2是基础类型.value1不是基础类型.但已实现IConvertible接口.则将value1转为value2类型
                if (!isSystemType1 && value1 is IConvertible)
                {
                    value1 = ((IConvertible)value1).ToType(value2.GetType(), null);
                }
                else if (value1.GetType() != value2.GetType() && value1 is IConvertible)
                {
                    value1 = ((IConvertible)value1).ToType(value2.GetType(), null);
                }
            }

            int result = 1;

            if (value1 is IComparable)
            {
                success = true;
                result = ((IComparable)value1).CompareTo(value2);
            }
            else if (value2 is IComparable)
            {
                success = true;
                //value2 与 value1相比较.所以结果为相反
                result = 0 - ((IComparable)value2).CompareTo(value1);
            }
            if (success && result == 0)
                return true;

            return false;
        }

        /// <summary>
        /// 判断字符串是否是空数据(null或empty)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 将对象类型转化成对应类型对象
        /// </summary>
        /// <param name="value">对象值</param>
        /// <param name="type">转化目标类型</param>
        /// <returns></returns>
        public static object ChangeTo(this object value, Type _type)
        {
            if (value == null)
            {
                switch (Type.GetTypeCode(_type))
                {
                    case TypeCode.Boolean: return false;
                    case TypeCode.Byte: return new Byte();
                    case TypeCode.Char: return new char();
                    case TypeCode.DateTime: return DateTime.Now;
                    case TypeCode.Decimal: return 0;
                    case TypeCode.Double: return 0;
                    case TypeCode.Empty: return "";
                    case TypeCode.Int16: return 0;
                    case TypeCode.Int32: return 0;
                    case TypeCode.Int64: return 0;
                    case TypeCode.Object: return new object();
                    case TypeCode.SByte: return new SByte();
                    case TypeCode.Single: return new Single();
                    case TypeCode.String: return "";
                    case TypeCode.UInt16: return 0;
                    case TypeCode.UInt32: return 0;
                    case TypeCode.UInt64: return 0;
                    default: return "";
                }
            }

            if (_type.Name.Contains("Enum"))
            {
                if (value is int)
                    return Enum.ToObject(_type, value);
                else
                    return Enum.Parse(_type, value.ToString());
            }
            else if (Type.GetTypeCode(_type) == TypeCode.Boolean)
            {
                int v;
                if (Int32.TryParse(value.ToString(), out v))
                    value = v;
            }
            else if (Type.GetTypeCode(_type) == TypeCode.DateTime
                && value.ToString().Length == 8)
            {
                value = DateTime.ParseExact(value.ToString(), "yyyyMMdd",
                    new System.Globalization.CultureInfo("Zh-cn"));
            }

            return System.Convert.ChangeType(value, _type);
        }

        /// <summary>
        /// 强制数据类型转换成指定格式
        /// </summary>
        /// <typeparam name="T">指定数据类型</typeparam>
        /// <param name="value">需转换值</param>
        public static T Convert<T>(this object value)
        {
            return value.Convert<T>(default(T));
        }

        /// <summary>
        /// 强制数据类型转换成指定格式
        /// </summary>
        /// <typeparam name="T">指定数据类型</typeparam>
        /// <param name="value">需转换值</param>
        public static T Convert<T>(this object value, T defaultValue)
        {
            if (value == null)
                return defaultValue;

            if (value is string && string.IsNullOrEmpty(value.ToString()))
                return defaultValue;

            if (value is bool && Type.GetTypeCode(typeof(T)) == TypeCode.Int32)
            {
                value = (bool)value ? 1 : 0;
            }
            else if (defaultValue is Enum)
            {
                if (value is int)
                    return (T)Enum.ToObject(typeof(T), value);
                else
                    return (T)Enum.Parse(typeof(T), value.ToString());
            }
            else if (Type.GetTypeCode(typeof(T)) == TypeCode.Boolean)
            {
                if (value.ToString().Length == 1)
                {
                    int v;
                    if (Int32.TryParse(value.ToString(), out v))
                        value = v;
                }
            }
            else if (Type.GetTypeCode(typeof(T)) == TypeCode.DateTime
                && value.ToString().Length == 8)
            {
                value = DateTime.ParseExact(value.ToString(), "yyyyMMdd",
                    new System.Globalization.CultureInfo("Zh-cn"));
            }
            else if (value is string && Type.GetTypeCode(typeof(T)) == TypeCode.Int32)
            {
                string v = value.ToString();
                if (v.Equals("false", StringComparison.OrdinalIgnoreCase) || v.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    value = v.Equals("false", StringComparison.OrdinalIgnoreCase) ? 0 : 1;
                }
            }

            return (T)System.Convert.ChangeType(value, typeof(T));
        }


        /// <summary>
        /// 重写字符串实现format方法，便于快速使用
        /// </summary>
        /// <param name="original">原有字符串值</param>
        /// <param name="values">参数值</param>
        public static string ToFormat(this string original, params object[] args)
        {
            return string.Format(original, args);
        }

        /// <summary>
        /// 将字符串与一定分隔符标识的转换成集合类型
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="split">分隔符</param>
        public static List<string> ToList(this string value, string split)
        {
            return value.Split(new string[] { split }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        /// 将字符串与一定分隔符标识的转换成集合类型
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="split">分隔符</param>
        public static List<T> ToList<T>(this string value, string split)
        {
            List<T> result = new List<T>();

            value.Split(new string[] { split }, StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(v =>
            {
                result.Add(v.Convert(default(T)));
            });

            return result;
        }

        #endregion

        #region 集合

        /// <summary>
        /// 扁平化迭代。将含有多级的集合迭代出成为同一级别信息。（举例：树形节点迭代成一个list)
        /// </summary>
        /// <param name="array">多层级集合</param>
        /// <returns></returns>
        public static IEnumerable Flatten(this IEnumerable array)
        {
            foreach (var item in array)
                if (item is string)
                    yield return item;
                else if (item is IEnumerable)
                    foreach (var subitem in Flatten((IEnumerable)item))
                    {
                        yield return subitem;
                    }
                else
                    yield return item;
        }

        /// <summary>
        /// 循环读取集合类信息并返回所在位置。
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="callback">回调方法</param>
        public static void EachWithIndex<T>(this IEnumerable<T> array, Action<T, int> callback)
            where T : class
        {
            int index = 0;

            foreach (T item in array)
            {
                callback(item, index);
                ++index;
            }
        }

        /// <summary>
        /// 将集合转换成字符串类型
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">集合值</param>
        /// <param name="split">拼接符</param>
        public static string Joint<T>(this IEnumerable<T> value, string split)
        {
            StringBuilder newValue = new StringBuilder();

            value.ToList().ForEach(v =>
            {
                newValue.AppendFormat("{0}{1}", v, split);
            });

            if (newValue.Length > 0)
                newValue = newValue.Replace(split, "", newValue.Length - split.Length, split.Length);

            return newValue.ToString();
        }

        /// <summary>
        /// 将基类集合转换成继承类
        /// </summary>
        public static List<T> ToList<T, V>(this IEnumerable<V> value) where T : class
        {
            List<T> result = new List<T>();

            value.ToList().ForEach(v =>
            {
                result.Add(v as T);
            });

            return result;
        }

        #endregion.

        #region 发射调用处理

        /// <summary>
        /// 发送方法或属性请求，确认方法或属性是否可用。
        /// 确认反射类中是否存在需要的方法或属性。
        /// </summary>
        /// <param name="value">反射类</param>
        /// <param name="member">反射方法或属性名</param>
        /// <param name="ensureNoParameters">确保没有参数</param>
        /// <returns>反射方法中存在对应的方法或属性</returns>
        public static bool Reflect_Respond(this object value, string member, bool ensureNoParameters = true)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            Type type = value.GetType();

            MethodInfo methodInfo = type.GetMethod(member);
            if (methodInfo != null && (!ensureNoParameters || !methodInfo.GetParameters().Any()))
                return true;

            PropertyInfo propertyInfo = type.GetProperty(member);
            if (propertyInfo != null && propertyInfo.CanRead)
                return true;

            return false;
        }

        /// <summary>
        /// 调用反射方法或属性并读取返回信息
        /// </summary>
        /// <param name="value">反射类</param>
        /// <param name="member">反射方法或属性名</param>
        /// <param name="parameters">参数集合</param>
        /// <returns>属性值或方法返回值</returns>
        public static object Reflect_Call(this object value, string member, object[] parameters = null)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            Type type = value.GetType();

            MethodInfo methodInfo = type.GetMethod(member);
            if (methodInfo != null)
                return methodInfo.Invoke(value, parameters);

            PropertyInfo propertyInfo = type.GetProperty(member);
            if (propertyInfo != null)
                return propertyInfo.GetValue(value, null);

            return null;
        }

        /// <summary>
        /// 反射获取对象属性值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象值</param>
        /// <param name="propertyname">属性名称</param>
        /// <returns>属性值</returns>
        public static object PropertyVal<T>(this T t, string propertyname) where T : class
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return null;

            return property.GetValue(t, null);
        }

        /// <summary>
        /// 反射设置对象属性值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象值</param>
        /// <param name="propertyname">属性名称</param>
        /// <param name="propertyvalue">属性值</param>
        public static void PropertyVal<T>(this T t, string propertyname, object propertyvalue) where T : class
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return;

            property.SetValue(t, propertyvalue, null);
        }

        /// <summary>
        /// 反射创建实体类
        /// </summary>
        public static T Create<T>(this Type t) where T : class, new()
        {
            return (T)Activator.CreateInstance(t);
        }

        #endregion.

        #region JSON字符串处理

        /// <summary>
        /// 将JSON字符串转换成实体类
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="jsonstr">JSON字符串</param>
        public static T ToEntity<T>(this string jsonstr, bool _transtime = true) where T : class, new()
        {
            if (string.IsNullOrEmpty(jsonstr)) return default(T);

            if (_transtime)
            {
                string p = @"\d{4}-\d{1,2}-\d{1,2}\s\d{1,2}:\d{1,2}:\d{1,2}";
                MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
                Regex reg = new Regex(p);

                jsonstr = reg.Replace(jsonstr, matchEvaluator);
            }
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonstr)))
            {
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }
        }

        /// <summary>
        /// 将实体类转换成JSON字符串
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体类</param>
        public static string ToJson<T>(this T entity, bool _transtime = true) where T : class
        {
            if (entity == null) return "";

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(entity.GetType());

            using (MemoryStream ms = new MemoryStream())
            {

                serializer.WriteObject(ms, entity);

                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();

                if (_transtime)
                {
                    //替换Json的Date字符串    
                    string p = @"\\/Date\(-{0,1}(\d+)\+\d+\)\\/";
                    MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
                    Regex reg = new Regex(p);
                    jsonString = reg.Replace(jsonString, matchEvaluator);
                }

                return jsonString;
            }
        }

        /// <summary>
        /// 将JSON字符串反转义
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJsonString(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }
            string v = value.Replace("[\"{", "[{").Replace("}\"]", "}]").Replace("\\\"", "\"").Replace("\"Result\":\"{", "\"Result\":{").Replace("}\"}", "}}").Replace("}\"", "}").Replace("\"{", "{");
            return v;
        }



        /// <summary>    
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串    
        /// </summary>    
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }
        /// <summary>    
        /// 将时间字符串转为Json时间    
        /// </summary>    
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }

        #endregion.
    }
}
