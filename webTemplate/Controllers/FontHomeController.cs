using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.FontHome;
using ViewModels.Share;
using webTemplate.Utility;
using WebTemplateDB.Interface;
using WebTemplateDB.Service;

namespace webTemplate.Controllers
{
    public class FontHomeController : BaseController
    {

        public string _HomeImagePath = ConfigurationManager.AppSettings["HomeImagePath"];

        protected IFontHomeService _fontHomeService;
        protected IBackOperationService _backOperationService;
        string OperationName = "前台首頁，";

        public FontHomeController()
        {
            _fontHomeService = new FontHomeService();
            _backOperationService = new BackOperationService();
        }
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 取得首頁圖片
        /// </summary>
        /// <param name="filesname"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<FileResult> FontHomePhotos(string filename)
        {
            string path = Server.MapPath(_HomeImagePath) + filename;
            if (!System.IO.File.Exists(path))
                throw new HttpException(404, "Some description");

            var imgData = await Task.Run(() => System.IO.File.ReadAllBytes(path));
            string mimeType = MimeMapping.GetMimeMapping(filename);
            return await Task.Run(() => new FileStreamResult(new System.IO.MemoryStream(imgData), mimeType));
        }
        /// <summary>
        /// 取得首頁圖片列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> GetFontHomeList(SearchModel model, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel res = new ResWithPaginationViewModel();

            try
            {
                var result = await _fontHomeService.GetFontHomeList(model, pagination);
                res.Data = result.Data;
                res.Pagination = result.Pagination;
                res.MinDateTime = result.MinDateTime;
                res.MaxDateTime = result.MaxDateTime;
                res.MinStartDate = result.MinStartDate;
                res.MaxStartDate = result.MaxStartDate;
                res.Success = true;
                res.Message = "成功取得前台首頁管理清單";
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

        /// <summary>
        /// 新增首頁圖
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> CreateFontHome(FontHomeViewModel model)
        {
            ResponseViewModel res = new ResponseViewModel();

            string CreateUser = CurrendUserid;

            string _path = Server.MapPath(_HomeImagePath);

            try
            {
                if (model.PhotoFile != null)
                {
                    var PhoneFileName = model.PhotoFile.FileName;

                    var saveResult = ImageHandler.SaveFileToPath(model.PhotoFile, _path, PhoneFileName);

                    model.CreateUser = CreateUser;
                    var data = await _fontHomeService.CreateFontHome(model);
                    res.Data = data.Data;
                    res.Success = true;
                    res.Message = data.Success ? "新增前台首頁成功" : "新增前台首頁失敗";
                    res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                    await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "新增", CurrendUserIp);
                }
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

        /// <summary>
        /// 取得變更圖片ID
        /// </summary>
        /// <param name="FontHomeId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEditFontHome(int FontHomeId)
        {
            ViewBag.FontHomeId = FontHomeId;

            return View("Edit");
        }
        /// <summary>
        /// 取得變更圖片內容
        /// </summary>
        /// <param name="FontHomeId"></param>
        /// <returns></returns>
        [HttpGet]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> GetEditFontHomeItem(int FontHomeId)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var data = await _fontHomeService.GetFontHomeItem(FontHomeId);
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
        /// <summary>
        /// 變更前台圖片內容
        /// </summary>
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<ActionResult> EditFontHomeItem(FontHomeViewModel model)
        {
            ResponseViewModel res = new ResponseViewModel();
            string CreateUser = CurrendUserid;

            string _path = Server.MapPath(_HomeImagePath);

            try
            {
                if (model.PhotoFile != null)
                {
                    var PhoneFileName = model.PhotoFile.FileName;

                    var saveResult = ImageHandler.SaveFileToPath(model.PhotoFile, _path, PhoneFileName);

                    model.UpdateUser = CreateUser;
                    var data = await _fontHomeService.EditFontHome(model);
                    res.Data = data.Data;
                    res.Success = data.Success;
                    res.Message = data.Message;
                    res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                    await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "編輯", CurrendUserIp);
                }
                else 
                {
                    model.UpdateUser = CreateUser;
                    var data = await _fontHomeService.EditFontHome(model);
                    res.Data = data.Data;
                    res.Success = data.Success;
                    res.Message = data.Message;
                    res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                    await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "編輯", CurrendUserIp);
                }
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
        /// <summary>
        /// 刪除前台首頁圖
        /// </summary>
        /// <param name="FontHomeId"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> DeleteFontHome(int FontHomeId) 
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var data = await _fontHomeService.DeleteFontHome(FontHomeId);
                res.Data = data;
                res.Success = true;
                res.Message = "取得首頁管理內容成功";
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "刪除", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }
    }
}