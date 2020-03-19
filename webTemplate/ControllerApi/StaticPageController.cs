using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using ViewModel.StaticPage;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Service;

namespace webTemplate.ControllerApi
{
    public class StaticPageController : ApiController
    {
        protected WebTemplateEntities _db;
        protected IStaticPageService _staticPageService;

        public StaticPageController()
        {
            _db = new WebTemplateEntities();
            _staticPageService = new StaticPageService();
        }

        // GET: api/StaticPage
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                List<StaticPageViewModel> data = new List<StaticPageViewModel>();
                data = await _staticPageService.GetStaticPageList();
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

        // GET: api/StaticPage/1
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                StaticPageViewModel data = new StaticPageViewModel();
                data = await _staticPageService.GetStaticPage(id);
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
