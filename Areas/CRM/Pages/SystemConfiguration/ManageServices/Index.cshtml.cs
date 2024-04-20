using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.CRM.Pages.SystemConfiguration.ManageServices
{
    public class IndexModel : PageModel
    {
		private CRMDBContext _context;
		private readonly IWebHostEnvironment _hostEnvironment;
		private readonly IToastNotification _toastNotification;


		[BindProperty]
		public Services service { get; set; }
		public List<Services> Services { set; get; }


		public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
		{
			_context = context;
			_hostEnvironment = hostEnvironment;
			_toastNotification = toastNotification;

		}
		public ActionResult OnGet()
		{
			Services = _context.Services.ToList();

			return Page();

		}
	}
}
