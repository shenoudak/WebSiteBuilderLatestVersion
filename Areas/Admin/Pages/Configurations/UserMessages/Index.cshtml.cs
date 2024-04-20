using iTech.Data;
using iTech.DataTables;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Linq.Dynamic.Core;

namespace iTech.Areas.Admin.Pages.Configurations.UserMessages
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        


        public List<ContactUs> contactus = new List<ContactUs>();

       

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            contactus = new List<ContactUs>();
        }
        //public void OnGet()
        //{
        //    url = $"{this.Request.Scheme}://{this.Request.Host}";
        //}

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
        public IActionResult OnGetSingleMessageForView(int ContactId)
        {
            var Result = _context.contactUs.Where(c => c.ContactUsID == ContactId).FirstOrDefault();

            return new JsonResult(Result);
        }
        public IActionResult OnGetSingleMessageForDelete(int ContactId)
        {
            var contactUs = _context.contactUs.Where(c => c.ContactUsID == ContactId).FirstOrDefault();
            return new JsonResult(contactUs);
        }
        public async Task<IActionResult> OnPostDeleteMessage(int ContactId)
        {
            try
            {
                Models.ContactUs contactUs = _context.contactUs.Where(e => e.ContactUsID == ContactId).FirstOrDefault();


                if (contactUs != null)
                {
                    _context.contactUs.Remove(contactUs);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("User Message Deleted successfully");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("User Message can't be deleted");
                }
            }
            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("User Message can't be deleted");
            }

            return Redirect("/Admin/Configurations/UserMessages/Index");
        }

    }
}
