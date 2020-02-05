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

namespace webTemplate.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        /*
        * 目前構想此控制項用於處理外部人員資訊 
        * 包含登入、外部註冊、修改密碼(確認原密碼)等等
        */
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

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
                    Email = model.Email,
                    RealName = model.RealName,
                    CreateTime = DateTime.Now,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = true,
                    RegisterDate = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            return View(model);
        }

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

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // 產生並傳送 Token
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }
    }
}