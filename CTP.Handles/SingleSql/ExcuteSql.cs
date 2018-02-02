using System;
using System.Collections.Generic;
using System.Text;

namespace CTP.Handles
{
    public class ExcuteSql
    {
        private Dictionary<string, string> _sqlDic=null;
        public ExcuteSql()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            dic.Add("GetUserByName", "select a.*,b.user_type,b.userprop1,b.userprop2,b.userprop3,c.area_name from kdt_user a " +
                                            " LEFT JOIN kdt_user_extend b on a.user_id = b.user_id " +
                                            " left join static_area c on a.user_classify = c.area_type " +
                                            " where a.user_id = '{0}'");
            dic.Add("GetUserTotalInfo", "select a.user_classify,b.user_type,b.*,c.area_name,d.role_key,e.role_nick,f.status,f.is_frozen,g.user_position from kdt_user a " +
                                        " LEFT JOIN kdt_user_extend b on a.user_id = b.user_id "+
                                        " left join static_area c on a.user_classify = c.area_type "+
                                        " left join kdt_role_user d on a.user_id = d.user_id "+
                                        " left join kdt_role e on d.role_key = e.auto_no "+
                                        " left join static_check_total f on a.user_id = f.user_id "+
                                        " left join kdt_role_user g on a.user_id = g.user_id "+
                                        " where a.user_id = '{0}' order by a.auto_no");
            dic.Add("GetLoginUserInfo", "select a.user_classify,b.user_type,b.*,c.area_name,d.role_key,e.role_nick,f.status,f.is_frozen,g.user_position from kdt_user a " +
                                        " LEFT JOIN kdt_user_extend b on a.user_id = b.user_id " +
                                        " left join static_area c on a.user_classify = c.area_type " +
                                        " left join kdt_role_user d on a.user_id = d.user_id " +
                                        " left join kdt_role e on d.role_key = e.auto_no " +
                                        " left join static_check_total f on a.user_id = f.user_id " +
                                        " left join kdt_role_user g on a.user_id = g.user_id " +
                                        " where a.user_id = '{0}' and a.user_pwd='{1}' order by a.auto_no");
            dic.Add("GetPassportOrder", "select t.user_id,c.orderby,c.value from kdt_user t " +
                                        " left join " +
                                        " (select ROW_NUMBER() OVER(ORDER BY b.value desc) AS orderby, b.* from( " +
                                        " select id_leaf, sum(cast(a.c_value as int)) value from " +
                                        " (select * from[eb54337fb2589139] where auto_no in  " +
                                        " (select(auto_no + 1) as auto_no from [eb54337fb2589139] where c_value = '2016市级' or c_value = '2017市级')) a " +
                                        " group by a.id_leaf)b)c " +
                                        " on t.user_id = c.id_leaf where t.user_id = '{0}' order by c.value desc ");

            dic.Add("GetCheckScoreExcel", "select e.*, (cast(additional_score as numeric(8,1)) + basis + expert_avg) as total , (cast(additional_score as numeric(8,1)) + basis + expert_avg) as all_total from " +
                                       "(select d.*, cast(round(cast(basis_score as numeric(8, 1)) / bate, 2) as numeric(38, 2)) as basis  "+
                                        "from(select a.*, k.area_name, f.user_classify, h.expert1, h.expert2, h.expert3, h.expert4, h.expert5,h.expert6,h.expert7, b.user_type, " +
                                        "CASE b.user_type WHEN 0 THEN 0.7 ELSE 1 END 'bate', CASE WHEN c.expert_avg is null THEN 0 ELSE c.expert_avg END 'expert_avg' from static_total_score a left " +
                                        "join kdt_user_extend b on a.user_id = b.user_id  left " +
                                        "join kdt_user f on a.user_id = f.user_id  left " +
                                        "join static_area k on f.user_classify = k.area_type  left " +
                                        " join(" +
                                        "SELECT[user_id], SUM(CASE expert WHEN '施强华' THEN total ELSE 0 END) AS 'expert1', " +
                                        "SUM(CASE expert WHEN '王建平' THEN total ELSE 0 END) AS 'expert2', " +
                                        "SUM(CASE expert WHEN '杨平' THEN total ELSE 0 END) AS 'expert3', " +
                                        "SUM(CASE expert WHEN '章文峻' THEN total ELSE 0 END) AS 'expert4', " +
                                        "SUM(CASE expert WHEN '吴瑞虎' THEN total ELSE 0 END) AS 'expert5', " +
                                        "SUM(CASE expert WHEN '刘文斌' THEN total ELSE 0 END) AS 'expert6', " +
                                        "SUM(CASE expert WHEN '彭芳' THEN total ELSE 0 END) AS 'expert7' " +
                                        "FROM static_expert_score  GROUP BY user_id) h on a.user_id = h.user_id left join " +
                                        "(select user_id, cast(round(avg(cast(total as numeric(8, 1))), 1) as numeric(8, 1)) " +
                                        "as expert_avg from static_expert_score group by user_id) c on a.user_id = c.user_id) d) e where basis != 0 ");

            dic.Add("GetToTalScoreExcel", "select e.*, (cast(additional_score as numeric(8,1)) + basis + expert_avg) as total , (cast(additional_score as numeric(8,1)) + basis + expert_avg) as all_total from " +
                                       "(select d.*, cast(round(cast(basis_score as numeric(8, 1)) / bate, 2) as numeric(38, 2)) as basis  " +
                                        "from(select a.*, k.area_name, f.user_classify, h.expert1, h.expert2, h.expert3, h.expert4, h.expert5,h.expert6,h.expert7, b.user_type, " +
                                        "CASE b.user_type WHEN 0 THEN 0.7 ELSE 1 END 'bate', CASE WHEN c.expert_avg is null THEN 0 ELSE c.expert_avg END 'expert_avg' from static_total_score a left " +
                                        "join kdt_user_extend b on a.user_id = b.user_id  left " +
                                        "join kdt_user f on a.user_id = f.user_id  left " +
                                        "join static_area k on f.user_classify = k.area_type  left " +
                                        " join(" +
                                        "SELECT[user_id], SUM(CASE expert WHEN '施强华' THEN total ELSE 0 END) AS 'expert1', " +
                                        "SUM(CASE expert WHEN '王建平' THEN total ELSE 0 END) AS 'expert2', " +
                                        "SUM(CASE expert WHEN '杨平' THEN total ELSE 0 END) AS 'expert3', " +
                                        "SUM(CASE expert WHEN '章文峻' THEN total ELSE 0 END) AS 'expert4', " +
                                        "SUM(CASE expert WHEN '吴瑞虎' THEN total ELSE 0 END) AS 'expert5', " +
                                        "SUM(CASE expert WHEN '刘文斌' THEN total ELSE 0 END) AS 'expert6', " +
                                        "SUM(CASE expert WHEN '彭芳' THEN total ELSE 0 END) AS 'expert7' " +
                                        "FROM static_expert_score  GROUP BY user_id) h on a.user_id = h.user_id left join " +
                                        "(select user_id, cast(round(avg(cast(total as numeric(8, 1))), 1) as numeric(8, 1)) " +
                                        "as expert_avg from static_expert_score group by user_id) c on a.user_id = c.user_id) d) e ");


            dic.Add("GetYearScoreExcel", "select a.*,c.user_type as btype,c.userprop1 as address,c.userprop2 as linkman,d.area_name aname from base_score_year a left join kdt_user b on a.user_id = b.user_id " +
                                       " left join kdt_user_extend c on  b.user_id = c.user_id left join static_area d on b.user_classify = d.area_type order by create_time desc ");
            _sqlDic =  dic;
        }
        public string GetDictByKey(string key)
        {
            if (_sqlDic != null)
            {
                return _sqlDic[key].ToString();
            }
            else
            {
                throw new Exception("未找到Key值");
            }
        }
    }
}
