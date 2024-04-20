using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string Image { get; set; }
    }
}
