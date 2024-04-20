using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using System.Reflection.Metadata;

namespace iTech.Pages
{
    public class ServicesDetailsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly CRMDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public Services services { get; set; }
        public string phone { get; set; }
        public ServicesDetailsModel(IToastNotification toastNotification, ILogger<IndexModel> logger, CRMDBContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _toastNotification = toastNotification;
            _context = context;
            _userManager = userManager;
            services = new Services();
        }
        public void OnGet(int id)
        {
            services = _context.Services.Where(e => e.ServicesId == id).FirstOrDefault();
            phone = _context.SocialMediaLinks.Where(e => e.SocialMediaLinkId == 1).FirstOrDefault().ContactPhone1;
        }
    }
}
