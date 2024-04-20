using System.ComponentModel.DataAnnotations.Schema;

namespace iTech.Models
{
    public class SitePage
    {
        public int SitePageId { get; set; }
        public string? PageTitle { get; set; }
        public string? CssContent { get; set; }
        public string? Document { get; set; }
        public string? HtmlContent { get; set; }
        public string? MetaContent { get;set; }
        [ForeignKey("Site")]
        public int? SiteId { get; set; }
        public Site? Sites { get; set; }
    }
}
