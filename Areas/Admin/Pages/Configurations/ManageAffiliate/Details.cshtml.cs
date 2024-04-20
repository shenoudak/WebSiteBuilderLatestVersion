using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iTech.Areas.Admin.Pages.Configurations.ManageAffiliate
{
    public class DetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;      
        private CRMDBContext _context;
        public ApplicationUser AffiliateUsers { get; set; }
        public double PointsAmount { get; set; }
        public double SiteCount { get; set; }
        public double SiteRevenue { get; set; }
        public List<Site> Sites { get; set; }
        public DetailsModel(CRMDBContext context, UserManager<ApplicationUser> userManager/*, RoleManager<ApplicationUser> roleManager*/)
        {
            _context = context;
            _userManager = userManager;
            AffiliateUsers = new ApplicationUser();
        }
        public void OnGet(string id= "ec0590a9-1a7a-423e-a7c1-9c1e2c343bc1")
        {
            AffiliateUsers =  _userManager.Users.Where(e=>e.Id== id).FirstOrDefault();
            var Affiliatepoint = AffiliateUsers.Point;
            var point = _context.PointConfigurations.FirstOrDefault().Value;
            PointsAmount = Affiliatepoint * point;
            SiteCount = _context.Sites.Where(e => e.AffiliateUser == id).Count();
            SiteRevenue = _context.Sites.Where(e => e.AffiliateUser == id).Sum(e=>e.UserTemplatePrice);
            Sites= _context.Sites.Include(e=>e.Templates).Where(e => e.AffiliateUser == id).ToList();
        }
    }
}
