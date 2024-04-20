using iTech.Data;
using iTech.Migrations;
using iTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NToastNotify;
using System.Text;

namespace iTech.Areas.Admin.Pages
{
	public class PublicContentEditorModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		private readonly IToastNotification _toastNotification;
		public IRequestCultureFeature locale;
		public string BrowserCulture;
		private readonly CRMDBContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IWebHostEnvironment _hostEnvironment;

		[BindProperty]
		public SocialMediaLink? contactInfo { get; set; }
		public List<Models.PublicHomeContent> publicHomeContentList { get; set; }

		public List<Models.Brands> Brands { get; set; }
		public List<Models.Feature> Features { get; set; }


		public List<AffiliatePrice> AffiliatePriceList;
		public int AffliateCount { get; set; }
		public int SitesCount { get; set; }
		public double PointsCount { get; set; }
		public double SitesReveuve { get; set; }
		public IList<ApplicationUser> AffiliateUsers { get; set; }
		public List<PublicSlider> publicSliders { get; set; }

		public List<PageContent> pageContents { set; get; }
		public List<Services> Services { set; get; }
		public ContactUs contactUsObj { get; set; }
		public List<PublicVideo> PublicVideo { get; set; }
		[BindProperty]
		public PublicVideo PublicVideoObj { get; set; }
		public List<Plan> plans { get; set; }
		[BindProperty]
		public Plan PlanObj { get; set; }
		public List<BrandsBrief> brandsBriefs { get; set; }
		[BindProperty]
		public BrandsBrief BrandBriefObj { get; set; }
        [BindProperty]
        public BrandsBrief? BrandBriefTitle { get; set; }
        [BindProperty]
        public BrandsBrief? BrandBriefCaption { get; set; }
        [BindProperty]
        public BrandsBrief? AffiliateTitle { get; set; }
        [BindProperty]
        public BrandsBrief? AffiliateCaption { get; set; }
        public List<CountInfo> CountInfos { get; set; }
		public List<AffilateFeedback> affilateFeedbacks { get; set; }
		[BindProperty]
		public AffilateFeedback affilateFeedbackObj { get; set; }

		public List<ContactUs> userMessages { get; set; }
		[BindProperty]
		public CountInfo CountInfoObj { get; set; }

		[BindProperty]
		public PublicSlider publicSlider { get; set; }


		[BindProperty]
		public Models.Brands Brand { get; set; }
        [BindProperty]
        public Models.Feature Feature { get; set; }
        [BindProperty]
        public Models.CountDownSection CountDown { get; set; }
        [BindProperty]
		public Models.PublicHomeContent publicHomeContent { get; set; }
		[BindProperty]
		public AffiliateVideo affiliateVideo { get; set; }
		public List<AffiliateVideo> affiliateVideos = new List<AffiliateVideo>();
		public PublicContentEditorModel(IToastNotification toastNotification, ILogger<IndexModel> logger, CRMDBContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostEnvironment)
		{
			_logger = logger;
			_toastNotification = toastNotification;
			_context = context;
			_userManager = userManager;
			_hostEnvironment = hostEnvironment;
			publicSlider = new Models.PublicSlider();
			PublicVideoObj = new Models.PublicVideo();
			PlanObj = new Models.Plan();
			CountInfoObj = new Models.CountInfo();
			BrandBriefObj = new Models.BrandsBrief();
			contactUsObj = new Models.ContactUs();
			affiliateVideo = new AffiliateVideo();
			affilateFeedbackObj = new AffilateFeedback();
			Brand = new Models.Brands();
			Feature = new Models.Feature();
            publicHomeContent = new Models.PublicHomeContent();

        }
		public async Task<IActionResult> OnGet()
		{
			affilateFeedbacks = _context.AffilateFeedbacks.ToList();
			PublicVideo = _context.PublicVideos.ToList();
			affiliateVideos = _context.AffiliateVideos.ToList();
			userMessages = _context.contactUs.ToList();
			CountInfos = _context.CountInfos.ToList();
			plans = _context.Plans.ToList();
			brandsBriefs = _context.BrandsBrief.ToList();
			contactInfo = _context.SocialMediaLinks.FirstOrDefault();
			publicSliders = _context.PublicSliders.ToList();
			AffiliatePriceList = _context.AffiliatePrices.ToList();
			publicHomeContentList = _context.PublicHomeContents.ToList();
            Features = _context.Features.ToList();
            Brands = _context.Brands.ToList();
			AffiliateUsers = await _userManager.GetUsersInRoleAsync("Affiliate");
			PointsCount = AffiliateUsers.Sum(e => e.Point);
			AffliateCount = AffiliateUsers.Count();
			foreach (var user in AffiliateUsers)
			{
				SitesCount = _context.Sites.Where(e => e.AffiliateUser == user.Id && e.IsActive == true).Count();
				SitesReveuve = _context.Sites.Where(e => e.AffiliateUser == user.Id && e.IsActive == true).Sum(e => e.UserTemplatePrice);
			}
			pageContents = _context.PageContents.ToList();
			Services = _context.Services.ToList();
            CountDown = _context.CountDownSections.Where(a => a.CountDownSectionId == 1).FirstOrDefault();
			BrandBriefTitle = _context.BrandsBrief.Where(a => a.BrandsBriefId == 1).FirstOrDefault();
			BrandBriefCaption = _context.BrandsBrief.Where(a => a.BrandsBriefId == 2).FirstOrDefault();
			AffiliateCaption = _context.BrandsBrief.Where(a => a.BrandsBriefId == 3).FirstOrDefault();
			AffiliateTitle = _context.BrandsBrief.Where(a => a.BrandsBriefId == 4).FirstOrDefault();
			//var templates= _context.Templates.ToList();
			//foreach (var template in templates)
			//{
			//	////Add Product
			//	var clientProduct = new HttpClient();
			//	var requestProduct = new HttpRequestMessage(HttpMethod.Post, "http://162.19.146.134:1158/api/web/woocommerce/add_product");
			//	requestProduct.Headers.Add("secret_key", "nB/SZsQ6K0m4lwbmq/rZvhPouZXaB7dYIVPm2pq0dRppux9VnC5n8l7o6oJQD7OxBF/Y/NcUKR3vaGV6fPnYHOAIo1COom1WsOGKBpIcMCBOwjnFd8/E9VEAJ3Gvu/HAzqfqeJtkiC9HpnRfKHCYZ5kluxOQt/fHrOp7IDUUemKHbq6GmT2Xm4ZuBGt61CYrNJI40+AHa9B0szE/6yw34mZ7T0DGcJ4DRG87zMXpdXC6UyZ5AowHMQHGYSW0U8Il1UEymK0HA3bvbKP4fQI1/3dipSfHRTsmdfRmZ6hvOZHWyhdaqzSkFTiKXM74RaDmvtMYZpJNwk/Lvbhu9vWJUw==");
			//	requestProduct.Headers.Add("prefix", "990");
			//	requestProduct.Headers.Add("access-token", "c0674468-00e2-4ab1-b63e-0be02a4bb03a");

			//	var product = new
			//	{
			//		name = template.TitleEn,
			//		package_id = template.MLMId,
			//		price = template.TemplatePrice,
			//		pv_value = 0,


			//	};
			//	var jsonData = JsonConvert.SerializeObject(product);
			//	var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

			//	requestProduct.Content = content;
			//	var responseProduct = await clientProduct.SendAsync(requestProduct);
			//	var res = await responseProduct.Content.ReadAsStringAsync();
			//}
			
			
			return Page();
		}
        public async Task<IActionResult> OnPostUpdateBrandBreif()
        {
            try
            {
                var BrandsBriefT = _context.BrandsBrief.Attach(BrandBriefTitle);
                BrandsBriefT.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                var BrandsBriefC = _context.BrandsBrief.Attach(BrandBriefCaption);
                BrandsBriefC.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Brand Brief Title Updated successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Brand Brief Title can't be updated");
            }
            return Redirect("/Admin/PublicContentEditor");
        }
        public async Task<IActionResult> OnPostUpdateAffiliateBreif()
        {
            try
            {
                var AffiliateBriefT = _context.BrandsBrief.Attach(AffiliateTitle);
                AffiliateBriefT.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                var AffiliateBriefC = _context.BrandsBrief.Attach(AffiliateCaption);
                AffiliateBriefC.State = Microsoft.EntityFrameworkCore.EntityState.Modified;


                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Brand Brief Caption Updated successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Brand Brief Caption can't be updated");
            }
            return Redirect("/Admin/PublicContentEditor");
        }

        public async Task<IActionResult> OnPostUpdateContactInfo()
		{
			try
			{
				var UpdatedContactInfo = _context.SocialMediaLinks.Attach(contactInfo);
				UpdatedContactInfo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				await _context.SaveChangesAsync();
				_toastNotification.AddSuccessToastMessage("Contact Info Updated successfully");

			}
			catch (Exception)
			{

				_toastNotification.AddErrorToastMessage("Contact Info can't be updated");
			}
			return Redirect("/Admin/PublicContentEditor");
		}
		public async Task<IActionResult> OnGetSingleAfVideoForEdit(int AffiliateVideoId)
		{
			affiliateVideo = _context.AffiliateVideos.Where(c => c.AffiliateVideoId == AffiliateVideoId).FirstOrDefault();

			return new JsonResult(affiliateVideo);

		}
		public async Task<IActionResult> OnPostEditAfVideo(int AffiliateVideoId, IFormFile Editfile)
		{
			try
			{
				var model = _context.AffiliateVideos.Where(c => c.AffiliateVideoId == AffiliateVideoId).FirstOrDefault();
				if (model == null)
				{
					return Redirect("/Admin/PublicContentEditor");
				}


				if (Editfile != null)
				{
					if (model.VideoMedia != null)
					{
						var ImagePath = Path.Combine(_hostEnvironment.WebRootPath, model.VideoMedia);
						if (System.IO.File.Exists(ImagePath))
						{
							System.IO.File.Delete(ImagePath);
						}
					}

					string folder = "Images/affiliateVideo/";
					model.VideoMedia = await UploadImage(folder, Editfile);
				}
				else
				{
					model.VideoMedia = affiliateVideo.VideoMedia;
				}

				if (affiliateVideo.IsUrl == false)
				{
					model.VideoUrl = " ";
				}
				else
				{
					model.VideoUrl = affiliateVideo.VideoUrl;
				}
				model.IsUrl = affiliateVideo.IsUrl;
				model.VideoTitleAr = affiliateVideo.VideoTitleAr;
				model.VideoTitleEn = affiliateVideo.VideoTitleEn;
				model.VideoMiniDescAr = affiliateVideo.VideoMiniDescAr;
				model.VideoMiniDescEn = affiliateVideo.VideoMiniDescEn;


				var UpdatedBanner = _context.AffiliateVideos.Attach(model);
				UpdatedBanner.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				_context.SaveChanges();
				_toastNotification.AddSuccessToastMessage("Affiliate Video Section Edited successfully");


			}
			catch (Exception e)
			{
				_toastNotification.AddErrorToastMessage("Something went Error");
				return RedirectToPage("/Admin/PublicContentEditor");
			}
			return Redirect("/Admin/PublicContentEditor");

		}
		public async Task<IActionResult> OnGetSingleAfFBCForEdit(int AffilateFeedbackId)
		{
			affilateFeedbackObj = _context.AffilateFeedbacks.Where(c => c.AffilateFeedbackId == AffilateFeedbackId).FirstOrDefault();

			return new JsonResult(affilateFeedbackObj);

		}
		public async Task<IActionResult> OnPostEditAfFBC(int AffilateFeedbackId)
		{
			try
			{
				var model = _context.AffilateFeedbacks.Where(c => c.AffilateFeedbackId == AffilateFeedbackId).FirstOrDefault();
				if (model == null)
				{
					return Redirect("/Admin/PublicContentEditor");
				}





				model.TitleAr = affilateFeedbackObj.TitleAr;
				model.TitleEn = affilateFeedbackObj.TitleEn;
				model.UserOneFeedbackAr = affilateFeedbackObj.UserOneFeedbackAr;
				model.UserOneFeedbackEn = affilateFeedbackObj.UserOneFeedbackEn;
				model.UserTwoFeedbackAr = affilateFeedbackObj.UserTwoFeedbackAr;
				model.UserTwoFeedbackEn = affilateFeedbackObj.UserTwoFeedbackEn;
				model.UserThreeFeedbackAr = affilateFeedbackObj.UserThreeFeedbackAr;
				model.UserThreeFeedbackEn = affilateFeedbackObj.UserThreeFeedbackEn;


				var UpdatedAffFBC = _context.AffilateFeedbacks.Attach(model);
				UpdatedAffFBC.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				_context.SaveChanges();
				_toastNotification.AddSuccessToastMessage("Affiliate Feedback Section Edited successfully");


			}
			catch (Exception e)
			{
				_toastNotification.AddErrorToastMessage("Something went Error");
				return RedirectToPage("/Admin/PublicContentEditor");
			}
			return Redirect("/Admin/PublicContentEditor");

		}

		public IActionResult OnGetSingleAfVideoView(int AffiliateVideoId)
		{
			var Result = _context.AffiliateVideos.Where(c => c.AffiliateVideoId == AffiliateVideoId).FirstOrDefault();
			return new JsonResult(Result);
		}
		public IActionResult OnGetSingleAfFBCView(int AffilateFeedbackId)
		{
			var Result = _context.AffilateFeedbacks.Where(c => c.AffilateFeedbackId == AffilateFeedbackId).FirstOrDefault();
			return new JsonResult(Result);
		}
		public IActionResult OnGetSingleSliderForEdit(int PublicSliderId)
		{
			var Result = _context.PublicSliders.Where(c => c.PublicSliderId == PublicSliderId).FirstOrDefault();
			return new JsonResult(Result);

		}
		public IActionResult OnGetSinglePublicVideoForEdit(int PublicVideoId)
		{
			var Result = _context.PublicVideos.Where(c => c.PublicVideoId == PublicVideoId).FirstOrDefault();
			return new JsonResult(Result);

		}
		public IActionResult OnGetSinglePlanForEdit(int PlanId)
		{
			var Result = _context.Plans.Where(c => c.PlanId == PlanId).FirstOrDefault();
			return new JsonResult(Result);

		}
		public IActionResult OnGetSingleCountInfoForEdit(int CountInfoId)
		{
			var Result = _context.CountInfos.Where(c => c.CountInfoId == CountInfoId).FirstOrDefault();
			return new JsonResult(Result);

		}
		public IActionResult OnGetSingleBrandsBriefForEdit(int BrandsBriefId)
		{
			var Result = _context.BrandsBrief.Where(c => c.BrandsBriefId == BrandsBriefId).FirstOrDefault();
			return new JsonResult(Result);

		}
		public IActionResult OnGetSingleMessageForView(int ContactId)
		{
			var Result = _context.contactUs.Where(c => c.ContactUsID == ContactId).FirstOrDefault();

			return new JsonResult(Result);
		}



		

		public IActionResult OnGetSingleMessageForDelete(int ContactId)
		{
			var contactUs = _context.contactUs.Where(c => c.ContactUsID == ContactId).FirstOrDefault();
			return new JsonResult(contactUs);
		}

		public async Task<IActionResult> OnPostDeleteMessage(int ContactUsID)
		{
			try
			{
				ContactUs MessageObj = _context.contactUs.Where(e => e.ContactUsID == ContactUsID).FirstOrDefault();


				if (MessageObj != null)
				{


					_context.contactUs.Remove(MessageObj);

					await _context.SaveChangesAsync();

					_toastNotification.AddSuccessToastMessage("Message Deleted successfully");


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

			return RedirectToPage("/Admin/PublicContentEditor");
		}




		public async Task<IActionResult> OnPostEditPublicBrand(IFormFile fileBrand)
		{
			try
			{
				var model = _context.Brands.Where(c => c.BrandsId == Brand.BrandsId).FirstOrDefault();
				if (model == null)
				{
					_toastNotification.AddErrorToastMessage("Brand is not Existed");

					return Redirect("/Admin/PublicContentEditor");
				}

				if (fileBrand != null)
				{
					string folder = "Images/Brands/";
					model.Images = await UploadImage(folder, fileBrand);

				}
				else
				{
					model.Images = Brand.Images;
				}

				var UpdatedPublicSlider = _context.Brands.Attach(model);
				UpdatedPublicSlider.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				await _context.SaveChangesAsync();
				_toastNotification.AddSuccessToastMessage("Brand Edited successfully ");


			}
			catch (Exception)
			{
				_toastNotification.AddErrorToastMessage("Brand can't be edited");

			}
			return Redirect("/Admin/PublicContentEditor");
		}


		public async Task<IActionResult> OnPostEditPublicSlider(IFormFile file)
		{
			try
			{
				var model = _context.PublicSliders.Where(c => c.PublicSliderId == publicSlider.PublicSliderId).FirstOrDefault();
				if (model == null)
				{
					_toastNotification.AddErrorToastMessage("Public Slider is not Existed");

					return Redirect("/Admin/PublicContentEditor");
				}

				if (file != null)
				{
					string folder = "Images/Publicslider/";
					model.Background = await UploadImage(folder, file);

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
			return Redirect("/Admin/PublicContentEditor");
		}
		public async Task<IActionResult> OnPostEditPublicVideo(IFormFile BackgroundEditfile)
		{
			try
			{
				var model = _context.PublicVideos.Where(c => c.PublicVideoId == PublicVideoObj.PublicVideoId).FirstOrDefault();
				if (model == null)
				{
					_toastNotification.AddErrorToastMessage("Public Video is not Existed");

					return Redirect("/Admin/PublicContentEditor");
				}

				if (BackgroundEditfile != null)
				{
					string folder = "Images/Publicslider/";
					model.Background = await UploadImage(folder, BackgroundEditfile);

				}
				
				model.TitleAr = PublicVideoObj.TitleAr;
				model.TitleEn = PublicVideoObj.TitleEn;
				model.BriefAr = PublicVideoObj.BriefAr;
				model.BriefEn = PublicVideoObj.BriefEn;
				model.VideoUrl = PublicVideoObj.VideoUrl;

				var UpdatedPublicVideo = _context.PublicVideos.Attach(model);
				UpdatedPublicVideo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				await _context.SaveChangesAsync();
				_toastNotification.AddSuccessToastMessage("Public Video Edited successfully ");


			}
			catch (Exception)
			{
				_toastNotification.AddErrorToastMessage("Public Video can't be edited");

			}
			return Redirect("/Admin/PublicContentEditor");
		}
		public async Task<IActionResult> OnPostEditPlan()
		{
			try
			{
				var model = _context.Plans.Where(c => c.PlanId == PlanObj.PlanId).FirstOrDefault();
				if (model == null)
				{
					_toastNotification.AddErrorToastMessage("Plan is not Existed");

					return Redirect("/Admin/PublicContentEditor");
				}


				model.TitleAr = PlanObj.TitleAr;
				model.TitleEn = PlanObj.TitleEn;
				model.PriceAfterDiscount = PlanObj.PriceAfterDiscount;
				model.PagesCount = PlanObj.PagesCount;
				model.Price = PlanObj.Price;


				var UpdatedPlan = _context.Plans.Attach(model);
				UpdatedPlan.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				await _context.SaveChangesAsync();
				_toastNotification.AddSuccessToastMessage("Plan Edited successfully ");


			}

			catch (Exception)
			{
				_toastNotification.AddErrorToastMessage("Plan can't be edited");

			}
			return Redirect("/Admin/PublicContentEditor");
		}
		public async Task<IActionResult> OnPostEditBrandsBrief()
		{
			try
			{
				var model = _context.BrandsBrief.Where(c => c.BrandsBriefId == BrandBriefObj.BrandsBriefId).FirstOrDefault();
				if (model == null)
				{
					_toastNotification.AddErrorToastMessage("Brands Brief is not Existed");

					return Redirect("/Admin/PublicContentEditor");
				}


				model.BriefAR = BrandBriefObj.BriefAR;
				model.BriefEN = BrandBriefObj.BriefEN;



				var UpdatedBrand = _context.BrandsBrief.Attach(model);
				UpdatedBrand.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				await _context.SaveChangesAsync();
				_toastNotification.AddSuccessToastMessage("Brands Brief Edited successfully ");


			}

			catch (Exception)
			{
				_toastNotification.AddErrorToastMessage("Plan can't be edited");

			}
			return Redirect("/Admin/PublicContentEditor");
		}
		public async Task<IActionResult> OnPostEditCountInfo()
		{
			try
			{
				var model = _context.CountInfos.Where(c => c.CountInfoId == CountInfoObj.CountInfoId).FirstOrDefault();
				if (model == null)
				{
					_toastNotification.AddErrorToastMessage("Count Info is not Existed");

					return Redirect("/Admin/PublicContentEditor");
				}


				model.TitleAR = CountInfoObj.TitleAR;
				model.TitleEN = CountInfoObj.TitleEN;
				model.DescEN = CountInfoObj.DescEN;
				model.DescAR = CountInfoObj.DescAR;


				var UpdatedCountInfo = _context.CountInfos.Attach(model);
				UpdatedCountInfo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				await _context.SaveChangesAsync();
				_toastNotification.AddSuccessToastMessage("Count Info Edited successfully ");


			}
			catch (Exception)
			{
				_toastNotification.AddErrorToastMessage("Count Info can't be edited");

			}
			return Redirect("/Admin/PublicContentEditor");
		}

        public IActionResult OnGetSingleBrandForDelete(int BrandsId)
        {
            var Brand = _context.Brands.Where(c => c.BrandsId == BrandsId).FirstOrDefault();
            return new JsonResult(Brand);
        }

        public async Task<IActionResult> OnPostDeleteBrand(int BrandsId)
        {
            try
            {
                var BrandObj = _context.Brands.Where(e => e.BrandsId == BrandsId).FirstOrDefault();
                
                if (BrandObj != null)
                {


                    _context.Brands.Remove(BrandObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("Brand Deleted successfully");


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

            return Redirect("/Admin/PublicContentEditor");
        }

        public IActionResult OnGetSingleBrandForEdit(int BrandsId)
        {
            var Result = _context.Brands.Where(c => c.BrandsId == BrandsId).FirstOrDefault();
            return new JsonResult(Result);

        }
        public async Task<IActionResult> OnPostEditBrand(int BrandsId, IFormFile Editfile)
        {
            try
            {
                
                var model = _context.Brands.FirstOrDefault(a => a.BrandsId == BrandsId);

                if (model is null)
                {
                    _toastNotification.AddErrorToastMessage("Brands Not Found");

					return Redirect("/Admin/PublicContentEditor");
				}
                if (Editfile != null)
                {
                    string folder = "Images/Brands/";
                    model.Images = await UploadImage(folder, Editfile);

					var UpdatedBrand = _context.Brands.Attach(model);
					UpdatedBrand.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

					await _context.SaveChangesAsync();
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
		
					public async Task<IActionResult> OnPostAddSlider(IFormFile file)
		{
			try
			{
				if (file != null)
				{
					string folder = "Images/Brands/";
					publicSlider.Background = await UploadImage(folder, file);
				}

				_context.PublicSliders.Add(publicSlider);
				_context.SaveChanges();
				_toastNotification.AddSuccessToastMessage("Slider Added Successfully");

			}
			catch (Exception)
			{

				_toastNotification.AddErrorToastMessage("Something went wrong");
			}
			return Redirect("/Admin/PublicContentEditor");
		}

		public async Task<IActionResult> OnPostAddBrand(IFormFile file)
		{
			try
			{
				if (file != null)
				{
					string folder = "Images/Brands/";
					Brand.Images = await UploadImage(folder, file);
				}

				_context.Brands.Add(Brand);
				_context.SaveChanges();
				_toastNotification.AddSuccessToastMessage("Brand Added Successfully");

			}
			catch (Exception)
			{

				_toastNotification.AddErrorToastMessage("Something went wrong");
			}
			return Redirect("/Admin/PublicContentEditor");
		}
        

                    public IActionResult OnGetSingleSliderForDelete(int PublicSliderId)
        {
            var Slider = _context.PublicSliders.Where(c => c.PublicSliderId == PublicSliderId).FirstOrDefault();
            return new JsonResult(Slider);
        }

        public IActionResult OnGetSingleFeatureForDelete(int FeatureId)
        {
            var Feature = _context.Features.Where(c => c.FeatureId == FeatureId).FirstOrDefault();
            return new JsonResult(Feature);
        }
        public async Task<IActionResult> OnPostDeleteSlider(int PublicSliderId)
        {
            try
            {
                var SliderObj = _context.PublicSliders.Where(e => e.PublicSliderId == PublicSliderId).FirstOrDefault();

                if (SliderObj != null)
                {


                    _context.PublicSliders.Remove(SliderObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("Slider Deleted successfully");


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

            return Redirect("/Admin/PublicContentEditor");
        }

        public async Task<IActionResult> OnPostDeleteFeature(int FeatureId)
        {
            try
            {
                var FeatureObj = _context.Features.Where(e => e.FeatureId == FeatureId).FirstOrDefault();

                if (FeatureObj != null)
                {


                    _context.Features.Remove(FeatureObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("Feature Deleted successfully");


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

            return Redirect("/Admin/PublicContentEditor");
        }

        public IActionResult OnGetSingleFeatureForEdit(int FeatureId)
        {
            var Result = _context.Features.Where(c => c.FeatureId == FeatureId).FirstOrDefault();
            return new JsonResult(Result);

        }
        public async Task<IActionResult> OnPostEditFeature(int FeatureId, IFormFile Editfile)
        {
            try
            {

                var model = _context.Features.FirstOrDefault(a => a.FeatureId == Feature.FeatureId);

                if (model is null)
                {
                    _toastNotification.AddErrorToastMessage("Feature Not Found");

					return Redirect("/Admin/PublicContentEditor");
				}
                if (Editfile != null)
                {
                    string folder = "Images/Features/";
                    model.Image = await UploadImage(folder, Editfile);
                }
                else
                {
                    //model.Images = Brand.Images;
                    _toastNotification.AddSuccessToastMessage("Feature photo not changed");
                }


				model.TitleEn = Feature.TitleEn;
				model.TitleAr = Feature.TitleAr;

				var UpdatedFeatures = _context.Features.Attach(model);
				UpdatedFeatures.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				await _context.SaveChangesAsync();

				_toastNotification.AddSuccessToastMessage("Feature Updated successfully");

            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something went Error");
            }

            return Redirect("/Admin/PublicContentEditor");
        }

        public async Task<IActionResult> OnPostAddFeature(IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    string folder = "Images/Brands/";
                    Feature.Image = await UploadImage(folder, file);
                }

                _context.Features.Add(Feature);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Feature Added Successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/Admin/PublicContentEditor");
        }
        
         public IActionResult OnGetSingleCountDownForEdit(int CountDownSectionId)
        {
            var Result = _context.CountDownSections.Where(c => c.CountDownSectionId == CountDownSectionId).FirstOrDefault();
            return new JsonResult(Result);

        }
        public async Task<IActionResult> OnPostEditCountDown()
        {
            try
            {

                var model = _context.CountDownSections.FirstOrDefault(a => a.CountDownSectionId == CountDown.CountDownSectionId);

                if (model is null)
                {
                    _toastNotification.AddErrorToastMessage("Count Down Not Found");

                    return Redirect("/Admin/PublicContentEditor");
                }
              


                model.TextEN = CountDown.TextEN;
                model.TextAR = CountDown.TextAR;
                model.CountDown = CountDown.CountDown;

                var UpdatedCountDown = _context.CountDownSections.Attach(model);
                UpdatedCountDown.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _context.SaveChangesAsync();

                _toastNotification.AddSuccessToastMessage("Count Down Updated successfully");

            }
            catch
            {
                _toastNotification.AddErrorToastMessage("Something went Error");
            }

            return Redirect("/Admin/PublicContentEditor");
        }
        public IActionResult OnGetSinglePublicHomeContentForEdit(int PublicHomeContentId)
        {
            var Result = _context.PublicHomeContents.Where(c => c.PublicHomeContentId == PublicHomeContentId).FirstOrDefault();
            return new JsonResult(Result);

        }
        public IActionResult OnGetSinglePublicHomeContentForView(int PublicHomeContentId)
        {
            var Result = _context.PublicHomeContents.Where(c => c.PublicHomeContentId == PublicHomeContentId).FirstOrDefault();

            return new JsonResult(Result);
        }
        public async Task<IActionResult> OnPostEditPublicHomeContent(int PublicHomeContentId, IFormFile Editfile)
        {
            try
            {

                var model = _context.PublicHomeContents.FirstOrDefault(a => a.PublicHomeContentId == publicHomeContent.PublicHomeContentId);

                if (model is null)
                {
                    _toastNotification.AddErrorToastMessage("Public Home Content Not Found");

                    return Redirect("/Admin/PublicContentEditor");
                }
                if (Editfile != null)
                {
                    string folder = "Images/Features/";
                    model.Pic = await UploadImage(folder, Editfile);
                }
                else
                {
                    //model.Images = Brand.Images;
                    _toastNotification.AddSuccessToastMessage("Public Home Content photo not changed");
                }


                model.AboutSectionTitleEn = publicHomeContent.AboutSectionTitleEn;
                model.AboutSectionTitleAr = publicHomeContent.AboutSectionTitleAr;
                model.AboutSectionDescEn = publicHomeContent.AboutSectionDescEn;
                model.AboutSectionDescAr = publicHomeContent.AboutSectionDescAr;
                
                var UpdatedFeatures = _context.PublicHomeContents.Attach(model);
                UpdatedFeatures.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _context.SaveChangesAsync();

                _toastNotification.AddSuccessToastMessage("Public Home Content Updated successfully");

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
