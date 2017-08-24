using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CC.Web.Http;
using HtmlAgilityPack;
using CC.ORM;

namespace BaiDuTB
{
    class Program
    {
        static void Main(string[] args)
        {
            DataToBase(FirstList());
        }
        /// <summary>
        /// 抓取贴吧主页信息
        /// </summary>
        static List<baidutb> FirstList()
        {
            string url = "http://tieba.baidu.com/f?kw=%E7%BD%91%E9%A1%B5&fr=fenter&prequery=%E7%BD%91%E9%A1%B5%E6%8A%93%E5%8F%96";
            //返回主页源码
            HttpResult hp = HttpHelper.DefaultHttp.Get(url);  
            while (hp.StatusCode!=System.Net.HttpStatusCode.OK)
            {
                System.Threading.Thread.Sleep(1000);
                hp = HttpHelper.DefaultHttp.Get(url);
            }
            HtmlDocument doc=new HtmlDocument ();
            doc.LoadHtml(hp.Html);

            //缓存列表
            List<baidutb> list = new List<baidutb>();
            baidutb bd=new baidutb();
            //XPATH路径解析
            string pageTitle = doc.DocumentNode.SelectSingleNode("//title").InnerHtml;
            DateTime lastUp = DateTime.Now ;
            bd.name=pageTitle;
            list.Add(bd);
            return list;
        }

        /// <summary>
        /// 数据库录入
        /// </summary>
        /// <param name="list">缓存列表</param>
        static void DataToBase(List<baidutb> list)
        {
            MysqlFactory.Instance.DefaultConnStr = MysqlFactory.GetConnStr("localhost", "root", "456123", "fortest");
            ORMHelper.DefaultDataFactory = MysqlFactory.Instance;
            DataControl dc = ORMHelper.DefaultDataFactory.Create();
            dc.BeginTransaction();
            foreach (var item in list)
            {
                ORMHelper.InsertOrUpdate(item, dc);
            }
            dc.Commit();
            dc.Close();
        }
    }
}
