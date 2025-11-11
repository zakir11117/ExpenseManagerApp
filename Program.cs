using ExpenseManagerApp.Data;
using ExpenseManagerApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add EF Core InMemory for demo/testing
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseInMemoryDatabase("ExpenseDb"));

// Register CSV export service (implementation file will be added)
builder.Services.AddScoped<ICsvExportService, CsvExportService>();

var app = builder.Build();

// Seed some sample data on startup (optional)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!db.Expenses.Any())
    {
        db.Expenses.AddRange(new[]
        {
            new ExpenseManagerApp.Models.Expense { Date = DateTime.UtcNow.AddDays(-2), Category = "Food", Amount = 10.5m, Note = "Lunch" },
            new ExpenseManagerApp.Models.Expense { Date = DateTime.UtcNow.AddDays(-1), Category = "Transport", Amount = 5.0m, Note = "Bus" },
            new ExpenseManagerApp.Models.Expense { Date = DateTime.UtcNow, Category = "Office", Amount = 25.0m, Note = "Stationery, \"urgent\"" }
        });
        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
