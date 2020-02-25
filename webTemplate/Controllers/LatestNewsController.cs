using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.LatestNews;
using ViewModels.Share;
using webTemplate.Utility;
using WebTemplateDB.Interface;
using WebTemplateDB.Service;

namespace webTemplate.Controllers
{
    public class LatestNewsController : BaseController
    {
        public string _LatestNewsImagePath = ConfigurationManager.AppSettings["LatestNewsPath"];

        protected ILatestNewsService _latestnewsService;
        protected IBackOperationService _backOperationService;
        string OperationName = "最新消息，";

        public LatestNewsController()
        {
            _latestnewsService = new LatestNewsService();
            _backOperationService = new BackOperationService();
        }


        // GET: LatestNews
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
        /// 取得最新消息圖片
        /// </summary>
        /// <param name="filesname"></param>
        /// <returns></returns>
        public async Task<FileResult> LatestNewsPhoto(string filename)
        {
            string path = Server.MapPath(_LatestNewsImagePath) + filename;
            if (!System.IO.File.Exists(path))
                throw new HttpException(404, "Some description");

            var imgData = await Task.Run(() => System.IO.File.ReadAllBytes(path));
            string mimeType = MimeMapping.GetMimeMapping(filename);
            return await Task.Run(() => new FileStreamResult(new System.IO.MemoryStream(imgData), mimeType));
        }

        /// <summary>
        /// 新增最新消息圖片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public async Task<ActionResult> CreateLatestNews(LatestNewsViewModel model)
        {
            ResponseViewModel res = new ResponseViewModel();

            string CreateUser = CurrendUserid;

            string _path = Server.MapPath(_LatestNewsImagePath);

            try
            {
                if (model.PhotoFile != null)
                {
                    var PhoneFileName = model.PhotoFile.FileName;

                    var saveResult = ImageHandler.SaveFileToPath(model.PhotoFile, _path, PhoneFileName);

                    model.ImageName = model.PhotoFile.FileName;
                }
                model.CreateUser = CreateUser;
                var data = await _latestnewsService.CreateLatestNews(model);
                res.Data = data.Data;
                res.Success = true;
                res.Message = data.Success ? "新增前台首頁成功" : "新增前台首頁失敗";
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

        public async Task<JsonResult> GetLatestNewsList(SearchModel model, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel res = new ResWithPaginationViewModel();

            try
            {
                var data = await _latestnewsService.GetLatestNewsList(model, pagination);
                res.Data = data.Data;
                res.Pagination = data.Pagination;
                res.MinDateTime = data.MinDateTime;
                res.MaxDateTime = data.MaxDateTime;
                res.MinStartDate = data.MinStartDate;
                res.MaxStartDate = data.MaxStartDate;
                res.Success = true;
                res.Message = data.Success ? "新增前台首頁成功" : "新增前台首頁失敗";
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

        public async Task<JsonResult> GetEditLatestNewsItem(int LatestNewsId)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var data = await _latestnewsService.GetLatestNewsItem(LatestNewsId);
                res.Data = data;
                res.Success = true;
                res.Message = "取得首頁管理內容成功";
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "取得編輯資料", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "伺服器發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public async Task<JsonResult> EditLatestNewsItem(LatestNewsViewModel model)
        {
            ResponseViewModel res = new ResponseViewModel();

            string CreateUser = CurrendUserid;

            string _path = Server.MapPath(_LatestNewsImagePath);

            try
            {
                if (model.PhotoFile != null)
                {
                    var PhoneFileName = model.PhotoFile.FileName;

                    var saveResult = ImageHandler.SaveFileToPath(model.PhotoFile, _path, PhoneFileName);

                    model.ImageName = model.PhotoFile.FileName;
                }

                model.UpdateUser = CreateUser;
                var result = await _latestnewsService.EditLatestNewsItem(model);
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "編輯", CurrendUserIp);
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

        public async Task<JsonResult> DeleteLatestNews(int LatestNewsId)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _latestnewsService.DeleteLatestNewsItem(LatestNewsId);
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