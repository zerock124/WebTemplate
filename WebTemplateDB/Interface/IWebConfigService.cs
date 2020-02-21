using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.WebConfig;
using ViewModels.Verity;

namespace WebTemplateDB.Interface
{
    public interface IWebConfigService
    {
        /// <summary>
        /// 取得網站設定內容
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<WebConfigViewModel> GetWebConfig(int Id);
        /// <summary>
        /// 變更網站設定
        /// </summary>
        /// <returns></returns>
        Task<VerityResult> EditWebConfig(WebConfigViewModel model);
    }
}
