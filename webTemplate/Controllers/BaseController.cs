using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using webTemplate.Models;
using WebTemplateDB.Models;
namespace webTemplate.Controllers
{
    [HandleError]
    [Authorize]
    public class BaseController : Controller
    {
        protected WebTemplateEntities _db;

        public BaseController()
        {
            _db = new WebTemplateEntities();
            var WebName = _db.WebConfig.FirstOrDefault().WebName;
            ViewBag.WebName = WebName;
            var Copyright = _db.WebConfig.FirstOrDefault().Copyright;
            ViewBag.Copyright = Copyright;
            var meta_title = _db.WebConfig.FirstOrDefault().meta_title;
            ViewBag.meta_title = meta_title;
        }

        /// <summary>
        /// Serilog 
        /// </summary>
        public readonly ILogger _logger = Log.Logger;
        public readonly AuthRepository _repository = new AuthRepository();
        //public readonly AuthRepository _repository = new AuthRepository();
        public ApplicationUserManager _userManager => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        public ApplicationRoleManager _roleManager => HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
        /// <summary>
        /// 取得當前的使用者ID
        /// </summary>
        public string CurrendUserid => User.Identity.GetUserId();
        /// <summary>
        /// 取得當前的使用者IP
        /// </summary>
        public string CurrendUserIp => GetClientIP();

        public string GetClientIP()
        {
            //判所client端是否有設定代理伺服器
            if (Request.ServerVariables["HTTP_VIA"] == null)
                return Request.ServerVariables["REMOTE_ADDR"].ToString();
            else
                return Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        }

    }
}