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
    public class MenuUIService : IMenuUIService
    {
        private IGenericRepository<WebFunctionList> _WebFunctionList;

        protected WebTemplateTestEntities _db
        {
            get;
            private set;
        }

        public MenuUIService()
        {
            _WebFunctionList = new GenericRepository<WebFunctionList>();
            _db = new WebTemplateTestEntities();
        }

        /// <summary>
        /// 取所有項目 by帳號
        /// </summary>
        /// <returns></returns>
        public List<WebMenuViewModel> GetMenuByUserId()
        {
            List<WebMenuViewModel> root = new List<WebMenuViewModel>();
            if (_WebFunctionList.GetAll().Any())
            {
                var query = (from x in _db.WebFunctionList
                                 //!c.ParentId.HasValue (之前是Guid)
                             select new WebMenuViewModel
                             {
                                 Id = x.Id,
                                 IId = x.IId,
                                 ParentId = x.ParentId,
                                 Text = x.Text,
                                 URL = x.URL,
                                 ActionName = x.ActionName,
                                 Controller = x.Controller,
                                 Sort = x.Sort,
                                 sValue = x.sValue,
                                 LastLevel = x.LastLevel,
                                 iclassTag = x.iclassTag
                             });

                root = query.OrderBy(s => s.Sort).ToList();
            }
            return root;
        }
    }
}
