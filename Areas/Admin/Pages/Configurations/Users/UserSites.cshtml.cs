using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NToastNotify;
using NToastNotify.Helpers;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Microsoft.AspNetCore.Localization;
using iTech.DataTables;
using System.Linq.Dynamic.Core;

namespace iTech.Areas.Admin.Pages.Configurations.Users
{
    public class UserSitesModel : PageModel
    {
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly ILogger<UserSitesModel> _logger;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CRMDBContext _context;
        [BindProperty]
        public Models.Site SiteObj { get; set; }
        [BindProperty]
        public Template newTemplate { get; set; }
        public static int TemplateId { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        public int SitesCount { get; set; }

        [BindProperty]
        public List<Site> Sites { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        public UserSitesModel(ILogger<UserSitesModel> logger, CRMDBContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            SiteObj = new Site();
            Sites = new List<Site>();

        }

        public async Task<IActionResult> OnGet(string id)
        {
            try
            {
                locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                BrowserCulture = locale.RequestCulture.UICulture.ToString();
                var user = _userManager.Users.Where(e => e.Id == id).FirstOrDefault(); ;
                SitesCount = _context.Sites.Where(e => e.UserID == user.Id && e.IsActive == true).Count();

                if (user is null)
                {
                    return Redirect("/Login");
                }

                var totalItemCount = _context.Sites.Count(e => e.UserID == user.Id);

              
                Sites = _context.Sites
                    .Include(e => e.Templates)
                    .Where(e => e.UserID == user.Id)
                    .ToList();
            }
            catch (Exception)
            {
                return Page();
            }

            return Page();
        }


        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }

        public async Task<JsonResult> OnPostAsync()
        {

            var recordsTotalList = await _userManager.GetUsersInRoleAsync("Affiliate");
            var recordsTotal = recordsTotalList.Count();

            var customersQuery = recordsTotalList.AsQueryable();

            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.UserName.ToUpper().Contains(searchText)
                );
            }

            var recordsFiltered = customersQuery.Count();

            var sortColumnName = DataTablesRequest.Columns.ElementAt(DataTablesRequest.Order.ElementAt(0).Column).Name;
            var sortDirection = DataTablesRequest.Order.ElementAt(0).Dir.ToLower();
            if (sortColumnName != null && sortDirection != null)
            {
                // using System.Linq.Dynamic.Core
                customersQuery = customersQuery.OrderBy($"{sortColumnName} {sortDirection}");
            }

            var skip = DataTablesRequest.Start;
            var take = DataTablesRequest.Length;
            var data = customersQuery
                .Skip(skip)
                .Take(take)
                .ToList();

            return new JsonResult(new
            {
                draw = DataTablesRequest.Draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data
            });
        }
    }
}
