using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace iTech.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? LastName { get; set; }
        //[RegularExpression(@"^\d{14}$", ErrorMessage = "Please enter a valid number.")]
        public string? AccountNumber { get; set; }
        public string? BankName { get; set; }
        public string? MerchantID { get; set; }
        public string? IPan { get; set; }
        public string? Address { get; set; }
        //public string? TypeofBusiness { get; set; }
        public bool? MLM { get; set; }
        public string? sponserName { get; set; }
        public DateTime JoinedDate { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
		public string CountryCode { get; set; }

		public string? Legalname { get; set; }
        public bool? is_licensed { get; set; }
        public string? licensedtype { get; set; }
        public int? licensednumber { get; set; }
        public string? BrandName { get; set; }
        public string? CoverImage { get; set; }
        public int? CountryId { get; set; }
        public string? AffiliateToken { get; set; }
        public double Point { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegisterdData { get; set; }
        public string? UserPic { get; set; }
        public string? response { get; set; }

        public static implicit operator ApplicationUser(ClaimsPrincipal v)
        {
            throw new NotImplementedException();
        }
    }
}
