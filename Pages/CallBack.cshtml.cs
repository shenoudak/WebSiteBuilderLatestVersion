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
    public class CallBackModel : PageModel
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
        public CallBackModel(IRazorPartialToStringRenderer renderer, UserManager<ApplicationUser> userManager, CRMDBContext context, IWebHostEnvironment webHostEnvironment
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

                var requestFileName = GenerateUniqueFileName("payment_capture_request", tap_id);
                var responseFileName = GenerateUniqueFileName("payment_capture_response", tap_id);

                string requestInfo = $"Method: {request.Method}\nResource: {request.Resource}\nParameters: {string.Join(", ", request.Parameters)}";

                await SaveDataToFileAsync(requestFileName, requestInfo);
                
                await SaveDataToFileAsync(responseFileName, response.Content);

                var DeserializedResponse = JsonConvert.DeserializeObject<JObject>(response.Content);

                var Status = DeserializedResponse["status"].ToString();
                PaymentStatus = Status.ToString();

                var MetaData = DeserializedResponse["metadata"];
                var SiteName = MetaData?["udf2"];
                var UserId = MetaData?["udf1"];
                var Token = MetaData?["udf3"];
                var PlanId = MetaData?["udf4"];
                var Domain = MetaData?["udf5"];
                var Reference = DeserializedResponse["reference"];
                var TemplateId = Reference?["order"];

                if (Status == "CAPTURED")
                {

                    var IsSiteNameExisted = _context.Sites.Any(o => o.SiteTitle == SiteName.ToString());

                    //if (IsSiteNameExisted)
                    //{
                    //    _toastNotification.AddInfoToastMessage("You already have a site with the same name");

                    //    return Redirect("/index");
                    //}



                    var TemplateFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Preview").ToLower();
                    var TargetPath="";
                    //if (Domain is not null)
                    //{
                    //    TargetPath = Path.Combine(_webHostEnvironment.WebRootPath, Domain.ToString()).ToLower();

                    //}
                    //else
                    //{
                        TargetPath = Path.Combine(_webHostEnvironment.WebRootPath, SiteName.ToString()).ToLower();

                    //}
                    var Template = _context.Templates.Where(i => i.TemplateId == (int)TemplateId).FirstOrDefault();
                    var planObj = _context.Plans.Where(i => i.PlanId == (int)PlanId).FirstOrDefault();
                    string SitePath = TemplateFolder + "\\" + Template.TemplateName;

                    DirectoryInfo TemplateFolders = new DirectoryInfo(SitePath);
                    DirectoryInfo TargetPaths = new DirectoryInfo(TargetPath);

                    CopyFilesRecursively(TemplateFolders, TargetPaths);
                    string TokenValue = (string)Token;

                    if (TokenValue != null)
                    {
                        AffiliateUser = _userManager.Users.Where(e => e.AffiliateToken == TokenValue).FirstOrDefault();
                        if (AffiliateUser != null)
                        {
                            double Points = 0;
                            var affiliatePrices = _context.AffiliatePrices.ToList();
                            foreach (var item in affiliatePrices)
                            {
                                if(planObj.PriceAfterDiscount > 0)
                                {
                                    if (planObj.PriceAfterDiscount >= item.From && planObj.PriceAfterDiscount <= item.To)
                                    {
                                        Points = item.Point;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (planObj.Price >= item.From && planObj.Price <= item.To)
                                    {
                                        Points = item.Point;
                                        break;
                                    }
                                }
                               

                            }
                            AffiliateUser.Point = AffiliateUser.Point + Points;
                            _db.SaveChanges();

                        }

                    }

                    Site NewSite = new Site
                    {
                        TemplateId = (int)TemplateId,
                        UserID = UserId.ToString(),
                        SiteTitle = SiteName.ToString(),
                        CreatedDate = DateTime.Now.Date,
                        IsActive = true,
                        UserDomain = (string)Domain,
                        IsPaid = true,
						UserTemplatePrice = CalculateUserTemplatePrice(Template.TemplatePrice, planObj.PriceAfterDiscount, planObj.Price),
						AffiliateUser = AffiliateUser == null ? null : AffiliateUser.Id,
                    };
                    

                    _context.Sites.Add(NewSite);
                    _context.SaveChanges();
                    SiteSubscription siteSubscription = new SiteSubscription
                    {
                        SiteId = NewSite.SiteId,
                        Price = CalculateUserTemplatePrice(Template.TemplatePrice, planObj.PriceAfterDiscount, planObj.Price),
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(12),
                        PlanId=planObj.PlanId,
                    };
                    _context.SiteSubscriptions.Add(siteSubscription);
                    _context.SaveChanges();

					
					//SendEmailSite((string)UserId,(int)PlanId, (int)TemplateId);
					var user = await _userManager.FindByIdAsync(NewSite.UserID);
                    if (user is not null)
                    {
						
						
						if (user.MLM == true)
                        {
							

							var clientProduct = new HttpClient();
							var requestProduct = new HttpRequestMessage(HttpMethod.Post, "https://admin.techsitekwmlm.com/node_api/web/woocommerce/addPurchaseDetails");
							requestProduct.Headers.Add("secret_key", "nB/SZsQ6K0m4lwbmq/rZvhPouZXaB7dYIVPm2pq0dRppux9VnC5n8l7o6oJQD7OxBF/Y/NcUKR3vaGV6fPnYHOAIo1COom1WsOGKBpIcMCBOwjnFd8/E9VEAJ3Gvu/HAzqfqeJtkiC9HpnRfKHCYZ5kluxOQt/fHrOp7IDUUemKHbq6GmT2Xm4ZuBGt61CYrNJI40+AHa9B0szE/6yw34mZ7T0DGcJ4DRG87zMXpdXC6UyZ5AowHMQHGYSW0U8Il1UEymK0HA3bvbKP4fQI1/3dipSfHRTsmdfRmZ6hvOZHWyhdaqzSkFTiKXM74RaDmvtMYZpJNwk/Lvbhu9vWJUw==");
							requestProduct.Headers.Add("prefix", "990");
							requestProduct.Headers.Add("access-token", "c0674468-00e2-4ab1-b63e-0be02a4bb03a");

							var content = new StringContent(@$"{{
    ""username"": ""{user.sponserName}"",
    ""name"": ""{Template.TemplateName}"",
    ""order_data"": {{
        ""{planObj.MLMId}"": 1,
        ""{Template.MLMId}"": 1
    }}
}}", null, "application/json");

							//var content = new StringContent(@$"{{
							//                     ""username"": ""user01"",
							//                     ""name"": ""{Template.TemplateName}"",
							//                     ""order_data"": {{
							//                         ""{planObj.MLMId}"": 1
							//                     }}
							//                 }}", null, "application/json");

							requestProduct.Content = content;
							var responseProduct = await clientProduct.SendAsync(requestProduct);
							var res = await responseProduct.Content.ReadAsStringAsync();

						}

						
                        //if (domain is not null)
                        //{
                        //    EmailSite.Domain = domain;
                        //}
                        //else
                        //{
                        //    EmailSite.Domain = "Domain not Found";
                        //}
                    }
                    var domainsite = Domain != null ? EmailSite.Domain = (string?)Domain : "Domain not saved";
                        EmailSite.SiteNameAr = NewSite.SiteTitle;
                  

                   
                        EmailSite.StartDate = siteSubscription.StartDate;
                        EmailSite.EndDate = siteSubscription.EndDate;
                    
                    EmailSite.TemplatePrice = await _context.Templates.Where(a => a.TemplateId == NewSite.TemplateId).Select(a => a.TemplatePrice).FirstOrDefaultAsync();
                    EmailSite.PlanNameAr = await _context.Plans.Where(a => a.PlanId == siteSubscription.PlanId).Select(a => a.TitleAr).FirstOrDefaultAsync();

                    EmailSite.SiteEmail = await _context.SocialMediaLinks.Select(a => a.ContactMail).FirstOrDefaultAsync();
                    if(planObj.PriceAfterDiscount > 0)
                    {
                        EmailSite.PlanPrice = planObj.PriceAfterDiscount;
                    }
                    else
                    {
                        EmailSite.PlanPrice = planObj.Price;
                    }
                    
                    var messageBody = await _renderer.RenderPartialToStringAsync("_Invoice", EmailSite);

                    await _emailSender.SendEmailAsync(user.Email, "Purchase Details", messageBody);

                    return Page();
                }

                return Page();
            }
            catch (Exception ex)
            {

                return RedirectToPage("SomethingwentError", new { Message = ex.Message });
                //return Page();
            }

        }

        //private async Task SendEmailSite(string userId, int planId, int templateId)
        //{
        //    try
        //    {
               
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it appropriately
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //        // You might also consider throwing the exception here if it needs to be propagated.
        //    }
        //}

        private double CalculateUserTemplatePrice(double templatePrice, double priceAfterDiscount, double price)
		{
			if (priceAfterDiscount == 0)
			{
				return templatePrice + price; 
			}
			else
			{
				return templatePrice + priceAfterDiscount;
			}
		}
		private void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {

            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));


            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }


        private string GenerateUniqueFileName(string baseName, string paymentId)
        {
            return $"{baseName}_{paymentId}.txt";
        }


        private async Task SaveDataToFileAsync(string fileName, string content)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(webRootPath, fileName);

            await using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                await writer.WriteLineAsync(content);
            }
        }
    }
}
