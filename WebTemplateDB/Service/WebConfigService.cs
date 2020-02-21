using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.WebConfig;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;

namespace WebTemplateDB.Service
{
    public class WebConfigService : IWebConfigService
    {
        protected IGenericRepository<WebConfig> _webconfig;

        public WebConfigService()
        {
            _webconfig = new GenericRepository<WebConfig>();
        }

        public async Task<WebConfigViewModel> GetWebConfig(int Id)
        {
            WebConfigViewModel item = new WebConfigViewModel();

            var query = from a in _webconfig.FindBy(x => x.Id == Id)
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
                item = data;
            }

            return await Task.Run(() => item);
        }

        public async Task<VerityResult> EditWebConfig(WebConfigViewModel model)
        {
            VerityResult result = new VerityResult();

            var query = _webconfig.FindBy(x => x.Id == model.Id);
            try
            {
                if (query.Any())
                {
                    WebConfig item = query.FirstOrDefault();
                    item.WebName = model.WebName;
                    item.ServiceMail = model.ServiceMail;
                    item.Copyright = model.Copyright;
                    item.SystemEmailName = model.SystemEmailName;
                    item.SystemEmail = model.SystemEmail;
                    item.meta_title = model.meta_title;
                    item.meta_keyword = model.meta_keyword;
                    item.meta_description = model.meta_description;
                    item.meta_url = model.meta_url;
                    item.meta_image = model.meta_image;

                    _webconfig.Update(item);
                    result.Success = true;
                    result.Message = "更新網頁設定成功";
                }
            }
            catch
            {
                result.Success = false;
                result.Message = "更新網頁設定失敗";
            }
            return await Task.Run(() => result);
        }

    }
}
