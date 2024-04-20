namespace iTech.ViewModel
{
public class EmailSite
{
    public string? Domain { get; set; }
    public string PlanNameAr { get; set; }
    public string SiteNameAr { get; set; }
    public string SiteEmail { get; set; }
        
    public double TemplatePrice { get; set; }
    public double PlanPrice { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int TotalDuration
        {
        get
        {
            return EndDate.Day - StartDate.Day;
        }
    }
	
		public double TotalPrice
	{
			get
			{
				return PlanPrice + TemplatePrice;
			}
		}

		// Constructor to initialize properties
		public EmailSite(string siteEmail, double totalPrice, double planPrice, string domain, string planNameAr, string siteNameAr, double templatePrice, DateTime startDate, DateTime endDate)
    {
        Domain = domain;
        PlanNameAr = planNameAr;
        SiteNameAr = siteNameAr;
        TemplatePrice = templatePrice;
        StartDate = startDate;
        EndDate = endDate;
            SiteEmail = siteEmail;
            PlanPrice = planPrice;
    }

        public EmailSite()
        {
        }
    }
}
