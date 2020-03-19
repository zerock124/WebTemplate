using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels.Share;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;
using ViewModel.Contact;
using WebTemplateDB.Interface;
using WebTemplateDB.Service;
using ViewModels.Verity;
using webTemplate.Utility;

namespace webTemplate.Controllers
{
    public class ContactController : BaseController
    {
        protected IContactService _contactService;
        protected IBackOperationService _backOperationService;
        string OperationName = "聯絡我們，";

        public ContactController()
        {
            _contactService = new ContactService();
            _backOperationService = new BackOperationService();
        }

        // GET: Contact
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> GetContactList(SearchModel searchModel, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel res = new ResWithPaginationViewModel();

            try
            {
                var result = await _contactService.GetContactList(searchModel, pagination);
                res.Data = result.Data;
                res.Pagination = result.Pagination;
                res.MaxDateTime = result.MaxDateTime;
                res.MinDateTime = result.MinDateTime;
                res.MaxStartDate = result.MaxStartDate;
                res.MinStartDate = result.MinStartDate;
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "查詢", CurrendUserIp);
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

        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> GetEditContactItem(int ContactId)
        {
            ResponseViewModel res = new ResponseViewModel();
            try
            {
                var data = await _contactService.GetEditContact(ContactId);
                res.Data = data;
                res.Success = true;
                res.Message = "取得編輯聯絡我們內容成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "取得編輯聯絡我們", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連接發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> EditContactItem(ContactViewModel model) 
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _contactService.EditContactItem(model, CurrendUserid);
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "編輯", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連接發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> DeleteContact(int ContactId)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _contactService.DeleteContact(ContactId);
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "刪除", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連接發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }
    }
}