using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.LatestNews;
using ViewModels.Share;
using ViewModels.Verity;

namespace WebTemplateDB.Interface
{
    public interface ILatestNewsService
    {
        /// <summary>
        /// 新增最新消息圖片
        /// </summary>
        /// <returns></returns>
        Task<DataVerityResult<LatestNewsViewModel>> CreateLatestNews(LatestNewsViewModel model);
        /// <summary>
        /// 取得最新消息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PageDataVerityResult> GetLatestNewsList(SearchModel model, PaginationViewModel pagination);
        /// <summary>
        /// 取得變更最新消息圖片
        /// </summary>
        /// <returns></returns>
        Task<LatestNewsViewModel> GetLatestNewsItem(int LatestNewsId);
        /// <summary>
        /// 變更最新消息圖片
        /// </summary>
        /// <returns></returns>
        Task<VerityResult> EditLatestNewsItem(LatestNewsViewModel model);
        /// <summary>
        /// 刪除最新消息
        /// </summary>
        /// <param name="LatestNewsId"></param>
        /// <returns></returns>
        Task<VerityResult> DeleteLatestNewsItem(int LatestNewsId);
    }
}
