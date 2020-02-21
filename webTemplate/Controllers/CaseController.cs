using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.Case;
using ViewModels.Share;
using ViewModels.Verity;
using webTemplate.Utility;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Service;

namespace webTemplate.Controllers
{
    public class CaseController : BaseController
    {
        public string _CasePath = ConfigurationManager.AppSettings["CasePath"];

        protected ICaseService _caseServuce;
        protected IBackOperationService _backOperationService;
        string OperationName = "案例介紹，";

        public CaseController()
        {
            _caseServuce = new CaseService();
            _backOperationService = new BackOperationService();
        }

        // GET: Case
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
        /// <summary>
        /// 取得案例圖片
        /// </summary>
        /// <param name="filesname"></param>
        /// <returns></returns>
        public async Task<FileResult> CasePhoto(string filename)
        {
            string path = Server.MapPath(_CasePath) + filename;
            if (!System.IO.File.Exists(path))
                throw new HttpException(404, "Some description");

            var imgData = await Task.Run(() => System.IO.File.ReadAllBytes(path));
            string mimeType = MimeMapping.GetMimeMapping(filename);
            return await Task.Run(() => new FileStreamResult(new System.IO.MemoryStream(imgData), mimeType));
        }

        /// <summary>
        /// 新增案例圖片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public async Task<ActionResult> CreateCase(CaseViewModel model)
        {
            ResponseViewModel res = new ResponseViewModel();

            string CreateUser = CurrendUserid;

            string _path = Server.MapPath(_CasePath);

            try
            {
                if (model.PhotoFile != null)
                {
                    var PhoneFileName = model.PhotoFile.FileName;

                    var saveResult = ImageHandler.SaveFileToPath(model.PhotoFile, _path, PhoneFileName);

                    model.ImageName = model.PhotoFile.FileName;
                }
                model.CreateUser = CreateUser;
                var data = await _caseServuce.CreateCase(model);
                res.Success = data.Success;
                res.Message = data.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "新增", CurrendUserIp);

            }
            catch
            {
                res.Success = false;
                res.Message = "伺服器發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> GetCaseList(SearchModel model, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel res = new ResWithPaginationViewModel();

            try
            {
                var result = await _caseServuce.GetCaseList(model, pagination);
                res.Data = result.Data;
                res.Pagination = result.Pagination;
                res.MinDateTime = result.MinDateTime;
                res.MaxDateTime = result.MaxDateTime;
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "查詢", CurrendUserIp);
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

        public async Task<JsonResult> GetEditCaseItem(int CaseId)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var data = await _caseServuce.GetEditCaseItem(CaseId);
                res.Data = data;
                res.Success = true;
                res.Message = "取得案列介紹成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "取得編輯資料", CurrendUserIp);
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
        [ValidateInput(false)]
        [HttpPost]
        public async Task<JsonResult> EditCaseItem(CaseViewModel model)
        {
            ResponseViewModel res = new ResponseViewModel();

            string UpdateUser = CurrendUserid;

            string _path = Server.MapPath(_CasePath);

            try
            {
                if (model.PhotoFile != null)
                {
                    var PhoneFileName = model.PhotoFile.FileName;

                    var saveResult = ImageHandler.SaveFileToPath(model.PhotoFile, _path, PhoneFileName);

                    model.ImageName = model.PhotoFile.FileName;
                }
                model.UpdateUser = UpdateUser;
                var result = await _caseServuce.EditCaseItem(model);
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

        public async Task<JsonResult> DeleteCaseItem(int CaseId)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _caseServuce.DeleteCaseItem(CaseId);
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "刪除", CurrendUserIp);
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
    }
}