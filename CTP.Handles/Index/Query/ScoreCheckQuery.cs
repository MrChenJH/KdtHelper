using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;

namespace CTP.Handles.Index
{
    public class ScoreCheckQuery
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Field(null, "auto_no")]
        public int autono { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Field(null, "user_id")]
        public string uid { get; set; }

        /// <summary>
        /// 年累计开放天数
        /// </summary>
        [Field(null, "sub_year")]
        public int syear { get; set; }

        /// <summary>
        /// 节庆假日开放情况
        /// </summary>
        [Field(null, "sub_holi")]
        public int sholi { get; set; }

        /// <summary>
        /// 延长或夜间开放情况
        /// </summary>
        [Field(null, "nignt")]
        public int nignt { get; set; }

        /// <summary>
        /// 科普专职人员数量
        /// </summary>
        [Field(null, "peo_num")]
        public int pnum { get; set; }

        /// <summary>
        /// 科普讲解比赛成绩
        /// </summary>
        [Field(null, "match_score")]
        public string mscore { get; set; }


        /// <summary>
        ///本年度新增展示面积
        /// </summary>
        [Field(null, "new_area")]
        public int narea { get; set; }

        /// <summary>
        ///本年度更新展品展项
        /// </summary>
        [Field(null, "new_update")]
        public int nupdate { get; set; }

        /// <summary>
        ///年接待参观人次总量
        /// </summary>
        [Field(null, "sub_visit")]
        public int svisit { get; set; }

        /// <summary>
        ///年接待参观人次增长率
        /// </summary>
        [Field(null, "rate_visit")]
        public int rvisit { get; set; }


        /// <summary>
        ///科技节期间参与场馆惠民活动
        /// </summary>
        [Field(null, "huimin_activity")]
        public int hmactiv { get; set; }


        /// <summary>
        ///科技节期间开展特色科普活动
        /// </summary>
        [Field(null, "kepu_activity")]
        public int kpactiv { get; set; }

        /// <summary>
        ///表彰和奖励
        /// </summary>
        [Field(null, "collect_reward")]
        public int colreward { get; set; }

        /// <summary>
        ///参加科普护照
        /// </summary>
        [Field(null, "passport")]
        public int pass { get; set; }

        /// <summary>
        ///2017年市级科普护照使用量
        /// </summary>
        [Field(null, "pass_use")]
        public string upass { get; set; }

        /// <summary>
        ///2017年市级科普护照使用量
        /// </summary>
        [Field(null, "total_score")]
        public string tscore { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        [Field(null, "create_time")]
        public string ctime { get; set; }


        /// <summary>
        ///基地地址
        /// </summary>
        [Field(null, "address")]
        public string address { get; set; }

        /// <summary>
        ///联系人
        /// </summary>
        [Field(null, "linkman")]
        public string linkman { get; set; }

        /// <summary>
        ///区域名称
        /// </summary>
        [Field(null, "aname")]
        public string aname { get; set; }
    }
}
