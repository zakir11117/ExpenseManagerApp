using System;

namespace ExpenseManagerApp.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Category { get; set; } = "";
        public decimal Amount { get; set; }
        public string Note { get; set; } = "";
    }
}
