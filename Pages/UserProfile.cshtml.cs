using iTech.Areas.Identity.Pages.Account;
using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using NToastNotify;

namespace iTech.Pages
{
	public class UserProfileModel : PageModel
	{
		private readonly IWebHostEnvironment _hostEnvironment;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ILogger<LogoutModel> _logger;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IToastNotification _toastNotification;
		private readonly CRMDBContext _context;
		private readonly ApplicationDbContext _db;

		[BindProperty]
		public ChangePasswordVM ChangePasswordVM { get; set; }
		public ApplicationUser user { get; set; }

		public UserProfileModel(ApplicationDbContext db, CRMDBContext Context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
		{
			_context = Context;
			_signInManager = signInManager;
			_logger = logger;
			_userManager = userManager;
			_hostEnvironment = hostEnvironment;
			_toastNotification = toastNotification;
			ChangePasswordVM = new ChangePasswordVM();
			_db = db;
		}
		public async Task<IActionResult> OnGet()
		
		{
			 user = await _userManager.GetUserAsync(User);

			if (user == null)
			{

				return Redirect("~/login");
			}
			if (ChangePasswordVM.CurrentEmail != null)
			{
				
				// Update the email if it's provided
				user.Email = ChangePasswordVM.CurrentEmail;
				user.UserName = ChangePasswordVM.CurrentEmail;
				
				}
			
			else
			{
				 ChangePasswordVM.CurrentEmail = user.Email ;
			}



			return Page();
		}

		public async Task<IActionResult> OnPostAsync(IFormFile file)
		{

			user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return Redirect("~/login");
			}
			if (file != null)
			{
				string folder = "Users/";
				user.UserPic = UploadImage(folder, file);
				var UpdatedUser = _db.Users.Attach(user);
				UpdatedUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				_db.SaveChanges();
			}

			if (ChangePasswordVM.CurrentEmail !=null)
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

			if (ChangePasswordVM.CurrentPassword!=null&& ChangePasswordVM.NewPassword!=null&& ChangePasswordVM.ConfirmPassword != null)
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
			// Update the password

			return Redirect("/UserProfile");
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
