using Microsoft.EntityFrameworkCore;
using ExpenseManagerApp.Models;

namespace ExpenseManagerApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Expense> Expenses { get; set; } = null!;
    }
}
