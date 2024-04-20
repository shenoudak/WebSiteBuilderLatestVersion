using iTech.Data;
using iTech.DataTables;
using iTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace iTech.Areas.Admin.Pages.Configurations.ManagePaymentRequest
{
    public class IndexModel : PageModel
    {

        private readonly UserManager<ApplicationUser> _userManager;
        public int PaymentRequestsCount { get; set; }
        public List<PaymentRequest> PaymentRequests { get; set; }
        private CRMDBContext _context;

        public IndexModel(CRMDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> OnGet()
        {

            PaymentRequests =  _context.PaymentRequests.Include(e => e.PaymentType).ToList();

            PaymentRequestsCount = PaymentRequests.Count();
            
            return Page();
        }
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }

        public async Task<JsonResult> OnPostAsync()
        {
            var recordsTotal = _context.PaymentRequests.Include(e => e.PaymentType).Count();

            var customersQuery = _context.PaymentRequests.Include(e => e.PaymentType).AsQueryable();

            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                
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
