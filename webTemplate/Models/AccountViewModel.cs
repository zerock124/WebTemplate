using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webTemplate.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        public string RealName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }
        public string returnUrl { get; set; }
    }

    /// <summary>
    /// 登入帳號/密碼
    /// </summary>
    public class LoginViewModel
    {
        //[Required]
        //[Display(Name = "電子郵件")]
        //[EmailAddress]
        //public string Email { get; set; }
        [Required]
        [Display(Name = "登入帳號")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Display(Name = "記住我?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel 
    {
        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "帳號")]
        public string UserName { get; set; }

        [Display(Name = "真實姓名")]
        public string RealName { get; set; }

        //[RegularExpression(@"(^[09]{2}[0-9]{8}$|^[+886]{4}[0-9]{9}$)", ErrorMessage = "必須為手機號碼格式，例如：0987654321、+886987654321")]
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

        [Display(Name = "角色名稱")]
        public string RoleName { get; set; }

        [Display(Name = "角色Id")]
        public string RoleId { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}