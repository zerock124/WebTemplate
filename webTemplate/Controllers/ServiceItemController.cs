using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.ServiceItem;
using ViewModels.Share;
using webTemplate.Utility;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Service;

namespace webTemplate.Controllers
{
    public class ServiceItemController : BaseController
    {
        public string _ServiceItemPath = ConfigurationManager.AppSettings["ServiceItemPath"];

        protected IServiceItemService _serviceItem;
        protected IBackOperationService _backOperationService;
        string OperationName = "服務項目，";

        public ServiceItemController() {
            _serviceItem = new ServiceItemService();
            _backOperationService = new BackOperationService();
        }

        // GET: ServiceItem
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 取得服務項目圖片
        /// </summary>
        /// <param name="filesname"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<FileResult> ServiceItemPhoto(string filename)
        {
            string path = Server.MapPath(_ServiceItemPath) + filename;
            if (!System.IO.File.Exists(path))
                throw new HttpException(404, "Some description");

            var imgData = await Task.Run(() => System.IO.File.ReadAllBytes(path));
            string mimeType = MimeMapping.GetMimeMapping(filename);
            return await Task.Run(() => new FileStreamResult(new System.IO.MemoryStream(imgData), mimeType));
        }
        /// <summary>
        /// 取得服務項目列表
        /// </summary>
        /// <returns></returns>
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> GetServiceItemList() 
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var date = await _serviceItem.GetServiceItemList();
                res.Data = date;
                res.Success = true;
                res.Message = "取得服務項目列表成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "查詢", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線失敗";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-DD hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增服務項目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> CreateServiceItemList(List<ServiceItemViewModel> model) 
        {
            ResponseViewModel res = new ResponseViewModel();

            string _path = Server.MapPath(_ServiceItemPath);

            try
            {
                foreach (var item in model)
                {
                    if (item.PhotoFile != null)
                    {
                        var PhoneFileName = item.PhotoFile.FileName;

                        var saveResult = ImageHandler.SaveFileToPath(item.PhotoFile, _path, PhoneFileName);
                    }
                }

                var result = await _serviceItem.CreateServiceItemList(model ,CurrendUserid);
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "新增", CurrendUserIp);
            }
            catch 
            {
                res.Success = false;
                res.Message = "與伺服器連線失敗";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }

            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-DD hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }
    }

}