using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Share;
using ViewModel.Contact;
using ViewModels.Verity;

namespace WebTemplateDB.Interface
{
    public interface IContactService
    {
        /// <summary>
        /// 取得連絡管理列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<ResWithPaginationViewModel> GetContactList(SearchModel searchModel, PaginationViewModel pagination);
        /// <summary>
        /// 取得編輯聯絡我們
        /// </summary>
        /// <param name="ContactId"></param>
        /// <returns></returns>
        Task<ContactViewModel> GetEditContact(int ContactId);
        /// <summary>
        /// 編輯聯絡我們
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<VerityResult> EditContactItem(ContactViewModel model, string CurrendUserid);
        /// <summary>
        /// 刪除聯絡我們
        /// </summary>
        /// <param name="ContactId"></param>
        /// <returns></returns>
        Task<VerityResult> DeleteContact(int ContactId);

        ///-------------------------API---------------------------

        /// <summary>
        /// 新增聯絡我們
        /// </summary>
        /// <returns></returns>
        Task<VerityResult> CreateContact(ContactViewModel model, string url);

        ///-------------------------------------------------------
    }
}
