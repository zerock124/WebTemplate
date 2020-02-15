using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.BackOperationRecord;
using ViewModels.Verity;

namespace WebTemplateDB.Interface
{
    public interface IBackOperationService
    {
        /// <summary>
        /// 寫入操作紀錄
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<VerityResult> CreateBackOperation(string Id, string ContentText, string IP);
    }
}
