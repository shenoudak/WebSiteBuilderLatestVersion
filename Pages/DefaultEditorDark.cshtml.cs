using iTech.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using System.Linq.Dynamic.Core;
using System.Text;

namespace iTech.Pages
{
    public class DefaultEditorDarkModel : PageModel
    {
        private readonly CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public int SiteId { get; set; }
        public int PlanId { get; set; }
        public string SiteName { get; set; }
        public string DomainName { get; set; }
        public IEnumerable<string> TemplatePages { get; set; }

        public string stylesfiles { get; set; }
        public string jsfiles { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        public string url { get; set; }

        public DefaultEditorDarkModel(CRMDBContext context, IWebHostEnvironment hostEnvironment, UserManager<ApplicationUser> userManager)
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
                        PlanId = _context.SiteSubscriptions.Where(a => a.SiteId == id).Select(a => a.PlanId).FirstOrDefault();
                        if (Site.UserID == user.Id || User.IsInRole("Admin") || User.IsInRole("WebEditor"))
                        {
                            SiteName = Site.SiteTitle;
                            DomainName = Site.UserDomain;

                            if(Site.TemplateId != 1)
                            {
                                string styles = Path.Combine(_hostEnvironment.WebRootPath, Site.SiteTitle + "\\canvasstyle.txt").ToLower();
                                string js = Path.Combine(_hostEnvironment.WebRootPath, Site.SiteTitle + "\\canvasjs.txt").ToLower();

                                stylesfiles = System.IO.File.ReadAllText(styles);
                                jsfiles = System.IO.File.ReadAllText(js);

                            }
                        }
                        else
                        {
                            return Redirect("/Login");

                        }

                    }
                    TemplatePages = GetHtmlFiles(SiteName);

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

        private IEnumerable<string> GetHtmlFiles(string templateName)
        {
            var wwwrootPath = Path.Combine(_hostEnvironment.WebRootPath, templateName);
            var directoryInfo = new DirectoryInfo(wwwrootPath);

            if (directoryInfo.Exists)
            {
                var htmlFiles = directoryInfo.GetFiles("*.html").Select(file =>
                {
                    AddCharsetMetaTag(file.FullName);
                    return file.Name;
                }); return htmlFiles;
            }

            return Enumerable.Empty<string>();
        }

        private void AddCharsetMetaTag(string filePath)
        {
            string fileContent = System.IO.File.ReadAllText(filePath);
            int headIndex = fileContent.IndexOf("<head>", StringComparison.OrdinalIgnoreCase);
            if (headIndex != -1)
            {
                string modifiedContent = fileContent.Insert(headIndex + "<head>".Length, "\n<meta charset=\"UTF-8\">");
                System.IO.File.WriteAllText(filePath, modifiedContent);
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

            sw.Write(string.Empty);
            sw.Write(ProjectData);
            sw.Close();

            return new JsonResult(true);
        }
    }
}
