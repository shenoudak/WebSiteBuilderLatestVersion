using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.iTech.Pages.Configurations.ManagePublicHomeContent
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public PublicHomeContent publicHomeContent { get; set; }


        public List<PublicHomeContent> publicHomeContentList = new List<PublicHomeContent>();

        public PublicHomeContent publicHomeContentObj { get; set; }

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            publicHomeContent = new PublicHomeContent();
            publicHomeContentObj = new PublicHomeContent();
        }
        public void OnGet()
        {
            publicHomeContentList = _context.PublicHomeContents.ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }

        public IActionResult OnGetSinglePublicHomeContentForEdit(int PublicHomeContentId)
        {
            publicHomeContent = _context.PublicHomeContents.Where(c => c.PublicHomeContentId == PublicHomeContentId).FirstOrDefault();

            return new JsonResult(publicHomeContent);
        }

        public async Task<IActionResult> OnPostEditPublicHomeContent(int PublicHomeContentId, IFormFile Editfile)
        {
            try
            {
                var model = _context.PublicHomeContents.Where(c => c.PublicHomeContentId == PublicHomeContentId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Public Home Content Not Found");

                    return Redirect("/CRM/Configurations/ManagePublicHomeContent/Index");
                }


                if (Editfile != null)
                {


                    string folder = "Images/PublicHomeContent/";

                    model.Pic = UploadImage(folder, Editfile);
                }
                else
                {
                    model.Pic = publicHomeContent.Pic;
                }

              
                model.AboutSectionDescEn = publicHomeContent.AboutSectionDescEn;
                model.AboutSectionDescAr = publicHomeContent.AboutSectionDescAr;
                model.AboutSectionTitleAr = publicHomeContent.AboutSectionTitleAr;
                model.AboutSectionTitleEn = publicHomeContent.AboutSectionTitleEn;

                var UpdatedPublicHomeContent = _context.PublicHomeContents.Attach(model);

                UpdatedPublicHomeContent.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Category Edited successfully");

                return Redirect("/CRM/Configurations/ManagePublicHomeContent/Index");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/CRM/Configurations/ManagePublicHomeContent/Index");
        }


        public IActionResult OnGetSinglePublicHomeContentForView(int PublicHomeContentId)
        {
            var Result = _context.PublicHomeContents.Where(c => c.PublicHomeContentId == PublicHomeContentId).FirstOrDefault();
            return new JsonResult(Result);
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
