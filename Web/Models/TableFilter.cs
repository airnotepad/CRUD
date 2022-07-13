namespace Web.Models
{
    public class TableFilter
    {
        public List<string>? providers { get; set; }
        public string? number { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
        public string? itemName { get; set; }
        public string? itemUnit { get; set; }
    }
}