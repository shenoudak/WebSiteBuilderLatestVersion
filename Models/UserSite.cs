using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iTech.Data;
namespace iTech.Models
{
    public class UserSite
    {
        [Key]
        public int UserSiteId { get; set; }
        public string? ShortName { get; set; }
        public string? PageName { get; set; }
        public string? TemplateName { get; set; }
        public string? CategoryName { get; set; }

        public string? htmlContent { get; set; }
        public string? cssContent { get; set; }
        public int TemplateId { get; set; }
        public int PageId { get; set; }
        public int CategoryId { get; set; }

        //public virtual Template Template { get; set; }
        //public virtual Page Page { get; set; }

        //public DateTime CreatedDate { get; set; }
        //[ForeignKey("ApplicationUser")]
        //public string UserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }

    }
}
