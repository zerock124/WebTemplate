using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ServiceItem;
using ViewModels.Verity;

namespace WebTemplateDB.Interface
{
    public interface IServiceItemService
    {
        /// <summary>
        /// 取得服務項目列表
        /// </summary>
        /// <returns></returns>
        Task<List<ServiceItemViewModel>> GetServiceItemList();
        /// <summary>
        /// 新增服務項目
        /// </summary>
        /// <param name="model"></param>
        /// <param name="CurrendUserid"></param>
        /// <returns></returns>
        Task<VerityResult> CreateServiceItemList(List<ServiceItemViewModel> model, string CurrendUserid);
        /// <summary>
        /// 刪除服務項目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<VerityResult> DeleteServiceItem(ServiceItemViewModel model);
    }
}
