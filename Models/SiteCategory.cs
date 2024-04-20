using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
    public class SiteCategory
    {
        [Key]
        public int SiteCategoryId { get; set; }
        public string? TitleAr { get; set; }
        public string? TitleEn { get; set; }
        public string? Pic { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Template> Templates { get; set; }
    }
}
