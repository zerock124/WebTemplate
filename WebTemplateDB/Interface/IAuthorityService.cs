using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Authority;
using ViewModels.Share;
using ViewModels.Verity;

namespace WebTemplateDB.Interface
{
    public interface IAuthorityService
    {
        /// <summary>
        /// 取得帳號權限管理列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <param name="pagination"></param>
        /// <returns></returns>
        Task<ResWithPaginationViewModel> GetAuthorityList(SearchModel searchModel, PaginationViewModel pagination);
        /// <summary>
        /// 取得角色列表
        /// </summary>
        /// <returns></returns>
        Task<List<AspNetUserRolesViewModel>> GetRoleOptions();
        /// <summary>
        /// 確認是否有權限
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="CurrendUserId"></param>
        /// <returns></returns>
        Task<VerityResult> CheckAuthority(string Id, string CurrendUserid);
        /// <summary>
        /// 取得編輯帳號資料
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<AuthorityViewModel> GetEditAuthorityItem(string Id, string CurrendUserid);
        /// <summary>
        /// 編輯帳號資料
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<VerityResult> EditAuthorityItem(AuthorityViewModel model);
        /// <summary>
        /// 刪除帳號資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<VerityResult> DeleteAuthorityItem(string id);
    }
}
