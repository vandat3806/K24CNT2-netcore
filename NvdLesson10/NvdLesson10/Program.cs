using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NvdLesson10.Data;
using NvdLesson10.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPasswordHasher<NvdMember>, PasswordHasher<NvdMember>>();

var databaseProvider = builder.Configuration["DatabaseProvider"] ?? "SqlServer";
if (databaseProvider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase))
{
    var sqliteConnection = builder.Configuration.GetConnectionString("NvdSqliteConnection")
        ?? "Data Source=nvdlesson10.db";
    builder.Services.AddDbContext<NvdLesson10EfDbContext>(options => options.UseSqlite(sqliteConnection));
}
else
{
    var sqlServerConnection = builder.Configuration.GetConnectionString("NvdSqlServerConnection")
        ?? throw new InvalidOperationException("Chưa cấu hình ConnectionStrings:NvdSqlServerConnection.");
    builder.Services.AddDbContext<NvdLesson10EfDbContext>(options => options.UseSqlServer(sqlServerConnection));
}

var app = builder.Build();

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

if (databaseProvider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase))
{
    await NvdDbInitializer.InitializeAsync(app.Services);
}

app.Run();
