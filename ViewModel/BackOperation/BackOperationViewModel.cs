﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.BackOperationRecord
{
    public class BackOperationViewModel
    {
        /// <summary>
        /// 操作紀錄ID
        /// </summary>
        public int BackOperationId { get; set; }
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string AspNetUserId { get; set; }
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色帳號
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 操作內容
        /// </summary>
        public string ContentText { get; set; }
        /// <summary>
        /// 結果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// IP位置
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 新建時間
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 新建的使用者
        /// </summary>
        public string CreateUser { get; set; }
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
