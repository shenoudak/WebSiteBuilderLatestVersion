using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
	public class WebEditorPlan
	{
		[Key]
		public int WebEditorPlanId { get; set; }
		public string TitleAr { get; set; }
		public string TitleEn { get; set; }
		public double Price { get; set; }
		public double PriceAfterDiscount { get; set; }
		public int RequestsCount { get; set; }
	}
}
