using iTech.Data;
using iTech.DataTables;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Text.RegularExpressions;
using System.Linq.Dynamic.Core;

namespace iTech.Areas.Admin.Pages.Configurations.ManageTemplates
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public Models.Template Template { get; set; }
		public List<Models.Template> TemplateList = new List<Models.Template>();



		public IndexModel(CRMDBContext context
                        , IWebHostEnvironment hostEnvironment
                        , IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            Template = new Models.Template();
        }
		//public void OnGet()
		//{
		//	TemplateList = _context.Templates.Include(a => a.SiteCategories).ToList();

		//	url = $"{this.Request.Scheme}://{this.Request.Host}";
		//}

		[BindProperty]
		public DataTablesRequest DataTablesRequest { get; set; }

		public async Task<JsonResult> OnPostAsync()
		{
			var recordsTotal = _context.Templates.Count();

			var customersQuery = _context.Templates.AsQueryable();

			var searchText = DataTablesRequest.Search.Value?.ToUpper();
			if (!string.IsNullOrWhiteSpace(searchText))
			{
				customersQuery = customersQuery.Where(s =>
					s.TitleAr.ToUpper().Contains(searchText) ||
					s.TitleEn.ToUpper().Contains(searchText) 
					//s.SiteCategories.TitleEn.ToUpper().Contains(searchText) ||
					//s.SiteCategories.TitleAr.ToString().ToUpper().Contains(searchText)
				);
			}

			var recordsFiltered = customersQuery.Count();

			var sortColumnName = DataTablesRequest.Columns.ElementAt(DataTablesRequest.Order.ElementAt(0).Column).Name;
			var sortDirection = DataTablesRequest.Order.ElementAt(0).Dir.ToLower();
			//if (sortColumnName == "TemplateTitleEn")
			//{
			//	customersQuery = sortDirection == "asc"
			//		? customersQuery.OrderBy(t => t.TitleEn)
			//		: customersQuery.OrderByDescending(t => t.TitleEn);
			//}
			//else if (sortColumnName == "SiteCategories.TitleEn")
			//{
			//	customersQuery = sortDirection == "asc"
			//		? customersQuery.OrderBy(t => t.SiteCategories.TitleEn)
			//		: customersQuery.OrderByDescending(t => t.SiteCategories.TitleEn);
			//}
			// using System.Linq.Dynamic.Core
            if(sortColumnName!=null&& sortDirection != null)
            {
				customersQuery = customersQuery.OrderBy($"{sortColumnName} {sortDirection}");
			}

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
		public IActionResult OnGetSingleTemplateForEdit(int TemplateId)
        {
            var Result = _context.Templates.Where(c => c.TemplateId == TemplateId).Include(a => a.SiteCategories).Select(i => new
			{
				TemplateId = i.TemplateId,
				TitleEn = i.TitleEn,
				TitleAr = i.TitleAr,
				TemplatePrice = i.TemplatePrice,
				SiteCategoryId = i.SiteCategoryId,
				SortOrder = i.SortOrder,
				IsActive = i.IsActive,
			}).FirstOrDefault();
			return new JsonResult(Result);

        }

        public async Task<IActionResult> OnPostEditTemplate(int TemplateId, IFormFile file)
        {
            try
            {
                var model = _context.Templates.Where(c => c.TemplateId == TemplateId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Template is not Existed");

                    return Redirect("/Admin/Configurations/ManageTemplates/Index");
                }
                if (file != null)
                {
                    string folder = "Images/Template/";
                    model.TemplatePic = await UploadImage(folder, file);

                }
               
                model.TemplatePrice = Template.TemplatePrice;
                model.TitleAr = Template.TitleAr;
                model.TitleEn = Template.TitleEn;
                model.IsActive = Template.IsActive;
                model.SortOrder = Template.SortOrder;
                model.SiteCategoryId = Template.SiteCategoryId;
                var UpdatedTemplate = _context.Templates.Attach(model);
				UpdatedTemplate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Template Edited successfully ");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Template can't be edited");

            }
            return Redirect("/Admin/Configurations/ManageTemplates/Index");
        }
        public IActionResult OnGetSingleTemplateForView(int TemplateId)
        {
            var Result = _context.Templates.Where(c => c.TemplateId == TemplateId).Include(a => a.SiteCategories).Select(i => new
            {
                TemplateId = i.TemplateId,
                TemplateTitleEn = i.TitleEn,
                TemplateTitleAr = i.TitleAr,
                TemplatePrice = i.TemplatePrice,
                SiteCategoryId = i.SiteCategoryId,
                SortOrder = i.SortOrder,
                IsActive = i.IsActive,
				SiteCategory = i.SiteCategories.TitleEn,
				TemplatePic = i.TemplatePic,
            }).FirstOrDefault();
            
            return new JsonResult(Result);
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
