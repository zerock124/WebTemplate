using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.BackOperationRecord;
using ViewModels.Share;
using webTemplate.Utility;
using WebTemplateDB.Interface;
using WebTemplateDB.Service;

namespace webTemplate.Controllers
{
    public class BackOperationController : BaseController
    {
        protected IBackOperationService _backOperationService;

        public BackOperationController() 
        {
            _backOperationService = new BackOperationService();
        }

        // GET: BackOperationRecord
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 取得操作紀錄列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> GetBackOperationList(SearchModel searchModel, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel res = new ResWithPaginationViewModel();

            try 
            {
                var result = await _backOperationService.GetBackOperationList(searchModel, pagination);
                res.Data = result.Data;
                res.Pagination = result.Pagination;
                res.MinDateTime = result.MinDateTime;
                res.MaxDateTime = result.MaxDateTime;
                res.Success = true;
                res.Message = "取得操作紀錄列表成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
            }
            catch 
            {
                res.Success = false;
                res.Message = "取得操作紀錄列表失敗";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }
    }
}