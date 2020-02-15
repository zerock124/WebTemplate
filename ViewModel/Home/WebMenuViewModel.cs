using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Home
{
    public class WebMenuViewModel
    {
        /// <summary>
        /// 自動編號ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 功能ID
        /// </summary>
        public string IId { get; set; }
        /// <summary>
        /// 父層ID
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 功能名稱(中)
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 連結網址
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// 頁面名稱(Action)
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 控制項名稱(Controller)
        /// </summary>
        public string Controller { get; set; }
        /// 排序層級
        /// </summary>
        public int? Sort { get; set; }
        /// <summary>
        /// 字串化層級 "010000"
        /// </summary>
        public string sValue { get; set; }
        /// <summary>
        /// 最後階層
        /// </summary>
        public int? LastLevel { get; set; }
        /// <summary>
        /// Icon名稱
        /// </summary>
        public string iclassTag { get; set; }
    }

}
