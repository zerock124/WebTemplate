using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using ViewModel.Contact;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Service;

namespace webTemplate.ControllerApi
{
    public class ContactController : ApiController
    {
        protected WebTemplateEntities _db;
        protected IContactService _contactService;

        public ContactController()
        {
            _db = new WebTemplateEntities();
            _contactService = new ContactService();
        }

        // Post: api/Contact
        public async Task<IHttpActionResult> Post(ContactViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var url = HttpContext.Current.Request.Url.AbsoluteUri + "/ResponseMessage";
                    var result = await _contactService.CreateContact(model, url);
                    return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.OK, result));
                }
                var IsValid = false;
                return new ResponseMessageResult(Request.CreateResponse(HttpStatusCode.OK, IsValid));
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

        public async Task<string> ResponseMessage(int kmsgid, string dstaddr, DateTime dlvtime, DateTime donetime)
        {
            var k = kmsgid;
            var d = dstaddr;
            var dl = dlvtime;
            var dn = donetime;

            return await Task.Run(() => dstaddr);

        }
    }
}
