using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.StaticPage
{
    public class StaticPageViewModel
    {
        /// <summary>
        /// 靜態頁ID
        /// </summary>
        public int StaticPageId { get; set; }
        /// <summary>
        /// 靜態頁分類
        /// </summary>
        public int? StaticPageEnum { get; set; }
        /// <summary>
        /// 靜態頁內容
        /// </summary>
        public string PageContent { get; set; }
        /// <summary>
        /// 新建時間
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 新建的使用者
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 更新的使用者
        /// </summary>
        public string UpdateUser { get; set; }
    }
}
