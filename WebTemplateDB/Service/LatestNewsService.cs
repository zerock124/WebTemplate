using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.LatestNews;
using ViewModels.Share;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;

namespace WebTemplateDB.Service
{
    public class LatestNewsService : ILatestNewsService
    {
        WebTemplateEntities _db;
        readonly IGenericRepository<LatestNews> _latestnews;

        public LatestNewsService()
        {
            _db = new WebTemplateEntities();
            _latestnews = new GenericRepository<LatestNews>();
        }

        public async Task<DataVerityResult<LatestNewsViewModel>> CreateLatestNews(LatestNewsViewModel model)
        {
            DataVerityResult<LatestNewsViewModel> result = new DataVerityResult<LatestNewsViewModel>();

            LatestNews createLatestNews = new LatestNews
            {
                ImageName = model.ImageName,
                LatestNewsEnum = model.LatestNewsEnum,
                StartDateTime = model.StartDateTime,
                LatestNewsTitle = model.LatestNewsTitle,
                LatestNewsContent = model.LatestNewsContent,
                Remark = model.Remark,
                Status = model.Status,
                CreateTime = DateTime.Now,
                CreateUser = model.CreateUser
            };

            LatestNewsViewModel LatestNews = new LatestNewsViewModel
            {
                ImageName = model.ImageName,
                LatestNewsEnum = model.LatestNewsEnum,
                StartDateTime = model.StartDateTime,
                LatestNewsTitle = model.LatestNewsTitle,
                LatestNewsContent = model.LatestNewsContent,
                Remark = model.Remark,
                Status = model.Status,
                CreateTime = DateTime.Now,
                CreateUser = model.CreateUser
            };

            try
            {
                _latestnews.Create(createLatestNews);

                result.Data = LatestNews;
                result.Success = true;
                result.Message = "新增最新消息成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "新增最新消息失敗";
            }

            return await Task.Run(() => result);
        }

        public async Task<ResWithPaginationViewModel> GetLatestNewsList(SearchModel model, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel pageData = new ResWithPaginationViewModel();
            List<LatestNewsViewModel> latestNews = new List<LatestNewsViewModel>();

            var query = from a in _latestnews.GetAll()
                        select new LatestNewsViewModel
                        {
                            LatestNewsId = a.LatestNewsId,
                            ImageName = a.ImageName,
                            LatestNewsEnum = (int)a.LatestNewsEnum,
                            StartDateTime = a.StartDateTime,
                            LatestNewsTitle = a.LatestNewsTitle,
                            LatestNewsContent = a.LatestNewsContent,
                            Remark = a.Remark,
                            Status = a.Status,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser
                        };

            pageData.MaxDateTime = query.OrderByDescending(x => x.CreateTime).FirstOrDefault().CreateTime;
            pageData.MinDateTime = query.OrderBy(x => x.CreateTime).FirstOrDefault().CreateTime;
            pageData.MaxStartDate = query.OrderByDescending(x => x.StartDateTime).FirstOrDefault().StartDateTime;
            pageData.MinStartDate = query.OrderBy(x => x.StartDateTime).FirstOrDefault().StartDateTime;

            if (model.StartDateTime != null)
            {
                query = query.Where(x => x.CreateTime >= model.StartDateTime);
            }
            if (model.EndDateTime != null)
            {
                query = query.Where(x => x.CreateTime <= model.EndDateTime);
            }
            if (model.StartOnlineDateTime != null)
            {
                query = query.Where(x => x.StartDateTime >= model.StartOnlineDateTime);
            }
            if (model.EndOnlineDateTime != null)
            {
                query = query.Where(x => x.StartDateTime <= model.EndOnlineDateTime);
            }
            if (!string.IsNullOrEmpty(model.Query))
            {
                var QueryString = model.Query.Trim();

                switch (model.SearchEnum)
                {
                    case 0:
                        query = query.Where(x => x.LatestNewsEnum == 0);
                        break;
                    case 1:
                        query = query.Where(x => x.LatestNewsTitle.ToLower().Contains(model.Query));
                        break;
                    case 2:
                        query = query.Where(x => x.LatestNewsContent.ToLower().Contains(model.Query));
                        break;
                    case 3:
                        query = query.Where(x => x.Remark.ToLower().Contains(model.Query));
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
                .Skip(pagination.GetSkipLength())
                .Take(pagination.PerPage);

            if (query.Any())
            {
                var list = await query.OrderByDescending(x => x.StartDateTime).ToListAsync();
                latestNews = list;
            }

            pageData.Data = latestNews;
            pageData.Success = true;
            return await Task.Run(() => pageData);
        }

        public async Task<LatestNewsViewModel> GetLatestNewsItem(int LatestNewsId)
        {
            LatestNewsViewModel latestNews = new LatestNewsViewModel();

            var query = from a in _latestnews.FindBy(x => x.LatestNewsId == LatestNewsId)
                        select new LatestNewsViewModel
                        {
                            LatestNewsId = a.LatestNewsId,
                            ImageName = a.ImageName,
                            LatestNewsEnum = (int)a.LatestNewsEnum,
                            StartDateTime = a.StartDateTime,
                            LatestNewsTitle = a.LatestNewsTitle,
                            LatestNewsContent = a.LatestNewsContent,
                            Status = a.Status,
                            Remark = a.Remark,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser
                        };
            if (query.Any())
            {
                latestNews = query.SingleOrDefault();
            }

            return await Task.Run(() => latestNews);
        }

        public async Task<VerityResult> EditLatestNewsItem(LatestNewsViewModel model)
        {
            DataVerityResult result = new DataVerityResult();

            try
            {
                var query = _latestnews.FindBy(x => x.LatestNewsId == model.LatestNewsId);
                if (query.Any())
                {
                    LatestNews latestNews = query.FirstOrDefault();
                    latestNews.LatestNewsEnum = model.LatestNewsEnum;
                    latestNews.StartDateTime = model.StartDateTime;
                    latestNews.LatestNewsTitle = model.LatestNewsTitle;
                    latestNews.LatestNewsContent = model.LatestNewsContent;
                    latestNews.Remark = model.Remark;
                    latestNews.Status = model.Status;
                    latestNews.UpdateTime = DateTime.Now;
                    latestNews.UpdateUser = model.UpdateUser;

                    _latestnews.Update(latestNews);

                    result.Success = true;
                    result.Message = "變更最新消息成功";
                }
            }
            catch
            {
                result.Success = false;
                result.Message = "變更最新消息失敗";
            }

            return await Task.Run(() => result);
        }

        public async Task<VerityResult> DeleteLatestNewsItem(int LatestNewsId)
        {
            DataVerityResult result = new DataVerityResult();

            try
            {
                var query = _latestnews.FindBy(x => x.LatestNewsId == LatestNewsId).FirstOrDefault();
                if (query != null)
                {
                    _latestnews.Delete(query);

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

        public async Task<List<LatestNewsViewModel>> GetLatestNewsList()
        {
            List<LatestNewsViewModel> list = new List<LatestNewsViewModel>();

            var query = from a in _latestnews.GetAll()
                        select new LatestNewsViewModel
                        {
                            LatestNewsId = a.LatestNewsId,
                            ImageName = a.ImageName,
                            LatestNewsEnum = (int)a.LatestNewsEnum,
                            StartDateTime = a.StartDateTime,
                            LatestNewsTitle = a.LatestNewsTitle,
                            LatestNewsContent = a.LatestNewsContent,
                            Remark = a.Remark,
                            Status = a.Status,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser
                        };

            if (query.Any())
            {
                var data = query.ToList();
                list = data;
            }

            return await Task.Run(() => list);
        }
    }
}
