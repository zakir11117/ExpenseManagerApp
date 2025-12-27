using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseManagerApp.Models;
using System.Text;

namespace ExpenseManagerApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Populate categories, but never throw to the caller
        private async Task PopulateCategoriesAsync()
        {
            try
            {
                // If _context or _context.Expenses is not ready, this may throw.
                // We swallow exceptions and return an empty list so UI still renders.
                var categories = await _context.Expenses
                    .Select(e => e.Category)
                    .Where(c => !string.IsNullOrEmpty(c))
                    .Distinct()
                    .OrderBy(c => c)
                    .ToListAsync();

                ViewBag.Categories = categories ?? new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Warning: PopulateCategoriesAsync failed: " + ex.Message);
                ViewBag.Categories = new List<string>();
            }
        }

        // GET: Expenses
        // Supports optional search and category filter
        public async Task<IActionResult> Index(string search, string category)
        {
            // Populate categories for the dropdown
            await PopulateCategoriesAsync();

            // Start with all expenses
            var query = _context.Expenses.AsQueryable();

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e => e.Title.Contains(search));
            }

            // Apply category filter if provided
            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(e => e.Category == category);
            }

            // Execute query and get results ordered by date
            var expenses = await query.OrderByDescending(e => e.Date).ToListAsync();

            // Store search parameters in ViewBag for the form
            ViewBag.Search = search;
            ViewBag.Category = category;

            // Calculate total amount for display
            ViewBag.TotalAmount = expenses.Sum(e => e.Amount);

            return View(expenses);
        }

        // Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
                if (expense == null) return NotFound();
                return View(expense);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Details error: " + ex.Message);
                return NotFound();
            }
        }

        // Create GET
        public async Task<IActionResult> Create()
        {
            await PopulateCategoriesAsync();
            return View();
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(expense);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Create error: " + ex.Message);
                    ModelState.AddModelError("", "Unable to save expense.");
                }
            }

            await PopulateCategoriesAsync();
            return View(expense);
        }

        // Edit GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var expense = await _context.Expenses.FindAsync(id);
                if (expense == null) return NotFound();
                await PopulateCategoriesAsync();
                return View(expense);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Edit(GET) error: " + ex.Message);
                return NotFound();
            }
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expense);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Edit(POST) error: " + ex.Message);
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }

            await PopulateCategoriesAsync();
            return View(expense);
        }

        // Delete GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var expense = await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
                if (expense == null) return NotFound();
                return View(expense);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete(GET) error: " + ex.Message);
                return NotFound();
            }
        }

        // Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var expense = await _context.Expenses.FindAsync(id);
                if (expense != null)
                {
                    _context.Expenses.Remove(expense);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteConfirmed error: " + ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        // CSV Export
        public async Task<IActionResult> ExportCsv()
        {
            try
            {
                var expenses = await _context.Expenses.ToListAsync();
                var csv = new StringBuilder();
                csv.AppendLine("Id,Title,Amount,Category,Date");
                foreach (var e in expenses)
                {
                    csv.AppendLine($"{e.Id},{e.Title},{e.Amount},{e.Category},{e.Date:yyyy-MM-dd}");
                }
                return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "expenses.csv");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ExportCsv error: " + ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
