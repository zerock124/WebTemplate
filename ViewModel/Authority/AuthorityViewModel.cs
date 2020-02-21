using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Authority
{
    public class AuthorityViewModel
    {
        /// <summary>
        /// 帳號ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 真實姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 帳號
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密碼
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名稱
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 註冊日期
        /// </summary>
        public DateTime RegisterDate { get; set; }
        /// <summary>
        /// 新建日期
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 新建的使用者ID
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }

    public class SearchModel
    {
        /// <summary>
        /// 搜尋類別
        /// </summary>
        public int SearchEnum { get; set; }
        /// <summary>
        /// 起始時間
        /// </summary>
        public DateTime? StartDateTime { get; set; }
        /// <summary>
        /// 結束時間
        /// </summary>
        public DateTime? EndDateTime { get; set; }
        /// <summary>
        /// 搜尋字串
        /// </summary>
        public string Query { get; set; }
    }

    public class AspNetUserRolesViewModel 
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
