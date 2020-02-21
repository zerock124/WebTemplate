using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.StaticPage;
using ViewModels.Share;
using ViewModels.Verity;

namespace WebTemplateDB.Interface
{
    public interface IStaticPageService
    {
        /// <summary>
        /// 取得靜態頁列表內容
        /// </summary>
        /// <returns></returns>
        Task<List<StaticPageViewModel>> GetStaticPageList();

        /// <summary>
        /// 新增靜態頁內容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DataVerityResult<StaticPageViewModel>> CreateStaticPage(StaticPageViewModel model);

        /// <summary>
        /// 變更靜態頁內容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<DataVerityResult<StaticPageViewModel>> EditStaticPage(StaticPageViewModel model);

        ///-------------------------API---------------------------

        /// <summary>
        /// 取得靜態頁內容
        /// </summary>
        /// <returns></returns>
        Task<StaticPageViewModel> GetStaticPage(int StaticPageEnum);

        ///-------------------------------------------------------
    }
}
