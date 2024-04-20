using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using iTech.Data;
using NToastNotify;
using iTech.ViewModel;
using Newtonsoft.Json;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.AccessControl;
using Microsoft.Extensions.Hosting;
using System.Net.Http;

namespace iTech.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly CRMDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _hostEnvironment;

        [BindProperty]
        public MLMRegister MLMRegister { get; set; }
		[BindProperty]
		public bool IsMLM { get; set; }
        public RegisterModel(
            UserManager<ApplicationUser> userManager

			 , IToastNotification toastNotification,
			IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            CRMDBContext context,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender,
            IWebHostEnvironment hostEnvironment
)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
			_toastNotification = toastNotification;
			_roleManager = roleManager;
            _hostEnvironment = hostEnvironment;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Enter your Email")]
            [EmailAddress(ErrorMessage = "Not a vaild Email address")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Enter your password")]
            [StringLength(100, ErrorMessage = "The password must be at least 6 and at max 100 characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

			[Display(Name = "Country")]
			public string CountryCode { get; set; }
			[Required]
			public string PhoneNumber { get; set; }
			/// <summary>
			///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
			///     directly from your code. This API may change or be removed in future releases.
			/// </summary>
			//[Required(ErrorMessage = "Confirm Password is required")]
			//[DataType(DataType.Password)]
			//[Display(Name = "Confirm password")]
			//[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			//public string ConfirmPassword { get; set; }

		}


        public async Task OnGetAsync(string? returnUrl = null, bool isTrue=false)
        {
            if (isTrue)
            {
				IsMLM = isTrue;
            }
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            //if (ModelState.IsValid)
            //{
                if (Input!=null)
                {

                    var userExists = await _userManager.FindByEmailAsync(Input.Email);
                    if (userExists != null)
                    {
                        _toastNotification.AddErrorToastMessage("Email is already exist");
                        return Page();
                    }
                    var user = new ApplicationUser();

                    if (IsMLM == false)
                    {
                        user = new ApplicationUser { UserName = Input.Email, Email = Input.Email,   PhoneNumber = Input.PhoneNumber , CountryCode = Input.CountryCode};
					var result = await _userManager.CreateAsync(user, Input.Password);

					if (result.Succeeded)
					{
						_logger.LogInformation("User created a new account with password.");

						await _userManager.AddToRoleAsync(user, "User");

						await _signInManager.SignInAsync(user, isPersistent: true);
						return LocalRedirect(returnUrl);
					}
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                            _toastNotification?.AddErrorToastMessage(error.Description);
                        }
                        
                    }
					
				}
                    else
                    {
                        var gender = Request.Form["Gender"];
					//var DateOfBirth = MLMRegister.dob.HasValue ? MLMRegister.dob.Value : DateTime.MinValue;
					user = new ApplicationUser { 
                        UserName = MLMRegister.username, 
                        Email = Input.Email, 
                        FullName=MLMRegister.name,
                        PhoneNumber=Input.PhoneNumber, 
                        sponserName= MLMRegister.sponsor_username, 
                        JoinedDate=DateTime.Now,
                        Gender= gender, 
                        CountryCode = Input.CountryCode };

					
						var client = new HttpClient();
						var request = new HttpRequestMessage(HttpMethod.Post, "https://admin.techsitekwmlm.com/node_api/web/woocommerce/user_registration");
						request.Headers.Add("secret_key", "nB/SZsQ6K0m4lwbmq/rZvhPouZXaB7dYIVPm2pq0dRppux9VnC5n8l7o6oJQD7OxBF/Y/NcUKR3vaGV6fPnYHOAIo1COom1WsOGKBpIcMCBOwjnFd8/E9VEAJ3Gvu/HAzqfqeJtkiC9HpnRfKHCYZ5kluxOQt/fHrOp7IDUUemKHbq6GmT2Xm4ZuBGt61CYrNJI40+AHa9B0szE/6yw34mZ7T0DGcJ4DRG87zMXpdXC6UyZ5AowHMQHGYSW0U8Il1UEymK0HA3bvbKP4fQI1/3dipSfHRTsmdfRmZ6hvOZHWyhdaqzSkFTiKXM74RaDmvtMYZpJNwk/Lvbhu9vWJUw==");
						request.Headers.Add("prefix", "990");
						request.Headers.Add("access-token", "c0674468-00e2-4ab1-b63e-0be02a4bb03a");
						var data = new
						{
							sponsor_username = MLMRegister.sponsor_username,
							username = MLMRegister.username,
							email = Input.Email,
							password = Input.Password,
							name = MLMRegister.name,
                            country = Input.CountryCode,
                            mobile_no = Input.PhoneNumber,
                        };
						var jsonData = JsonConvert.SerializeObject(data);
						var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        var requestFileName = GenerateUniqueFileName("upayments_request", DateTime.Now.ToString("yyyyMMddHHmmss"));
                        await SaveDataToFileAsync(requestFileName, jsonData);
                        request.Content = content;
                        var responseFileName = GenerateUniqueFileName("upayments_response", DateTime.Now.ToString("yyyyMMddHHmmss"));
                        var response = await client.SendAsync(request);
					    var res = await response.Content.ReadAsStringAsync();
                        await SaveDataToFileAsync(responseFileName, res);





                    if (response.IsSuccessStatusCode)
                        {
                            var result = await _userManager.CreateAsync(user, Input.Password);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User created a new account with password.");

                            await _userManager.AddToRoleAsync(user, "User");
                            await _signInManager.SignInAsync(user, isPersistent: true);
                            return LocalRedirect(returnUrl);
                        }
                        
                        foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                       

                    }
                    else
                        {
                            var MLMRes = JsonConvert.DeserializeObject<MLMRegisterStatusCode>(res);
                            if(MLMRes.error != null)
                            {
                                var errorMessage = MLMRes.error.description;
                                showToast(errorMessage);
                            return Redirect("/Register");
                            }
                        }
                      
				}
				

                }
				
            return Redirect("/");
        }

        //public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        //{
        //    returnUrl ??= Url.Content("~/");
        //    ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        //    if (ModelState.IsValid)
        //    {
        //        var user = CreateUser();

        //        await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
        //        await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
        //        var result = await _userManager.CreateAsync(user, Input.Password);

        //        if (result.Succeeded)
        //        {
        //            _logger.LogInformation("User created a new account with password.");
        //            var customer = new CustomerN()
        //            {
        //                Email = Input.Email,
        //                RegisterDate = DateTime.Now,
        //                CustomerName = Input.FullName,
        //                Phone = Input.Phone,

        //            };
        //            _context.CustomerNs.Add(customer);
        //            _context.SaveChanges();
        //            await _userManager.AddToRoleAsync(user, "Customer");

        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return Page();
        //}
        private void showToast(string message)
        {
            _toastNotification.AddErrorToastMessage(message);

            
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }


        private string GenerateUniqueFileName(string baseName, string paymentId)
        {
            return $"{baseName}_{paymentId}.txt";
        }


        private async Task SaveDataToFileAsync(string fileName, string content)
        {
            var webRootPath = _hostEnvironment.WebRootPath;
            var filePath = Path.Combine(webRootPath, fileName);

            await using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                await writer.WriteLineAsync(content);
            }
        }

    }
}
