using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using System.Globalization;

namespace iTech.Areas.Admin.Pages.Configurations.ManagePaymentRequest
{
    public class PaymentRequestDetailsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private CRMDBContext _context;
        private readonly IToastNotification _toastNotification;

        public ApplicationUser AffiliateUsers { get; set; }
        public double PointsAmount { get; set; }
        public double SiteCount { get; set; }
        [BindProperty]
        public PaymentRequest PaymentRequest { get; set; }

        [BindProperty]
        public int PaymentId { get; set; }

        public double RequestPoints { get; set; }

        public double ConfirmAmount { get; set; }

        public List<PaymentType> PaymentType { get; set; }
        public double point { get; set; }
        [BindProperty]
        public string affliateId { get; set; }

        public PaymentRequestDetailsModel(CRMDBContext context, IToastNotification toastNotification, UserManager<ApplicationUser> userManager/*, RoleManager<ApplicationUser> roleManager*/)
        {
            _context = context;
            _toastNotification = toastNotification;
            _userManager = userManager;
            AffiliateUsers = new ApplicationUser();
            PaymentType = new List<PaymentType>();
        }
        public void OnGet(int id)
        {
            try
            {
                PaymentRequest = _context.PaymentRequests.Where(e => e.PaymentRequestId == id).FirstOrDefault();
                PaymentType = _context.PaymentTypes.ToList();
                if (PaymentRequest!=null)
                {
                    AffiliateUsers = _userManager.Users.Where(e => e.Id == PaymentRequest.AffiliateId).FirstOrDefault();
                    if (AffiliateUsers!=null)
                    {
                        var Affiliatepoint = AffiliateUsers.Point;
                        point = _context.PointConfigurations.FirstOrDefault().Value;
                        PointsAmount = Affiliatepoint * point;
                    }
                    affliateId = PaymentRequest.AffiliateId;
                    PaymentRequest.ConfirmAmount = 0;
                    
                    
                    SiteCount = _context.Sites.Where(e => e.AffiliateUser == PaymentRequest.AffiliateId).Count();
                }

            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);
            };

        }

        public async Task<IActionResult> OnPost(string id)
        {
            var model = _context.PaymentRequests.Where(e => e.AffiliateId == affliateId).FirstOrDefault();
            var idS = id.ToString();
            try
            {
                point = _context.PointConfigurations.FirstOrDefault().Value;
                if (model != null)
                {
                    if (decimal.TryParse(Request.Form["CConfirmPointsID"], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal point))
                    {
                        PaymentRequest.ConfirmPoints = (double)point;
                    }
                    //if (decimal.TryParse(Request.Form["CConfirmAmount"], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal ppoint))
                    //{
                    //    PaymentRequest.ConfirmAmount = (double)ppoint;
                    //}
                    var ConfirmAmount = Request.Form["ConfirmAmount"].ToString();
                    var splitConfirmAmount = ConfirmAmount.Split(" ");
                    var AmountNumber = splitConfirmAmount[0];
                    var cleanedConfirmAmount = 0.0;
                    if (decimal.TryParse(AmountNumber, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal poinyt))
                    {
                        cleanedConfirmAmount = (double)poinyt;
                    }
                    //var IndexConfirmAmount = double.Parse(cleanedConfirmAmount);
                    //var RequestAmount = Request.Form["RequestAmount"].ToString();
                    //var splitRequestAmount = RequestAmount.Split(" ");
                    //var IndexRequestAmount = double.Parse(splitRequestAmount[0]);
                    var Payment = Request.Form["option2"];
                    var parsePayment = int.Parse(Payment);
                    var NewRequestPoint = PaymentRequest.RequestPoints - PaymentRequest.ConfirmPoints;
                    var NewRequestAmount = (decimal)NewRequestPoint * point;
                    var NewConfirmAmount = (decimal)cleanedConfirmAmount * point;
                    
                        ////Update in Payment Request Model     
                        model.ConfirmPoints = PaymentRequest.ConfirmPoints;
                        model.ConfirmAmount = (double)NewConfirmAmount;
                        model.PaymentTypeId = parsePayment;
                        model.IsPaid = true;
                        model.RequestStatusId = 3;
                        var UpdatedPaymentRequest = _context.PaymentRequests.Attach(model);
                        UpdatedPaymentRequest.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        //await _context.SaveChangesAsync();
                        ///////update in Asp.net Users Model
                        var user = await _userManager.FindByIdAsync(affliateId);
                        if (user != null)
                        {
                            user.Point = user.Point - PaymentRequest.RequestPoints + NewRequestPoint;
                            await _userManager.UpdateAsync(user);
                        }
                        await _context.SaveChangesAsync();
                        return Redirect("/Admin/Configurations/ManagePaymentRequest/PaymentRequestDetails?id=" + idS);


                }


            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage(ex.Message);

            }
            return Redirect("/Admin/Configurations/ManagePaymentRequest/PaymentRequestDetails?id=" + idS);
        }
    }
}
