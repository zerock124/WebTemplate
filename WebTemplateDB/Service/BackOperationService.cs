using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.BackOperationRecord;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Repositories;
using WebTemplateDB.Models;
using ViewModels.Share;
using System.Data.Entity;

namespace WebTemplateDB.Service
{
    public class BackOperationService : IBackOperationService
    {
        WebTemplateEntities _db;
        IGenericRepository<BackOperation> _backoperation;
        IGenericRepository<AspNetUsers> _aspnetUser;
        IGenericRepository<AspNetRoles> _aspnetRole;
        IGenericRepository<AspNetUserRoles> _aspnetUserRole;

        public BackOperationService()
        {
            _db = new WebTemplateEntities();
            _backoperation = new GenericRepository<BackOperation>(_db);
            _aspnetUser = new GenericRepository<AspNetUsers>();
            _aspnetRole = new GenericRepository<AspNetRoles>();
            _aspnetUserRole = new GenericRepository<AspNetUserRoles>();
        }

        public async Task<VerityResult> CreateBackOperation(string Id, string ContentText, string IP)
        {
            var result = new VerityResult();

            try
            {
                var aspnetUser = _aspnetUser.FindBy(x => x.Id == Id).FirstOrDefault();
                var aspnetUserRole = _aspnetUserRole.FindBy(x => x.UserId == aspnetUser.Id).FirstOrDefault();
                var aspnetRole = _aspnetRole.FindBy(x => x.Id == aspnetUserRole.RoleId).FirstOrDefault();

                BackOperation actionItem = new BackOperation
                {
                    AspNetUserId = aspnetUser.Id,
                    RoleId = aspnetRole.Id,
                    ContentText = ContentText,
                    IP = IP,
                    Result = true,
                    CreateTime = DateTime.Now,
                    CreateUser = Id
                };

                _backoperation.Create(actionItem);

                result.Success = true;
                result.Message = "成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "失敗";
            }

            return await Task.Run(() => result);
        }

        public async Task<ResWithPaginationViewModel> GetBackOperationList(SearchModel searchModel, PaginationViewModel pagination) 
        {
            ResWithPaginationViewModel pageData = new ResWithPaginationViewModel();
            List<BackOperationViewModel> listItem = new List<BackOperationViewModel>();

            var query = from a in _db.BackOperation
                        join b in _db.AspNetUsers on a.AspNetUserId equals b.Id into aspnetUser
                        from b in aspnetUser.DefaultIfEmpty()
                        join c in _db.AspNetRoles on a.RoleId equals c.Id into aspnetRole
                        from c in aspnetRole.DefaultIfEmpty()
                        select new BackOperationViewModel
                        {
                            BackOperationId = a.BackOperationId,
                            AspNetUserId = a.AspNetUserId,
                            UserName = b.UserName,
                            RoleId = a.RoleId,
                            RoleName = c.Name,
                            ContentText = a.ContentText,
                            Result = a.Result,
                            IP = a.IP,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser
                        };

            pageData.MaxDateTime = query.OrderByDescending(x => x.CreateTime).FirstOrDefault().CreateTime;
            pageData.MinDateTime = query.OrderBy(x => x.CreateTime).FirstOrDefault().CreateTime;

            if (searchModel.StartDateTime != null) 
            {
                query = query.Where(x => x.CreateTime >= searchModel.StartDateTime);
            }
            if(searchModel.EndDateTime != null) 
            {
                query = query.Where(x => x.CreateTime <= searchModel.EndDateTime);
            }
            if(!string.IsNullOrEmpty(searchModel.Query)) 
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
                        query = query.Where(x => x.ContentText.ToLower().Trim().Contains(searchModel.Query));
                        break;
                    case 3:
                        query = query.Where(x => x.IP.ToLower().Trim().Contains(searchModel.Query));
                        break;
                    default:
                        break;
                }
            }

            int _TotalCount = query.Count();
            pageData.Pagination = new PaginationViewModel
            {
                PerPage = pagination.PerPage,
                CurrentPage = pagination.CurrentPage,
                TotalCounts = _TotalCount
            };

            query = query
                .OrderBy(x => x.BackOperationId)
                .Skip(pagination.GetSkipLength())
                .Take(pagination.PerPage);

            if (query.Any()) 
            {
                var list = await query.OrderBy(x => x.BackOperationId).ToListAsync();
                listItem = list;
            }

            pageData.Data = listItem;
            pageData.Success = true;
            return await Task.Run(() => pageData);
        }
    }
}
