using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ViewModel.ServiceItem
{
    public class ServiceItemViewModel
    {
        /// <summary>
        /// 服務項目ID
        /// </summary>
        public int ServiceItemId { get; set; }
        /// <summary>
        /// 服務項目名稱
        /// </summary>
        public string ServiceItemName { get; set; }
        /// <summary>
        /// 服務項目圖片名稱
        /// </summary>
        public string ImageName { get; set; }
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
        /// <summary>
        /// 服務項目上傳圖片
        /// </summary>
        public HttpPostedFileBase PhotoFile { get; set; }
        /// <summary>
        /// IconName
        /// </summary>
        public string IconName { get; set; }
        /// <summary>
        /// 模式選擇
        /// </summary>
        public string Mode { get; set; }
    }
}
