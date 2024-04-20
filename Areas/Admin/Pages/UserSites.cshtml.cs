using iTech.Data;
using iTech.DataTables;
using iTech.Models;
using iTech.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Drawing.Printing;
using System.Linq.Dynamic.Core;
namespace iTech.Areas.Admin.Pages
{
    public class UserSitesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;

        public int UsersCount { get; set; }
        public IList<ApplicationUser> Users { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        private CRMDBContext _context;
        [BindProperty]
        public Site userSite { get; set; }
        public List<Site> siteList { get; set; }
        public int PageSize { get; } = 20; // Number of items to display per page.
        public int SitesCount { get; set; }
        public List<Site> CurrentPageSites { get; private set; }

        public UserSitesModel(CRMDBContext context, UserManager<ApplicationUser> userManager,IToastNotification toastNotification)
        {
            _context = context;
            _userManager = userManager;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> OnGet(int? pageId)
        {
            Users = await _userManager.GetUsersInRoleAsync("User");
            UsersCount = Users.Count();
            if (pageId == null)
            {
                CurrentPage = 1;
            }
            else
            {
                CurrentPage = pageId.Value;
            }
            foreach (var user in Users)
            {
                SitesCount += _context.Sites.Where(e => e.UserID == user.Id && e.IsActive == true).Count();

                siteList = _context.Sites
                    .Include(e => e.Templates)
                    .ToList();
                int totalItems = siteList.Count;
                int startIndex = (CurrentPage - 1) * PageSize;
                CurrentPageSites = siteList.Skip(startIndex).Take(PageSize).ToList();


            }


            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user is null)
                {
                    return Redirect("/Login");
                }

                var site = _context.Sites.FirstOrDefault(e => e.SiteId == id && e.UserID == user.Id);
                if (site != null)
                {
                    _context.Sites.Remove(site);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

            }

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetSingleUserForView(string UserID)
        {
            ApplicationUser User = await _userManager.FindByIdAsync(UserID);

            return new JsonResult(User);

        }
        public async Task<IActionResult> OnGetSingleUserSiteForEdit(int SiteId)
        {
            Site SiteObj = _context.Sites.Where(c => c.SiteId == SiteId).FirstOrDefault();

            return new JsonResult(SiteObj);

        }
        public async Task<IActionResult> OnPostEditUserSite(int SiteId)
        {
            try
            {
                var model = _context.Sites.Where(c => c.SiteId == SiteId).FirstOrDefault();
                if (model == null)
                {
                    return RedirectToPage();
                }

                model.SiteTitle = userSite.SiteTitle;
                
                var UpdatedUserSite = _context.Sites.Attach(model);
                UpdatedUserSite.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("User Site Edited successfully");


            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");
                return RedirectToPage();
            }
            return RedirectToPage();

        }
        public IActionResult OnGetSingleUserSiteView(int SiteId)
        {
            var Result = _context.Sites.Where(c => c.SiteId == SiteId).FirstOrDefault();
            //var Result = _context.Sites.Where(c => c.SiteId == SiteId).Include(a => a.Templates).FirstOrDefault();
            return new JsonResult(Result);
        }
        public IActionResult OnGetSingleUserSiteForDelete(int SiteId)
        {
            var Site = _context.Sites.Where(c => c.SiteId == SiteId).FirstOrDefault();
            return new JsonResult(Site);
        }

        public async Task<IActionResult> OnPostDeleteUserSite(int SiteId)
        {
            try
            {
                Site SiteObj = _context.Sites.Where(e => e.SiteId == SiteId).FirstOrDefault();


                if (SiteObj != null)
                {


                    _context.Sites.Remove(SiteObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("User Site Deleted successfully");


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

            return RedirectToPage();
        }



    }
}
