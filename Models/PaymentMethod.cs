using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
	public class PaymentMethod
	{
		[Key]
		public int PaymentMethodId { get; set; }
		public string? PaymentMethodAR { get; set; }
		public string? PaymentMethodEN { get; set; }
		public string? PaymentMethodPic { get; set; }
	}
}
