using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.Home;
using WebTemplateDB.Service;

namespace webTemplate.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private MenuUIService _MenuUI;

        public HomeController()
        {
            _MenuUI = new MenuUIService();
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 主選單設置
        /// </summary>
        /// <returns></returns>
        public ActionResult MainMenu()
        {
            List<WebMenuViewModel> model = new List<WebMenuViewModel>();
            try
            {
                //var _user = _userManager.FindByName(User.Identity.Name);
                model = _MenuUI.GetMenuByUserId();
            }
            catch (Exception)
            {
                ViewBag.errorMsg = "讀取選單時發生錯誤";
            }

            return PartialView(model);
        }
    }
}