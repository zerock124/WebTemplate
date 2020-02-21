using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.FontHome;
using ViewModels.Share;
using ViewModels.Verity;

namespace WebTemplateDB.Interface
{
    public interface IFontHomeService
    {
        /// <summary>
        /// 新增前台首頁圖片
        /// </summary>
        /// <returns></returns>
        Task<DataVerityResult<FontHomeViewModel>> CreateFontHome(FontHomeViewModel model);
        /// <summary>
        /// 取得首頁管理列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResWithPaginationViewModel> GetFontHomeList(SearchModel model, PaginationViewModel pagination);
        /// <summary>
        /// 取得變更前台首頁圖片
        /// </summary>
        /// <returns></returns>
        Task<FontHomeViewModel> GetFontHomeItem(int FontHomeId);
        /// <summary>
        /// 變更前台首頁圖片
        /// </summary>
        /// <returns></returns>
        Task<DataVerityResult<FontHomeViewModel>> EditFontHome(FontHomeViewModel model);
        /// <summary>
        /// 刪除前台首頁圖片
        /// </summary>
        /// <param name="FontHomeId"></param>
        /// <returns></returns>
        Task<DataVerityResult> DeleteFontHome(int FontHomeId);

        ///-------------------------API---------------------------

        /// <summary>
        /// 取得Banner圖片
        /// </summary>
        /// <returns></returns>
        Task<List<FontHomeViewModel>> GetFontHomeList();

        ///-------------------------------------------------------
    }
}
