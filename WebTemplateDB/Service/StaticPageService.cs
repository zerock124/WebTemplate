using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.StaticPage;
using ViewModels.Share;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;

namespace WebTemplateDB.Service
{
    public class StaticPageService: IStaticPageService
    {
        WebTemplateEntities _db;
        readonly IGenericRepository<StaticPage> _staticPage;

        public StaticPageService()
        {
            _db = new WebTemplateEntities();
            _staticPage = new GenericRepository<StaticPage>(_db);
        }
        /// <summary>
        /// 取得靜態頁列表內容
        /// </summary>
        /// <returns></returns>
        public async Task<List<StaticPageViewModel>> GetStaticPageList() 
        {
            List<StaticPageViewModel> StaticPageList = new List<StaticPageViewModel>();

            var query = from a in _staticPage.GetAll()
                        select new StaticPageViewModel
                        {
                            StaticPageId = a.StaticPageId,
                            StaticPageEnum = a.StaticPageEnum,
                            PageContent = a.PageContent,
                            CreateUser = a.CreateUser,
                            CreateTime = a.CreateTime,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser
                        };

            if (query.Any())
            {
                StaticPageList = query.ToList();
            }

            return await Task.Run(() => StaticPageList);
        }
        /// <summary>
        /// 新增靜態頁內容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DataVerityResult<StaticPageViewModel>> CreateStaticPage(StaticPageViewModel model) 
        {
            DataVerityResult<StaticPageViewModel> result = new DataVerityResult<StaticPageViewModel>();

            StaticPage staticpage = new StaticPage
            {
                StaticPageEnum = (int)model.StaticPageEnum,
                PageContent = model.PageContent,
                CreateTime = DateTime.Now,
                CreateUser = model.CreateUser
            };

            try
            {
                _staticPage.Create(staticpage);

                StaticPageViewModel staticpageItem = new StaticPageViewModel
                {
                    StaticPageId = staticpage.StaticPageId,
                    StaticPageEnum = staticpage.StaticPageEnum,
                    PageContent = staticpage.PageContent,
                    CreateTime = staticpage.CreateTime,
                    CreateUser = staticpage.CreateUser,
                    UpdateUser = staticpage.UpdateUser,
                    UpdateTime = staticpage.UpdateTime
                };

                result.Data = staticpageItem;
                result.Success = true;
                result.Message = "新增靜態頁-公司介紹-成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "新增靜態頁-公司介紹-失敗";
            }

            return await Task.Run(() => result);
        }
        /// <summary>
        /// 變更靜態頁內容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<DataVerityResult<StaticPageViewModel>> EditStaticPage(StaticPageViewModel model)
        {
            DataVerityResult<StaticPageViewModel> result = new DataVerityResult<StaticPageViewModel>();

            try
            {
                var staticpage = _staticPage.FindBy(x => x.StaticPageEnum == model.StaticPageEnum).FirstOrDefault();

                staticpage.PageContent = model.PageContent;
                staticpage.UpdateTime = DateTime.Now;
                staticpage.UpdateUser = model.UpdateUser;

                _staticPage.Update(staticpage);

                StaticPageViewModel staticpageItem = new StaticPageViewModel
                {
                    StaticPageId = staticpage.StaticPageId,
                    StaticPageEnum = staticpage.StaticPageEnum,
                    PageContent = staticpage.PageContent,
                    CreateTime = staticpage.CreateTime,
                    CreateUser = staticpage.CreateUser,
                    UpdateUser = staticpage.UpdateUser,
                    UpdateTime = staticpage.UpdateTime
                };

                result.Data = staticpageItem;
                result.Success = true;
                result.Message = "新增靜態頁-公司介紹-成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "新增靜態頁-公司介紹-失敗";
            }

            return await Task.Run(() => result);
        }
        /// <summary>
        /// 取得靜態頁內容 -- API Service
        /// </summary>
        /// <param name="StaticPageEnum"></param>
        /// <returns></returns>
        public async Task<StaticPageViewModel> GetStaticPage(int StaticPageEnum) 
        {
            StaticPageViewModel item = new StaticPageViewModel();

            var query = from a in _staticPage.GetAll()
                        where a.StaticPageEnum == StaticPageEnum
                        select new StaticPageViewModel
                        {
                            StaticPageId = a.StaticPageId,
                            StaticPageEnum = a.StaticPageEnum,
                            PageContent = a.PageContent,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser
                        };

            if (query.Any()) 
            {
                var data = query.FirstOrDefault();
                item = data;
            }

            return await Task.Run(() => item);
        }
    }
}
