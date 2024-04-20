using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using iTech.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc.Filters;
using NToastNotify;

namespace iTech.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EditorApiController : ControllerBase
    {

        private readonly CRMDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;

        public EditorApiController(CRMDBContext context, IToastNotification toastNotification, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;

        }



        [HttpPost]
        //[Route("{id}")]
        public async Task SaveProjectData()
        {
            string body = "";


            using (StreamReader stream = new StreamReader(Request.Body))
            {
                body = await stream.ReadToEndAsync();
            }


            //string body = await new StreamReader(Request.Body).ReadToEndAsync();


            //Posted Data
            JObject data = JObject.Parse(body);

            var JObjectSiteId = data["id"];

            var SiteId = ((int)JObjectSiteId);

            //ProjectData in Posted Data
            var ProjectData = data["data"];

            //Pages in Project Data
            var Pages = ProjectData["pages"];

            var userPlan = _context.SiteSubscriptions.Where(a => a.SiteId == SiteId).FirstOrDefault();

            
                if (userPlan is not null)
                {
                if ((userPlan.PlanId == 3) || (Pages.Count() <= 3 && userPlan.PlanId == 1))
                {
                  //Store Page Names
                        string[] PagesName = new string[Pages.Count()];

                        int counter = 0;

                        foreach (var page in Pages)
                        {

                            PagesName[counter] = page["id"].ToString();

                            counter++;

                        }


                        //Pages Content in Posted Data

                        var ContentOfPages = data["pagesHtml"];


                        string[] HTMLContent = new string[ContentOfPages.Count()];
                        string[] CSSContent = new string[ContentOfPages.Count()];


                        int PageContentCounter = 0;

                        var dbUserSite = _context.Sites.Where(s => s.SiteId == SiteId).FirstOrDefault();

                        /* string SitePath = Path.Combine(_hostEnvironment.WebRootPath, "site test1").ToLower();*///testing
                                                                                                                  //SiteName From DataBase


                        //string SitePath = "";
                        //if (dbUserSite.UserDomain is not null)
                        //{
                        //    SitePath = Path.Combine(_hostEnvironment.WebRootPath, dbUserSite.UserDomain).ToLower();
                        //}
                        //else
                        //{
                        string SitePath = Path.Combine(_hostEnvironment.WebRootPath, dbUserSite.SiteTitle).ToLower();
                        //}

                        //if (Directory.Exists(SitePath))
                        //{
                        //    Directory.Delete(SitePath,true);
                        //}


                        foreach (string filePath in Directory.GetFiles(SitePath, "*.html"))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        //Create Directory
                        //CreateDir("site test1");//Testing
                        CreateDir(SitePath);
                        string StyleLinkPath = "";
                        string StyleLink = "";
                        string JsLinkPath = "";
                        string JsLinks = "";
                        //if (dbUserSite.UserDomain is not null)
                        //{
                        //    StyleLinkPath = Path.Combine(_hostEnvironment.WebRootPath, dbUserSite.UserDomain + "\\StyleLink.txt").ToLower();

                        //    JsLinkPath = Path.Combine(_hostEnvironment.WebRootPath, dbUserSite.UserDomain + "\\JsLink.txt").ToLower();
                        //}
                        //else
                        //{
                        StyleLinkPath = Path.Combine(_hostEnvironment.WebRootPath, dbUserSite.SiteTitle + "\\StyleLink.txt").ToLower();

                        JsLinkPath = Path.Combine(_hostEnvironment.WebRootPath, dbUserSite.SiteTitle + "\\JsLink.txt").ToLower();
                        //}
                        StyleLink = System.IO.File.ReadAllText(StyleLinkPath);

                        JsLinks = System.IO.File.ReadAllText(JsLinkPath);

                        //Store html and css in arrays Save content as files .html
                        foreach (var item in ContentOfPages)
                        {
                            HTMLContent[PageContentCounter] = item["html"].ToString();

                         
                            CSSContent[PageContentCounter] = item["css"].ToString();


                            string BootStrap = "<link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD\" crossorigin=\"anonymous\">\r\n" +
                                "    <script src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/js/bootstrap.bundle.min.js\" integrity=\"sha384-w76AqPfDkMBDXo30jS1Sgez6pr3x5MlQ1ZAGC+nuZB+EYdgRZgiwxhTBTkF7CXvN\" crossorigin=\"anonymous\"></script>\r\n";

                            string Style = "<head>\n" + BootStrap + StyleLink + "<style>\n" + CSSContent[PageContentCounter] + "\n </style> \n<meta charset=\"UTF-8\">\n</head>";

                            string RemoveClosingBodyTag = HTMLContent[PageContentCounter].Substring(0, HTMLContent[PageContentCounter].Length - 7);


                            string File = Style + RemoveClosingBodyTag + JsLinks + "</body>";

                           
                            System.IO.File.WriteAllText(@"" + SitePath + "/" + PagesName[PageContentCounter] + ".html", File);

                            PageContentCounter++;

                        }


                        string path = "";
                       
                        path = Path.Combine(_hostEnvironment.WebRootPath, dbUserSite.SiteTitle + "\\ProjectData.json").ToLower();
                       
                        TextWriter sw = new StreamWriter(path);
                        //Write a line of text
                        sw.Write(string.Empty);
                        sw.Write(ProjectData);
                        //Close the file
                        sw.Close();
                    
                }
                    
                    else
                    {
                        _toastNotification.AddErrorToastMessage("Your Plan is Limited to Only 3 Pages");

                    }
                }
            
           

           
        }


        [HttpGet]
        [Route("{projectID}")]
        public IActionResult LoadProjectData(int projectID)
        {

            var Site = _context.Sites.Where(s => s.SiteId == projectID).FirstOrDefault();
            //string path = Path.Combine(_hostEnvironment.WebRootPath, "site test1\\projectdatatest.txt").ToLower(); //for testing
            //string path = "";
            //    if(Site.UserDomain is not null)
            //{
            //     path = Path.Combine(_hostEnvironment.WebRootPath, Site.UserDomain + "\\ProjectData.json").ToLower();

            //}
            //else
            //{
                var path = Path.Combine(_hostEnvironment.WebRootPath, Site.SiteTitle + "\\ProjectData.json").ToLower();

            //}



            string text = System.IO.File.ReadAllText(path);
            return Ok(text);


            
        }

           

        //********************************Helper Fun************************************//
        [NonAction]
        public void CreateDir(string PagePath)
        {
            string path = Path.Combine(_hostEnvironment.WebRootPath, PagePath).ToLower();

            //if path does not exist -> create it
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }











        // GET: api/Categories
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        //{
        //    var data = await _context.Categories.ToListAsync();

        //    return Ok(new {  data});
        //}

        // GET: api/Categories/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Category>> GetCategory(int id)
        //{
        //    var category = await _context.Categories.FindAsync(id);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return category;
        //}

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCategory(int id, Category category)
        //{
        //    if (id != category.CategoryId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(category).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CategoryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Category>> PostCategory(Category category)
        //{
        //    _context.Categories.Add(category);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);
        //}

        // DELETE: api/Categories/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCategory(int id)
        //{
        //    var category = await _context.Categories.FindAsync(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    //_context.Categories.Remove(category);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CategoryExists(int id)
        //{
        //    //return _context.Categories.Any(e => e.CategoryId == id);
        //}


        private void AddCharsetMetaTagToHtmlFile(string filePath)
        {
            string fileContent = System.IO.File.ReadAllText(filePath);

            int headIndex = fileContent.IndexOf("<head>", StringComparison.OrdinalIgnoreCase);

            if (headIndex != -1)
            {
                if (fileContent.IndexOf("<meta charset=\"UTF-8\">", StringComparison.OrdinalIgnoreCase) == -1)
                {
                    string modifiedContent = fileContent.Insert(headIndex + "<head>".Length, "\n<meta charset=\"UTF-8\">");

                    System.IO.File.WriteAllText(filePath, modifiedContent);
                }
            }
        }

    }
}
 