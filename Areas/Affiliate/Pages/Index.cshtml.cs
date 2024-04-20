using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using iTech.Data;
using iTech.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace iTech.Areas.Affiliate.Pages
{
	public class IndexModel : PageModel
	{
		private CRMDBContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ApplicationDbContext _db;
		public int SitesCount { get; set; }
		public double SitesAmount { get; set; }
		public double CurrentPointsCount { get; set; }
		public double CurrentPointsAmount { get; set; }
		public double PaidPointsCount { get; set; }
		public double PaidPointsAmount { get; set; }
		[BindProperty]
		public bool ShowWithdrawalBtn { get; set; }
		public string token { get; set; }
		public List<PaymentRequest> paymentRequests { get; set; }


		public IndexModel(CRMDBContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
		{
			_context = context;
			_userManager = userManager;
			_db = db;

		}
		public async Task<IActionResult> OnGet()
		{
			//
			
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Redirect("/login");

			}
            paymentRequests = _context.PaymentRequests.Where(e => e.AffiliateId == user.Id).ToList();

            token = "https://techsitekw.com?affiliateId=" + user.AffiliateToken;
            //token = "https://localhost:44352?affiliateId=" + user.AffiliateToken;

            var request = _context.PaymentRequests.Where(e => e.AffiliateId == user.Id).OrderByDescending(e => e.PaymentRequestId).FirstOrDefault();
			if (user.Point > 0)
			{
				if (request == null)
				{
					ShowWithdrawalBtn = true;
				}
				else
				{
					ShowWithdrawalBtn = request.RequestStatusId == 1 ? false : true;
				}

			}
			else
			{
				ShowWithdrawalBtn = false;
			}


			var Sites = _context.Sites.Where(e => e.AffiliateUser == user.Id).ToList();
			SitesCount = Sites.Count();
			SitesAmount = Sites.Sum(e => e.UserTemplatePrice);
			CurrentPointsCount = user.Point;
			var PointCost = _context.PointConfigurations.FirstOrDefault().Value;
			CurrentPointsAmount = CurrentPointsCount * PointCost;

			PaidPointsAmount = _context.PaymentRequests.Where(e => e.IsPaid == true && e.AffiliateId == user.Id).Sum(e => e.ConfirmAmount);
			PaidPointsCount = _context.PaymentRequests.Where(e => e.IsPaid == true && e.AffiliateId == user.Id).Sum(e => e.ConfirmPoints);

			return Page();

		}
		public IActionResult OnGetSingleAffiliateRequestForView(int RequestId)
		{
			var Result = _context.PaymentRequests.Where(c => c.PaymentRequestId == RequestId).Include(c => c.RequestStatus).Include(c => c.PaymentType).Select(i => new
			{
				RequestDate = i.RequestData.ToShortDateString(),
				RequestPoints = i.RequestPoints,
				CurrentBalance = i.CurrentBalance,
				ConfirmPoints = i.ConfirmPoints,
				ConfirmAmount = i.ConfirmAmount,
				Payment = i.PaymentType.TitleEn,
				IsPaid = i.IsPaid,
				Status = i.RequestStatus.StatusTitleEn

			}).FirstOrDefault();
			return new JsonResult(Result);
		}
		public async Task<IActionResult> OnPostAffiliateWithdrawalNewRequest()
		{
			try
			{
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
				{
					return Redirect("/login");

				}
				var PointCost = _context.PointConfigurations.FirstOrDefault().Value;
				double AffiliateAmount = user.Point * PointCost;
				var PaymentRequest = new PaymentRequest()
				{
					AffiliateId = user.Id,
					IsPaid = false,
					PaymentTypeId = 2,
					RequestData = DateTime.Now,
					CurrentBalance = AffiliateAmount,
					RequestPoints = user.Point,
					RequestStatusId = 1
				};
				_context.PaymentRequests.Add(PaymentRequest);
				_context.SaveChanges();

			}
			catch (Exception)

			{


			}

			return Redirect("/Affiliate/Index");
		}

	}
}
