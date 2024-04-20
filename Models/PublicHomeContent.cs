using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
    public class PublicHomeContent
    {
        [Key]
        public int PublicHomeContentId { get; set; }
        public string AboutSectionTitleAr { get; set; }
        public string AboutSectionTitleEn { get; set; }
        public string AboutSectionDescAr { get; set; }
        public string AboutSectionDescEn { get; set; }
        public string Pic { get; set; }
    }
}
