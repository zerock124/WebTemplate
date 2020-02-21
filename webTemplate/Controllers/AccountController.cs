using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected WebTemplateEntities _db;
        protected IGenericRepository<AspNetRoles> _aspnetRoles;
        protected IGenericRepository<AspNetUserRoles> _aspnetUserRoles;
        protected IGenericRepository<AspNetUsers> _aspnetUser;
        protected IBackOperationService _backOperationService;
        string OperationName = "權限管理";


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
                //_logger.Error(e, "Account Login Exception:" + e.ToString());
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
            return RedirectToAction("Index","Authority");
        }

        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // 要求重新導向至外部登入提供者
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
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

        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
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
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
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
                    UserName = model.Email,
                    Email = "test@sp88.com.tw",
                    RealName = model.Email,
                    CreateTime = DateTime.Now,
                    PhoneNumber = "0912345678",
                    PhoneNumberConfirmed = true,
                    RegisterDate = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    CreateUser = CurrendUserid
                };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

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