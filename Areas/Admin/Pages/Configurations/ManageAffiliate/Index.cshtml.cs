using iTech.Data;
using iTech.DataTables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace iTech.Areas.Admin.Pages.Configurations.ManageAffiliate
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
       public int AffliateCount { get; set; }
        public int SitesCount { get; set; }
        public double PointsCount { get; set; }
        public double SitesReveuve { get; set; }
        public IList<ApplicationUser> AffiliateUsers{ get; set; }
           private CRMDBContext _context;

        public IndexModel(CRMDBContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {
            AffiliateUsers = await _userManager.GetUsersInRoleAsync("Affiliate");
            PointsCount= AffiliateUsers.Sum(e => e.Point);
            AffliateCount = AffiliateUsers.Count();
            foreach (var user in AffiliateUsers)
            {
                SitesCount = _context.Sites.Where(e => e.AffiliateUser == user.Id&&e.IsActive==true).Count();
                SitesReveuve = _context.Sites.Where(e => e.AffiliateUser == user.Id && e.IsActive == true).Sum(e=>e.UserTemplatePrice);
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
            //var data = await  customersQuery
            //    .Skip(skip)
            //    .Take(take)
            //    .ToListAsync();
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
