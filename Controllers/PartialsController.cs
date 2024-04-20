using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iTech.Data;
using iTech.Models;

namespace iTech.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PartialsController : Controller
    {
        private CRMDBContext _context;

        public PartialsController(CRMDBContext context)
        {
            _context = context;
        }
        
        public IActionResult GetPartial(string handler)
        {

            //switch (handler)
            //{
            //    case "_Overview":
            //        return OnGet_Overview();
            //    case "_ProfileSettings":
            //        return OnGet_ProfileSettings();
            //    case "_SMLinks":
            //        return OnGet_SMLinks();
            //    case "_NewLetters":
            //        return OnGet_NewLetters();
            //    case "_PublicSlider":
            //        return OnGet_PublicSlider();
            //    case "_PageContent":
            //        return OnGet_PageContent();
            //    case "_Services":
            //        return OnGet_Services();
            //}

            return NotFound();
        }
        


    }
}