using iTech.Areas.Identity.Pages.Account;
using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.Admin.Pages.Configurations.Profile
{
    public class IndexModel : PageModel
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
        public ApplicationUser Admin { get; set; }

        public IndexModel(CRMDBContext Context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
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
            Admin = await _userManager.GetUserAsync(User);

            if (Admin == null)
            {

                return Redirect("~/login");
            }
            if (ChangePasswordVM.CurrentEmail != null)
            {
                Admin.Email = ChangePasswordVM.CurrentEmail;
                Admin.UserName = ChangePasswordVM.CurrentEmail;

            }

            else
            {
                ChangePasswordVM.CurrentEmail = Admin.Email;
            }



            return Page();
        }

        public async Task<IActionResult> OnPostEditAccountInfo(IFormFile file)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    _toastNotification.AddErrorToastMessage("Admin Not Found");

                    return Redirect("/login");
                }


                if (file != null)
                {


                    string folder = "Images/Admin/";

                    user.UserPic = await UploadImage(folder, file);
                }
                else
                {
                    user.UserPic = Admin.UserPic;
                }

                user.FullName = Admin.FullName;
                user.Legalname = Admin.Legalname;
                user.PhoneNumber = Admin.PhoneNumber;
                user.UserName = Admin.Email;
                user.Email = Admin.Email;

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

                    return Redirect("/Admin/Profile/Index");
                }
                

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("Index");
        }


        public async Task<IActionResult> OnPostChangePassword()
        {
           
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _toastNotification.AddErrorToastMessage("Admin Not Found");

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

            return Redirect("/Admin/Profile/Index");
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

