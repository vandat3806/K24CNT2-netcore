var builder = WebApplication.CreateBuilder(args);

// Đăng ký dịch vụ Controller và Razor View.
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

// Kích hoạt những route khai báo bằng attribute trong Controller.
app.MapControllers();

// Route riêng cho trang thông tin sinh viên.
app.MapControllerRoute(
    name: "student",
    pattern: "sinh-vien",
    defaults: new { controller = "Home", action = "About" });

// Custom conventional route cho trang chi tiết sản phẩm.
app.MapControllerRoute(
    name: "product-detail",
    pattern: "san-pham/{id}",
    defaults: new { controller = "NvdProduct", action = "NvdDetails" });

// Route mặc định: /Controller/Action/Id
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
