using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.Admin.Pages
{
	public class EditServiceModel : PageModel
	{
		private CRMDBContext _context;
		private readonly IToastNotification _toastNotification;
		private readonly IWebHostEnvironment _hostEnvironment;

		[BindProperty]
		public Services service { get; set; }


		public EditServiceModel(CRMDBContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
		{
			_context = context;
			_toastNotification = toastNotification;
			_hostEnvironment = hostEnvironment;
		}


		public ActionResult OnGet(int id)
		{
			service = _context.Services.FirstOrDefault(a => a.ServicesId == id);

			ViewData["ContentEn"] = service.ContentEn;

			return Page();
		}


		public async Task<IActionResult> OnPost(int id,IFormFile Editfile)
		{
			try
			{
				//if (!ModelState.IsValid)
				//{
				//	_toastNotification.AddErrorToastMessage("Please Enter All Required Data");

				//	return Page();
				//}

				var page = _context.Services.FirstOrDefault(a => a.ServicesId == id);

				if (page is null)
				{
					_toastNotification.AddErrorToastMessage("Services Not Found");

					return NotFound();
				}
				if (Editfile != null)
				{
					string folder = "Images/Services/";
					page.Pic = await UploadImage(folder, Editfile);

				}
				
				page.TitleAr = service.TitleAr;
				page.TitleEn = service.TitleEn;
				page.ContentAr = service.ContentAr;

				page.ContentEn = service.ContentEn;

				_context.SaveChanges();

				_toastNotification.AddSuccessToastMessage("Service Updated successfully");

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