using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.Authority;
using ViewModels.Share;
using ViewModels.Verity;
using webTemplate.Models;
using webTemplate.Utility;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;
using WebTemplateDB.Service;

namespace webTemplate.Controllers
{
    public class AuthorityController : BaseController
    {

        protected IAuthorityService _authorityService;
        protected IBackOperationService _backOperationService;
        protected IGenericRepository<AspNetRoles> _aspnetRoles;
        protected IGenericRepository<AspNetUserRoles> _aspnetUserRoles;
        protected IGenericRepository<AspNetUsers> _aspnetUser;

        string OperationName = "權限管理，";

        public AuthorityController()
        {
            _authorityService = new AuthorityService();
            _backOperationService = new BackOperationService();
            _aspnetRoles = new GenericRepository<AspNetRoles>();
            _aspnetUserRoles = new GenericRepository<AspNetUserRoles>();
            _aspnetUser = new GenericRepository<AspNetUsers>();

        }

        // GET: Authority
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult Edit()
        {
            return View();
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// 取得角色清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetRoleOptions()
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var data = await _authorityService.GetRoleOptions();
                res.Data = data;
                res.Success = true;
                res.Message = "取得角色清單成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "取得角色清單", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 取得權限管理的列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateJsonAntiForgeryToken] //Ajax防止CSRF攻擊的方法
        public async Task<JsonResult> GetAuthorityList(SearchModel searchModel, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel res = new ResWithPaginationViewModel();

            try
            {
                var result = await _authorityService.GetAuthorityList(searchModel, pagination);
                res.Data = result.Data;
                res.Pagination = result.Pagination;
                res.MinDateTime = result.MinDateTime;
                res.MaxDateTime = result.MaxDateTime;
                res.Success = true;
                res.Message = "取得權限管理列表成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "查詢", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 確認權限是否可以編輯此帳號
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> CheckAuthority(string Id)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _authorityService.CheckAuthority(Id, CurrendUserid);
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "取得編輯資料", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> CreateUser(CreateUserViewModel model)
        {
            ResponseViewModel res = new ResponseViewModel();

            if (!ModelState.IsValid)
            {
                res.Success = false;
                res.Data = ModelState.Values
                    .SelectMany(state => state.Errors)
                    .Select(error => error.ErrorMessage);
                res.Message = "資料驗證有誤";
                return Json(res, JsonRequestBehavior.DenyGet);
            }

            try
            {
                var hasUser = _repository.CheckHasUserByName(model.UserName);
                if (hasUser)
                {
                    res.Success = false;
                    res.Message = "已有此帳戶存在！";
                    return Json(res, JsonRequestBehavior.DenyGet);
                }

                var CreateUser = new RegisterViewModel
                {
                    UserName = model.UserName,
                    Email = "test@sp88.com.tw",
                    RealName = model.UserName,
                    PhoneNumber = "0912345678",
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword,
                    RoleId = model.RoleId,
                    CreateUser = CurrendUserid
                };

                var result = await _repository.RegisterUser(CreateUser);

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

                    var user = await _repository.FindByName(model.UserName);

                    await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "新增", CurrendUserIp);

                    res.Data = user;
                }

                res.Message = result.Succeeded ? "註冊成功" : "註冊失敗";
                res.Success = result.Succeeded;
            }
            catch (Exception ex)
            {
                res.Success = false;
                res.Message = "伺服器發生錯誤";
            }

            return Json(res, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 取得編輯使用者的資訊
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> GetEditAuthorityItem(string Id)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var date = await _authorityService.GetEditAuthorityItem(Id, CurrendUserid);
                res.Data = date;
                res.Success = true;
                res.Message = "取得帳號資料成功";
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "取得編輯資料", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 編輯使用者
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> EditAuthorityItem(AuthorityViewModel model)
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _authorityService.EditAuthorityItem(model);
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "編輯", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "與伺服器連線發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        /// 刪除使用者
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<JsonResult> DeleteAuthorityItem(string Id) 
        {
            ResponseViewModel res = new ResponseViewModel();

            try
            {
                var result = await _authorityService.DeleteAuthorityItem(Id);
                res.Success = result.Success;
                res.Message = result.Message;
                res.HttpStatusCode = System.Net.HttpStatusCode.OK;
                await _backOperationService.CreateBackOperation(CurrendUserid, OperationName + "刪除", CurrendUserIp);
            }
            catch
            {
                res.Success = false;
                res.Message = "伺服器發生錯誤";
                res.HttpStatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            res.ResponseTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            return Json(res, JsonRequestBehavior.DenyGet);
        }
    }
}