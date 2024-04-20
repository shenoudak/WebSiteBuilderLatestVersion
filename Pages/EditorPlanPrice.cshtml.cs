using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using iTech.Data;
using Microsoft.AspNetCore.Hosting;
using NToastNotify;

namespace iTech.Pages
{
    public class EditorPlanPriceModel : PageModel
    {
        public IRequestCultureFeature locale;
        public string BrowserCulture;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CRMDBContext _context;
        public int UserSiteId { get; set; }
        public EditorPlanPriceModel(CRMDBContext context, UserManager<ApplicationUser> userManager, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment)
        {

            _context = context;
            this.userManager = userManager;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
        }
        public void OnGet(int SiteId)
        {
            UserSiteId = SiteId;
            locale = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            BrowserCulture = locale.RequestCulture.UICulture.ToString();
        }
        public async Task<IActionResult> OnPost(int WebEditorPlanId, int UserSiteId)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);
               
                    var IsPlanExisted = _context.WebEditorSubscriptions.Any(o => o.SiteId == UserSiteId);

                    if (IsPlanExisted)
                    {

                        ModelState.AddModelError(string.Empty, "That plan already taken");

                        _toastNotification.AddInfoToastMessage("You already have a plan with the same site");

                        return Redirect($"/EditorPlanPrice?SiteId={UserSiteId}");

                    }
                   
                    var EditorPlanObj = _context.WebEditorPlans.Where(e => e.WebEditorPlanId == WebEditorPlanId).FirstOrDefault();
                double planprice=0;
                if(EditorPlanObj != null && EditorPlanObj.PriceAfterDiscount > 0)
                {
                    planprice = EditorPlanObj.PriceAfterDiscount;
                }
                else
                {
                    planprice = EditorPlanObj.Price;
                }
                var TapMessage = new
                    {
                        amount = EditorPlanObj.PriceAfterDiscount,
                        currency = "KWD",
                        threeDSecure = true,
                        save_card = false,
                        description = "iTech Site Web Editor",
                        statement_descriptor = "Sample",
                        metadata = new
                        {
                            udf1 = user.Id,
                            udf2 = WebEditorPlanId,
                            udf3 = UserSiteId,
                        },
                        reference = new
                        {
                            transaction = "txn_0001",
                            order = EditorPlanObj.WebEditorPlanId
                        },
                        receipt = new
                        {
                            email = false,
                            sms = true
                        },
                        customer = new
                        {
                            first_name = user.UserName,
                            middle_name = "test",
                            last_name = "test",
                            email = user.UserName,
                            phone = new
                            {
                                country_code = "965",
                                number = "50143413"
                            }
                        },
                        merchant = new { id = "25045774" },
                        username = "182063@tap",
                        password = "182063@q8",
                        api_key = "96tap25",
                        source = new { id = "src_all" },

                        //redirect = new { url = "https://localhost:44352/EditorCallBack" }
                        redirect = new { url = "https://techsitekw.com/EditorCallBack" }

                    };

                    var sendPaymentRequestJSON = JsonConvert.SerializeObject(TapMessage);

                    var client = new RestClient("https://api.tap.company/v2/charges");
                    var request = new RestRequest();
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("authorization", "Bearer sk_live_96WmFki8Yb2QjNvyHU3TSspn");
                    request.AddParameter("application/json", sendPaymentRequestJSON, ParameterType.RequestBody);
                    RestResponse response = await client.PostAsync(request);


                    //Response as below
                    //Redirect to  "url": "https://sandbox.payments.tap.company/test_gosell/v2/payment/tap_process.aspx?chg=8D9e9fdEo5N2FTRb5POoT6b%2fkIhg5nP3QgCJug6HnFA%3d",

                    var DeserializeObjectResopnse = JsonConvert.DeserializeObject<JObject>(response.Content);

                    var Transaction = DeserializeObjectResopnse.GetValue("transaction");

                    var Url = Transaction["url"].ToString();

                    return Redirect(Url);
                    
                }
               
            
            catch (Exception ex)
            {


                return Redirect("/UserSites");
            }

        }

    }
}
