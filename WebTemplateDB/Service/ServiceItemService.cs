using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ServiceItem;
using ViewModels.Verity;
using WebTemplateDB.Interface;
using WebTemplateDB.Models;
using WebTemplateDB.Repositories;

namespace WebTemplateDB.Service
{
    public class ServiceItemService : IServiceItemService
    {
        WebTemplateEntities _db;
        IGenericRepository<ServiceItem> _serviceItem;

        public ServiceItemService()
        {
            _db = new WebTemplateEntities();
            _serviceItem = new GenericRepository<ServiceItem>();
        }

        public async Task<List<ServiceItemViewModel>> GetServiceItemList()
        {
            List<ServiceItemViewModel> serviceItemList = new List<ServiceItemViewModel>();

            var query = from a in _serviceItem.GetAll()
                        select new ServiceItemViewModel
                        {
                            ServiceItemId = a.ServiceItemId,
                            ServiceItemName = a.ServiceItemName,
                            ImageName = a.ImageName,
                            CreateTime = a.CreateTime,
                            CreateUser = a.CreateUser,
                            UpdateTime = a.UpdateTime,
                            UpdateUser = a.UpdateUser
                        };
            if (query.Any())
            {
                serviceItemList = query.ToList();
            }

            return await Task.Run(() => serviceItemList);
        }

        public async Task<VerityResult> CreateServiceItemList(List<ServiceItemViewModel> model, string CurrendUserid)
        {
            VerityResult result = new VerityResult();

            List<ServiceItem> createItemList = new List<ServiceItem>();
            List<ServiceItem> updateItemList = new List<ServiceItem>();
            try
            {
                foreach (var modelItem in model)
                {
                    var checkcreate = _serviceItem.FindBy(x => x.ServiceItemId == modelItem.ServiceItemId);
                    if (checkcreate.Any())
                    {
                        ServiceItem updateItem = checkcreate.SingleOrDefault();
                        updateItem.ServiceItemName = modelItem.ServiceItemName;
                        updateItem.ImageName = modelItem.ImageName;
                        updateItem.UpdateTime = DateTime.Now;
                        updateItem.UpdateUser = CurrendUserid;

                        updateItemList.Add(updateItem);
                    }
                    else
                    {
                        ServiceItem createItem = new ServiceItem
                        {
                            ServiceItemName = modelItem.ServiceItemName,
                            ImageName = modelItem.ImageName,
                            CreateTime = DateTime.Now,
                            CreateUser = CurrendUserid,
                        };
                        createItemList.Add(createItem);
                    }
                }

                if (createItemList.Any())
                {
                    _serviceItem.CreateMultiple(createItemList);
                }
                if (updateItemList.Any())
                {
                    _serviceItem.UpdateMultiple(updateItemList);
                }
                result.Success = true;
                result.Message = "新增/變更 服務項目成功";
            }
            catch
            {
                result.Success = false;
                result.Message = "新增/變更 服務項目失敗";
            }


            return await Task.Run(() => result);
        }
    }
}
