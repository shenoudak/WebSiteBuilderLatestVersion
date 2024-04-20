namespace iTech.Models
{
    public class SiteSubscription
    {
        public int SiteSubscriptionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public int SiteId { get; set; }
        public int PlanId { get; set; }
        public virtual Site Site { get; set; }
        public virtual Plan Plan { get; set; }
    }
}
