namespace iTech.ViewModel
{
    public class MerchantWallets
    {
        public string Id { get; set; }
        public long Created { get; set; }
        public string BaseCurrency { get; set; }
        public string Country { get; set; }
        public string SettlementBy { get; set; }
        public bool PrimaryWallet { get; set; }
        public MerchantBankAccount BankAccount { get; set; }
        public bool IsMerchant { get; set; }
        public bool IsNonResident { get; set; }
    }
}
