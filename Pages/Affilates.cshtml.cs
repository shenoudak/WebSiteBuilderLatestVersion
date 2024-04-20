using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Localization;


namespace iTech.Pages
{
    public class AffilatesModel : PageModel
    {
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly CRMDBContext _context;
        [BindProperty]
        public AffiliateVideo videoPublicSite { get; set; }
        [BindProperty]
        public AffilateFeedback affilateFeedback { get; set; }
        [BindProperty]
        public List<AffiliatePrice> affiliatePrices { get; set; }
        public double PointValue { get; set; }

        public AffilatesModel(CRMDBContext context)
        {

            _context = context;

        }
        public void OnGet()
        {
            try
            {

                locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                BrowserCulture = locale.RequestCulture.UICulture.ToString();
                videoPublicSite = _context.AffiliateVideos.FirstOrDefault();
                affilateFeedback = _context.AffilateFeedbacks.FirstOrDefault();
                affiliatePrices = _context.AffiliatePrices.ToList();
                PointValue = _context.PointConfigurations.FirstOrDefault().Value;


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
