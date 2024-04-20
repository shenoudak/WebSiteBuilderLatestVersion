namespace iTech.Models
{
    public class CountInfo
    {
        public int CountInfoId { get; set; }
        public string TitleEN { get; set; }  
        public string TitleAR { get; set;}
        public string DescEN { get; set;} 
        public string DescAR { get; set;}
        public ICollection<Count> counts { get; set; }
    }
}
