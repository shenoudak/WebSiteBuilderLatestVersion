namespace iTech.ViewModel
{

    public class Wallet
    {
        public string? Id { get; set; }
        public string? Status { get; set; }
        public long Created { get; set; }
        public string? BaseCurrency { get; set; }
        public string? Country { get; set; }
        public bool PrimaryWallet { get; set; }
        public BankAccount? BankAccount { get; set; }
        public bool IsMerchant { get; set; }
        public bool IsNonResident { get; set; }
    }
}
