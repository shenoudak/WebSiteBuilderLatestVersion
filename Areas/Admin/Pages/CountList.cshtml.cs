using iTech.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;

namespace iTech.Areas.Admin.Pages
{
    public class CountListModel : PageModel
    {
		private CRMDBContext _context;
		private readonly IWebHostEnvironment _hostEnvironment;
		private readonly IToastNotification _toastNotification;

		[BindProperty]
		public Models.Count countObj { get; set; }

		public List<Models.Count> Counts = new List<Models.Count>();
		public static int staticCount = 0;



        public CountListModel(CRMDBContext context
						, IWebHostEnvironment hostEnvironment
						, IToastNotification toastNotification)
		{
			_context = context;
			_hostEnvironment = hostEnvironment;
			_toastNotification = toastNotification;
			countObj = new Models.Count();
		}
		public void OnGet(int id)
		{
			Counts = _context.Count.Where(e=>e.CountInfoId==id).ToList();
			if (Counts != null)
			{
				staticCount = id;
			}
			
		}
		public IActionResult OnGetSingleCountForEdit(int CountId)
		{
			var Result = _context.Count.Where(c => c.CountId == CountId).FirstOrDefault();
			return new JsonResult(Result);

		}

		public async Task<IActionResult> OnPostEditCount(int CountId)
		{
			try
			{
				var model = _context.Count.Where(c => c.CountId == CountId).FirstOrDefault();
				if (model == null)
				{
					_toastNotification.AddErrorToastMessage("Count is not Existed");

					return Redirect("/Admin/CountList?id="+staticCount);
				}


				model.TitleAR = countObj.TitleAR;
				model.TitleEN = countObj.TitleEN;
				model.count = countObj.count;
				var UpdatedCount = _context.Count.Attach(model);
				UpdatedCount.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

				await _context.SaveChangesAsync();
				_toastNotification.AddSuccessToastMessage("Count Edited successfully ");


			}
			catch (Exception)
			{
				_toastNotification.AddErrorToastMessage("Count can't be edited");

			}
			return Redirect("/Admin/CountList?id="+staticCount);
		}
		
	}
}
