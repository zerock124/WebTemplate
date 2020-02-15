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

namespace webTemplate.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// Serilog 
        /// </summary>
        public readonly ILogger _logger = Log.Logger;

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
        /// <summary>
        /// Image Path
        /// </summary>
        //public readonly string _adImagePath = ConfigurationManager.AppSettings["ADImagePath"];
        //public readonly string _driverImagePath = ConfigurationManager.AppSettings["DriverImagePath"];
        //public readonly string _vehicleImagePath = ConfigurationManager.AppSettings["VehicleImagePath"];
        //public readonly string _activityImagePath = ConfigurationManager.AppSettings["ActivityImagePath"];
        //public static readonly string NO_IMAGE_FILE = "NoImage.jpg";
        /// <summary>
        /// 簡訊API網址
        /// </summary>
        //public static string smsApiUrl = ConfigurationManager.AppSettings["SMSAPIUrl"].ToString();
        /// <summary>
        /// 推播API網址
        /// </summary>
        //public static string pushApiUrl = ConfigurationManager.AppSettings["PushAPIUrl"].ToString();
        /// <summary>
        /// 站台位置(固定)
        /// </summary>
        //public static string baseWebUrl = ConfigurationManager.AppSettings["WebSiteUrl"].ToString();
        /// <summary>
        /// 伺服器端認本機取資料位置(變動)
        /// </summary>
        //public static string localWebUrl = ConfigurationManager.AppSettings["WebLoaclUrl"].ToString();

        /// <summary>
		/// Google Map API 註冊碼
		/// </summary>
		//public static string BingMapKey = ConfigurationManager.AppSettings["BingMapKey"].ToString();

        ///// <summary>
        ///// 非同步執行推播API
        ///// </summary>
        ///// <param name="PushMsg"></param>
        ///// <returns></returns>
        //public async Task<Uri> RunPushApiAsync(PushMessageToModel PushMsg)//(PushMessageViewModel PushMsg)
        //{
        //    string pushMsgJson = JsonConvert.SerializeObject(PushMsg);
        //    _logger.Debug($"RunPushApiAsync_PushModel : {pushMsgJson} ");
        //    try
        //    {
        //        _logger.Debug($"推播字串 {JsonConvert.SerializeObject(PushMsg)}");
        //        var baseurl = new Uri(pushApiUrl);
        //        httpClient.DefaultRequestHeaders.Accept.Clear();
        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(pushMsgJson));
        //        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        HttpResponseMessage response = await httpClient.PostAsync(new Uri(baseurl, "api/Push/PushNoticeMessage"), byteContent);
        //        response.EnsureSuccessStatusCode();
        //        // return URI of the created resource.
        //        return response.Headers.Location;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Verbose($"推播錯誤(RunPushApiAsync) {ex.ToString()}");
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// 取得指定Key後填入發送推播
        ///// </summary>
        ///// <param name="pushMessage"></param>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //protected async Task<VerityResult> GetUserMKeyAndPushToClient(PushMessageToModel pushMessage, PushClientTypeEnum type)
        //{
        //    VerityResult result = new VerityResult();
        //    try
        //    {
        //        #region 取得App端Keynumber與type
        //        switch (type)
        //        {
        //            case PushClientTypeEnum.Passenger:
        //                var passMobie = await new PassengerMobileService().FindUserMobilePushInfo(pushMessage.Readerid);
        //                pushMessage.PushTo = "Passenger";
        //                pushMessage.MobileType = passMobie.MobileType;
        //                pushMessage.KeyNumber = passMobie.KeyNumber;
        //                break;
        //            case PushClientTypeEnum.Driver:
        //                var userMobie = await new MobileService().FindUserMobilePushInfo(pushMessage.Readerid);
        //                pushMessage.PushTo = "Driver";
        //                pushMessage.MobileType = userMobie.MobileType;
        //                pushMessage.KeyNumber = userMobie.KeyNumber;
        //                break;
        //        }
        //        #endregion
        //        pushMessage.Pushtime = DateTime.Now;
        //        await Task.Run(() => RunPushApiAsync(pushMessage));
        //        result.Success = true;
        //        result.Message = string.Format("已成功推播至{0}端", type.ToString());
        //    }
        //    catch
        //    {
        //        result.Success = false;
        //        result.Message = string.Format("推播至{0}端失敗", type.ToString());
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// 儲存 Seroilog
        ///// </summary>
        ///// <param name="input">要存的參數</param>
        ///// <returns></returns>
        //public bool SerilogWriter(SerilogInputViewModel input)
        //{
        //    try
        //    {
        //        string con = string.IsNullOrWhiteSpace(input.Controller) ? "UnKnowController" : input.Controller
        //            , method = string.IsNullOrWhiteSpace(input.Method) ? "UnknowMethod" : input.Method
        //            , strval = input.LogString ?? "";
        //        string serilogString = //string.Format($"{con}_{method} {strval}: {JsonConvert.SerializeObject(input.Input)}");
        //            string.Format("{0}_{1}_{2}: {3}", con, method, strval, JsonConvert.SerializeObject(input.Input));

        //        switch (input.SerilogType)
        //        {
        //            case SerilogTypeEnum.Verbose:
        //                _logger.Debug(serilogString);
        //                break;

        //            case SerilogTypeEnum.Warning:
        //                _logger.Warning(serilogString);
        //                break;

        //            case SerilogTypeEnum.Debug:
        //                _logger.Debug(serilogString);
        //                break;

        //            case SerilogTypeEnum.Fatal:
        //                _logger.Fatal(serilogString);
        //                break;

        //            case SerilogTypeEnum.Error:
        //                _logger.Error(serilogString);
        //                break;

        //            case SerilogTypeEnum.Information:
        //            default:
        //                _logger.Information(serilogString);
        //                break;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error("SerilogService: {0} \n {1}", ex.Message.ToString(), JsonConvert.SerializeObject(input.Input));
        //        _logger.Error("SerilogService: {0} \n {1}", ex.ToString(), JsonConvert.SerializeObject(input.Input));
        //        //return false;
        //        throw ex;
        //    }
        //    return true;
        //}


        ///// <summary>
        ///// 儲存圖片到指定路徑
        ///// </summary>
        ///// <param name="file">檔案</param>
        ///// <param name="path">路徑</param>
        ///// <param name="filename">儲存名稱(空白時使用GUID命名)</param>
        ///// <returns></returns>
        //protected SaveFileVerityResult SaveImgFileToPath(HttpPostedFileBase file, string path, string filename = "")
        //{
        //    SaveFileVerityResult result =
        //        ImageHandler.SaveFileToPath(file, path, filename);
        //    return result;
        //}

        ///// <summary>
        ///// 刪除指定路徑的檔案，找不到則停止
        ///// </summary>
        ///// <param name="path"></param>
        ///// <param name="filename"></param>
        //protected void DeleteFileIfDefindInPath(string path, string filename)
        //{
        //    if (string.IsNullOrEmpty(filename)) { return; }
        //    FileHandler.DeleteFileIfDefind(path, filename);
        //}
    }
}