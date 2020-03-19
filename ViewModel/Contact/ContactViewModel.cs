using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModel.Contact
{
    public class ContactViewModel
    {
        /// <summary>
        /// 聯絡我們ID
        /// </summary>
        public int ContactId { get; set; }
        /// <summary>
        /// 公司名稱
        /// </summary>
        [Required]
        public string CompanyName { get; set; }
        /// <summary>
        /// 聯絡人姓名
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 性別
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        /// <summary>
        /// 連絡電話
        /// </summary>
        [Required]
        public string ContactPhone { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        public int ContactEnum { get; set; }
        /// <summary>
        /// 類型
        /// </summary>
        public int ContactStatus { get; set; }
        /// <summary>
        /// 預算
        /// </summary>
        public string Budget { get; set; }
        /// <summary>
        /// 預計上線時間
        /// </summary>
        public string OnlineDate { get; set; }
        /// <summary>
        /// 需求說明
        /// </summary>
        public string Demand { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        [Required]
        public string Remark { get; set; }
        /// <summary>
        /// 新建時間
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 更新的使用者
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 選擇寄送方式
        /// </summary>
        public int SendMode { get; set; }
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
}
