using iTech.Models;

namespace iTech.ViewModel
{
	public class PublicCountVM
	{
		public string TitleEN { get; set; }
		public string TitleAR { get; set; }
		public string DescEN { get; set; }
		public string DescAR { get; set; }
		public ICollection<Count> counts { get; set; }
	}
}
