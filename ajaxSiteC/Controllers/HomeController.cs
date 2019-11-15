using ajaxSiteC.Models;
using ajaxSiteC.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ajaxSiteC.Controllers
{

    public class HomeController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var q = db.Categories.Select(x => new
            {
                x.CategoryID,
                x.CategoryName,
            });
            SelectList selectLists = new SelectList(q, "CategoryID", "CategoryName");
            ViewBag.drop = selectLists;
            return View();
        }
        public ActionResult test()
        {
            var q = db.Categories.Select(x => new
            {
                x.CategoryID,
                x.CategoryName
            });
            return Json(q, JsonRequestBehavior.AllowGet);
        }
        public ActionResult products()
        {
            return View();
        }
        //即時性
        public void Events()
        {
            Response.ContentType = "text/event-stream";
            Response.Write(string.Format("data:{0}\n\n", DateTime.Now.ToString()));
            Response.Write(string.Format("retry:{0}\n", 1000));
        }
        //作業練習
        public ActionResult listDisplay()
        {
            var q = db.Categories.Select(x => new
            {
                x.CategoryID,
                x.CategoryName,
            });
            SelectList selectLists = new SelectList(q, "CategoryID", "CategoryName");
            return Json(selectLists, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProducts(int categoryid)
        {
            var products = db.Products.Where(x => x.CategoryID == categoryid).Select(x =>
            new
            {
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                QuantityPerUnit = x.QuantityPerUnit,
                UnitsInStock = x.UnitsInStock
            });
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /////YouBike作業 
        /// </summary>
        /// <returns></returns>
        public ActionResult YouBike()
        {
            return View();
        }
        public void GetYouBikeData()
        {
            ///////////來源資料網址
            //https://tcgbusfs.blob.core.windows.net/blobyoubike/YouBikeTP.json
            //https://data.taipei/#/dataset/detail?id=8ef1626a-892a-4218-8344-f7ac46e1aa48
            /////////////////////////////////////////////////////////////////////////////////////
            string urlstr = "https://tcgbusfs.blob.core.windows.net/blobyoubike/YouBikeTP.json";
            HttpClient client = new HttpClient();
            string response = client.GetStringAsync(urlstr).Result;
            YouBikeOrigin youBikes = JsonConvert.DeserializeObject<YouBikeOrigin>(response);
            var stations = youBikes.retVal.PropertyValues()
                                       .Where(item => item.HasValues)
                                       .Select
                                       (
                                           item => JsonConvert.DeserializeObject<YouBike>
                                           (
                                               item.ToString()
                                           )
                                       )
                                       .ToList();
            var json = JsonConvert.SerializeObject(stations);
            Response.ContentType = "text/event-stream";
            Response.Write(string.Format("data:{0}\n\n", json));
            Response.Write(string.Format("retry:{0}\n", 60000));
        }
        //=======================================================
        public ActionResult Shippers()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:63384");
            HttpResponseMessage message = client.GetAsync("api/Shippers/").Result;
            IEnumerable<Shippers> shippers = null;
            if (message.IsSuccessStatusCode)
            {
                shippers = message.Content.ReadAsAsync<IEnumerable<Shippers>>().Result;
            }
            return View(shippers);
        }
    }
}

