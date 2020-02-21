using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.Authority;
using ViewModels.Share;
using ViewModels.Verity;
using webTemplate.Models;
using WebTemplateDB.Interface;
using WebTemplateDB.Service;

namespace webTemplate.Controllers
{
    public class AuthorityController : BaseController
    {

        protected IAuthorityService _authorityService;
        protected IBackOperationService _backOperationService;
        string OperationName = "權限管理，";

        public AuthorityController()
        {
            _authorityService = new AuthorityService();
            _backOperationService = new BackOperationService();
        }

        // GET: Authority
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<JsonResult> GetRoleOptions()
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var data = await _authorityService.GetRoleOptions();
                res.Data = data;
                res.Success = true;
                res.Message = "取得角色清單成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "取得角色清單", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> GetAuthorityList(SearchModel searchModel, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel res = new ResWithPaginationViewModel();

            try
            {
                var result = await _authorityService.GetAuthorityList(searchModel, pagination);
                res.Data = result.Data;
                res.Pagination = result.Pagination;
                res.MinDateTime = result.MinDateTime;
                res.MaxDateTime = result.MaxDateTime;
                res.Success = true;
                res.Message = "取得權限管理列表成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "查詢", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }

        public async Task<JsonResult> CheckAuthority(string Id)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _authorityService.CheckAuthority(Id, CurrendUserid);
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "取得編輯資料", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetEditAuthorityItem(string Id)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var date = await _authorityService.GetEditAuthorityItem(Id, CurrendUserid);
                res.Data = date;
                res.Success = true;
                res.Message = "取得帳號資料成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "取得編輯資料", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> EditAuthorityItem(AuthorityViewModel model)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _authorityService.EditAuthorityItem(model);
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "編輯", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteAuthorityItem(string Id) 
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _authorityService.DeleteAuthorityItem(Id);
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "刪除", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "伺服器發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }
    }
}