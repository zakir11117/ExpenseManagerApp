using Microsoft.EntityFrameworkCore;

namespace ExpenseManagerApp.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Table for Expenses
        public DbSet<Expense> Expenses { get; set; }
    }
}
