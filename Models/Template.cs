using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
    public class Template
    {
        
        [Key]
        public int TemplateId { get; set; }
        public string? TitleAr { get; set; }
        public string? TitleEn { get; set; }
        public string? MLMId { get; set; }
        public string? TemplatePath { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplatePic { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public int? SiteCategoryId { get; set; }
        public double TemplatePrice { get; set; }
        public  SiteCategory? SiteCategories { get; set; }
        public  ICollection<TemplatePage> TemplatePages { get; set; } = new HashSet<TemplatePage>();
        public  ICollection<Site> Sites { get; set; } = new HashSet<Site>();


    }
}
