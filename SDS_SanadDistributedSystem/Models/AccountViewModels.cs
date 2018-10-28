using SDS_SanadDistributedSystem.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SDS_SanadDistributedSystem.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Key]
        public string Id { get; set; }

      //  [Required]
      ////  [Remote("IsAlreadySignedUserName", "Account", HttpMethod = "Post", ErrorMessage = "اسم المستخدم موجود مسبقا")]
      //  [Display(Name = "UserName", ResourceType = typeof(RegisterViewModelResource))]
      //  public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]//[EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(RegisterViewModelResource))]
        //[Remote("IsAlreadySignedEmail", "Account", HttpMethod = "Post", ErrorMessage = "البريد الالكتروني موجود مسبقا")]
        public string Email { get;  set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(RegisterViewModelResource))]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$")]//, ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(RegisterViewModelResource))]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "كلمة المرور لا تتطابق مع تأكيد كلمة المرور")]
        public string ConfirmPassword { get; set; }

        //   [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        //   [DataType(DataType.PhoneNumber)]
        //[Remote("IsAlreadySignedPhone", "Account", HttpMethod = "Post", ErrorMessage = "رقم الهاتف موجود مسبقا")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(RegisterViewModelResource))]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "idcenter_FK", ResourceType = typeof(RegisterViewModelResource))]
        public string idcenter_FK { get; set; }

        //[Required]
        [Display(Name = "Roles", ResourceType = typeof(RegisterViewModelResource))]
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }

        [Required]
        [Display(Name = "enabled", ResourceType = typeof(RegisterViewModelResource))]
        public virtual bool enabled { get; set; }

        [Display(Name = "ShowName", ResourceType = typeof(RegisterViewModelResource))]
        public virtual string ShowName { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
