using iTech.Data;
using iTech.DataTables;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Linq.Dynamic.Core;

namespace iTech.Areas.Admin.Pages.Configurations.ManageSiteRequests
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public SiteModificationRequest SiteModificationRequest { get; set; }


        public List<SiteModificationRequest> SiteModificationRequestList = new List<SiteModificationRequest>();

        public SiteModificationRequest SiteModificationRequestObj { get; set; }

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            SiteModificationRequest = new SiteModificationRequest();
            SiteModificationRequestObj = new SiteModificationRequest();
        }
      
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }

        public async Task<JsonResult> OnPostAsync()
        {
            var recordsTotal = _context.SiteModificationRequests.Count();

            var customersQuery = _context.SiteModificationRequests.OrderBy(a => a.StartDate).AsQueryable();

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

        public IActionResult OnGetSingleSiteModificationRequestForEdit(int SiteModificationRequestId)
        {
            SiteModificationRequest = _context.SiteModificationRequests.Where(c => c.SMReqId == SiteModificationRequestId).FirstOrDefault();

            return new JsonResult(SiteModificationRequest);
        }

        public IActionResult OnGetSingleSiteModificationRequestsForView(int SiteModificationRequestId)
        {
            var Result = _context.SiteModificationRequests.Where(c => c.SMReqId == SiteModificationRequestId).FirstOrDefault();
            return new JsonResult(Result);
        }

        public async Task<IActionResult> OnPostEditSiteModificationRequest(int SiteModificationRequestId, IFormFile Editfile)
        {
            try
            {
                var model = _context.SiteModificationRequests.Where(c => c.SMReqId == SiteModificationRequestId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("SiteModificationRequest Not Found");

                    return Redirect("/Admin/Configurations/SiteModificationRequests/Index");
                }

                var UpdatedSiteModificationRequest = _context.SiteModificationRequests.Attach(model);

                UpdatedSiteModificationRequest.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("SiteModificationRequest Edited successfully");

                return Redirect("/Admin/Configurations/SiteModificationRequests/Index");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/Configurations/SiteModificationRequests/Index");
        }
        public async Task<IActionResult> OnPostAddSiteModificationRequest(IFormFile file)
        {
            try
            {
               

                _context.SiteModificationRequests.Add(SiteModificationRequest);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("SiteModificationRequest Added Successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/Admin/Configurations/SiteModificationRequests/Index");
        }

        public IActionResult OnGetSingleSiteModificationRequestForDelete(int SiteModificationRequestId)
        {
            SiteModificationRequest = _context.SiteModificationRequests.Where(c => c.SMReqId == SiteModificationRequestId).FirstOrDefault();
            return new JsonResult(SiteModificationRequest);
        }

        public async Task<IActionResult> OnPostSiteModificationRequestDelete(int SiteModificationRequestId)
        {
            try
            {
                SiteModificationRequest SiteModificationRequestObj = _context.SiteModificationRequests.Where(e => e.SMReqId == SiteModificationRequestId).FirstOrDefault();
                
                if (SiteModificationRequestObj != null)
                {


                    _context.SiteModificationRequests.Remove(SiteModificationRequestObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("SiteModificationRequest Deleted successfully");


                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Something went wrong Try Again");
                }
            }
            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
            }

            return Redirect("/Admin/Configurations/SiteModificationRequests/Index");
        }

        private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }

    }
}
