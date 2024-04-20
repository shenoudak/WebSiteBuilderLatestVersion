using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
	public class WebEditorSubscription
	{
		[Key]
		public int WESubscriptionId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public double Price { get; set; }
		public int SiteId { get; set; }
		public int PlanId { get; set; }
		public virtual Site Site { get; set; }
		public virtual WebEditorPlan Plan { get; set; }
		public ICollection<SiteModificationRequest> SiteModificationRequests { get; set; } = new HashSet<SiteModificationRequest>();

	}
}
