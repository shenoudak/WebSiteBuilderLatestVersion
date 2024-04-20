using iTech.Data;
using iTech.DataTables;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;

namespace iTech.Areas.Admin.Pages.Configurations.AffiliatePrices
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public Models.AffiliatePrice AffiliatePrice { get; set; }

        //public List<Models.AffiliatePrice> AffiliatePriceList = new List<Models.AffiliatePrice>();

       


        public IndexModel(CRMDBContext context
                        , IWebHostEnvironment hostEnvironment
                        , IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            AffiliatePrice = new Models.AffiliatePrice();
        }
        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }

        public async Task<JsonResult> OnPostAsync()
        {
            var recordsTotal = _context.AffiliatePrices.Count();

            var customersQuery = _context.AffiliatePrices.AsQueryable();

            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.AffiliatePriceTitle.ToUpper().Contains(searchText)
                );
            }

            var recordsFiltered = customersQuery.Count();

            var sortColumnName = DataTablesRequest.Columns.ElementAt(DataTablesRequest.Order.ElementAt(0).Column).Name;
            var sortDirection = DataTablesRequest.Order.ElementAt(0).Dir.ToLower();

            // using System.Linq.Dynamic.Core
            customersQuery = customersQuery.OrderBy($"{sortColumnName} {sortDirection}");

            var skip = DataTablesRequest.Start;
            var take = DataTablesRequest.Length;
            var data = await customersQuery
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return new JsonResult(new
            {
                draw = DataTablesRequest.Draw,
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFiltered,
                data = data
            });
        }
        //public void OnGet()
        //{
        //    AffiliatePriceList = _context.AffiliatePrices.ToList();

        //    url = $"{this.Request.Scheme}://{this.Request.Host}";
        //}
        public IActionResult OnGetSingleAffiliatePricesForEdit(int AffiliatePriceId)
        {
            var Result = _context.AffiliatePrices.Where(c => c.AffiliatePriceId == AffiliatePriceId).FirstOrDefault();
            return new JsonResult(Result);

        }

        public async Task<IActionResult> OnPostEditAffiliatePrices(int AffiliatePriceId)
        {
            try
            {
                var model = _context.AffiliatePrices.Where(c => c.AffiliatePriceId == AffiliatePriceId).FirstOrDefault();
                if (model == null)
                {
                    _toastNotification.AddErrorToastMessage("Affiliate Price is not Existed");

                    return Redirect("/Admin/Configurations/AffiliatePrices/Index");
                }

                if (decimal.TryParse(Request.Form["AffPoint"], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal point))
                {
					AffiliatePrice.Point = (double)point;
				}
				if (decimal.TryParse(Request.Form["AffFrom"], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal From))
				{
					AffiliatePrice.From = (double)From;
				}
				if (decimal.TryParse(Request.Form["AffTo"], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal To))
				{
					AffiliatePrice.To = (double)To;
				}
				model.AffiliatePriceTitle = AffiliatePrice.AffiliatePriceTitle;
                model.Point = AffiliatePrice.Point;
					model.From = AffiliatePrice.From;
				model.To = AffiliatePrice.To;
				var UpdatedAffliatePrice = _context.AffiliatePrices.Attach(model);
                UpdatedAffliatePrice.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _context.SaveChangesAsync();
                _toastNotification.AddSuccessToastMessage("Affliate Price Edited successfully ");


            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("Affliate Price can't be edited");

            }
            return Redirect("/Admin/Configurations/AffiliatePrices/Index");
        }
        public async Task<IActionResult> OnPostAddAffiliatePrices()
        {
            try
            {
              

                _context.AffiliatePrices.Add(AffiliatePrice);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage("Affiliate Price added successfully");

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Affiliate Price can't be added");
            }
            return Redirect("/Admin/Configurations/AffiliatePrices/Index");
        }
        public IActionResult OnGetSingleAffiliatePricesForView(int AffiliatePriceId)
        {
            var Result = _context.AffiliatePrices.Where(c => c.AffiliatePriceId == AffiliatePriceId).Select(i => new
            {
                AffiliatePriceId = i.AffiliatePriceId,
                AffiliatePriceTitle = i.AffiliatePriceTitle,
                From = i.From,
                To = i.To,
                Point = i.Point,
            }).FirstOrDefault();
            return new JsonResult(Result);
        }

        public IActionResult OnGetAffiliatePricesForDelete(int AffiliatePriceId)
        {
            AffiliatePrice = _context.AffiliatePrices.Where(c => c.AffiliatePriceId == AffiliatePriceId).FirstOrDefault();
            return new JsonResult(AffiliatePrice);
        }

        public async Task<IActionResult> OnPostDeleteAffiliatePrices(int AffiliatePriceId)
        {
            try
            {
                Models.AffiliatePrice AffiliatePriceObj = _context.AffiliatePrices.Where(e => e.AffiliatePriceId == AffiliatePriceId).FirstOrDefault();


                if (AffiliatePriceObj != null)
                {
                    _context.AffiliatePrices.Remove(AffiliatePriceObj);
                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Affiliate Price Deleted successfully");
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Affiliate Price can't be deleted");
                }
            }
            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("Affiliate Price can't be deleted");
            }

            return Redirect("/Admin/Configurations/AffiliatePrices/Index");
        }
    }
}
