using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace iTech.Models
{
    public class TemplatePage
    {
        public int TemplatePageId { get; set; }

        public string? PageTitle { get; set; }
        public string? Document { get; set; }
        public string? HtmlContent { get; set; }
        public string? CssContent { get; set; }
        public string? MetaContent { get; set; }
        [ForeignKey("Template")]
        public int? TemplateId { get; set; }
        [JsonIgnore]
        public Template? Templates { get; set; }  

    }
}
