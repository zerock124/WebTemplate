using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.WebConfig
{
    public class WebConfigViewModel
    {
        /// <summary>
        /// 網站設定ID
        /// </summary>
        public int Id{ get; set; }
        /// <summary>
        /// 網站名稱
        /// </summary>
        public string WebName { get; set; }
        /// <summary>
        /// 客服信箱
        /// </summary>
        public string ServiceMail { get; set; }
        /// <summary>
        /// Copyright
        /// </summary>
        public string Copyright { get; set; }
        /// <summary>
        /// 系統寄件者名稱
        /// </summary>
        public string SystemEmailName { get; set; }
        /// <summary>
        /// 系統寄件者信箱
        /// </summary>
        public string SystemEmail { get; set; }
        /// <summary>
        /// meta_title
        /// </summary>
        public string meta_title { get; set; }
        /// <summary>
        /// meta_keyword
        /// </summary>
        public string meta_keyword { get; set; }
        /// <summary>
        /// meta_description
        /// </summary>
        public string meta_description { get; set; }
        /// <summary>
        /// meta_url
        /// </summary>
        public string meta_url { get; set; }
        /// <summary>
        /// meta_image
        /// </summary>
        public string meta_image { get; set; }

    }
}
