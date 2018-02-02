using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using KdtHelper.Core;
using System.Data;
using KdtHelper.Core.ExecuterEx;
using KdtHelper.Common;
using CTP.Handles;
using CTP.API.Util;

namespace KdtHelper.Test
{
    [TestClass]
    public class Mysql
    {
        [TestMethod]
       public void TestMysqlSignerAdd()
        {
            using (Invoke helper = new Invoke())
            {
                var entity = new MysqlTestHandler();
                entity.Name.FeildValue = GetName();
                ResponseData result = helper.Add(entity);
                int newid = entity.NewId();
                Assert.IsTrue(newid > 0);

            }
        }
        [TestMethod]
        public void GetListByName()
        {
            using (MysqlExecute helper = new MysqlExecute())
            {
                var entity = new MysqlTestHandler();
                entity.Name.FeildValue = "金婧诗";
                List<MySqlTestQuery> test = helper.SelectList<MySqlTestQuery>(entity, "selectByName");
                var aa = 0;
                //helper.Add(entity);
            }
        }

        [TestMethod]
        public void GetAllPage()
        {
            using (MysqlExecute helper = new MysqlExecute())
            {
                int start = 0;
                int size = 5;
                var entity = new MysqlTestHandler();
                KdtPageEx page = new KdtPageEx()
                {
                    selpage = "selectPage",
                    selpagetotal = "selectPageCount",
                    start = start,
                    end = start + size
                };
                var test = helper.SelectPage<MySqlTestQuery>(entity, page);
                var aa = 0;
                //helper.Add(entity);
            }
        }

        [TestMethod]
        public void GetEntity()
        {
            using (MysqlExecute helper = new MysqlExecute())
            {
                var entity = new MysqlTestHandler();
                entity.Id.FeildValue = 1;
                var test = helper.SelectEntity<MySqlTestQuery>(entity, "selectById");
                var aa = 0;
                //helper.Add(entity);
            }
        }
        [TestMethod]
        public void GetField()
        {
            using (MysqlExecute helper = new MysqlExecute())
            {
                var entity = new MysqlTestHandler();
                entity.Id.FeildValue = 1;
                var test = helper.SelectField<string>(entity, "selectNameById");
                //var test = helper.SelectEntity<MySqlTestQuery>(entity, "selectNameById");
                var aa = 0;
                //helper.Add(entity);
            }
        }

        [TestMethod]
        public void UpdateTable()
        {
            using (MysqlExecute helper = new MysqlExecute())
            {
                var entity = new MysqlTestHandler();
                entity.Id.FeildValue = 1;
                entity.Name.FeildValue = "ADMIN";
                helper.Update(entity);
                //var test = helper.SelectEntity<MySqlTestQuery>(entity, "selectNameById");
                var aa = 0;
                //helper.Add(entity);
            }
        }

        [TestMethod]
        public void UpdateOrAddTable()
        {
            using (MysqlExecute helper = new MysqlExecute())
            {
                var entity = new MysqlTestHandler();
                //ID传空值时报错
                entity.Name.FeildValue = "ADMIN1ADMIN";
                helper.AddOrUpdate(entity);
                //var test = helper.SelectEntity<MySqlTestQuery>(entity, "selectNameById");
                var aa = 0;
                //helper.Add(entity);
            }
        }

        #region 私有方法

        private string GetName()
        {
            List<string> xin = "赵;钱;孙;李;周;吴;郑;王;冯;陈;褚;卫;蒋;沈;韩;杨;朱;秦;尤;许;何;吕;施;张;孔;曹;严;华;金;魏;陶;姜;戚;谢;邹;喻;柏;水;窦;章;云;苏;潘;葛;奚;范;彭;郎;鲁;韦;昌;马;苗;凤;花;方".ToList(";");
            List<string> name = "雅冬;莉丽;美雅;旭媛;梅莲;锦珠;桂美;彩锦;帛玉;彩琛;媛雪;婧敏;惠美;静桐;萱帛;曦橘;璐蕾;锦薇;婧诗;洁帆;怡冰  楠曦;蔚彩;璇蔚;蕾雅;洁彦;茹婷;雅彦;寒嘉;柔冰;凡锦;碧菲;玥萱;彩昕;梅帆;雪妍;璟梅;彩采;芝凌;桂林;婧雪;楠彤  晨妍;妍琪;弦琪;橘玉;凌璐;月柔;锦静;美柏;瑶呈;涵初;珠初;锦碧;锦栀;鸿丽;琛彩;蕾惠;锦梅;初萱;婷梦;枫欣;妮彤  梦慧;蕾采;帆雅;萱帆;婷锦;彬美;寒云;雪琳;雯媛;薇彬;锦梦;昕莉;韵雨;倩楠;可莲;冬芸;娜心".ToList(";");
            Random rm = new Random();
            return "{0}{1}".ToFormat(xin[rm.Next(0, xin.Count - 1)], name[rm.Next(0,name.Count-1)]);
        }

        #endregion.
    }

    public class MysqlExecute : KdtHelper.Core.ExecuterEx.KdtExecuterEx
    {
        protected override DbDriverMember Driver
        {
            get
            {
                return base.CreateDriver("mysqldb");
            }
        }

        protected override CommandType CmdType
        {
            get
            {
                return CommandType.Text;
            }
        }
    }


}
