using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;

namespace KdtHelper.Common.Json
{
    public class Table : System.Data.DataTable
    {
        public Table(string json)
        {
            var table = JsonToDataTable(json);
            foreach (DataColumn item in table.Columns)
            {
                this.Columns.Add(item.ColumnName, item.DataType);
            }
            foreach (DataRow item in table.Rows)
            {
                this.Rows.Add(item);
            }
        }

        public Table() { this.TableName = "data"; }

        public void AddColumns(params string[] columns)
        {
            columns.ToList().ForEach(c =>
            {
                this.Columns.Add(c);
            });
        }

        public void AddRow<T>(T entity) where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties();

            var row = this.NewRow();

            properties.ToList().ForEach(property =>
            {
                if (this.Columns.Contains(property.Name))
                {
                    row[property.Name] = property.GetValue(entity, null);
                }
            });

            this.Rows.Add(row);
        }

        /// <summary>
        /// 根据Json返回DateTable,JSON数据格式如:
        /// {table:[{column1:1,column2:2,column3:3},{column1:1,column2:2,column3:3}]}
        /// </summary>
        /// <param name="strJson">Json字符串</param>
        /// <returns></returns>
        private DataTable JsonToDataTable(string strJson)
        {
            //取出表名
            Regex rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split(',');

                //创建表
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        DataColumn dc = new DataColumn();
                        string[] strCell = str.Split(':');
                        dc.ColumnName = strCell[0].ToString();
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split(':')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
        }

        /// <summary>
        /// 根据DataTable返回JSON数据格式如:
        /// {table:[{column1:1,column2:2,column3:3},{column1:1,column2:2,column3:3}]}
        /// </summary>
        /// <param name="tb">需要转换的表</param>
        /// <returns></returns>
        private string DateTableToJson(DataTable tb)
        {
            if (tb == null || tb.Rows.Count == 0)
            {
                return "";
            }

            string strName = tb.TableName;
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("{");
            sbJson.Append("\"" + strName + "\":[");
            Hashtable htColumns = new Hashtable();
            for (int i = 0; i < tb.Columns.Count; i++)
            {
                htColumns.Add(i, tb.Columns[i].ColumnName.Trim());
            }

            for (int j = 0; j < tb.Rows.Count; j++)
            {
                if (j != 0)
                {
                    sbJson.Append(",");
                }
                sbJson.Append("{");
                for (int c = 0; c < tb.Columns.Count; c++)
                {
                    sbJson.Append(htColumns[c].ToString() + ":\"" +
                        tb.Rows[j][c].ToString().Replace(",", "，").Replace(":", "：").Replace("\r\n", " ") + "\",");
                }
                sbJson.Append("index:" + j.ToString()); //序号
                sbJson.Append("}");
            }
            sbJson.Append("]}");
            return sbJson.ToString();
        }

        public override string ToString()
        {
            return DateTableToJson(this);
        }
    }
}
