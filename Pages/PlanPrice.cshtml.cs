using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Localization;

namespace iTech.Pages
{
    public class PlanPriceModel : PageModel
    {
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        public int UserTemplateId { get; set; }
        public void OnGet(int TemplateId)
        {
            UserTemplateId = TemplateId;
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
        }
    }
}
