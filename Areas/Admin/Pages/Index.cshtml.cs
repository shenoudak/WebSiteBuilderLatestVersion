using iTech.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace iTech.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
		private CRMDBContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ApplicationDbContext _db;
		public int SitesCount { get; set; }
		public double SitesAmount { get; set; }
		public int AffiliateCount { get; set; }
		public double TotalPointsCountNotPaid { get; set; }
		public double TotalPointAmountPaid { get; set; }
		public double TotalPointAmountNotPaid { get; set; }



		public IndexModel(CRMDBContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
		{
			_context = context;
			_userManager = userManager;
			_db = db;

		}
		public async Task<IActionResult> OnGet()
		{
			SitesCount = _context.Sites.Count();
			SitesAmount = _context.Sites.Include(e => e.Templates).Where(e => e.IsPaid == true).Sum(e => e.UserTemplatePrice);
			var AffiliateUsers = await _userManager.GetUsersInRoleAsync("Affiliate");
			AffiliateCount = AffiliateUsers.Count();
			TotalPointAmountPaid = _context.PaymentRequests.Where(e => e.IsPaid == true).Sum(e => e.ConfirmAmount);
			TotalPointsCountNotPaid = AffiliateUsers.Sum(e => e.Point);
			var PointCost = _context.PointConfigurations.FirstOrDefault().Value;
			TotalPointAmountNotPaid = TotalPointsCountNotPaid * PointCost;

			//string adminRoleId = _db.Roles.Where(e => e.Name == "Affiliate").FirstOrDefault().Id;
			//AffiliateCount = _db.UserRoles.Where(e => e.RoleId == adminRoleId).Count();


			return Page();

		}
	}
}
