using iTech.Data;
using iTech.DataTables;
using iTech.Models;
using iTech.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;

namespace iTech.Areas.Admin.Pages.Configurations.ManageDesigner
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public string url { get; set; }
		[BindProperty]
		public ApplicationUser Editor { get; set; }
		[BindProperty]
		public AddDesignerVM Input { get; set; }

        public IndexModel(UserManager<ApplicationUser> userManager, 
            CRMDBContext context, IWebHostEnvironment hostEnvironment,
            IToastNotification toastNotification,
			RoleManager<IdentityRole> roleManager)
        {
			_userManager = userManager;
			_context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
			_roleManager = roleManager;
			Editor = new ApplicationUser();
			Input = new AddDesignerVM();
		}
      
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public async Task<JsonResult> OnPostAsync()
        {
            var webEditorRole = await _roleManager.FindByNameAsync("WebEditor");
            var webEditorUsers = await _userManager.GetUsersInRoleAsync(webEditorRole.Name);

            var recordsTotal = webEditorUsers.Count();

            var customersQuery = webEditorUsers.AsQueryable();

            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.FullName.ToUpper().Contains(searchText) ||
                    s.Email.ToUpper().Contains(searchText)
                );
            }

            var recordsFiltered = customersQuery.Count();


            var skip = DataTablesRequest.Start;
            var take = DataTablesRequest.Length;

            return new JsonResult(new
            {
                draw = DataTablesRequest.Draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = customersQuery
            });
        }


        public IActionResult OnGetSingleEditorForEdit(string EditorId)
        {
            Editor = _userManager.Users.Where(a => a.Id == EditorId).FirstOrDefault();

            return new JsonResult(Editor);
        }

        public IActionResult OnGetSingleSiteCategoriesForView(string EditorId)
        {
            var Result = _userManager.Users.Where(a => a.Id == EditorId).FirstOrDefault();
			return new JsonResult(Result);
        }

        public async Task<IActionResult> OnPostEditEditor(string EditorId, IFormFile Editfile)
        {
            try
            {
                var model = _userManager.Users.Where(a => a.Id == EditorId).FirstOrDefault();
				if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Editor Not Found");

                    return Redirect("/Admin/Configurations/ManageDesigner/Index");
                }


                if (Editfile != null)
                {


                    string folder = "Images/Editor/";

                    model.UserPic = UploadImage(folder, Editfile);
                }
                else
                {
                    model.UserPic = Editor.UserPic;
                }


                model.Email = Editor.Email;
                model.IsActive = Editor.IsActive;
                model.FullName = Editor.FullName;

                var UpdatedEditor = await _userManager.UpdateAsync(model);
				if (UpdatedEditor.Succeeded)
				{
					_toastNotification.AddSuccessToastMessage("Editor Edited successfully");
				}
				else
				{
					_toastNotification.AddErrorToastMessage("Failed to edit Editor");
				}

				return Redirect("/Admin/Configurations/ManageDesigner/Index");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Something went Error");

            }
            return Redirect("/Admin/Configurations/ManageDesigner/Index");
        }
        public async Task<IActionResult> OnPostAddEditor(IFormFile file)
        {
            try
            {
               
					var user = CreateUser();
                    user.RegisterdData = Input.RegisterdData;
					user.FullName = Input.FullName;
					user.UserName = Input.Email;
					user.Email = Input.Email;
                user.IsActive = Input.IsActive;
					user.CountryCode = "KW";
					if (file != null)
					{
                        string folder = "Images/Editor/";

						user.UserPic = UploadImage(folder, file);
					}

					var result = await _userManager.CreateAsync(user, Input.Password);

					if (result.Succeeded)
					{
						var roleExists = await _roleManager.RoleExistsAsync("WebEditor");
						if (roleExists)
						{
							await _userManager.AddToRoleAsync(user, "WebEditor");
						}

						var userId = await _userManager.GetUserIdAsync(user);
						var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
						code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

					}
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				

				_toastNotification.AddSuccessToastMessage("Editor Added Successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Redirect("/Admin/Configurations/ManageDesigner/Index");
        }

        public IActionResult OnGetSingleEditorForDelete(string EditorId)
		{
	        Editor = _userManager.Users.Where(a => a.Id == EditorId).FirstOrDefault();

            return new JsonResult(Editor);
        }

		public async Task<IActionResult> OnPostEditorDelete(string EditorId)
		{
			try
			{
				var editor = await _userManager.FindByIdAsync(EditorId);

				if (editor == null)
				{
					_toastNotification.AddErrorToastMessage("Editor Not Found");
					return Redirect("/Admin/Configurations/ManageDesigner/Index");
				}

				var result = await _userManager.DeleteAsync(editor);

				if (result.Succeeded)
				{
					_toastNotification.AddSuccessToastMessage("Editor Deleted successfully");
				}
				else
				{
					_toastNotification.AddErrorToastMessage("Failed to delete Editor");
				}
			}
			catch (Exception)
			{
				_toastNotification.AddErrorToastMessage("Something went wrong");
			}

			return Redirect("/Admin/Configurations/ManageDesigner/Index");
		}

		private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
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
	}
}
