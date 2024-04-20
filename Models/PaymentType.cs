using System.ComponentModel.DataAnnotations;
namespace iTech.Models
{
    public class PaymentType
    {
        [Key]
        public int PaymentTypeId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
    }
}
