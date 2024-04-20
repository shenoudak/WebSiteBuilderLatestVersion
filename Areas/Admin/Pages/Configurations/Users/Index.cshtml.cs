using iTech.Data;
using iTech.DataTables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace iTech.Areas.Admin.Pages.Configurations.Users
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
       
        public IList<ApplicationUser> Users { get; set; }
        private CRMDBContext _context;
		public int SitesCount { get; set; }
		public IndexModel(CRMDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {
            Users = await _userManager.GetUsersInRoleAsync("User");
            //PointsCount = AffiliateUsers.Sum(e => e.Point);
            //AffliateCount = AffiliateUsers.Count();
            foreach (var user in Users)
            {
                SitesCount = _context.Sites.Where(e => e.UserID == user.Id && e.IsActive == true).Count();
            }
            return Page();
        }


		[BindProperty]
		public DataTablesRequest DataTablesRequest { get; set; }

		public async Task<JsonResult> OnPostAsync()
		{
			var users = await _userManager.GetUsersInRoleAsync("User");
			var recordsTotal = users.Count();

			var customersQuery = users.AsQueryable();

			var searchText = DataTablesRequest.Search.Value?.ToUpper();
			if (!string.IsNullOrWhiteSpace(searchText))
			{
				customersQuery = customersQuery.Where(s =>
					s.Email.ToUpper().Contains(searchText) ||
					s.FullName.ToUpper().Contains(searchText) ||
					s.UserName.ToUpper().Contains(searchText)
					
				);
			}

			var recordsFiltered = customersQuery.Count();

			var sortColumnName = DataTablesRequest.Columns.ElementAt(DataTablesRequest.Order.ElementAt(0).Column).Name;
			var sortDirection = DataTablesRequest.Order.ElementAt(0).Dir.ToLower();

			// using System.Linq.Dynamic.Core
			if (sortColumnName!=null&& sortDirection!=null)
			{
                customersQuery = customersQuery.OrderBy($"{sortColumnName} {sortDirection}");

            }
			

			var skip = DataTablesRequest.Start;
			var take = DataTablesRequest.Length;
			var data =  customersQuery
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
