using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NToastNotify;
using System.Security.Policy;
using Microsoft.AspNetCore.Localization;
using iTech.Migrations;


namespace iTech.Pages
{
    public class ChooseTemplateModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly IToastNotification _toastNotification;
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly CRMDBContext _context;

        public List<SiteCategory> siteCategories { get; set; }
        public List<Template> Templates { get; set; }
        public int planPriceId { get; set; }

        public ChooseTemplateModel(IToastNotification toastNotification, ILogger<IndexModel> logger, CRMDBContext context)
        {
            _logger = logger;
            _toastNotification = toastNotification;
            _context = context;

            siteCategories = new List<SiteCategory>();
            Templates = new List<Template>();
        }

        public void OnGet(int? planId)
        {
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
            siteCategories = _context.SiteCategories.Where(e => e.IsActive == true).ToList();
            Templates = _context.Templates.Include(e => e.SiteCategories).Where(e => e.IsActive && e.SiteCategoryId != null).ToList();

            if (planId != null)
            {
                planPriceId = planId.Value;
            }
        }
    }
}
