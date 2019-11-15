using ajaxSiteC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace ajaxSiteC.Controllers
{
    public class ValuesController : ApiController
    {
        NorthwindEntities db = new NorthwindEntities();
        // GET api/values
        public IEnumerable<Categories> GetCategories()
        {

            return db.Categories;
        }

        // GET api/values/5
        public IEnumerable<Products> GetProductsByCategoryID(int id)
        {
            var q = db.Products.Where(x => x.CategoryID == id);
            return q;
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
