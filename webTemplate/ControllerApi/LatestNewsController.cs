using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ViewModel.LatestNews;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Service;

namespace webTemplate.ControllerApi
{
    public class LatestNewsController : ApiController
    {
        public string _ServiceItemPath = ConfigurationManager.AppSettings["ServiceItemPath"];
        protected WebTemplateEntities _db;
        protected ILatestNewsService _latestNewsService;

        public LatestNewsController()
        {
            _db = new WebTemplateEntities();
            _latestNewsService = new LatestNewsService();
        }
        // GET: api/LatestNews
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                List<LatestNewsViewModel> data = new List<LatestNewsViewModel>();
                data = await _latestNewsService.GetLatestNewsList();
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.OK, data));
            }
            catch (Exception ex)
            {
                VerityResult result = new VerityResult();
                result.Success = false;
                result.Message = JsonConvert.SerializeObject(ex);
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.InternalServerError, result));
                throw;
            }
        }
    }
}
