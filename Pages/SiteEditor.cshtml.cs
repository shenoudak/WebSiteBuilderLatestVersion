using iTech.Data;

using iTech.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Pages
{
    public class SiteEditorModel : PageModel
    {
        private readonly CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string DomainName { get; set; }

        public string stylesfiles { get; set; }
        public string jsfiles { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        public string url { get; set; }

        public SiteEditorModel(CRMDBContext context, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;

        }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                url = $"{this.Request.Scheme}://{this.Request.Host}";
                var user = await _userManager.GetUserAsync(User);
                if (user is null)
                {
                    return Redirect("/Login");
                }
                else
                {
                    SiteId = id;

                    var Site = _context.Sites.Where(s => s.SiteId == id).FirstOrDefault();
                    if (Site != null)
                    {
                        if (Site.UserID == user.Id || User.IsInRole("Admin"))
                        {
                            SiteName = Site.SiteTitle;
                            DomainName = Site.UserDomain;

                            string styles = Path.Combine(_hostEnvironment.WebRootPath, Site.SiteTitle + "\\canvasstyle.txt").ToLower();
                            string js = Path.Combine(_hostEnvironment.WebRootPath, Site.SiteTitle + "\\canvasjs.txt").ToLower();

                            stylesfiles = System.IO.File.ReadAllText(styles);
                            jsfiles = System.IO.File.ReadAllText(js);
                        }
                        else
                        {
                            return Redirect("/Login");

                        }

                    }
                }



                return Page();


            }
            catch
            {
                stylesfiles = null!;
                jsfiles = null!;
                return Page();
            }

        }
        public IActionResult OnGetContact(string name, string email, string message)
        {

            return Page();
        }
        public IActionResult OnPostSaveProjectData(int siteID)
        {
            return default;
        }

        public IActionResult OnPostEditSitePage(int SiteId, string ProjectData,
                            string HTML, string CSS, string PageName)
        {
            //select site path form db
            //var SitePage = _context.SitePage.where(e=>e.SitePageId==SitePageId).FirstOrDefault();
            var Site = _context.Sites.Where(e => e.SiteId == SiteId).FirstOrDefault();
            string PagePath = Path.Combine(_hostEnvironment.WebRootPath, Site.SiteTitle).ToLower();


            if (PagePath != null)
            {
                FileInfo fileInfo = new FileInfo(Site.SiteTitle);

                if (fileInfo != null)
                {
                    fileInfo.Delete();
                }
            }

            string BootStrap = "<link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD\" crossorigin=\"anonymous\">\r\n" +
                "<script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js\" integrity=\"sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN\" crossorigin=\"anonymous\"></script>\r\n";

            string Style = "<head>\n" + BootStrap + "<style>\n" + CSS + "\n </style> \n</head>";

            string File = Style + HTML;

            System.IO.File.WriteAllText(@"" + PagePath + "/" + PageName + ".html", File);


            string ProjectDataPath = Path.Combine(_hostEnvironment.WebRootPath, Site.SiteTitle + "\\" + "ProjectData.json").ToLower();

            TextWriter sw = new StreamWriter(ProjectDataPath);

            //Write a line of text
            sw.Write(string.Empty);
            sw.Write(ProjectData);
            //Close the file
            sw.Close();


            return new JsonResult(true);
        }
    }
}
