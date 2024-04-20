using iTech.Areas.Identity.Pages.Account;
using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.Affiliate.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;
        private readonly CRMDBContext _context;

        [BindProperty]
        public ChangePasswordVM ChangePasswordVM { get; set; }
        [BindProperty]
        public ApplicationUser Affiliate { get; set; }

        public ProfileModel(CRMDBContext Context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _context = Context;
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            ChangePasswordVM = new ChangePasswordVM();
        }
        public async Task<IActionResult> OnGet()

        {
            Affiliate = await _userManager.GetUserAsync(User);

            if (Affiliate == null)
            {

                return Redirect("~/login");
            }
            if (ChangePasswordVM.CurrentEmail != null)
            {
                Affiliate.Email = ChangePasswordVM.CurrentEmail;
                Affiliate.UserName = ChangePasswordVM.CurrentEmail;

            }

            else
            {
                ChangePasswordVM.CurrentEmail = Affiliate.Email;
            }



            return Page();
        }

        public async Task<IActionResult> OnPostEditAccountInfo(IFormFile file, IFormFile EditFile)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                   
                    return Redirect("/login");
                }


                if (file != null)
                {


                    string folder = "Images/Affiliate/";

                    user.UserPic = await UploadImage(folder, file);
                }
                else
                {
                    user.UserPic = Affiliate.UserPic;
                }
                if (EditFile != null)
                {


                    string folder = "Images/Affiliate/";

                    user.CoverImage = await UploadImage(folder, EditFile);
                }
                else
                {
                    user.CoverImage = Affiliate.CoverImage;
                }
                user.FullName = Affiliate.FullName;
                user.Legalname = Affiliate.Legalname;
                user.PhoneNumber = Affiliate.PhoneNumber;
                user.UserName = Affiliate.UserName;
                user.Email = Affiliate.Email;
                user.IPan = Affiliate.IPan;
                user.BankName = Affiliate.BankName;
                user.BrandName = Affiliate.BrandName;
                user.Address = Affiliate.Address;
                user.licensedtype = Affiliate.licensedtype;
                user.licensednumber = Affiliate.licensednumber;
                user.UserName = Affiliate.Email;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
                else
                {
                    _context.SaveChanges();

                    _toastNotification.AddSuccessToastMessage("User Information Edited successfully");

                    return Redirect("/Affiliate/Profile");
                }


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Affiliate/Profile");
        }


        public async Task<IActionResult> OnPostChangePassword()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _toastNotification.AddErrorToastMessage("Affiliate Not Found");

                return Redirect("/login");
            }


            if (ChangePasswordVM.CurrentEmail != null)
            {
                if (ChangePasswordVM.CurrentEmail != user.Email)
                {
                    var userExists = await _userManager.FindByEmailAsync(ChangePasswordVM.CurrentEmail);
                    if (userExists != null)
                    {
                        _toastNotification.AddErrorToastMessage("Email is already exist");
                        return Page();
                    }
                    // Update the email if it's provided
                    user.Email = ChangePasswordVM.CurrentEmail;
                    user.UserName = ChangePasswordVM.CurrentEmail;
                    var setEmailResult = await _userManager.SetEmailAsync(user, user.Email);
                    if (!setEmailResult.Succeeded)
                    {
                        foreach (var error in setEmailResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return Page();
                    }

                }

            }
            else
            {
                //user.Email = ChangePasswordVM.CurrentEmail;
                _toastNotification.AddErrorToastMessage("Please Enter New Email");
                return Page();
            }

            if (ChangePasswordVM.CurrentPassword != null && ChangePasswordVM.NewPassword != null && ChangePasswordVM.ConfirmPassword != null)
            {
                var changePasswordResult = await _userManager.ChangePasswordAsync(
                user,
                ChangePasswordVM.CurrentPassword,
                ChangePasswordVM.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return Page();
                }
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
                await _signInManager.RefreshSignInAsync(user);

            }
           

            return Redirect("/Affiliate/Index");
        }
        public async Task<IActionResult> OnPostAffiliateWithdrawalNewRequest()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Redirect("/login");

                }
                if (user.Point == 0)
                {
                    _toastNotification.AddErrorToastMessage("Can Not Withdraw Request Because Your Balance Equal To Zero Point");
                    return Redirect("/Affiliate/Index");
                }
                var PointCost = _context.PointConfigurations.FirstOrDefault().Value;
                double AffiliateAmount = user.Point * PointCost;
                if (AffiliateAmount == 0)
                {
                    _toastNotification.AddErrorToastMessage("Can Not Withdraw Request Because Your Balance Equal To Zero");
                    return Redirect("/Affiliate/Index");
                }
                var PaymentRequest = new PaymentRequest()
                {
                    AffiliateId = user.Id,
                    IsPaid = false,
                    PaymentTypeId = 2,
                    RequestData = DateTime.Now,
                    CurrentBalance = AffiliateAmount,
                    RequestPoints = user.Point,
                    RequestStatusId = 1
                };
                _context.PaymentRequests.Add(PaymentRequest);
                _context.SaveChanges();

            }
            catch (Exception)

            {


            }

            return Redirect("/Affiliate/Index");
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
