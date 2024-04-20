using iTech.Models;

namespace iTech.ViewModel
{
	public class ManageEditorPlanVm
	{
		public int WebEditorPlanId { get; set; }

		public string TitleAr { get; set; }
		public string TitleEn { get; set; }
		public double Price { get; set; }
		public double PriceAfterDiscount { get; set; }
		public List<ManagePlanSpecs> PlanSpecs { get; set; }
	}
}
