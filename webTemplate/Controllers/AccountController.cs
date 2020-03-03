using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using webTemplate.Models;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;
using WebTemplateDB.Service;

namespace webTemplate.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        /*
        * 目前構想此控制項用於處理外部人員資訊 
        * 包含登入、外部註冊、修改密碼(確認原密碼)等等
        */
        private ApplicationSignInManager _signInManager;
        private new ApplicationUserManager _userManager;

        protected IGenericRepository<AspNetRoles> _aspnetRoles;
        protected IGenericRepository<AspNetUserRoles> _aspnetUserRoles;
        protected IGenericRepository<AspNetUsers> _aspnetUser;
        protected IGenericRepository<AspNetUserLogins> _login;
        protected IBackOperationService _backOperationService;
        string OperationName = "權限管理";

        //---------------------LineAPI設定---------------------
        string response_type = "code";
        string client_id = "1653889097";
        string redirect_uri = HttpUtility.UrlEncode("http://lab.sp88.com.tw/aspnet/project-release/Account/Linecallback");
        string state = "zerock851024";
        string client_secret = "d985d130a7464782a66e0737ba0bc827";
        //----------------------------------------------------

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public AccountController()
        {
            _db = new WebTemplateEntities();
            _aspnetRoles = new GenericRepository<AspNetRoles>();
            _aspnetUserRoles = new GenericRepository<AspNetUserRoles>();
            _aspnetUser = new GenericRepository<AspNetUsers>();
            _backOperationService = new BackOperationService();
            _login = new GenericRepository<AspNetUserLogins>();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View("SignIn");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginConfirmation()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // 這不會計算為帳戶鎖定的登入失敗
                // 若要啟用密碼失敗來觸發帳戶鎖定，請變更為 shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
                //_logger.Information("TMS Login Result: {0}", JsonConvert.SerializeObject(result));

                var user = await UserManager.FindByNameAsync(model.UserName);

                switch (result)
                {
                    case SignInStatus.Success:
                        #region _依帳號角色轉頁
                        //if (UserManager.IsInRole(user.Id, RolesEnum.SystemAdmin.ToString()))
                        //{
                        //    return RedirectToAction("Index", "TotalReport", new { area = "" });
                        //}
                        //else if (UserManager.IsInRole(user.Id, RolesEnum.SystemGeneral.ToString()))
                        //{
                        //    return RedirectToAction("Index", "TotalReport", new { area = "" });
                        //}
                        //else if (UserManager.IsInRole(user.Id, RolesEnum.FleetAdmin.ToString()))
                        //{
                        //    return RedirectToAction("Index", "TaxiManagers", new { area = "" });
                        //}
                        //else
                        //{
                        //    return RedirectToAction("Index", "Home", new { area = "" });
                        //}
                        return RedirectToAction("Index", "Home", new { area = "" });
                    #endregion
                    //return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "登入嘗試失試。");
                        return View(model);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Account Login Exception:" + e.ToString());
                ModelState.AddModelError("", "登入失敗,請洽系統人員。");
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            Session.Clear();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Signin", "Account");
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = "test@sp88.com.tw",
                    RealName = model.UserName,
                    CreateTime = DateTime.Now,
                    PhoneNumber = "0912345678",
                    PhoneNumberConfirmed = true,
                    RegisterDate = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CreateUser = CurrendUserid
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var role = _aspnetRoles.FindBy(x => x.Id == model.RoleId).FirstOrDefault();

                    var aspnetuser = _aspnetUser.FindBy(x => x.UserName == model.UserName).FirstOrDefault();

                    AspNetUserRoles userRoles = new AspNetUserRoles
                    {
                        UserId = aspnetuser.Id,
                        RoleId = role.Id
                    };

                    _aspnetUserRoles.Create(userRoles);
                    await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "新增", CurrendUserIp);
                }
                AddErrors(result);
            }
            return RedirectToAction("Index", "Authority");
        }

        /// <summary>
        /// 外部登入
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // 要求重新導向至外部登入提供者
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }


        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        /// <summary>
        /// 外部登入的callback
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("SignIn");
            }

            // 若使用者已經有登入資料，請使用此外部登入提供者登入使用者
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // 若使用者沒有帳戶，請提示使用者建立帳戶
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    ExternalLoginConfirmationViewModel model = new ExternalLoginConfirmationViewModel
                    {
                        RealName = loginInfo.DefaultUserName,
                        Email = loginInfo.Email,
                        returnUrl = returnUrl
                    };
                    return View("ExternalLoginConfirmation", model);
            }
        }

        /// <summary>
        /// 外部登入註冊帳號
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                // 從外部登入提供者處取得使用者資訊
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    RealName = model.RealName,
                    CreateTime = DateTime.Now,
                    PhoneNumber = null,
                    PhoneNumberConfirmed = true,
                    RegisterDate = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CreateUser = CurrendUserid
                };

                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    if (result.Succeeded)
                    {
                        AspNetUserLogins userLogin = new AspNetUserLogins
                        {
                            UserId = user.Id,
                            LoginProvider = info.Login.LoginProvider,
                            ProviderKey = info.Login.ProviderKey
                        };

                        _login.Create(userLogin);

                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        var role = _aspnetRoles.FindBy(x => x.Name == "user").FirstOrDefault();

                        var aspnetuser = _aspnetUser.FindBy(x => x.UserName == model.UserName).FirstOrDefault();

                        AspNetUserRoles userRoles = new AspNetUserRoles
                        {
                            UserId = aspnetuser.Id,
                            RoleId = role.Id
                        };

                        _aspnetUserRoles.Create(userRoles);
                        await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "新增", CurrendUserIp);

                        return RedirectToLocal(model.returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = model.returnUrl;
            return View(model);
        }

        /// <summary>
        /// LineLogin
        /// </summary>
        /// <returns></returns>
        #region LineLogin
        [AllowAnonymous]
        public ActionResult LineLoginDirect()
        {
            string LineLoginUrl = string.Format("https://access.line.me/oauth2/v2.1/authorize?response_type={0}&client_id={1}&redirect_uri={2}&state={3}&scope=openid%20profile",
                response_type,
                client_id,
                redirect_uri,
                state
                );
            return Redirect(LineLoginUrl);
        }
        /// <summary>
        /// LineLogincallback
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> Linecallback(string code, string state)
        {
            if (state == "zerock851024")
            {
                #region Api變數宣告
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                NameValueCollection nvc = new NameValueCollection();
                #endregion
                try
                {
                    //取回Token
                    string ApiUrl_Token = "https://api.line.me/oauth2/v2.1/token";
                    nvc.Add("grant_type", "authorization_code");
                    nvc.Add("code", code);
                    nvc.Add("redirect_uri", "http://lab.sp88.com.tw/aspnet/project-release/Account/Linecallback");
                    nvc.Add("client_id", "1653889097");
                    nvc.Add("client_secret", "d985d130a7464782a66e0737ba0bc827");
                    string JsonStr = Encoding.UTF8.GetString(wc.UploadValues(ApiUrl_Token, "POST", nvc));
                    LineLoginToken ToKenObj = JsonConvert.DeserializeObject<LineLoginToken>(JsonStr);
                    wc.Headers.Clear();

                    //取回User Profile
                    string ApiUrl_Profile = "https://api.line.me/v2/profile";
                    wc.Headers.Add("Authorization", "Bearer " + ToKenObj.access_token);
                    string UserProfile = wc.DownloadString(ApiUrl_Profile);
                    LineProfile ProfileObj = JsonConvert.DeserializeObject<LineProfile>(UserProfile);
                    ExternalLoginConfirmationViewModel model = new ExternalLoginConfirmationViewModel
                    {
                        Userid = ProfileObj.userId,
                        RealName = ProfileObj.displayName,
                        Email = "",
                        returnUrl = "",
                        Password = ProfileObj.userId
                    };
                    var FindLineUser = _aspnetUser.FindBy(x => x.Id == model.Userid);
                    if (FindLineUser.Any())
                    {
                        LoginViewModel LineLogin = new LoginViewModel
                        {
                            UserName = FindLineUser.FirstOrDefault().UserName,
                            Password = FindLineUser.FirstOrDefault().Id,
                            RememberMe = false
                        };
                        var result = await SignIn(LineLogin);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View("LineLoginConfirmation", model);
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    throw;
                }
            }
            return View("Login");
        }

        /// <summary>
        /// Line註冊帳號
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LineLoginConfirmation(ExternalLoginConfirmationViewModel model)
        {
            var FindUser = _aspnetUser.FindBy(x => x.Id == model.Userid);
            if (!FindUser.Any())
            {
                var user = new ApplicationUser
                {
                    Id = model.Userid,
                    UserName = model.UserName,
                    Email = model.Email,
                    RealName = model.RealName,
                    CreateTime = DateTime.Now,
                    PhoneNumber = null,
                    PhoneNumberConfirmed = true,
                    RegisterDate = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CreateUser = CurrendUserid
                };

                var result = await UserManager.CreateAsync(user, model.Userid);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    var role = _aspnetRoles.FindBy(x => x.Name == "user").FirstOrDefault();

                    var aspnetuser = _aspnetUser.FindBy(x => x.UserName == model.UserName).FirstOrDefault();

                    AspNetUserRoles userRoles = new AspNetUserRoles
                    {
                        UserId = aspnetuser.Id,
                        RoleId = role.Id
                    };

                    _aspnetUserRoles.Create(userRoles);
                    await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "新增", CurrendUserIp);

                    return RedirectToLocal(model.returnUrl);
                }
            }
            return View();
        }
        #endregion

        #region Helper
        // 新增外部登入時用來當做 XSRF 保護
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}