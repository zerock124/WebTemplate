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
        Task<List<ServiceItemViewModel>> GetServiceItemList();

        Task<VerityResult> CreateServiceItemList(List<ServiceItemViewModel> model, string CurrendUserid);
    }
}
