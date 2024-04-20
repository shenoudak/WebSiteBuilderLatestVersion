namespace iTech.Models
{
    public class PublicVideo
    {
        public int PublicVideoId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string BriefAr { get; set; }
        public string BriefEn { get; set; }
        public string VideoUrl { get; set; }= string.Empty;
        public string Background { get; set; }= string.Empty;
    }
}
