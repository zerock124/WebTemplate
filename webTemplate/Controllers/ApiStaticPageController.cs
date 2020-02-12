using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace webTemplate.Controllers
{
    public class ApiStaticPageController : ApiController
    {
        // GET: api/ApiStaticPage
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ApiStaticPage/5
        public string Get(int id)
        {
            return "value";
        }
    }
}
