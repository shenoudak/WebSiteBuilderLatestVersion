using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
    public class AffiliatePrice
    {
        [Key]
        public int AffiliatePriceId { get; set; }
        public string? AffiliatePriceTitle { get; set; }
        [RegularExpression(@"^[+-]?\d+(?:\.\d+)?$", ErrorMessage = "Please enter a valid number.")]

        public double From { get; set; }
        [RegularExpression(@"^[+-]?\d+(?:\.\d+)?$", ErrorMessage = "Please enter a valid number.")]

        public double To { get; set; }
        [RegularExpression(@"^[+-]?\d+(?:\.\d+)?$", ErrorMessage = "Please enter a valid number.")]

        public double Point { get; set; }
    }
}
