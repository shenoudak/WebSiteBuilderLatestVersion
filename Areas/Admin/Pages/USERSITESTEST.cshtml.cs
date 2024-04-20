using iTech.Data;
using iTech.Models;
using iTech.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;


namespace iTech.Areas.Admin.Pages
{
    public class USERSITESTESTModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public int UsersCount { get; set; }
        public IList<ApplicationUser> Users { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        private CRMDBContext _context;

        public List<Site> siteList { get; set; }
        public int PageSize { get; } = 20; // Number of items to display per page.
        public int SitesCount { get; set; }
        public List<Site> CurrentPageSites { get; private set; }

        public USERSITESTESTModel(CRMDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                SitesCount = _context.Sites.Where(e => e.UserID == user.Id && e.IsActive == true).Count();

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

    }
}
