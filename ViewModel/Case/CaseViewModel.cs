using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ViewModel.Case
{
    public class CaseViewModel
    {
        /// <summary>
        /// 案例ID
        /// </summary>
        public int CaseId { get; set; }
        /// <summary>
        /// 案例圖片檔案名稱
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// 案例連結網址
        /// </summary>
        public string CaseUrl { get; set; }
        /// <summary>
        /// 案例類別
        /// </summary>
        public int CaseEnum { get; set; }
        /// <summary>
        /// 案例名稱
        /// </summary>
        public string CaseName { get; set; }
        /// <summary>
        /// 案例內容
        /// </summary>
        [AllowHtml]
        public string CaseContent { get; set; }
        /// <summary>
        /// 案例狀態
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 新建的使用者
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 新建時間
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新的使用者
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        ///  更新時間
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 案例圖片檔案
        /// </summary>
        public HttpPostedFileBase PhotoFile { get; set; }

        public string LabelTag { get; set; }
        /// <summary>
        /// 流水編號
        /// </summary>
        public int Number { get; set; }
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


    public class APISearchModel
    {
        /// <summary>
        /// 搜尋類別
        /// </summary>
        public string CaseEnum { get; set; }
        /// <summary>
        /// Tag名稱
        /// </summary>
        public string LabelTag { get; set; }
    }
}
