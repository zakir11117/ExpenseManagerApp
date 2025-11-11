using Microsoft.AspNetCore.Mvc;
using ExpenseManagerApp.Data;
using ExpenseManagerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagerApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ExpensesController(ApplicationDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var list = await _db.Expenses.OrderByDescending(e => e.Date).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create() => View(new Expense { Date = DateTime.UtcNow });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (!ModelState.IsValid) return View(expense);
            _db.Expenses.Add(expense);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
