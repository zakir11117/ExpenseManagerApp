using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseManagerApp.Data;
using ExpenseManagerApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerApp.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Reports/CategorySummary
        public async Task<IActionResult> CategorySummary()
        {
            // Group expenses by category and sum totals
            var grouped = await _context.Expenses
                .AsNoTracking()
                .GroupBy(e => string.IsNullOrEmpty(e.Category) ? "Uncategorized" : e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(x => (decimal?)x.Amount) ?? 0m
                })
                .OrderByDescending(x => x.Total)
                .ToListAsync();

            var vm = new CategorySummaryViewModel
            {
                Labels = grouped.Select(g => g.Category).ToList(),
                Totals = grouped.Select(g => g.Total).ToList()
            };

            return View(vm);
        }
    }
}
