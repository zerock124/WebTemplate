using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Authority;
using ViewModels.Share;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;
using Microsoft.AspNet.Identity;

namespace WebTemplateDB.Service
{
    public class AuthorityService : IAuthorityService
    {
        WebTemplateEntities _db;
        protected IGenericRepository<AspNetUsers> _aspnetUser;
        protected IGenericRepository<AspNetRoles> _aspnetRole;
        protected IGenericRepository<AspNetUserRoles> _aspnetUserRole;
        PasswordHasher hasher;

        public AuthorityService()
        {
            _db = new WebTemplateEntities();
            _aspnetUser = new GenericRepository<AspNetUsers>();
            _aspnetRole = new GenericRepository<AspNetRoles>();
            _aspnetUserRole = new GenericRepository<AspNetUserRoles>();

            hasher = new PasswordHasher();
        }

        public async Task<ResWithPaginationViewModel> GetAuthorityList(SearchModel searchModel, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel pageData = new ResWithPaginationViewModel();
            List<AuthorityViewModel> userlist = new List<AuthorityViewModel>();

            var query = from a in _db.AspNetUsers
                        join b in _db.AspNetUserRoles on a.Id equals b.UserId into userRole
                        from b in userRole.DefaultIfEmpty()
                        join c in _db.AspNetRoles on b.RoleId equals c.Id into role
                        from c in role.DefaultIfEmpty()
                        join d in _db.AspNetUsers on a.CreateUser equals d.Id into user
                        from d in user.DefaultIfEmpty()
                        select new AuthorityViewModel
                        {
                            Id = a.Id,
                            RealName = a.RealName,
                            UserName = a.UserName,
                            RoleId = c.Id,
                            RoleName = c.Name,
                            RegisterDate = a.RegisterDate,
                            CreateTime = a.CreateTime,
                            CreateUser = d.UserName,
                            UpdateTime = a.UpdateTime,
                        };

            pageData.MaxDateTime = query.OrderByDescending(x => x.CreateTime).FirstOrDefault().CreateTime;
            pageData.MinDateTime = query.OrderBy(x => x.CreateTime).FirstOrDefault().CreateTime;

            if (searchModel.StartDateTime != null)
            {
                query = query.Where(x => x.CreateTime >= searchModel.StartDateTime);
            }
            if (searchModel.EndDateTime != null)
            {
                query = query.Where(x => x.CreateTime <= searchModel.EndDateTime);
            }
            if (!string.IsNullOrEmpty(searchModel.Query))
            {
                switch (searchModel.SearchEnum)
                {
                    case 0:
                        query = query.Where(x => x.UserName.ToLower().Trim().Contains(searchModel.Query));
                        break;
                    case 1:
                        query = query.Where(x => x.RoleName.ToLower().Trim().Contains(searchModel.Query));
                        break;
                    case 2:
                        query = query.Where(x => x.CreateUser.ToLower().Trim().Contains(searchModel.Query));
                        break;
                    default:
                        break;
                }
            }


            var _TotalCount = query.Count();
            pageData.Pagination = new PaginationViewModel
            {
                PerPage = pagination.PerPage,
                CurrentPage = pagination.CurrentPage,
                TotalCounts = _TotalCount
            };

            query = query
                .OrderBy(x => x.CreateTime)
                .Skip(pagination.GetSkipLength())
                .Take(pagination.PerPage);

            if (query.Any())
            {
                var list = await query.OrderBy(x => x.CreateTime).ToListAsync();
                userlist = list;
            }

            pageData.Data = userlist;
            pageData.Success = true;
            return await Task.Run(() => pageData);
        }

        public async Task<List<AspNetUserRolesViewModel>> GetRoleOptions()
        {
            List<AspNetUserRolesViewModel> listitem = new List<AspNetUserRolesViewModel>();

            var query = from a in _aspnetRole.GetAll()
                        select new AspNetUserRolesViewModel
                        {
                            Id = a.Id,
                            Name = a.Name
                        };

            if (query.Any())
            {
                var list = query.OrderBy(x => x.Id).ToList();
                listitem = list;
            }
            return await Task.Run(() => listitem);
        }
        public async Task<VerityResult> CheckAuthority(string Id, string CurrendUserid)
        {
            VerityResult result = new VerityResult();

            bool checkAuthority = false;
            bool IsAdmin = false;

            var checkAdmin = from a in _db.AspNetUserRoles.Where(x => x.UserId == CurrendUserid)
                             join b in _db.AspNetRoles on a.RoleId equals b.Id
                             where b.Name == "admin"
                             select a;

            if (checkAdmin.Any()) 
            {
                IsAdmin = true;
            }

            if (Id == CurrendUserid) 
            {
                checkAuthority = true;
            }

            if (checkAuthority || IsAdmin)
            {
                result.Success = true;
                result.Message = "您有此權限";
            }
            else 
            {
                result.Success = false;
                result.Message = "您無此權限";
            }
            return await Task.Run(() => result);
        }

        public async Task<AuthorityViewModel> GetEditAuthorityItem(string Id, string CurrendUserid)
        {
            AuthorityViewModel item = new AuthorityViewModel();


            var query = from a in _db.AspNetUsers.Where(x => x.Id == Id)
                        join b in _db.AspNetUserRoles on a.Id equals b.UserId into userRole
                        from b in userRole.DefaultIfEmpty()
                        join c in _db.AspNetRoles on b.RoleId equals c.Id into role
                        from c in role.DefaultIfEmpty()
                        join d in _db.AspNetUsers on a.CreateUser equals d.Id into user
                        from d in user.DefaultIfEmpty()
                        select new AuthorityViewModel
                        {
                            Id = a.Id,
                            RealName = a.RealName,
                            UserName = a.UserName,
                            RoleId = c.Id,
                            RoleName = c.Name,
                            RegisterDate = a.RegisterDate,
                            CreateTime = a.CreateTime,
                            CreateUser = d.UserName,
                            UpdateTime = a.UpdateTime,
                        };

            if (query.Any())
            {
                var list = query.FirstOrDefault();
                item = list;
            }
            return await Task.Run(() => item);

        }

        public async Task<VerityResult> EditAuthorityItem(AuthorityViewModel model)
        {
            VerityResult result = new VerityResult();

            try
            {
                var query = _aspnetUser.FindBy(x => x.Id == model.Id);

                if (query.Any())
                {
                    var list = query.FirstOrDefault();
                    AspNetUsers item = list;
                    item.UserName = model.UserName;
                    string hashPwd = hasher.HashPassword(model.Password);
                    item.PasswordHash = hashPwd;
                    item.UpdateTime = DateTime.Now;
                    _aspnetUser.Update(item);
                }

                var findUserRoles = _aspnetUserRole.FindBy(x => x.UserId == model.Id);
                if (findUserRoles.Any())
                {
                    var list = findUserRoles.FirstOrDefault();
                    AspNetUserRoles item = list;
                    item.UserId = model.Id;
                    item.RoleId = model.RoleId;
                    _aspnetUserRole.Update(item);
                }

                result.Success = true;
                result.Message = "編輯帳號資料成功";
            }

            catch
            {
                result.Success = false;
                result.Message = "編輯帳號資料失敗";
            }

            return await Task.Run(() => result);
        }

        public async Task<VerityResult> DeleteAuthorityItem(string Id)
        {
            DataVerityResult result = new DataVerityResult();

            try
            {
                var query = _aspnetUser.FindBy(x => x.Id == Id);
                var findUserRoles = _aspnetUserRole.FindBy(x => x.UserId == Id);
                if (query.Any() && findUserRoles.Any())
                {
                    AspNetUsers item = query.FirstOrDefault();
                    _aspnetUser.Delete(item);

                    AspNetUserRoles userrolesItem = findUserRoles.FirstOrDefault();
                    _aspnetUserRole.Delete(userrolesItem);

                    result.Success = true;
                    result.Message = "刪除最新消息成功";
                }
            }
            catch
            {
                result.Success = false;
                result.Message = "刪除最新消息失敗";
            }

            return await Task.Run(() => result);
        }

    }
}
