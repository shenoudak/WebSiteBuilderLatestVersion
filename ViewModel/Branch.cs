namespace iTech.ViewModel
{
    public class Branch
    {
        public string? Id { get; set; }
        public long Created { get; set; }
        public bool Virtual { get; set; }
        public List<string>? Brands { get; set; }
    }
}
