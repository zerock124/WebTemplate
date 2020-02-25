using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.Home;
using ViewModels.Share;
using WebTemplateDB.Interface;
using WebTemplateDB.Service;
using WebTemplateDB.Repositories;
using WebTemplateDB.Models;

namespace webTemplate.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        protected IGenericRepository<WebConfig> _webconfing;
        protected IHomeService _homeService;
        protected IBackOperationService _backOperationService;
        string OperationName = "Home頁，";

        public HomeController()
        {
            _webconfing = new GenericRepository<WebConfig>();
            _homeService = new HomeService();
            _backOperationService = new BackOperationService();
        }

        public class AccessUser
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public string expires_in { get; set; }
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetHomeDate()
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var data = await _homeService.GetHomeData();
                res.Data = data;
                res.Success = true;
                res.Message = "取得HomeData成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "取得內容", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "取得HomeData失敗";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}