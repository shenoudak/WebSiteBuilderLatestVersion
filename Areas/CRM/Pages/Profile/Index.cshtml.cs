using iTech.Data;
using iTech.Models;
using iTech.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace iTech.Areas.CRM.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private CRMDBContext _context;

        public ApplicationUser Admin { get; set; }
        public IndexModel(CRMDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            //Admin = await _userManager.GetUserAsync(User);
            //if (Admin == null)
            //{
            //    return Redirect("/identity/account/login");

            //}
            if (Request.Query.ContainsKey("handler"))
            {
                var handler = Request.Query["handler"].ToString();
                if (!string.IsNullOrEmpty(handler))
                {
                    OnGetPartial(handler);
                }
            }

            return Page();
        }
        public IActionResult OnGetPartial(string handler)
        {
            switch (handler)
            {
                case "_Overview":
                    return OnGet_Overview();
                case "_ProfileSettings":
                    return OnGet_ProfileSettings();
                case "_SMLinks":
                    return OnGet_SMLinks();
                case "_NewLetters":
                    return OnGet_NewLetters();
                case "_PublicSlider":
                    return OnGet_PublicSlider();
                case "_PageContent":
                    return OnGet_PageContent();
                case "_Services":
                    return OnGet_Services();
            }

            return NotFound();
        }
        public IActionResult OnGet_Overview()
        {
            return Partial("_Overview");
        }

        public IActionResult OnGet_ProfileSettings()
        {
            return Partial("_ProfileSettings");
        }

        public IActionResult OnGet_Services()
        {
            return Partial("_Services");
        }

        public IActionResult OnGet_PublicSlider()
        {
            return Partial("_PublicSlider");
        }

        public IActionResult OnGet_SMLinks()
        {
            return Partial("_SMLinks");
        }

        public IActionResult OnGet_NewLetters()
        {
            return Partial("_NewLetters");
        }
        public IActionResult OnGet_PublicHomeContent()
        {
            return Partial("_PublicHomeContent");
        }

        public IActionResult OnGet_PointConfiguration()
        {
            return Partial("_PointConfiguration");
        }

        public IActionResult OnGet_PageContent()
        {
            return Partial("_PageContent");
        }


    }
}
