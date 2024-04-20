using System.ComponentModel.DataAnnotations;

namespace iTech.ViewModel
{
    public class UserProfileVM
    {
        public string? FullName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? CurrentEmail { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? NewEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string? CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$",ErrorMessage = "Should have at least one lower case , one upper case , one number,one special character and minimum length 6 characters")]
        [Compare("NewPassword", ErrorMessage = "The New Password and Confirmation Password do not match")]
        public string? ConfirmPassword { get; set; }
        public string? AccountName { get; set; }
        public string? IBan { get; set; }
        public string? BankName { get; set; }
        public string? Adress { get; set; }
        public int? Country { get; set; }

        public string? CoverPic { get; set; }
        
    }
}
