using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
    public partial class Newsletter
    {
        [Key]
        public int NewsletterId { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }


    }
}