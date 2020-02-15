using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.BackOperationRecord;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Repositories;
using WebTemplateDB.Models;

namespace WebTemplateDB.Service
{
    public class BackOperationService : IBackOperationService
    {
        WebTemplateEntities _db;
        IGenericRepository<BackOperation> _backoperation;
        IGenericRepository<AspNetUsers> _aspnetUser;
        IGenericRepository<AspNetRoles> _aspnetRole;
        IGenericRepository<AspNetUserRoles> _aspnetUserRole;

        public BackOperationService()
        {
            _db = new WebTemplateEntities();
            _backoperation = new GenericRepository<BackOperation>(_db);
            _aspnetUser = new GenericRepository<AspNetUsers>();
            _aspnetRole = new GenericRepository<AspNetRoles>();
            _aspnetUserRole = new GenericRepository<AspNetUserRoles>();
        }

        public async Task<VerityResult> CreateBackOperation(string Id, string ContentText, string IP)
        {
            var result = new VerityResult();

            try
            {
                var aspnetUser = _aspnetUser.FindBy(x => x.Id == Id).FirstOrDefault();
                var aspnetUserRole = _aspnetUserRole.FindBy(x => x.UserId == aspnetUser.Id).FirstOrDefault();
                var aspnetRole = _aspnetRole.FindBy(x => x.Id == aspnetUserRole.RoleId).FirstOrDefault();

                BackOperation actionItem = new BackOperation
                {
                    AspNerUserId = aspnetUser.Id,
                    RoleId = aspnetRole.Id,
                    ContentText = ContentText,
                    IP = IP,
                    Result = true,
                    CreateTime = DateTime.Now,
                    CreateUser = Id
                };

                _backoperation.Create(actionItem);

                result.Success = true;
                result.Message = "成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "失敗";
            }

            return await Task.Run(() => result);
        }
    }
}
