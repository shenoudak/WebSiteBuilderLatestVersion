namespace iTech.Models
{
    public class Count
    {
        public int CountId { get; set; }
        public int count { get; set; }
        public string? TitleEN { get; set; }
        public string? TitleAR { get; set; }
        public int CountInfoId { get; set; }
        public CountInfo countInfo { get; set; }
    }
}
