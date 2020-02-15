using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Case;
using ViewModels.Share;
using ViewModels.Verity;

namespace WebTemplateDB.Interface
{
    public interface ICaseService
    {
        /// <summary>
        /// 新增案例
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<VerityResult> CreateCase(CaseViewModel model);
        /// <summary>
        /// 取得案例介紹列表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<PageDataVerityResult> GetCaseList(SearchModel model, PaginationViewModel pagination);
        /// <summary>
        /// 取得變更案例介紹圖片
        /// </summary>
        /// <returns></returns>
        Task<CaseViewModel> GetEditCaseItem(int FontHomeId);
        /// <summary>
        /// 變更案例介紹圖片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<VerityResult> EditCaseItem(CaseViewModel model);
        /// <summary>
        /// 刪除案例介紹圖片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<VerityResult> DeleteCaseItem(int CaseId);
    }
}
