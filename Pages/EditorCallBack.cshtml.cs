using Azure.Core;
using Email;
using iTech.Data;
using iTech.Models;
using iTech.RazorServices;
using iTech.ViewModel;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using RestSharp;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Numerics;
using System.Text;
using System.Threading;

namespace iTech.Pages
{
    public class EditorCallBackModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CRMDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IToastNotification _toastNotification;
        private readonly IEmailSender _emailSender;
        private readonly IRazorPartialToStringRenderer _renderer;

        public EmailSite EmailSite { get; set; }
        public ApplicationUser AffiliateUser;
        private readonly ApplicationDbContext _db;
        public string PaymentStatus { get; set; }
        public EditorCallBackModel(IRazorPartialToStringRenderer renderer, UserManager<ApplicationUser> userManager, CRMDBContext context, IWebHostEnvironment webHostEnvironment
                                        , IToastNotification toastNotification, ApplicationDbContext db, IEmailSender emailSender)
        {
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _toastNotification = toastNotification;
            _emailSender = emailSender;
            _db = db;
            EmailSite = new EmailSite();
            _renderer = renderer;
        }

        public async Task<ActionResult> OnGet(string tap_id, string data)
        {

            try
            {
                if (tap_id is null)
                {
                    return RedirectToPage("SomethingwentError");
                }

                string Id = tap_id;
                //string Data = data;

                var client = new RestClient("https://api.tap.company/v2/charges/" + Id);
                var request = new RestRequest();
                request.AddHeader("authorization", "Bearer sk_live_96WmFki8Yb2QjNvyHU3TSspn");
                request.AddParameter("undefined", "{}", ParameterType.RequestBody);
                //RestResponse response = client.Execute(request);
                RestResponse response = await client.ExecuteAsync(request);

                string requestInfo = $"Method: {request.Method}\nResource: {request.Resource}\nParameters: {string.Join(", ", request.Parameters)}";

                var DeserializedResponse = JsonConvert.DeserializeObject<JObject>(response.Content);

                var Status = DeserializedResponse["status"].ToString();
                PaymentStatus = Status.ToString();

                var MetaData = DeserializedResponse["metadata"];
                var WebEditorPlanId = MetaData?["udf2"];
                var UserId = MetaData?["udf1"];
                var UserSiteId = MetaData?["udf3"];
                var Reference = DeserializedResponse["reference"];
                var TemplateId = Reference?["order"];

                if (Status == "CAPTURED")
                {

                  var planObj = _context.WebEditorPlans.Where(i => i.WebEditorPlanId == (int)WebEditorPlanId).FirstOrDefault();
                 
                    WebEditorSubscription WebEditorSubscription = new WebEditorSubscription
                    {
                       PlanId = planObj.WebEditorPlanId,
                       SiteId = (int)UserSiteId,
                       StartDate = DateTime.Now,
                       EndDate = DateTime.Now.AddMonths(12),
                       Price = planObj.Price,
                    };
                    _context.WebEditorSubscriptions.Add(WebEditorSubscription);
                    _context.SaveChanges();
                    var savedSub = _context.WebEditorSubscriptions.Where(a => a.SiteId == (int)UserSiteId).FirstOrDefault();
                    SiteModificationRequest SiteModificationRequest = new SiteModificationRequest
                    {
                        WESubscriptionId = savedSub.WESubscriptionId,
                        UserId = (string)UserId,
                        SiteId = (int)UserSiteId,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(12),
                        ModificationDescription = "Site Request",
                        Notes = " ",
                        DesignerId="0000"
                    };
                    _context.SiteModificationRequests.Add(SiteModificationRequest);
                    _context.SaveChanges();


                    return Page();
                }

                return Page();
            }
            catch (Exception ex)
            {

                return RedirectToPage("SomethingwentError", new { Message = ex.Message });
            }

        }





    }
}
