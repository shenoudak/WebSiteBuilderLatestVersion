using iTech.Data;
using iTech.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Security.Claims;

namespace iTech.Areas.CRM.Pages.Profile
{
    public class EditProfileModel : PageModel
    {
        private readonly CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public UserProfileVM UserProfileVM { get; set; }
        private readonly IToastNotification _toastNotification;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public ApplicationUser user { get; set; }
        public string UserID { get; set; }

        public EditProfileModel(CRMDBContext context, ApplicationDbContext db, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            UserProfileVM = new UserProfileVM();
            _signInManager = signInManager;
            _userManager = userManager;
            user = new ApplicationUser();
            _db = db;
        }
        public void OnGet(string id)
        
        {
            user = _userManager.Users.Where(e => e.Id == id).FirstOrDefault();
            UserID = id;
            //UserProfileVM = new UserProfileVM()
            //{
            //    AccountName = user.AccountNumber,
            //    Adress = user.Email,
            //    BankName = user.BankName,
            //    CoverPic= user.CoverImage,
            //    FullName= user.FullName,
            //    Country= user.CountryId,
            //    IBan= user.IPan
                

            //};
        }

        public IActionResult OnPost(string id,IFormFile EditCoverfile)
        {
            try
            {
                var model = _userManager.Users.Where(e => e.Id == id).FirstOrDefault();
                if (EditCoverfile != null)
                {
                    string folder = "Users/";
                    model.CoverImage = UploadImage(folder, EditCoverfile);
                    var UpdatedUser = _db.Users.Attach(model);
                    UpdatedUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    _db.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Cover Edited successfully");
                }
                
            }
            catch(Exception e)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");
            }
            var idS = id.ToString();
            return Redirect("/CRM/Profile/EditProfile?id=" + idS);
        }

        public IActionResult OnPostEditUserProfile(string id, IFormFile EditMainfile)
        {
            try
            {
                var model = _userManager.Users.Where(e => e.Id == id).FirstOrDefault();
                if (EditMainfile != null)
                {
                    string folder = "Users/";
                    model.UserPic = UploadImage(folder, EditMainfile);
                    var UpdatedUser = _db.Users.Attach(model);
                    UpdatedUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    _db.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Image Edited successfully");
                }

            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");
            }
            var idS = id.ToString();
            return Redirect("/CRM/Profile/EditProfile?id=" + idS);
        }
        public async Task <IActionResult> OnPostEditUserMainProfile(string id)
        {
            try
            {
                var model = _userManager.Users.Where(e => e.Id == id).FirstOrDefault();
                var userExists = await _userManager.FindByEmailAsync(user.Email);
                if (userExists != null)
                {
                    _toastNotification.AddErrorToastMessage("Email is already taken. Enter another Email!");
                    return Page();
                }
                model.FullName = user.FullName;
                model.UserName = user.Email;
                model.Email = user.Email;
                await _userManager.UpdateAsync(model);
                _db.Attach(model).State = EntityState.Modified;
                _db.SaveChanges();
                await _signInManager.RefreshSignInAsync(model);
               
                    _toastNotification.AddSuccessToastMessage("Image Edited successfully");
                
                
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");
            }
            var idS = id.ToString();
            return Redirect("/CRM/Profile/EditProfile?id=" + idS);
        }
        public async Task<IActionResult> OnPostEditPassword(string id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();
                if (UserProfileVM.ConfirmPassword != UserProfileVM.NewPassword)
                {
                    _toastNotification.AddErrorToastMessage("Password not matched");
                }
                var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _userManager.FindByIdAsync(userid);
                var Result = await _userManager.ChangePasswordAsync(user, UserProfileVM.CurrentPassword, UserProfileVM.NewPassword);
                if (!Result.Succeeded)
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.TryAddModelError("", error.Description);
                    }
                    return Page();

                }
                _toastNotification.AddSuccessToastMessage("Password Changed");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");
            }
            var idS = id.ToString();
            return Redirect("/CRM/Profile/EditProfile?id=" + idS);
        }
        public async Task<IActionResult> OnPostEditBankInfo(string id)
        {
            try
            {
                var model = _userManager.Users.Where(e => e.Id == id).FirstOrDefault();
                if (model != null)
                {
                    model.IPan = user.IPan;
                    model.AccountNumber = user.AccountNumber;
                    model.BankName = user.BankName;
                    model.Address = user.Address;
                    model.CountryId = user.CountryId;
                    await _userManager.UpdateAsync(model);
                    _db.Attach(model).State = EntityState.Modified;
                    _db.SaveChanges();
                    _toastNotification.AddSuccessToastMessage("Bank Info Edited successfully");
                }
                
               
               


            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");
            }
            var idS = id.ToString();
            return Redirect("/CRM/Profile/EditProfile?id=" + idS);
        }
        
        private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }

    }
}
