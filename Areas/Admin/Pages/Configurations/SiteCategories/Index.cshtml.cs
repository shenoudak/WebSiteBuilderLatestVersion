using iTech.Data;
using iTech.DataTables;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Linq.Dynamic.Core;

namespace iTech.Areas.Admin.Pages.Configurations.SiteCategories
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
        //public void OnGet()
        //{
        //    categoriesList = _context.SiteCategories.ToList();
        //    url = $"{this.Request.Scheme}://{this.Request.Host}";
        //}
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }

        public async Task<JsonResult> OnPostAsync()
        {
            var recordsTotal = _context.SiteCategories.Count();

            var customersQuery = _context.SiteCategories.AsQueryable();

            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.TitleAr.ToUpper().Contains(searchText) ||
                    s.TitleEn.ToUpper().Contains(searchText) 
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

        public IActionResult OnGetSingleCategoryForEdit(int SiteCategoryId)
        {
            category = _context.SiteCategories.Where(c => c.SiteCategoryId == SiteCategoryId).FirstOrDefault();

            return new JsonResult(category);
        }

        public IActionResult OnGetSingleSiteCategoriesForView(int SiteCategoryId)
        {
            var Result = _context.SiteCategories.Where(c => c.SiteCategoryId == SiteCategoryId).FirstOrDefault();
            return new JsonResult(Result);
        }

        public async Task<IActionResult> OnPostEditCategory(int SiteCategoryId, IFormFile Editfile)
        {
            try
            {
                var model = _context.SiteCategories.Where(c => c.SiteCategoryId == SiteCategoryId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Category Not Found");

                    return Redirect("/Admin/Configurations/SiteCategories/Index");
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

                return Redirect("/Admin/Configurations/SiteCategories/Index");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/Configurations/SiteCategories/Index");
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
            return Redirect("/Admin/Configurations/SiteCategories/Index");
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
                var sites = _context.Sites.Include(e=>e.Templates).Where(e => e.Templates.SiteCategoryId == CatObj.SiteCategoryId).ToList();
                if (sites != null)
                {
                    if (sites.Count != 0)
                    {
                        _toastNotification.AddSuccessToastMessage("Can not Delete this category");
                        return Redirect("/Admin/Configurations/SiteCategories/Index");
                    }
                    

                }
                if (CatObj != null)
                {


                    _context.SiteCategories.Remove(CatObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("Category Deleted successfully");

                    
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

            return Redirect("/Admin/Configurations/SiteCategories/Index");
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
