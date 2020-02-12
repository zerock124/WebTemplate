using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webTemplate.Controllers
{
    public class CKEditorController : Controller
    {
        public string _CKEditorImagePath = ConfigurationManager.AppSettings["CKEditorUploadImage"];

        const string scriptTag = "<script type='text/javascript'>window.parent.CKEDITOR.tools.callFunction({0}, '{1}', '{2}')</script>";

        // GET: CKEditor
        public ActionResult Index()
        {
            string CKEditorImagepath = Server.MapPath(_CKEditorImagePath);

            var funcNum = 0;
            int.TryParse(Request["CKEditorFuncNum"], out funcNum);

            if (Request.Files == null || Request.Files.Count < 1)
                return BuildReturnScript(funcNum, null, "No file has been sent");

            if (!System.IO.Directory.Exists(CKEditorImagepath))
                return BuildReturnScript(funcNum, null, "basePath folder doesn't exist");

            var receivedFile = Request.Files[0];

            var fileName = receivedFile.FileName;
            if (string.IsNullOrEmpty(fileName))
            {
                return BuildReturnScript(funcNum, null, "File name is empty");
            }

            var sFileName = System.IO.Path.GetFileName(fileName);

            var nameWithFullPath = System.IO.Path.Combine(CKEditorImagepath, sFileName);
            //Note: you may want to consider using your own naming convention for files, as this is vulnerable to overwrites
            //e.g. at the moment if two users uploaded a file called image1.jpg, one would clash with the other.
            //In the past, I've used Guid.NewGuid() combined with the file extension to ensure uniqueness.
            receivedFile.SaveAs(nameWithFullPath);

            var url = CKEditorImagepath + sFileName;

            return BuildReturnScript(funcNum, url, null);
        }

        private ContentResult BuildReturnScript(int functionNumber, string url, string errorMessage)
        {
            return Content(
                string.Format(scriptTag, functionNumber, HttpUtility.JavaScriptStringEncode(url ?? ""), HttpUtility.JavaScriptStringEncode(errorMessage ?? "")),
                "text/html"
                );
        }
    }
}