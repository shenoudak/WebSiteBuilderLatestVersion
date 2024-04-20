namespace iTech.ViewModel
{
    public class MerchantOperator
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public long Created { get; set; }
        public string Name { get; set; }
        public string DeveloperId { get; set; }
        public bool IsMerchant { get; set; }
        public MerchantApiCredentials ApiCredentials { get; set; }
    }
}
