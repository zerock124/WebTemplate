using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace webTemplate.Models
{
    // 您可將更多屬性新增至 ApplicationUser 類別，藉此為使用者新增設定檔資料，如需深入了解，請瀏覽 https://go.microsoft.com/fwlink/?LinkID=317594。
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // 注意 authenticationType 必須符合 CookieAuthenticationOptions.AuthenticationType 中定義的項目
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // 在這裡新增自訂使用者宣告
            return userIdentity;
        }
                        
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 註冊日期
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 建立人員ID
        /// </summary>
        public string CreateUser { get; set; }

        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("Data Source=192.168.1.2,1439;Initial Catalog=Web_testDb;User ID=sa;Password=sp150128;MultipleActiveResultSets=True;Application Name=EntityFramework", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class LineLoginToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string id_token { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string token_type { get; set; }
    }

    public class LineProfile
    {
        public string userId { get; set; }
        public string displayName { get; set; }
        public string pictureUrl { get; set; }
        public string statusMessage { get; set; }
    }
}