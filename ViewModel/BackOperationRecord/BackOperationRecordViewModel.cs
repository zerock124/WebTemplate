using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.BackOperationRecord
{
    public class BackOperationRecordViewModel
    {
        /// <summary>
        /// 操作紀錄ID
        /// </summary>
        public int OperationId { get; set; }
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 操作內容
        /// </summary>
        public string ContentText { get; set; }
        /// <summary>
        /// 結果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 新建時間
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// IP位置
        /// </summary>
        public string IP { get; set; }
    }
}
