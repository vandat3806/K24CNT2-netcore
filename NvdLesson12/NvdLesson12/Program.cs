using Microsoft.EntityFrameworkCore;
using NvdLesson12.Data;
using NvdLesson12.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IImageStorageService, LocalImageStorageService>();

var databaseProvider = builder.Configuration["DatabaseProvider"] ?? "SqlServer";
if (databaseProvider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase))
{
    var sqliteConnection = builder.Configuration.GetConnectionString("NvdSqliteConnection")
        ?? "Data Source=nvdlesson12.db";
    builder.Services.AddDbContext<NvdLesson12DbContext>(options => options.UseSqlite(sqliteConnection));
}
else
{
    var sqlServerConnection = builder.Configuration.GetConnectionString("NvdSqlServerConnection")
        ?? throw new InvalidOperationException("Chưa cấu hình ConnectionStrings:NvdSqlServerConnection.");
    builder.Services.AddDbContext<NvdLesson12DbContext>(options => options.UseSqlServer(sqlServerConnection));
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

public partial class Program;
