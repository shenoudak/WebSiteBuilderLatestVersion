using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using iTech.Data;
using iTech.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;
using iTech.DataTables;
using System.Linq.Dynamic.Core;
namespace iTech.Areas.Editor.Pages
{
	public class IndexModel : PageModel
	{
		private CRMDBContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ApplicationDbContext _db;
		public List<SiteModificationRequest> SiteModificationRequest { get; set; }

        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }

        public IndexModel(CRMDBContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
		{
			_context = context;
			_userManager = userManager;
			_db = db;

		}
		public async Task<IActionResult> OnGet()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Redirect("/login");

			}

            return Page();

		}

        public async Task<JsonResult> OnPostAsync()
        {
            var recordsTotal = _context.SiteModificationRequests.Count();

            var customersQuery = _context.SiteModificationRequests.AsQueryable();

            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.ModificationDescription.ToUpper().Contains(searchText) ||
                    s.Notes.ToUpper().Contains(searchText)
                );
            }

            var recordsFiltered = customersQuery.Count();

            var sortColumnName = DataTablesRequest.Columns.ElementAt(DataTablesRequest.Order.ElementAt(0).Column).Name;
            var sortDirection = DataTablesRequest.Order.ElementAt(0).Dir.ToLower();

            // using System.Linq.Dynamic.Core
            customersQuery = customersQuery.OrderBy($"{sortColumnName} {sortDirection}");

            var skip = DataTablesRequest.Start;
            var take = DataTablesRequest.Length;
            var data = await customersQuery
                .Skip(skip)
                .Take(take)
                .ToListAsync();

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
