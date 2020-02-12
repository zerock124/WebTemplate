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
        public async Task<PageDataVerityResult> GetFontHomeList(SearchModel model, PaginationViewModel pagination)
        {
            PageDataVerityResult pageData = new PageDataVerityResult();
            List<FontHomeViewModel> fonthome = new List<FontHomeViewModel>();

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

            if (model.StartDateTime != null)
            {
                query = query.Where(x => x.StartDateTime >= model.StartDateTime);
            }
            if (model.EndDateTime != null)
            {
                query = query.Where(x => x.EndDateTime <= model.EndDateTime);
            }
            if (!string.IsNullOrEmpty(model.Query))
            {
                var QueryString = model.Query.Trim();

                switch (model.SearchEnum)
                {
                    case 0:
                        query = query.Where(x => x.ImageName.Trim().Contains(QueryString));
                        break;
                    case 1:
                        query = query.Where(x => x.FontHomeUrl.ToLower().Contains(model.Query));
                        break;
                    case 2:
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
                var list = await query.OrderBy(x=>x.FontHomeId).ToListAsync();
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

    }
}
