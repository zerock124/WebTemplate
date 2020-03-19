using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webTemplate.Models;

namespace webTemplate
{
	public partial class Startup
	{
		// 如需設定驗證的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301864
		public void ConfigureAuth(IAppBuilder app) 
		{
            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next();
            });

            // 設定資料庫內容、使用者管理員和登入管理員，以針對每個要求使用單一執行個體
            app.CreatePerOwinContext(ApplicationDbContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            //使用Role來判斷前後台使用者: 2.增加角色的OwinContext
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            // 讓應用程式使用 Cookie 儲存已登入使用者的資訊
            // 並使用 Cookie 暫時儲存使用者利用協力廠商登入提供者登入的相關資訊；
            // 在 Cookie 中設定簽章
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/SignIn"),
                Provider = new CookieAuthenticationProvider
                {
                    // 讓應用程式在使用者登入時驗證安全性戳記。
                    // 這是您變更密碼或將外部登入新增至帳戶時所使用的安全性功能。  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                },
                SlidingExpiration = false,
                ExpireTimeSpan = TimeSpan.FromMinutes(30)
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // 讓應用程式在雙因素驗證程序中驗證第二個因素時暫時儲存使用者資訊。
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // 讓應用程式記住第二個登入驗證因素 (例如電話或電子郵件)。
            // 核取此選項之後，將會在用來登入的裝置上記住登入程序期間的第二個驗證步驟。
            // 這類似於登入時的 RememberMe 選項。
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            app.UseFacebookAuthentication(
               appId: "308543876767980",
               appSecret: "689e57b1ef62d01b49cf2c91bf1b4b4e");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            { 
              ClientId = "786376027558-gjnf5ma10cofl6ivpr8lbh8k0pe0i0pk.apps.googleusercontent.com",
              ClientSecret = "2QL68UzzXDQs4-Lz-A_iG5Fg",
            });     
        }

    }
}