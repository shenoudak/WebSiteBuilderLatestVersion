using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.CRM.Pages.Configurations.ManageCountry
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        public string url { get; set; }
        [BindProperty]
        public Models.Country Country { get; set; }

        public List<Models.Country> Countries = new List<Models.Country>();
        public IndexModel(CRMDBContext context
                        , IWebHostEnvironment hostEnvironment
                        , IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            Country = new Models.Country();
        }
        public void OnGet()
        {
            Countries = _context.Countries.ToList();

            url = $"{this.Request.Scheme}://{this.Request.Host}";
        }

        public IActionResult OnPostAddCountry()
        {
            try
            {
                _context.Countries.Add(Country);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Country added successfully");

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Country can't be added");
            }
            return Redirect("/CRM/Configurations/ManageCountry/Index");
        }

        public IActionResult OnGetSingleCountryForEdit(int CountryId)
        {
            var Result = _context.Countries.Where(c => c.CountryId == CountryId).FirstOrDefault();
            return new JsonResult(Result);

        }

        public async Task<IActionResult> OnPostEditCountry(int CountryId)
        {
            try
            {
                var model = _context.Countries.Where(c => c.CountryId == CountryId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Affiliate Price is not Existed");

                    return Redirect("/CRM/Configurations/ManageCountry/Index");
                }


                model.TitleEn = Country.TitleEn;
                model.TitleAr = Country.TitleAr;
                model.SortOrder = Country.SortOrder;
                model.IsActive = Country.IsActive;
                var UpdatedCountry = _context.Countries.Attach(model);
                UpdatedCountry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Country Edited successfully ");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Country can't be edited");

            }
            return Redirect("/CRM/Configurations/ManageCountry/Index");
        }

        public IActionResult OnGetSingleCountryForView(int CountryId)
        {
            var Result = _context.Countries.Where(c => c.CountryId == CountryId).Select(i => new
            {
                CountryId = i.CountryId,
                TitleAr = i.TitleAr,
                TitleEn = i.TitleEn,
                SortOrder = i.SortOrder,
                IsActive = i.IsActive,
            }).FirstOrDefault();
            return new JsonResult(Result);
        }

        public IActionResult OnGetCountryForDelete(int CountryId)
        {
            Country = _context.Countries.Where(c => c.CountryId == CountryId).FirstOrDefault();
            return new JsonResult(Country);
        }

        public async Task<IActionResult> OnPostDeleteCountry(int CountryId)
        {
            try
            {
                Models.Country CountryObj = _context.Countries.Where(e => e.CountryId == CountryId).FirstOrDefault();
                if (CountryObj != null)
                {
                    _context.Countries.Remove(CountryObj);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Country Deleted successfully");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Country can't be deleted");
                }
            }
            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("Country can't be deleted");
            }

            return Redirect("/CRM/Configurations/ManageCountry/Index");
        }
    }
}
