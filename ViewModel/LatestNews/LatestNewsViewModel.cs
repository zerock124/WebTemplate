using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ViewModel.LatestNews
{
    public class LatestNewsViewModel
    {
        /// <summary>
        /// 最新消息ID
        /// </summary>
        public int LatestNewsId { get; set; }
        /// <summary>
        /// 最新消息圖片
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// 最新消息類別
        /// </summary>
        public int LatestNewsEnum { get; set; }
        /// <summary>
        /// 上架日期
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// 最新消息標題
        /// </summary>
        public string LatestNewsTitle { get; set; }
        /// <summary>
        /// 最新消息內文
        /// </summary>
        [AllowHtml]
        public string LatestNewsContent { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 狀態
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 新建時間
        /// </summary>
        public DateTime CreateTime { get; set; }
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
        /// <summary>
        /// 最新消息圖片檔案
        /// </summary>
        public HttpPostedFileBase PhotoFile { get; set; }
        public string LabelTag { get; set; }
        /// <summary>
        /// 排序編號
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
        /// 起始上線時間
        /// </summary>
        public DateTime? StartOnlineDateTime { get; set; }
        /// <summary>
        /// 結束上線時間
        /// </summary>
        public DateTime? EndOnlineDateTime { get; set; }

        /// <summary>
        /// 搜尋字串
        /// </summary>
        public string Query { get; set; }
    }
}
