using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
    public class Plan
    {
        [Key]
        public int PlanId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string? MLMId { get; set; }
        public double Price { get; set; }
        public double PriceAfterDiscount { get; set; }
        public int PagesCount { get; set; }
    }
}
