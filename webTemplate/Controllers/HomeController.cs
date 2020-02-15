using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.Home;
using ViewModels.Share;
using WebTemplateDB.Service;

namespace webTemplate.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetHomeDate()
        {
            ResponseViewModel res = new ResponseViewModel();



            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}