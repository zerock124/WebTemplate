using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ViewModel.FontHome
{
    public class FontHomeViewModel
    {
        /// <summary>
        /// 首頁圖片ID
        /// </summary>
        public int FontHomeId { get; set; }
        /// <summary>
        /// 首頁圖片名稱
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// 連結網址
        /// </summary>
        public string FontHomeUrl { get; set; }
        /// <summary>
        /// 上線時間
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// 下線時間
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 建立的使用者
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        ///  更新的使用者
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 前台首頁圖片檔案
        /// </summary>
        public HttpPostedFileBase PhotoFile { get; set; }
    }

    public class SearchModel {
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
}
