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
    public class BackOperationRecordService : IBackOperationRecordService
    {
        WebTemplateEntities _db;
        IGenericRepository<BackOperationRecord> _backoperationrecord;

        public BackOperationRecordService() 
        {
            _db = new WebTemplateEntities();
            _backoperationrecord = new GenericRepository<BackOperationRecord>(_db);
        }

        public Task<VerityResult> CreateBackOperationRecord(BackOperationRecordViewModel model)
        {
            var result = new VerityResult();


            try
            {
                BackOperationRecord actionItem = new BackOperationRecord() 
                {
                    UserName = model.UserName,
                    RoleId = model.RoleId,
                    ContentText = model.ContentText,
                    Result = model.Result,
                    CreateTime = DateTime.Now,
                    IP = model.IP
                };

                _backoperationrecord.Create(actionItem);

                result.Success = true;
                result.Message = "成功";
            }
            catch 
            {
                result.Success = false;
                result.Message = "失敗";
            }

            return Task.Run(() => result);
        }
    }
}
