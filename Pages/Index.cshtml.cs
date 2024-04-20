using iTech.Data;
using iTech.Migrations;
using iTech.Models;
using iTech.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace iTech.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		private readonly IToastNotification _toastNotification;
		public IRequestCultureFeature locale;
		public string BrowserCulture;
		private readonly CRMDBContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		[BindProperty]
		public ContactUs ContactUs { get; set; }
		public List<ViewModel.PublicSlider> publicSliders { get; set; }
		public Models.Brands brands { get; set; }
		public PublicFlowVM publicFlowVM { get; set; }
		public ViewModel.PublicVideo publicVideo { get; set; }
		public List<ViewModel.FeaturesVM> featuresVM { get; set; }
		public ViewModel.PlansVM plansVMs { get; set; }
		public CountDownVM CountDownVM { get; set; }
		public PublicCountVM PublicCountVM { get; set; }
		public PublicCustomerVM customerVM { get; set; }
        public PublicCustomerVM publicAffiliate { get; set; }
        public IndexModel(IToastNotification toastNotification, ILogger<IndexModel> logger, CRMDBContext context, UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_toastNotification = toastNotification;
			_context = context;
			_userManager = userManager;
			brands = new Models.Brands();
			ContactUs = new ContactUs();
			publicSliders = new List<ViewModel.PublicSlider>();
			publicVideo = new ViewModel.PublicVideo();
			featuresVM = new List<FeaturesVM>();
			CountDownVM = new CountDownVM();
			PublicCountVM = new PublicCountVM();

        }


		public void OnGet(string affiliateId)
		{
			if (!string.IsNullOrEmpty(affiliateId) && !string.IsNullOrWhiteSpace(affiliateId)) {
				if (!_userManager.Users.Any(a => a.AffiliateToken == affiliateId))
				{
					_toastNotification.AddErrorToastMessage("Invalid Token: Enter another TOKEN");

				}
			}
			var sliders = _context.PublicSliders.ToList();
			foreach (var slider in sliders)
			{
				var sliderobj = new ViewModel.PublicSlider()
				{
					Background = slider.Background,
					DescriptionAr = slider.DescriptionAr,
					DescriptionEn = slider.DescriptionEn,
					IsImage = slider.IsImage,
					Title1Ar = slider.Title1Ar,
					Title2Ar = slider.Title2Ar,
					Title1En = slider.Title1En,
					Title2En = slider.Title2En,
				};
				publicSliders.Add(sliderobj);
			}
			var publicFlow = _context.PublicHomeContents.FirstOrDefault();
			publicFlowVM = new PublicFlowVM
			{
				publicFlow = _context.PublicHomeContents.FirstOrDefault(a => a.PublicHomeContentId == 7),
				StepOne = _context.PublicHomeContents.FirstOrDefault(a => a.PublicHomeContentId == 3),
				StepTwo = _context.PublicHomeContents.FirstOrDefault(a => a.PublicHomeContentId == 4),
				StepThree = _context.PublicHomeContents.FirstOrDefault(a => a.PublicHomeContentId == 5),
				StepFour = _context.PublicHomeContents.FirstOrDefault(a => a.PublicHomeContentId == 6),
			};

			var publicVideoobj = _context.PublicVideos.FirstOrDefault();
			publicVideo.BriefAr = publicVideoobj.BriefAr;
			publicVideo.BriefEn = publicVideoobj.BriefEn;
			publicVideo.TitleAr = publicVideoobj.TitleAr;
			publicVideo.TitleEn = publicVideoobj.TitleEn;
			publicVideo.VideoUrl = publicVideoobj.VideoUrl;
			publicVideo.Background = publicVideoobj.Background;

			var publicFeatures = _context.Features.ToList();
			foreach (var feature in publicFeatures)
			{
				var fearureobj = new FeaturesVM()
				{
					TitleEn = feature.TitleEn,
					TitleAr = feature.TitleAr,
					Image = feature.Image,
				};
				featuresVM.Add(fearureobj);

			}

			plansVMs = new PlansVM
			{
				FirstPlan = _context.Plans.FirstOrDefault(),
				LastPlan = _context.Plans.OrderBy(a => a.PlanId).LastOrDefault(),
				planPrices = _context.Plans.ToList(),
			};
			var Text = _context.CountDownSections.Where(e => e.CountDownSectionId == 1).FirstOrDefault();
			CountDownVM.TextAR = Text.TextAR;
			CountDownVM.TextEN = Text.TextEN;
			CountDownVM.CountDown = Text.CountDown;

			var countINFO = _context.CountInfos.Include(e => e.counts).Where(e => e.CountInfoId == 1).FirstOrDefault();
			PublicCountVM.TitleAR = countINFO.TitleAR;
			PublicCountVM.TitleEN = countINFO.TitleEN;
			PublicCountVM.DescAR = countINFO.DescAR;
			PublicCountVM.DescEN = countINFO.DescEN;
			PublicCountVM.counts = countINFO.counts;

			customerVM = new PublicCustomerVM
			{
				Brands = _context.Brands.ToList(),
				BrandsBrief = _context.BrandsBrief.Where(e => e.BrandsBriefId == 1).FirstOrDefault(),
				BrandBriefTitle = _context.BrandsBrief.Where(e => e.BrandsBriefId == 2).FirstOrDefault()
			};
            publicAffiliate = new PublicCustomerVM
            {
                BrandsBrief = _context.BrandsBrief.Where(e => e.BrandsBriefId == 4).FirstOrDefault(),
                BrandBriefTitle = _context.BrandsBrief.Where(e => e.BrandsBriefId == 3).FirstOrDefault()
            };

        } 

		public async Task<IActionResult> OnPostAddSubscribe(string newsletterEmail)
		{
			try
			{
				var EmailsfromNewLetter = _context.Newsletters.Any(i => i.Email == newsletterEmail);
				if (EmailsfromNewLetter)
				{
					_toastNotification.AddInfoToastMessage("This Email Is already Subscribed");
					return RedirectToPage("/index");
				}

				var newSubscribe = new Newsletter()
				{
					Date = DateTime.Now,
					Email = newsletterEmail
				};
				_context.Newsletters.Add(newSubscribe);
				_context.SaveChanges();
				_toastNotification.AddSuccessToastMessage("You Are Subscribed Successifully Of ITECH News Letter");

			}
			catch (Exception)
			{

				_toastNotification.AddErrorToastMessage("Something Went Error");
			}
			return Redirect("/Index");
		}


        public IActionResult OnPostAddContactUS(string name, string email, string message)
        {
            try
            {
                var contactus = new ContactUs()
                {
                    SendingDate = DateTime.Now,
                    Email = email,
					Message=message,
					Name=name
                };
                
                _context.contactUs.Add(contactus);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Message Send successfully");
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage(e.Message);
            }
            return Redirect("/Index");
        }
    }
}