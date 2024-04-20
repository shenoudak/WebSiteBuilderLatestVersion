using iTech.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.Admin.Pages.Configurations.ManagePointConfiguration
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        public string url { get; set; }
        [BindProperty]
        public Models.PointConfiguration pointConfiguration { get; set; }

        public List<Models.PointConfiguration> pointConfigurations = new List<Models.PointConfiguration>();
        public IndexModel(CRMDBContext context
                        , IWebHostEnvironment hostEnvironment
                        , IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            pointConfiguration = new Models.PointConfiguration();
        }
        public void OnGet()
        {
            pointConfigurations = _context.PointConfigurations.ToList();

            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }

        public IActionResult OnGetSinglePointConfigurationForEdit(int PointConfigurationId)
        {
            var Result = _context.PointConfigurations.Where(c => c.PointConfigurationId == PointConfigurationId).FirstOrDefault();
            return new JsonResult(Result);

        }

        public async Task<IActionResult> OnPostEditPointConfiguration(int PointConfigurationId)
        {
            try
            {
                var model = _context.PointConfigurations.Where(c => c.PointConfigurationId == PointConfigurationId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Point Configuration is not Existed");

                    return Redirect("/Admin/Configurations/ManagePointConfiguration/Index");
                }


                model.Value = pointConfiguration.Value;
               
                var UpdatedPointConfigurations = _context.PointConfigurations.Attach(model);
                UpdatedPointConfigurations.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("PointConfigurations Edited successfully ");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Country can't be edited");

            }
            return Redirect("/Admin/Configurations/ManagePointConfiguration/Index");
        }
    }
}
