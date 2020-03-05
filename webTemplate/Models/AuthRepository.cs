using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace webTemplate.Models
{
    /// <summary>
    /// 自訂Model : 註冊與尋找使用者
    /// </summary>
    public class AuthRepository : IDisposable
    {
        private ApplicationDbContext _ctx;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;


        private readonly ILogger _logger = Log.Logger;

        public string _driverImagePath = ConfigurationManager.AppSettings["DriverImagePath"];

        /// <summary>
        /// AuthRepository
        /// </summary>
        public AuthRepository()
        {
            _ctx = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_ctx));
            _userManager.UserValidator = new UserValidator<ApplicationUser>(_userManager) { AllowOnlyAlphanumericUserNames = false };
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_ctx));
        }

        /// <summary>
        /// 註冊使用者資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IdentityResult> RegisterUser(RegisterViewModel model)
        {
            var nowTime = DateTime.Now;

            #region 資料建立
            // 註冊資料
            ApplicationUser user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = "test@sp88.com.tw",
                RealName = model.UserName,
                CreateTime = nowTime,
                PhoneNumber = "0912345678",
                PhoneNumberConfirmed = true,
                RegisterDate = nowTime,
                UpdateTime = nowTime,
                CreateUser = model.CreateUser
            };
            #endregion

            try
            {
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("AuthRepository RegisterUser Exception: {0} \n {1}", ex.Message.ToString(), ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// 確認使用者名稱是否重複
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckHasUserByName(string userName)
        {
            var user = _userManager.FindByName(userName);
            bool hasUser = !(user == null);
            return hasUser;
        }
        /// <summary>
        /// 確認該帳號是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool CheckHasUserByUserId(string userid)
        {
            var user = _userManager.FindById(userid);
            bool hasUser = !(user == null);
            return hasUser;
        }

        /// <summary>
        /// 依 Id 找出使用者
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public ApplicationUser FindById(string userid)
        {
            return _userManager.FindById(userid);
        }
        /// <summary>
        /// 依 Id 找出使用者
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindByIdAsync(string userid)
        {
            return await _userManager.FindByIdAsync(userid);
        }

        /// <summary>
        /// 依帳號找出使用者
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindByName(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        /// <summary>
        /// 依帳號、密碼找出使用者
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }

        /// <summary>
        /// 更新會員密碼
        /// </summary>
        /// <param name="username"></param>
        /// <param name="currentPassword"></param>
        /// <returns></returns>
        public async Task<IdentityResult> AutoGernerPassword(string username, string currentPassword)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(username);
            user.PasswordHash = currentPassword;
            return await _userManager.UpdateAsync(user);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
            _roleManager.Dispose();
        }
    }
}