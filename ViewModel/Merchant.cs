namespace iTech.ViewModel
{
    public class Merchant
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public long Created { get; set; }
        public string Object { get; set; }
        public bool LiveMode { get; set; }
        public string ApiVersion { get; set; }
        public string FeatureVersion { get; set; }
        public string DisplayName { get; set; }
        public string BusinessId { get; set; }
        public string BusinessEntityId { get; set; }
        public List<string> ChargeCurrencies { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public MerchantOperator Operator { get; set; }
        public string LegacyId { get; set; }
        public bool Marketplace { get; set; }
        public MerchantWallets Wallets { get; set; }
    }
}
