using iTech.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.Admin.Pages.PublicHomeContent
{
    public class EditPublicHomeContentModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _hostEnvironment;


        [BindProperty]
        public Models.PublicHomeContent publicHomeContent { get; set; }

        public EditPublicHomeContentModel(CRMDBContext context, IToastNotification toastNotification, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _toastNotification = toastNotification;
            _hostEnvironment = hostEnvironment;
        }


        public ActionResult OnGet(int id)
        {
            var _publicHomeContent = _context.PublicHomeContents.FirstOrDefault(a => a.PublicHomeContentId == id);
            if (_publicHomeContent == null)
            {
                return NotFound();
            }
            else
            {
                publicHomeContent = _publicHomeContent;

            }
            return Page();

        }

        public async Task<IActionResult> OnPostEditHomePage(IFormFile Editfile)
        {
            try
            {
                var model = _context.PublicHomeContents.Where(c => c.PublicHomeContentId == publicHomeContent.PublicHomeContentId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Public Home Content Not Found");

                    return Redirect("/Admin/PublicContentEditor");
                }


                if (Editfile != null)
                {


                    string folder = "Images/PublicHomeContent/";

                    model.Pic = await UploadImage(folder, Editfile);
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

                return Redirect("/Admin/PublicContentEditor");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/PublicContentEditor");
        }


        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }
    }
}

