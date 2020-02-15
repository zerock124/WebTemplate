using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Home;
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

        public HomeService()
        {
            _db = new WebTemplateEntities();
            _latestNews = new GenericRepository<LatestNews>();
            _case = new GenericRepository<Case>();
        }

        public async Task<HomeViewModel> GetHomeData()
        {
            HomeViewModel home = new HomeViewModel();

            home.LatestNewsNumber = _latestNews.GetAll().Count();
            home.CaseNumber = _case.GetAll().Count();

            return await Task.Run(() => home);
        }
    }
}
