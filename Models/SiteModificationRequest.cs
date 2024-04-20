using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace iTech.Models
{
	public class SiteModificationRequest
	{
		[Key]
		public long SMReqId { get; set; }
		public int SiteId { get; set; }
		public string DesignerId { get; set; }
		public string UserId { get; set; }
		public string ModificationDescription { get; set; }
		public string Notes { get; set; }
		public int RequestCount { get; set; }
		public bool IsAccepted { get; set; }
		public bool IsFinished { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		[ForeignKey("WESubscription")]
		public int? WESubscriptionId { get; set; }
		[JsonIgnore]
		public WebEditorSubscription? WESubscriptions { get; set; }
	}
}
