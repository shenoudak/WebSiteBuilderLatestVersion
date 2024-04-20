using iTech.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace iTech.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatisticsController : Controller
    {
        private CRMDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public StatisticsController(CRMDBContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _context = context;
            _userManager = userManager;
            _db = db;

        }
        //[HttpGet]
        //public object GetSitesRevienuePerTemplate()
        //{
        //    var data = _context.Sites.Where(e=>e.IsPaid==true).Include(e=>e.Templates)
        //        .GroupBy(c => c.TemplateId).

        //        Select(g => new
        //        {

        //            Lable =_context.Templates.Where(e=>e.TemplateId==g.Key).FirstOrDefault().TemplateName,

        //            Count = g.Count(),
        //            Sum = g.Sum(e => e.Templates.TemplatePrice)

        //        }).OrderByDescending(r => r.Sum).ToList();

        //    return data;


        //}
        [HttpGet]
        public object GetSystemExpensesForAffiliatePerMounth()
        {
            var data = _context.PaymentRequests.Where(e => e.IsPaid == true)
                .GroupBy(c => c.RequestData.Date.Month).

                Select(g => new
                {

                    Lable = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),

                    Count = g.Count(),
                    Sum = g.Sum(e => e.ConfirmAmount)

                }).OrderByDescending(r => r.Sum).ToList();

            return data;


        }
        
        [HttpGet]
        public object GetSitesRevienuePerMounth()
        {
            var data = _context.Sites.Where(e => e.IsPaid == true).Include(e => e.Templates)
                .GroupBy(c => c.CreatedDate.Date.Month).

                Select(g => new
                {

                    Lable = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),

                    Count = g.Count(),
                    Sum = g.Sum(e => e.Templates.TemplatePrice)

                }).OrderByDescending(r => r.Sum).ToList();

            return data;


        }
        //[HttpGet]
        //public async Task<object> GetAffiliateSites()
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    var data = _context.Sites.Where(e => e.AffiliateUser == user.Id)
        //        .GroupBy(c => c.CreatedDate.Date.Month).

        //        Select(g => new
        //        {

        //            Lable = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
        //            Count = g.Count(),
        //            Sum = g.Sum(e => e.Templates.TemplatePrice)

        //        }).OrderByDescending(r => r.Sum).ToList();

        //    return data;


        //}
        [HttpGet]
        public async Task<object> GetAffiliateRequestsPerMounth()
        {
            var user = await _userManager.GetUserAsync(User);

            var data = _context.PaymentRequests.Where(e => e.AffiliateId == user.Id)
                .GroupBy(c => c.RequestData.Date.Month).

                Select(g => new
                {

                    Lable = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                    Count = g.Count(),
                    Sum = g.Sum(e => e.CurrentBalance)

                }).OrderByDescending(r => r.Sum).ToList();

            return data;


        }
        [HttpGet]
        public List<object> GetDountChart()
        {
            List<object> dataDount = new List<object>();
            List<string> labels = new List<string>();
            List<double> Revenue = new List<double>();
            var SiteCategoryList = _context.SiteCategories.Where(e => e.IsActive == true).ToList();
            foreach (var item in SiteCategoryList)
            {
                labels.Add(item.TitleEn);
                double TemplatesRevenuePerCatagory = _context.Sites.Include(e=>e.Templates).Where(e => e.IsPaid == true && e.Templates.SiteCategoryId == item.SiteCategoryId).Sum(e => e.UserTemplatePrice);
                Revenue.Add(TemplatesRevenuePerCatagory);
            }
            dataDount.Add(labels);
            dataDount.Add(Revenue);
            return dataDount;
        }

    }
}
