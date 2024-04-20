using iTech.Data;
using iTech.ViewModel;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Pages
{

public class MLMModel : PageModel
{
    private readonly CRMDBContext _context;
    public IRequestCultureFeature locale;
    public string BrowserCulture;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IUserStore<ApplicationUser> _userStore;
		private readonly IUserEmailStore<ApplicationUser> _emailStore;
		private readonly ILogger<RegisterModel> _logger;
		private readonly IEmailSender _emailSender;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IToastNotification _toastNotification;
		[BindProperty]
		public MLMRegister MLMRegister { get; set; }
		[BindProperty]
		public bool IsMLM { get; set; }
		public string pageTitleEn { get; set; }
    public string pageTitleAr { get; set; }
    public string ContentAr { get; set; }

    public string ContentEn { get; set; }
    public MLMModel(UserManager<ApplicationUser> userManager

			 , IToastNotification toastNotification,
			IUserStore<ApplicationUser> userStore,
			SignInManager<ApplicationUser> signInManager,
			ILogger<RegisterModel> logger,
			CRMDBContext context,
			RoleManager<IdentityRole> roleManager,
			IEmailSender emailSender)
		{
			_userManager = userManager;
			_userStore = userStore;
			//_emailStore = GetEmailStore();
			_signInManager = signInManager;
			_logger = logger;
			_emailSender = emailSender;
			_context = context;
			_toastNotification = toastNotification;
			_roleManager = roleManager;
		}

		public void OnGet()
    {
        locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
        BrowserCulture = locale.RequestCulture.UICulture.ToString();
        var pageContent = _context.PageContents.FirstOrDefault(p => p.PageContentId == 4);
        if (pageContent != null)
        {
            ContentAr = pageContent.ContentAr;
            ContentEn = pageContent.ContentEn;
            pageTitleAr = pageContent.PageTitleAr;
            pageTitleEn = pageContent.PageTitleEn;
        }
    }
}
}

