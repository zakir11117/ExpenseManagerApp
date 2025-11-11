using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExpenseManagerApp.Data;

namespace ExpenseManagerApp.Services
{
    public interface ICsvExportService
    {
        Task<byte[]> GenerateExpensesCsvAsync(DateTime? from = null, DateTime? to = null);
    }

    public class CsvExportService : ICsvExportService
    {
        private readonly ApplicationDbContext _db;
        public CsvExportService(ApplicationDbContext db) => _db = db;

        public async Task<byte[]> GenerateExpensesCsvAsync(DateTime? from = null, DateTime? to = null)
        {
            var q = _db.Expenses.AsQueryable();
            if (from.HasValue) q = q.Where(e => e.Date >= from.Value.Date);
            if (to.HasValue) q = q.Where(e => e.Date <= to.Value.Date);

            var list = await q.OrderBy(e => e.Date).ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Date,Category,Amount,Note");
            foreach (var item in list)
            {
                var dateStr = item.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                var amountStr = item.Amount.ToString(CultureInfo.InvariantCulture);
                sb.AppendLine($"{EscapeCsv(dateStr)},{EscapeCsv(item.Category)},{EscapeCsv(amountStr)},{EscapeCsv(item.Note)}");
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private static string EscapeCsv(string s)
        {
            if (s is null) return "";
            bool quote = s.Contains(",") || s.Contains("\"") || s.Contains("\n") || s.Contains("\r");
            if (s.Contains("\"")) s = s.Replace("\"", "\"\"");
            return quote ? $"\"{s}\"" : s;
        }
    }
}
