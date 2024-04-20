using iTech.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTech.Models
{
    public class Site
    {
        [Key]
        public int SiteId { get; set; }
        public string? SiteTitle { get; set; }
        public string? PhysicalPath { get; set; }
        public string? UserDomain { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int? TemplateId { get; set; }
        public Template? Templates { get; set; }
        public bool IsPaid { get; set; }    
        public double UserTemplatePrice { get; set; }
        public string? UserID { get; set; }
        public string? AffiliateUser { get; set; }
        public ICollection<SitePage> SitePages { set; get; } = new HashSet<SitePage>();
    }
}
