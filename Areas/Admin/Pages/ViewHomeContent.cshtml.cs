using iTech.Data;
using iTech.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.Admin.Pages
{
    public class ViewHomeContentModel : PageModel
    {

        private CRMDBContext _context;
        public Models.PublicHomeContent publicHomeContent { get; set; }

        public ViewHomeContentModel(CRMDBContext context)
        {
            _context = context;
        }


        public ActionResult OnGet(int id)
        {
			var _publicHomeContent = _context.PublicHomeContents.FirstOrDefault(a =>a.PublicHomeContentId == id );

            if(_publicHomeContent != null )
            {
                publicHomeContent = _publicHomeContent;
            }
            else
            {
                return NotFound();

			}
            return Page();
        }


    }
}
