using ExpenseManagerApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------
// REGISTER DATABASE (SQLite) + ENTITY FRAMEWORK CONTEXT
// -------------------------------------------------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=expenses.db"));

// Add MVC services (controllers + views)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// -------------------------------------------------------------
// AUTOMATIC DATABASE CREATION (NO MIGRATIONS NEEDED)
// -------------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated(); // <-- creates DB and tables automatically
}

// -------------------------------------------------------------
// MIDDLEWARE CONFIGURATION
// -------------------------------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();  // Make sure static files are served

app.UseRouting();
app.UseAuthorization();

// -------------------------------------------------------------
// DEFAULT ROUTE
// -------------------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
