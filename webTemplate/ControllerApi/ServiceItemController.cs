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
using ViewModel.ServiceItem;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Service;

namespace webTemplate.ControllersApi
{
    public class ServiceItemController : ApiController
    {
        public string _ServiceItemPath = ConfigurationManager.AppSettings["ServiceItemPath"];
        protected WebTemplateEntities _db;
        protected IServiceItemService _serviceItemService;

        public ServiceItemController()
        {
            _db = new WebTemplateEntities();
            _serviceItemService = new ServiceItemService();
        }
        // GET: api/ServiceItem
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                List<ServiceItemViewModel> data = new List<ServiceItemViewModel>();
                data = await _serviceItemService.GetServiceItemList();
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
