using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webTemplate.Models
{
    public class AccountViewModel
    {
    }

    public class RegisterViewModel 
    {
        [Required]
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "帳號")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "真實姓名")]
        public string RealName { get; set; }

        [RegularExpression(@"(^[09]{2}[0-9]{8}$|^[+886]{4}[0-9]{9}$)", ErrorMessage = "必須為手機號碼格式，例如：0987654321、+886987654321")]
        [Display(Name = "行動電話")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 的長度至少必須為 {2} 個字元。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不相符。")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "角色名稱")]
        public string RoleName { get; set; }
    }
}