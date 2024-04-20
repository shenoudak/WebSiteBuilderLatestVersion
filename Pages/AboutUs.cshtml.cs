using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Localization;
using iTech.Data;
using iTech.ViewModel;

namespace iTech.Pages
{
    public class AboutUsModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private CRMDBContext _context;


        public string ContentAr { get; set; }

        public string ContentEn { get; set; }

		public string phone { get; set; }

		public AboutUsModel(CRMDBContext context)
        {
            _context = context;

        }


        public void OnGet()
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            var pageContent = _context.PageContents.FirstOrDefault(p => p.PageContentId == 2);
            if (pageContent != null)
            {
                ContentAr = pageContent.ContentAr;
                ContentEn = pageContent.ContentEn;

            }
			phone = _context.SocialMediaLinks.Where(e => e.SocialMediaLinkId == 1).FirstOrDefault().ContactPhone1;
		}
    }
}
