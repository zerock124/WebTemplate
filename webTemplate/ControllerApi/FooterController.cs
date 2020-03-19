using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using ViewModel.WebConfig;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Service;

namespace webTemplate.ControllerApi
{
    public class FooterController : ApiController
    {
        protected WebTemplateEntities _db;
        protected IWebConfigService _webconfigService;

        public FooterController()
        {
            _db = new WebTemplateEntities();
            _webconfigService = new WebConfigService();
        }

        public async Task<IHttpActionResult> Get(int Id)
        {
            try
            {
                WebConfigViewModel data = new WebConfigViewModel();
                data = await _webconfigService.GetWebConfig(Id);
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
