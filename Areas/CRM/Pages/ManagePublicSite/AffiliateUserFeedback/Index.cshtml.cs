using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.CRM.Pages.ManagePublicSite.AffiliateUserFeedback
{
    public class IndexModel : PageModel
    {
		private CRMDBContext _context;
		private readonly IWebHostEnvironment _hostEnvironment;
		private readonly IToastNotification _toastNotification;


		public string url { get; set; }


		[BindProperty]
		public AffilateFeedback affilateFeedback { get; set; }

		public List<AffilateFeedback> affilateFeedbacks = new List<Models.AffilateFeedback>();




		public IndexModel(CRMDBContext context
						, IWebHostEnvironment hostEnvironment
						, IToastNotification toastNotification)
		{
			_context = context;
			_hostEnvironment = hostEnvironment;
			_toastNotification = toastNotification;
			affilateFeedback = new AffilateFeedback();
		}
		public void OnGet()
		{
			affilateFeedbacks = _context.AffilateFeedbacks.ToList();

			url = $"{this.Request.Scheme}://{this.Request.Host}";
		}
		public IActionResult OnGetSingleAffiliateFeedbackForEdit(int AffilateFeedbackId)
		{
			var Result = _context.AffilateFeedbacks.Where(c => c.AffilateFeedbackId == AffilateFeedbackId).FirstOrDefault();
			return new JsonResult(Result);

		}

		public async Task<IActionResult> OnPostEditAffiliateFeedback(int AffilateFeedbackId)
		{
			try
			{
				var model = _context.AffilateFeedbacks.Where(c => c.AffilateFeedbackId == AffilateFeedbackId).FirstOrDefault();
				if (model == null)
				{
					_toastNotification.AddErrorToastMessage("Affiliate Feedback is not Existed");

					return Redirect("/CRM/ManagePublicSite/AffiliateUserFeedback/Index");
				}


				model.TitleAr = affilateFeedback.TitleAr;
				model.TitleEn = affilateFeedback.TitleEn;
				model.UserOneFeedbackEn = affilateFeedback.UserOneFeedbackEn;
				model.UserOneFeedbackAr = affilateFeedback.UserOneFeedbackAr;
				model.UserTwoFeedbackAr = affilateFeedback.UserTwoFeedbackAr;
				model.UserTwoFeedbackEn = affilateFeedback.UserTwoFeedbackEn;
				model.UserThreeFeedbackAr = affilateFeedback.UserThreeFeedbackAr;
				model.UserThreeFeedbackEn = affilateFeedback.UserThreeFeedbackEn;
				var UpdatedAffliateFeedback = _context.AffilateFeedbacks.Attach(model);
				UpdatedAffliateFeedback.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				await _context.SaveChangesAsync();
				_toastNotification.AddSuccessToastMessage("Affliate Feedbacks Edited successfully ");


			}
			catch (Exception)
			{
				_toastNotification.AddErrorToastMessage("Affliate Feedbacks can't be edited");

			}
			return Redirect("/CRM/ManagePublicSite/AffiliateUserFeedback/Index");
		}
	}
}
