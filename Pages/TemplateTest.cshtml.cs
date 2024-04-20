using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using ImageProcessor.Web.Services;
using iTech.RazorServices;

namespace iTech.Pages
{
	public class TemplateTestModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly IToastNotification _toastNotification;
		public IRequestCultureFeature locale;
		public string BrowserCulture;
		private readonly CRMDBContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public List<SiteCategory> siteCategories { get; set; }
		public List<Template> Templates { get; set; }
		public int planPriceId { get; set; }
		public TemplateTestModel(IWebHostEnvironment webHostEnvironment, IToastNotification toastNotification, ILogger<IndexModel> logger, CRMDBContext context)
		{
			_logger = logger;
			_toastNotification = toastNotification;
			_context = context;
			_webHostEnvironment = webHostEnvironment;
			siteCategories = new List<SiteCategory>();
			Templates = new List<Template>();

		}

		public void OnGet(int? planId)
		{
			locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
			BrowserCulture = locale.RequestCulture.UICulture.ToString();
			siteCategories = _context.SiteCategories.Where(e => e.IsActive == true).ToList();
			Templates = _context.Templates.Include(e => e.SiteCategories).Where(e => e.IsActive && e.SiteCategoryId != null).ToList();

			_context.SaveChanges();

			if (planId != null)
			{
				planPriceId = planId.Value;
			}



			//	var template = _context.Templates
			//		.Where(e => e.TemplateId == 1)

			//		  // Take the next 10 records
			//		.FirstOrDefault();


			//		if (!template.TemplatePic.Contains("Compressed"))
			//		{
			//			var folder = "/Images/Template/Compressed";
			//			var image = CompressAndSaveImage(template.TemplatePic, 50);
			//			if (image != "")
			//			{
			//			template.TemplatePic = image;
			//				var UpdatedImage = _context.Templates.Attach(template);

			//				UpdatedImage.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			//			}

			//		}




			//	_context.SaveChanges();




			//}



			//public string  CompressAndSaveImage(string relativePath, int quality)
			//{
			//	// Get the absolute wwwroot path
			//	string wwwRootPath = _webHostEnvironment.WebRootPath;
			//	var picName = relativePath.Split('/').Last();
			//	var isExist= IsImageExists(picName);
			//	if (isExist)
			//	{
			//		// Combine wwwroot path with the relative path
			//		string inputPath = Path.Combine(wwwRootPath, "preview/Blank/image/" + picName);

			//		// Specify the output directory within wwwroot/images for the compressed image
			//		string outputDirectory = Path.Combine(wwwRootPath, "Images", "Compressed");

			//		// Ensure the output directory exists
			//		Directory.CreateDirectory(outputDirectory);

			//		// Construct the output path by changing the extension to ".jpg"
			//		//string outputFileName = Path.GetFileNameWithoutExtension(relativePath) + ".jpg";
			//		//string outputPath = Path.Combine(outputDirectory, outputFileName);

			//		string outputFileName = Guid.NewGuid().ToString() + ".jpg";
			//		string outputPath = Path.Combine(outputDirectory, outputFileName);

			//		using (var image = Image.Load(inputPath))
			//		{
			//			var encoder = new JpegEncoder
			//			{
			//				Quality = quality,
			//			};

			//			image.Save(outputPath, encoder);
			//		}
			//		var newPath = Path.Combine("Images", "Compressed", outputFileName);
			//		return newPath;
			//	}
			//	return "";
			//}

			//public bool IsImageExists(string imagePath)
			//{
			//	// Combine the wwwroot path with the provided image path
			//	string wwwrootPath = _webHostEnvironment.WebRootPath;
			//	string imagePathInWwwRoot = Path.Combine(wwwrootPath, "preview/Blank/image", imagePath);

			//	// Check if the file exists
			//	bool imageExists = System.IO.File.Exists(imagePathInWwwRoot);

			//	return imageExists;
			//}
		}
	}
}