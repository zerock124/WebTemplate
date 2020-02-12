using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace webTemplate.Controllers
{
    public class ServiceItemController : BaseController
    {
        public string _ServiceItemPath = ConfigurationManager.AppSettings["ServiceItemPath"];

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
        public async Task<FileResult> ServiceItemPhoto(string filename)
        {
            string path = Server.MapPath(_ServiceItemPath) + filename;
            if (!System.IO.File.Exists(path))
                throw new HttpException(404, "Some description");

            var imgData = await Task.Run(() => System.IO.File.ReadAllBytes(path));
            string mimeType = MimeMapping.GetMimeMapping(filename);
            return await Task.Run(() => new FileStreamResult(new System.IO.MemoryStream(imgData), mimeType));
        }
    }

}