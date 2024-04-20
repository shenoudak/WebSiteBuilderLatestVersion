namespace iTech.ViewModel
{
    public class Entity
    {
        public string? Id { get; set; }
        public string? Status { get; set; }
        public long Created { get; set; }
        public Name? LegalName { get; set; }
        public string? Country { get; set; }
        public bool IsLicensed { get; set; }
        public bool Taxable { get; set; }
        public List<Wallet>? Wallets { get; set; }
        public List<Branch>? Branches { get; set; }
        public Operator Operator { get; set; }
    }
}
