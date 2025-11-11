using Microsoft.AspNetCore.Mvc;
using ExpenseManagerApp.Services;

namespace ExpenseManagerApp.Controllers
{
    public class ExportController : Controller
    {
        private readonly ICsvExportService _csv;
        public ExportController(ICsvExportService csv) => _csv = csv;

        public async Task<IActionResult> ExpensesCsv(DateTime? from = null, DateTime? to = null)
        {
            var bytes = await _csv.GenerateExpensesCsvAsync(from, to);
            var fileName = $"expenses_{(from?.ToString("yyyyMMdd") ?? "start")}_{(to?.ToString("yyyyMMdd") ?? "end")}.csv";
            var bom = System.Text.Encoding.UTF8.GetPreamble();
            var result = bom.Concat(bytes).ToArray();
            return File(result, "text/csv; charset=utf-8", fileName);
        }
    }
}
