using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webTemplate.Controllers
{
    public class PermissionController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}