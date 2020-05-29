using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Home;
using ViewModel.WebConfig;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;

namespace WebTemplateDB.Service
{
    public class HomeService : IHomeService
    {
        WebTemplateEntities _db;
        readonly IGenericRepository<LatestNews> _latestNews;
        readonly IGenericRepository<Case> _case;
        readonly IGenericRepository<Contact> _contact;
        readonly IGenericRepository<WebConfig> _webconfig;

        public HomeService()
        {
            _db = new WebTemplateEntities();
            _latestNews = new GenericRepository<LatestNews>(_db);
            _case = new GenericRepository<Case>(_db);
            _contact = new GenericRepository<Contact>(_db);
            _webconfig = new GenericRepository<WebConfig>(_db);
        }
        /// <summary>
        /// 取得HomeData
        /// </summary>
        /// <returns></returns>
        public async Task<HomeViewModel> GetHomeData()
        {
            HomeViewModel home = new HomeViewModel();
            WebConfigViewModel webconfigItem = new WebConfigViewModel();

            var query = from a in _webconfig.GetAll()
                        select new WebConfigViewModel
                        {
                            Id = a.Id,
                            WebName = a.WebName,
                            ServiceMail = a.ServiceMail,
                            Copyright = a.Copyright,
                            SystemEmailName = a.SystemEmailName,
                            SystemEmail = a.SystemEmail,
                            meta_title = a.meta_title,
                            meta_keyword = a.meta_keyword,
                            meta_description = a.meta_description,
                            meta_url = a.meta_url,
                            meta_image = a.meta_image
                        };
                
            if (query.Any())
            {
                var data = query.FirstOrDefault();
                webconfigItem = data;
            }

            home.WebConfig = webconfigItem;
            home.LatestNewsNumber = _latestNews.GetAll().Count();
            home.CaseNumber = _case.GetAll().Count();
            home.ContactNumber = _contact.GetAll().Count();

            return await Task.Run(() => home);
        }
    }
}
