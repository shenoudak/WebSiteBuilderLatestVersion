using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.iTech.Pages.Configurations.ManageCategory
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public SiteCategory category { get; set; }


        public List<SiteCategory> categoriesList = new List<SiteCategory>();

        public SiteCategory categoryObj { get; set; }

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            category = new SiteCategory();
            categoryObj = new SiteCategory();
        }
        public void OnGet()
        {
            categoriesList = _context.SiteCategories.ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }

        public IActionResult OnGetSingleCategoryForEdit(int SiteCategoryId)
        {
            category = _context.SiteCategories.Where(c => c.SiteCategoryId == SiteCategoryId).FirstOrDefault();

            return new JsonResult(category);
        }

        public async Task<IActionResult> OnPostEditCategory(int SiteCategoryId, IFormFile Editfile)
        {
            try
            {
                var model = _context.SiteCategories.Where(c => c.SiteCategoryId == SiteCategoryId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Category Not Found");

                    return Redirect("/CRM/Configurations/ManageCategory/Index");
                }


                if (Editfile != null)
                {


                    string folder = "Images/Category/";

                    model.Pic = UploadImage(folder, Editfile);
                }
                else
                {
                    model.Pic = category.Pic;
                }

              
                model.IsActive = category.IsActive;
                model.TitleAr = category.TitleAr;
                model.TitleEn = category.TitleEn;
                model.SortOrder = category.SortOrder;

                var UpdatedCategory = _context.SiteCategories.Attach(model);

                UpdatedCategory.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Category Edited successfully");

                return Redirect("/CRM/Configurations/ManageCategory/Index");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/CRM/Configurations/ManageCategory/Index");
        }


        public IActionResult OnGetSingleCategoryForView(int SiteCategoryId)
        {
            var Result = _context.SiteCategories.Where(c => c.SiteCategoryId == SiteCategoryId).FirstOrDefault();
            return new JsonResult(Result);
        }


        public IActionResult OnGetSingleCategoryForDelete(int SiteCategoryId)
        {
            category = _context.SiteCategories.Where(c => c.SiteCategoryId == SiteCategoryId).FirstOrDefault();
            return new JsonResult(category);
        }

        public async Task<IActionResult> OnPostCategoryDelete(int SiteCategoryId)
        {
            try
            {
                SiteCategory CatObj = _context.SiteCategories.Where(e => e.SiteCategoryId == SiteCategoryId).FirstOrDefault();


                if (CatObj != null)
                {


                    _context.SiteCategories.Remove(CatObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("Category Deleted successfully");

                    var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, "/" + category.Pic);

                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
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

            return Redirect("/CRM/Configurations/ManageCategory/Index");
        }


        public async Task<IActionResult> OnPostAddCategory(IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    string folder = "Images/Category/";
                    category.Pic = UploadImage(folder, file);
                }

                _context.SiteCategories.Add(category);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Category Added Successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/CRM/Configurations/ManageCategory/Index");
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
