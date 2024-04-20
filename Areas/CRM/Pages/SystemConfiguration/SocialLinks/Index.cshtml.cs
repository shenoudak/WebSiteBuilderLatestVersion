
using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using System.IO;

namespace iTech.Areas.CRM.Pages.SystemConfiguration.SocialLinks
{

    #nullable disable
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public SocialMediaLink socialMediaLink { get; set; }


        public List<SocialMediaLink> socialMediaLinksList = new List<SocialMediaLink>();
        

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment, 
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            socialMediaLink = new SocialMediaLink();
           
        }
        public void OnGet()
        {
            socialMediaLinksList = _context.SocialMediaLinks.ToList();
            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }

        public IActionResult OnGetSingleSocialForEdit(int SocialMediaLinkId)
        {
            socialMediaLink = _context.SocialMediaLinks.Where(c => c.SocialMediaLinkId == SocialMediaLinkId).FirstOrDefault();

            return new JsonResult(socialMediaLink);
        }

        public IActionResult OnPostEditSocial(int SocialMediaLinkId)
        {
            try
            {
                var model = _context.SocialMediaLinks
                                    .Where(c => c.SocialMediaLinkId == SocialMediaLinkId)
                                    .FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("SocialObj Not Found");
                 
                    return Redirect("/CRM/SystemConfiguration/SocialLinks/Index");
                }


                model.Facebook = socialMediaLink.Facebook;
                model.Twitter = socialMediaLink.Twitter;
                model.Instgram = socialMediaLink.Instgram;
                model.LinkedIn = socialMediaLink.LinkedIn;
                model.WhatSapp = socialMediaLink.WhatSapp;
                model.Address = socialMediaLink.Address;
                model.ContactMail = socialMediaLink.ContactMail;
                model.ContactPhone1 = socialMediaLink.ContactPhone1;
                model.ContactPhone1 = socialMediaLink.ContactPhone1;
                model.Youtube = socialMediaLink.Youtube;
                var UpdatedSocialLinks = _context.SocialMediaLinks.Attach(model);

                UpdatedSocialLinks.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();

                _toastNotification.AddSuccessToastMessage("Links Edited successfully");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");
               
            }
            return Redirect("/CRM/SystemConfiguration/SocialLinks/Index");
        }

    }
}
