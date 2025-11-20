namespace ExpenseManagerApp.Models
{
    public class CategorySummaryViewModel
    {
        public List<string> Labels { get; set; } = new();
        public List<decimal> Totals { get; set; } = new();
    }
}
