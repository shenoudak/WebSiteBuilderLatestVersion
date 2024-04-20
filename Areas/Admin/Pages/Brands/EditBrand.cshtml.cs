using iTech.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.Admin.Pages.Brands
{
    public class EditBrandModel : PageModel
    {
		private CRMDBContext _context;
		private readonly IToastNotification _toastNotification;
		private readonly IWebHostEnvironment _hostEnvironment;

		[BindProperty]
		public Models.Brands Brand { get; set; }


		public EditBrandModel(CRMDBContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
		{
			_context = context;
			_toastNotification = toastNotification;
			_hostEnvironment = hostEnvironment;
		}


		public ActionResult OnGet(int id)
		{
			Brand = _context.Brands.FirstOrDefault(a => a.BrandsId == id);

			

			return Page();
		}


		public async Task<IActionResult> OnPost(int id, IFormFile Editfile)
		{
			try
			{
				//if (!ModelState.IsValid)
				//{
				//	_toastNotification.AddErrorToastMessage("Please Enter All Required Data");

				//	return Page();
				//}

				var model = _context.Brands.FirstOrDefault(a => a.BrandsId == id);

				if (model is null)
				{
					_toastNotification.AddErrorToastMessage("Brands Not Found");

					return NotFound();
				}
				if (Editfile != null)
				{
					string folder = "Images/Brands/";
					model.Images = await UploadImage(folder, Editfile);
					_context.SaveChanges();
				}
				else
				{
					//model.Images = Brand.Images;
					_toastNotification.AddSuccessToastMessage("Brand photo not changed");
				}
				

				

				_toastNotification.AddSuccessToastMessage("Brand Updated successfully");

			}
			catch
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
