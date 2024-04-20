using iTech.Data;
using iTech.DataTables;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace iTech.Areas.Admin.Pages.Configurations.UserMessages
{
    public class ListManagementModel : PageModel
    {
        private readonly CRMDBContext _context;

        private readonly ILogger<IndexModel> _logger;
        public IList<ContactUs> lstRecords { get; set; }

        public ListManagementModel(ILogger<IndexModel> logger, CRMDBContext context)
        {
            _logger = logger;
            _context = context;
            //lstRecords = new List<ContactUs>();
        }

        public async Task OnGetAsync()
        {
            //Customers = await _context.Customers.ToListAsync();
        }

        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }

        public async Task<JsonResult> OnPostAsync()
        {
            var recordsTotal = _context.contactUs.Count();

            var customersQuery = _context.contactUs.AsQueryable();

            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.Name.ToUpper().Contains(searchText) ||
                    s.Email.ToUpper().Contains(searchText) ||
                    s.Message.ToUpper().Contains(searchText) ||
                    s.SendingDate.ToString().ToUpper().Contains(searchText)
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
