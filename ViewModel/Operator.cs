using System.Net;

namespace iTech.ViewModel
{
    public class Operator
    {
        public string? Id { get; set; }
        public string? Status { get; set; }
        public long Created { get; set; }
        public string? Name { get; set; }
        public string? DeveloperId { get; set; }
        public bool IsMerchant { get; set; }
        public ApiCredentials ApiCredentials { get; set; }
    }
}
