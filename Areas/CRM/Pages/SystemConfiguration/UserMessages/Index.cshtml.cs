
using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace iTech.Areas.CRM.Pages.SystemConfiguration.UserMessages
{

    #nullable disable
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public ContactUs contactUs { get; set; }


       


        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            contactUs = new ContactUs();
  
        }
        public void OnGet()
        {
           
            url = $"{this.Request.Scheme}://{this.Request.Host}";

        }

       
        

        public IActionResult OnGetSingleMessageForView(int ContactId)
        {
            var Result = _context.contactUs.Where(c => c.ContactUsID == ContactId).FirstOrDefault();
            
            return new JsonResult(Result);
        }


        public IActionResult OnGetSingleMessageForDelete(int ContactId)
        {
            contactUs = _context.contactUs.Where(c => c.ContactUsID == ContactId).FirstOrDefault();
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

            return RedirectToPage("/SystemConfiguration/UserMessages/Index");
        }

       
        

    }
}
