using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.FontHome;
using ViewModels.Share;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;

namespace WebTemplateDB.Service
{
    public class FontHomeService : IFontHomeService
    {
        WebTemplateEntities _db;
        readonly IGenericRepository<FontHome> _fontHome;

        public FontHomeService()
        {
            _db = new WebTemplateEntities();
            _fontHome = new GenericRepository<FontHome>();
        }

        public async Task<DataVerityResult<FontHomeViewModel>> CreateFontHome(FontHomeViewModel model)
        {
            DataVerityResult<FontHomeViewModel> result = new DataVerityResult<FontHomeViewModel>();

            FontHome fonthome = new FontHome
            {
                ImageName = model.ImageName,
                FontHomeUrl = model.FontHomeUrl,
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime,
                Remark = model.Remark,
                Status = model.Status,
                CreateTime = DateTime.Now,
                CreateUser = model.CreateUser,
            };

            FontHomeViewModel fonthomedata = new FontHomeViewModel
            {
                ImageName = model.ImageName,
                FontHomeUrl = model.FontHomeUrl,
                StartDateTime = model.StartDateTime,
                EndDateTime = model.EndDateTime,
                Remark = model.Remark,
                Status = model.Status,
                CreateTime = DateTime.Now,
                CreateUser = model.CreateUser
            };

            try
            {
                _fontHome.Create(fonthome);
                result.Data = fonthomedata;
                result.Success = true;
                result.Message = "新增前台首頁圖片成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "新增前台首頁圖片失敗";
            }

            return await Task.Run(() => result);
        }
        /// <summary>
        /// 取得首頁管理列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<ResWithPaginationViewModel> GetFontHomeList(SearchModel model, PaginationViewModel pagination)
        {
            ResWithPaginationViewModel pageData = new ResWithPaginationViewModel();
            List<FontHomeViewModel> fonthome = new List<FontHomeViewModel>();

            var query = from a in _fontHome.GetAll().ToList()
                        select new FontHomeViewModel
                        {
                            FontHomeId = a.FontHomeId,
                            ImageName = a.ImageName,
                            FontHomeUrl = a.FontHomeUrl,
                            StartDateTime = a.StartDateTime,
                            EndDateTime = a.EndDateTime,
                            Remark = a.Remark,
                            Status = a.Status,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser,
                        };

            pageData.MaxDateTime = query.OrderByDescending(x => x.CreateTime).FirstOrDefault().CreateTime;
            pageData.MinDateTime = query.OrderBy(x => x.CreateTime).FirstOrDefault().CreateTime;
            pageData.MaxStartDate = query.OrderByDescending(x => x.StartDateTime).FirstOrDefault().StartDateTime;
            pageData.MinStartDate = query.OrderBy(x => x.EndDateTime).FirstOrDefault().EndDateTime;

            if (model.StartDateTime != null)
            {
                query = query.Where(x => x.CreateTime >= model.StartDateTime).ToList();
            }
            if (model.EndDateTime != null)
            {
                query = query.Where(x => x.CreateTime <= model.EndDateTime).ToList();
            }
            if (model.OnlineDateTime != null)
            {
                query = query.Where(x => x.StartDateTime <= model.OnlineDateTime && x.EndDateTime >= model.OnlineDateTime).ToList();
            }
            if (!string.IsNullOrEmpty(model.Query))
            {
                var QueryString = model.Query.Trim();

                switch (model.SearchEnum)
                {
                    case 0:
                        query = query.Where(x => x.ImageName.Trim().Contains(QueryString)).ToList();
                        break;
                    case 1:
                        query = query.Where(x => x.FontHomeUrl.ToLower().Contains(model.Query)).ToList();
                        break;
                    case 2:
                        query = query.Where(x => x.Remark.ToLower().Contains(model.Query)).ToList();
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
                .Select((a, index) => new FontHomeViewModel
                {
                    FontHomeId = a.FontHomeId,
                    ImageName = a.ImageName,
                    FontHomeUrl = a.FontHomeUrl,
                    StartDateTime = a.StartDateTime,
                    EndDateTime = a.EndDateTime,
                    Remark = a.Remark,
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
                fonthome = list;
            }

            pageData.Data = fonthome;
            pageData.Success = true;
            return await Task.Run(() => pageData);
        }
        /// <summary>
        /// 取得變更前台首頁圖片
        /// </summary>
        /// <returns></returns>
        public async Task<FontHomeViewModel> GetFontHomeItem(int FontHomeId)
        {
            FontHomeViewModel fonthome = new FontHomeViewModel();

            var query = from a in _fontHome.FindBy(x => x.FontHomeId == FontHomeId)
                        where a.FontHomeId == FontHomeId
                        select new FontHomeViewModel
                        {
                            FontHomeId = a.FontHomeId,
                            ImageName = a.ImageName,
                            FontHomeUrl = a.FontHomeUrl,
                            StartDateTime = a.StartDateTime,
                            EndDateTime = a.EndDateTime,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser,
                            Remark = a.Remark,
                            UpdateTime = a.UpdateTime,
                            Status = a.Status,
                            UpdateUser = a.UpdateUser,
                        };

            if (query.Any())
            {
                fonthome = query.SingleOrDefault();
            }

            return await Task.Run(() => fonthome);
        }
        /// <summary>
        /// 變更前台首頁圖片
        /// </summary>
        /// <returns></returns>
        public async Task<DataVerityResult<FontHomeViewModel>> EditFontHome(FontHomeViewModel model)
        {
            DataVerityResult<FontHomeViewModel> result = new DataVerityResult<FontHomeViewModel>();

            try
            {
                var query = _fontHome.FindBy(x => x.FontHomeId == model.FontHomeId);
                if (query.Any())
                {
                    FontHome fonthome = query.FirstOrDefault();
                    fonthome.ImageName = model.ImageName;
                    fonthome.FontHomeUrl = model.FontHomeUrl;
                    fonthome.StartDateTime = model.StartDateTime;
                    fonthome.EndDateTime = model.EndDateTime;
                    fonthome.Remark = model.Remark;
                    fonthome.Status = model.Status;
                    fonthome.UpdateTime = DateTime.Now;
                    fonthome.UpdateUser = model.UpdateUser;

                    _fontHome.Update(fonthome);
                    result.Success = true;
                    result.Message = "新增前台首頁圖片成功";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "新增前台首頁圖片失敗";
            }

            return await Task.Run(() => result);

        }

        public async Task<DataVerityResult> DeleteFontHome(int FontHomeId)
        {
            DataVerityResult result = new DataVerityResult();

            try
            {
                var data = _fontHome.FindBy(x => x.FontHomeId == FontHomeId).FirstOrDefault();
                if (data != null)
                {
                    _fontHome.Delete(data);
                    result.Success = true;
                    result.Message = "刪除前台首頁圖片成功";
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "刪除前台首頁圖片失敗";
            }

            return await Task.Run(() => result);

        }

        public async Task<List<FontHomeViewModel>> GetFontHomeList()
        {
            List<FontHomeViewModel> list = new List<FontHomeViewModel>();

            var query = from a in _fontHome.GetAll()
                        select new FontHomeViewModel
                        {
                            FontHomeId = a.FontHomeId,
                            ImageName = a.ImageName,
                            FontHomeUrl = a.FontHomeUrl,
                            StartDateTime = a.StartDateTime,
                            EndDateTime = a.EndDateTime,
                            Remark = a.Remark,
                            Status = a.Status,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser
                        };

            var DateNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            
            query = query.Where(x => x.StartDateTime <= DateNow);
            query = query.Where(x => x.EndDateTime >= DateNow);

            if (query.Any())
            {
                var data = query.ToList();
                list = data;
            }

            return await Task.Run(() => list);
        }
    }
}
