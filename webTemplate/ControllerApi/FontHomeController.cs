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
using ViewModel.FontHome;
using ViewModels.Share;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Service;
using WebTemplateDB.Models;

namespace webTemplate.ControllersApi
{
    public class FontHomeController : ApiController
    {
        public string _HomeImagePath = ConfigurationManager.AppSettings["HomeImagePath"];
        protected WebTemplateEntities _db;
        protected IFontHomeService _fontHomeService;

        public FontHomeController()
        {
            _db = new WebTemplateEntities();
            _fontHomeService = new FontHomeService();
        }
        // GET: api/FontHome
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                List<FontHomeViewModel> data = new List<FontHomeViewModel>();
                data = await _fontHomeService.GetFontHomeList();
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
