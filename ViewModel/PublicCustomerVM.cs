using iTech.Models;
using Microsoft.EntityFrameworkCore;

namespace iTech.ViewModel
{
    public class PublicCustomerVM
    {
        public List<Brands> Brands { get; set; }
        public BrandsBrief BrandsBrief { get; set; }
        public BrandsBrief BrandBriefTitle { get; set; }
    }
}
