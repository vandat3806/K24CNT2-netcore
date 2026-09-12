var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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
    name: "book-edit",
    pattern: "sach/chinh-sua/{id:int}",
    defaults: new { controller = "Book", action = "Edit" });

app.MapControllerRoute(
    name: "book-details",
    pattern: "sach/chi-tiet/{id:int}",
    defaults: new { controller = "Book", action = "Details" });

app.MapControllerRoute(
    name: "book-list",
    pattern: "thu-vien-sach",
    defaults: new { controller = "Book", action = "Index" });

app.MapControllerRoute(
    name: "product-details",
    pattern: "san-pham/{id:int}",
    defaults: new { controller = "Product", action = "Details" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
