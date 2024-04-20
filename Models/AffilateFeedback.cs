using System.ComponentModel.DataAnnotations;
namespace iTech.Models
{
	public class AffilateFeedback
	{
		[Key]
		public int AffilateFeedbackId { get; set; }
		public string TitleAr { get; set; }
		public string TitleEn { get; set; }
		public string UserOneFeedbackAr { get; set; }
		public string UserOneFeedbackEn { get; set; }
		public string UserTwoFeedbackEn { get; set; }
		public string UserTwoFeedbackAr { get; set; }
		public string UserThreeFeedbackEn { get; set; }
		public string UserThreeFeedbackAr { get; set; }


	}
}
