using iTech.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.CRM.Pages.Configurations.PublicSLiders
{
    public class IndexModel : PageModel
    {
		private CRMDBContext _context;
		private readonly IWebHostEnvironment _hostEnvironment;
		private readonly IToastNotification _toastNotification;
		public string url { get; set; }
		[BindProperty]
		public Models.PublicSlider publicSlider { get; set; }

		public List<Models.PublicSlider> publicSliders = new List<Models.PublicSlider>();
		public IndexModel(CRMDBContext context
						, IWebHostEnvironment hostEnvironment
						, IToastNotification toastNotification)
		{
			_context = context;
			_hostEnvironment = hostEnvironment;
			_toastNotification = toastNotification;
			publicSlider = new Models.PublicSlider();
		}
		public void OnGet()
		{
			publicSliders = _context.PublicSliders.ToList();

			url = $"{this.Request.Scheme}://{this.Request.Host}";
		}


		public IActionResult OnGetSingleSliderForEdit(int PublicSliderId)
		{
			var Result = _context.PublicSliders.Where(c => c.PublicSliderId == PublicSliderId).FirstOrDefault();
			return new JsonResult(Result);

		}

		public async Task<IActionResult> OnPostEditPublicSlider(int PublicSliderId, IFormFile avatar)
		{
			try
			{
				var model = _context.PublicSliders.Where(c => c.PublicSliderId == PublicSliderId).FirstOrDefault();
				if (model == null)
				{
					_toastNotification.AddErrorToastMessage("Public Slider is not Existed");

					return Redirect("/CRM/Configurations/PublicSliders/Index");
				}

				if (avatar != null)
				{
					string folder = "Images/Publicslider/";
					model.Background = await UploadImage(folder, avatar);

				}
				else
				{
					model.Background = publicSlider.Background;
				}
				model.Title1Ar = publicSlider.Title1Ar;
				model.Title1En = publicSlider.Title1En;
				model.Title2Ar = publicSlider.Title2Ar;
				model.Title2En = publicSlider.Title2En;
				model.DescriptionAr = publicSlider.DescriptionAr;
				model.DescriptionEn = publicSlider.DescriptionEn;
				model.IsImage = publicSlider.IsImage;
				var UpdatedPublicSlider = _context.PublicSliders.Attach(model);
				UpdatedPublicSlider.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				await _context.SaveChangesAsync();
				_toastNotification.AddSuccessToastMessage("Public Slider Edited successfully ");


			}
			catch (Exception)
			{
				_toastNotification.AddErrorToastMessage("Public Slider can't be edited");

			}
			return Redirect("/CRM/Configurations/PublicSliders/Index");
		}

		//public IActionResult OnGetSingleCountryForView(int CountryId)
		//{
		//	var Result = _context.Countries.Where(c => c.CountryId == CountryId).Select(i => new
		//	{
		//		CountryId = i.CountryId,
		//		TitleAr = i.TitleAr,
		//		TitleEn = i.TitleEn,
		//		SortOrder = i.SortOrder,
		//		IsActive = i.IsActive,
		//	}).FirstOrDefault();
		//	return new JsonResult(Result);
		//}

		private async Task<string> UploadImage(string folderPath, IFormFile file)
		{

			folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

			string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

			await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

			return folderPath;
		}
	}
}
