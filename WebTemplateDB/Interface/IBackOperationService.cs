using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.BackOperationRecord;
using ViewModels.Share;
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
        /// <summary>
        /// 取得後台操作紀錄列表
        /// </summary>
        /// <returns></returns>
        Task<ResWithPaginationViewModel> GetBackOperationList(SearchModel searchModel, PaginationViewModel pagination);
    }
}
