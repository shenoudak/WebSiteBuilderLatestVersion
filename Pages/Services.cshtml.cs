using iTech.Data;
using iTech.Migrations;
using iTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Pages
{
    public class ServicesModel : PageModel
    {
		private readonly IToastNotification _toastNotification;
		public IRequestCultureFeature locale;
		public string BrowserCulture;
		private readonly CRMDBContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ILogger<IndexModel> _logger;



		//public Services OperationServices { get; set; }
		//public Services StrategyServices { get; set; }
		//public Services MarketingServices { get; set; }


        public List<Services> lstServices { get; set; }


        public ServicesModel(IToastNotification toastNotification, ILogger<IndexModel> logger, CRMDBContext context, UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_toastNotification = toastNotification;
			_context = context;
			_userManager = userManager;
			//OperationServices = new Services();
			//MarketingServices = new Services();
			//StrategyServices = new Services();
			lstServices = new List<Services>();
		}


		public void OnGet()
        {
			lstServices = _context.Services.ToList();

			//OperationServices = _context.Services.Where(e => e.ServicesId == 1).FirstOrDefault();
			//MarketingServices = _context.Services.Where(e => e.ServicesId == 4).FirstOrDefault();
			//StrategyServices = _context.Services.Where(e => e.ServicesId == 3).FirstOrDefault();
		}
    }
}
