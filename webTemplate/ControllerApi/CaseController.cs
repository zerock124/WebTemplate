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
using ViewModel.Case;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Service;

namespace webTemplate.ControllerApi
{
    public class CaseController : ApiController
    {
        public string _CasePath = ConfigurationManager.AppSettings["CasePath"];
        protected WebTemplateEntities _db;
        protected ICaseService _caseService;

        public CaseController()
        {
            _db = new WebTemplateEntities();
            _caseService = new CaseService();
        }

        // POST: api/Case
        public async Task<IHttpActionResult> Post(APISearchModel model)
        {
            try
            {
                List<CaseViewModel> data = new List<CaseViewModel>();
                data = await _caseService.GetCaseList(model);
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
