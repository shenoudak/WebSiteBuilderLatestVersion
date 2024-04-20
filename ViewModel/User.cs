namespace iTech.ViewModel
{
    public class User
    {
        public string? Id { get; set; }
        public string? Status { get; set; }
        public long Created { get; set; }
        public Name? Name { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public bool IsAuthorized { get; set; }
        public bool IsVerified { get; set; }
    }
}
