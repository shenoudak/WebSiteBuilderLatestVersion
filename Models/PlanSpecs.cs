using System.ComponentModel.DataAnnotations;

namespace iTech.Models
{
    public class PlanSpecs
    {
        [Key]
        public int PlanSpecsId { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public int PlanId { get; set; }
        public virtual WebEditorPlan Plan { get; set; }
      

    }
}
