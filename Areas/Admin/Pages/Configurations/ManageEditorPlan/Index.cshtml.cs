using iTech.Data;
using iTech.DataTables;
using iTech.Migrations;
using iTech.Models;
using iTech.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace iTech.Areas.Admin.Pages.Configurations.ManageEditorPlan
{
    public class IndexModel : PageModel
    {
        private CRMDBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;


        public string url { get; set; }


        [BindProperty]
        public ManageEditorPlanVm WebEditorPlan { get; set; }


		public List<PlanSpecs> WebEditorPlanList = new List<PlanSpecs>();

        public WebEditorPlan WebEditorPlanObj { get; set; }

        public IndexModel(CRMDBContext context, IWebHostEnvironment hostEnvironment,
                                            IToastNotification toastNotification)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            WebEditorPlan = new ManageEditorPlanVm();
            WebEditorPlanObj = new WebEditorPlan();
        }

        [BindProperty]
        public DataTablesRequest DataTablesRequest { get; set; }
        public void OnGet()
        {

        }
        public async Task<JsonResult> OnPostAsync()
        {
            var recordsTotal = _context.WebEditorPlans.Count();

            var customersQuery = _context.WebEditorPlans.AsQueryable();

            var searchText = DataTablesRequest.Search.Value?.ToUpper();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                customersQuery = customersQuery.Where(s =>
                    s.TitleEn.ToUpper().Contains(searchText) ||
                    s.TitleAr.ToUpper().Contains(searchText)
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

        public IActionResult OnGetFindWebEditorPlan(int WebEditorPlanId)
        {
            var webEditorPlan = _context.WebEditorPlans.FirstOrDefault(c => c.WebEditorPlanId == WebEditorPlanId);
            if (webEditorPlan != null)
            {
                ManageEditorPlanVm editorPlanVm = new ManageEditorPlanVm
                {
                    WebEditorPlanId = webEditorPlan.WebEditorPlanId,
                    TitleEn = webEditorPlan.TitleEn,
                    TitleAr = webEditorPlan.TitleAr,
                    Price = webEditorPlan.Price,
                    PriceAfterDiscount = webEditorPlan.PriceAfterDiscount,
                    PlanSpecs = new List<ManagePlanSpecs>() 
                };

                var planSpecs = _context.PlansSpecs.Where(a => a.PlanId == webEditorPlan.WebEditorPlanId).ToList();
                
                foreach (var planSpec in planSpecs)
                {
                    ManagePlanSpecs planSpecsVm = new ManagePlanSpecs
                    {
                        PlanSpecsId = planSpec.PlanSpecsId,
                        TitleEn = planSpec.TitleEn,
                        TitleAr = planSpec.TitleAr
                    };

                    editorPlanVm.PlanSpecs.Add(planSpecsVm);
                }

                return new JsonResult(editorPlanVm);
            }

            return BadRequest("WebEditorPlan not found");
        }

        public async Task<IActionResult> OnPostManageWebEditorPlan(ManageEditorPlanVm ManageEditorPlan)
        {
            try
            {
                if(ManageEditorPlan.WebEditorPlanId != 0)
                {
                    var webplan = _context.WebEditorPlans.Where(a => a.WebEditorPlanId == ManageEditorPlan.WebEditorPlanId).FirstOrDefault();
                    if(webplan != null)
                    {
						webplan.TitleEn = ManageEditorPlan.TitleEn;
						webplan.TitleAr = ManageEditorPlan.TitleAr;
						webplan.Price = ManageEditorPlan.Price;
						webplan.PriceAfterDiscount = ManageEditorPlan.PriceAfterDiscount;
                        var plspecs = _context.PlansSpecs.Where(a => a.PlanId == webplan.WebEditorPlanId).ToList();
                        if(plspecs.Count > 0)
                        {
                            _context.PlansSpecs.RemoveRange(plspecs);
                            
                        }
                        if (ManageEditorPlan.PlanSpecs.Count > 0)
                        {

                            foreach (var spec in ManageEditorPlan.PlanSpecs)
                            {

                                PlanSpecs Planspec = new PlanSpecs()
                                {
                                    PlanId = webplan.WebEditorPlanId,
                                    TitleAr = spec.TitleAr,
                                    TitleEn = spec.TitleEn,
                                };
                                _context.PlansSpecs.Add(Planspec);

                            }
                        }

                        _context.SaveChanges();
					}
                    

					_toastNotification.AddSuccessToastMessage("WebEditorPlan Updated Successfully");
				}
                else
                {
					var editorPlan = new WebEditorPlan
					{
                        RequestsCount = 0,
						TitleEn = ManageEditorPlan.TitleEn,
						TitleAr = ManageEditorPlan.TitleAr,
						Price = ManageEditorPlan.Price,
						PriceAfterDiscount = ManageEditorPlan.PriceAfterDiscount,
					};
					_context.WebEditorPlans.Add(editorPlan);
					_context.SaveChanges();
					if (ManageEditorPlan.PlanSpecs != null)
					{
                        if (ManageEditorPlan.PlanSpecs.Count > 0)
                        {

                            foreach (var spec in ManageEditorPlan.PlanSpecs)
                            {

                                PlanSpecs Planspec = new PlanSpecs()
                                {
                                    PlanId = editorPlan.WebEditorPlanId,
                                    TitleAr = spec.TitleAr,
                                    TitleEn = spec.TitleEn,
                                };
                                _context.PlansSpecs.Add(Planspec);

                            }
                        }
                    }
					_context.SaveChanges();

					_toastNotification.AddSuccessToastMessage("WebEditorPlan Added Successfully");
				}
               

            }
            catch (Exception)
            {

                _toastNotification.AddErrorToastMessage("Something went wrong");
            }
                return new JsonResult(true);

        }


        public async Task<IActionResult> OnPostWebEditorPlanDelete(int WebEditorPlanId)
        {
            try
            {
                WebEditorPlan WebEditorPlanObj = _context.WebEditorPlans.Where(e => e.WebEditorPlanId == WebEditorPlanId).FirstOrDefault();

                if (WebEditorPlanObj != null)
                {
                    var planSpecs = _context.PlansSpecs.Where(e => e.PlanId == WebEditorPlanId).ToList();
                    if(planSpecs.Count > 0)
                    {
                       
                            _context.PlansSpecs.RemoveRange(planSpecs);

                    }

                    _context.WebEditorPlans.Remove(WebEditorPlanObj);

                    await _context.SaveChangesAsync();

                    _toastNotification.AddSuccessToastMessage("WebEditorPlan Deleted successfully");


                }
                else
                {
                    _toastNotification.AddErrorToastMessage("Something went wrong Try Again");
                }
            }
            catch (Exception)

            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
            }

            return Redirect("/Admin/Configurations/ManageEditorPlan/Index");
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

