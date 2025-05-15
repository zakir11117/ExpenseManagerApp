# 💸 Expense Manager App – ASP.NET Core MVC Demo

A full-stack **Expense Tracking** and **Profit/Loss Management** system built with **ASP.NET Core 9 MVC** and **Entity Framework Core**. This is a demo app for developers and financial planners who want to manage daily, weekly, and monthly expenses, incomes, and profitability.

📌 **Pinned Repository** · 🧠 Built with Clean Code Principles · 🛠️ Extensible for SaaS

---

## 🔧 Tech Stack

- **Backend**: ASP.NET Core 9 (MVC Pattern)
- **ORM**: Entity Framework Core (Code First)
- **Frontend**: Razor Views, Bootstrap 5, jQuery (Light UI)
- **Database**: SQL Server
- **Dev Tools**: Visual Studio 2022, GitHub, SSMS
- **Hosting (Optional)**: Azure / Local IIS / Docker

---

## 📁 Project Structure

```
ExpenseManagerApp/
│
├── Controllers/          → MVC controllers (Expenses, Income, Summary, Category etc.)
├── Models/               → Entity and View Models (Expense.cs, Income.cs, etc.)
├── Views/                → Razor views for all features
│   ├── Shared/           → Layout, partials
│   └── Expense/          → Index, Create, Edit, etc.
├── ViewModels/           → Strongly-typed view models
├── Data/                 → DbContext and SeedData
├── wwwroot/              → Static assets (css, js)
├── Migrations/           → EF Core migrations
├── appsettings.json      → Connection strings and config
└── Program.cs / Startup.cs → App startup and services
```

---

## 🌟 Key Features

✅ **Expense Tracking**  
✅ **Income Logging**  
✅ **Daily, Weekly, Monthly Reports**  
✅ **Profit / Loss Calculation**  
✅ **Category + Subcategory Management**  
✅ **Recurring Expenses Suggestions**  
✅ **Future/Planned Expenses Table**  
✅ **Role-based Access (Planned)**  
✅ **Modern UI (Upcoming)**  
✅ **SaaS-ready Architecture (Optional)**

---



## 🚀 Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/ExpenseManagerApp.git
   cd ExpenseManagerApp
   ```

2. **Open in Visual Studio 2022**  
   Make sure `.NET 9 SDK` is installed.

3. **Configure DB connection**  
   Update `appsettings.json` with your SQL Server connection string.

4. **Apply migrations & seed data**
   ```bash
   dotnet ef database update
   ```

5. **Run the app**
   ```bash
   dotnet run
   ```

6. **Use Seed Buttons (Optional)**  
   Navigate to `/Seed` route to add default Categories, Subcategories, Incomes, Expenses, and Recurring Expenses.

---

## 💡 Suggested Enhancements

- Multi-user support with roles
- Graphs for summaries (Chart.js or Recharts)
- Recurring Expense scheduler
- Export to PDF/Excel
- Notification system

---
📌 **Pinned Repository** · 🧠 Clean Architecture · 🛠️ SaaS-Ready · 📊 Finance Focused
---

## ⭐ Show Your Support!

If this project helped you or inspired your own expense tracking idea:

- 🌟 Star this repository
- 🍴 Fork it to extend or customize
- 🐛 Report issues or suggest features
- 🤝 Contribute by submitting a pull request

---

Built with ❤️ by [Asif Hameed](https://asifhameed.com) | Founder @ [ETEKsol](https://eteksol.com)

Let’s build better finance tools together!
