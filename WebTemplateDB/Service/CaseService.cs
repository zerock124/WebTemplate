using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Case;
using ViewModels.Share;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;

namespace WebTemplateDB.Service
{
    public class CaseService : ICaseService
    {

        WebTemplateEntities _db;
        readonly IGenericRepository<Case> _case;

        public CaseService()
        {
            _db = new WebTemplateEntities();
            _case = new GenericRepository<Case>();
        }
        /// <summary>
        /// 新增案例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<VerityResult> CreateCase(CaseViewModel model)
        {
            DataVerityResult<CaseViewModel> result = new DataVerityResult<CaseViewModel>();

            Case caseitem = new Case
            {
                ImageName = model.ImageName,
                CaseUrl = model.CaseUrl,
                CaseEnum = model.CaseEnum,
                CaseName = model.CaseName,
                CaseContent = model.CaseContent,
                Status = model.Status,
                CreateTime = DateTime.Now,
                CreateUser = model.CreateUser,
                LabelTag = model.LabelTag
            };

            try
            {
                _case.Create(caseitem);
                result.Success = true;
                result.Message = "新增案例圖片成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "新增案例圖片失敗";
            }

            return await Task.Run(() => result);
        }
        /// <summary>
        /// 取得案例介紹列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<ResWithPaginationViewModel> GetCaseList(SearchModel model, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel pageData = new ResWithPaginationViewModel();
            List<CaseViewModel> caseList = new List<CaseViewModel>();

            var query = from a in _case.GetAll().ToList()
                        select new CaseViewModel
                        {
                            CaseId = a.CaseId,
                            ImageName = a.ImageName,
                            CaseUrl = a.CaseUrl,
                            CaseEnum = a.CaseEnum,
                            CaseName = a.CaseName,
                            CaseContent = a.CaseContent,
                            Status = a.Status,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser
                        };

            pageData.MaxDateTime = query.OrderByDescending(x => x.CreateTime).FirstOrDefault().CreateTime;
            pageData.MinDateTime = query.OrderBy(x => x.CreateTime).FirstOrDefault().CreateTime;

            if (model.StartDateTime != null)
            {
                query = query.Where(x => x.CreateTime >= model.StartDateTime);
            }
            if (model.EndDateTime != null)
            {
                query = query.Where(x => x.CreateTime <= model.EndDateTime);
            }
            if (!string.IsNullOrEmpty(model.Query))
            {
                var QueryString = model.Query.ToLower().Trim();

                switch (model.SearchEnum)
                {
                    case 0:
                        if ("行銷活動".Contains(QueryString))
                        {
                            int Enum = 0;
                            query = query.Where(x => x.CaseEnum == Enum);
                        }
                        if ("臉書機器人".Contains(QueryString))
                        {
                            int Enum = 1;
                            query = query.Where(x => x.CaseEnum == Enum);
                        }
                        if ("官網".Contains(QueryString))
                        {
                            int Enum = 2;
                            query = query.Where(x => x.CaseEnum == Enum);
                        }
                        break;
                    case 1:
                        query = query.Where(x => x.CaseName.ToLower().Trim().Contains(QueryString));
                        break;
                    case 2:
                        query = query.Where(x => x.CaseContent.ToLower().Trim().Contains(QueryString));
                        break;
                    default:
                        break;
                }
            }

            /**取得分頁資訊*/
            int _TotalCounts = query.Count();
            pageData.Pagination = new PaginationViewModel
            {
                PerPage = pagination.PerPage,
                CurrentPage = pagination.CurrentPage,
                TotalCounts = _TotalCounts
            };

            /**設置分頁資訊*/
            query = query
                .OrderByDescending(o => o.CreateTime)
                .Select((a, index) => new CaseViewModel
                {
                    CaseId = a.CaseId,
                    ImageName = a.ImageName,
                    CaseUrl = a.CaseUrl,
                    CaseEnum = a.CaseEnum,
                    CaseName = a.CaseName,
                    CaseContent = a.CaseContent,
                    Status = a.Status,
                    CreateTime = a.CreateTime,
                    CreateUser = a.CreateUser,
                    UpdateTime = a.UpdateTime,
                    UpdateUser = a.UpdateUser,
                    Number = index + 1
                })
                .Skip(pagination.GetSkipLength())
                .Take(pagination.PerPage);

            if (query.Any())
            {
                var list = query.OrderByDescending(x => x.CreateTime).ToList();
                caseList = list;
            }

            pageData.Data = caseList;
            pageData.Success = true;
            return await Task.Run(() => pageData);
        }
        /// <summary>
        /// 取得變更案例介紹圖片
        /// </summary>
        /// <param name="CaseId"></param>
        /// <returns></returns>
        public async Task<CaseViewModel> GetEditCaseItem(int CaseId)
        {
            CaseViewModel caseitem = new CaseViewModel();

            var query = from a in _case.FindBy(x => x.CaseId == CaseId)
                        select new CaseViewModel
                        {
                            CaseId = a.CaseId,
                            ImageName = a.ImageName,
                            CaseUrl = a.CaseUrl,
                            CaseName = a.CaseName,
                            CaseContent = a.CaseContent,
                            CaseEnum = a.CaseEnum,
                            Status = a.Status,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser,
                            LabelTag = a.LabelTag
                        };
            if (query.Any())
            {
                caseitem = query.FirstOrDefault();
            }

            return await Task.Run(() => caseitem);
        }
        /// <summary>
        /// 變更案例介紹圖片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<VerityResult> EditCaseItem(CaseViewModel model)
        {
            VerityResult result = new VerityResult();

            try
            {
                var query = _case.FindBy(x => x.CaseId == model.CaseId);

                if (query.Any())
                {
                    Case caseitem = query.FirstOrDefault();
                    caseitem.CaseUrl = model.CaseUrl;
                    caseitem.CaseEnum = model.CaseEnum;
                    caseitem.CaseName = model.CaseName;
                    caseitem.CaseContent = model.CaseContent;
                    caseitem.Status = model.Status;
                    caseitem.UpdateTime = DateTime.Now;
                    caseitem.UpdateUser = model.UpdateUser;
                    caseitem.LabelTag = model.LabelTag;
                    caseitem.ImageName = model.ImageName;

                    _case.Update(caseitem);
                    result.Success = true;
                    result.Message = "變更案列介紹成功";
                }
            }
            catch
            {
                result.Success = false;
                result.Message = "變更案列介紹失敗";
            }

            return await Task.Run(() => result);
        }
        /// <summary>
        /// 刪除案例介紹圖片
        /// </summary>
        /// <param name="CaseId"></param>
        /// <returns></returns>
        public async Task<VerityResult> DeleteCaseItem(int CaseId)
        {
            VerityResult result = new VerityResult();

            try
            {
                var query = _case.FindBy(x => x.CaseId == CaseId);

                if (query.Any())
                {
                    Case caseitem = query.FirstOrDefault();

                    _case.Delete(caseitem);
                    result.Success = true;
                    result.Message = "刪除案列介紹成功";
                }
            }
            catch
            {
                result.Success = false;
                result.Message = "刪除案列介紹失敗";
            }

            return await Task.Run(() => result);
        }
        /// <summary>
        /// 取得案例介紹列表 -- API Service
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<CaseViewModel>> GetCaseList(APISearchModel model)
        {
            List<CaseViewModel> list = new List<CaseViewModel>();

            var query = from a in _case.GetAll()
                        select new CaseViewModel
                        {
                            CaseId = a.CaseId,
                            ImageName = a.ImageName,
                            CaseUrl = a.CaseUrl,
                            CaseEnum = a.CaseEnum,
                            CaseName = a.CaseName,
                            CaseContent = a.CaseContent,
                            Status = a.Status,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser
                        };

            if (!string.IsNullOrEmpty(model.LabelTag))
            {
                query = query.Where(x => x.LabelTag.Trim().ToLower().Contains(model.LabelTag));
            }
            if (!string.IsNullOrEmpty(model.CaseEnum))
            {
                var Casenumber = Convert.ToInt32(model.CaseEnum);
                query = query.Where(x => x.CaseEnum == Casenumber);
            }
            if (query.Any())
            {
                var data = query.ToList();
                list = data;
            }
            return await Task.Run(() => list);
        }
    }
}
