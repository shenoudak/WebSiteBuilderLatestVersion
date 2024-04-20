using iTech.Models;
using Microsoft.EntityFrameworkCore;

namespace iTech.ViewModel
{
    public class PlansVM
    {
    

        public Plan FirstPlan { get; set; }
        public Plan LastPlan { get; set; }
        public List<Plan> planPrices { get; set; }
    }
}
