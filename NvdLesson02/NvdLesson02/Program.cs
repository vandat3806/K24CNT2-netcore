var builder = WebApplication.CreateBuilder(args);

// Đăng ký dịch vụ MVC: Controller + View.
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

// Kích hoạt các route khai báo trực tiếp bằng attribute trong Controller.
app.MapControllers();

// Route quy ước tùy chỉnh: /sinh-vien
app.MapControllerRoute(
    name: "student",
    pattern: "sinh-vien",
    defaults: new { controller = "Home", action = "About" });

// Route quy ước có tham số và ràng buộc kiểu số nguyên: /san-pham/2
app.MapControllerRoute(
    name: "product-detail",
    pattern: "san-pham/{id:int:min(1)}",
    defaults: new { controller = "NvdProduct", action = "NvdDetails" });

// Route mặc định: /Controller/Action/Id
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
