using System.ComponentModel.DataAnnotations;
namespace iTech.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
