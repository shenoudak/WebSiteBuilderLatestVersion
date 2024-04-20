using System.Drawing.Drawing2D;
using System.Xml.Linq;

namespace iTech.ViewModel
{
    public class Business
    {
        public string? Id { get; set; }
        public string? Status { get; set; }
        public long Created { get; set; }
        public string? Object { get; set; }
        public bool LiveMode { get; set; }
        public string? ApiVersion { get; set; }
        public string? FeatureVersion { get; set; }
        public Name? Name { get; set; }
        public string? Type { get; set; }
        public List<Brand>? Brands { get; set; }
        public Entity? Entity { get; set; }
        public User? User { get; set; }
    }
}
