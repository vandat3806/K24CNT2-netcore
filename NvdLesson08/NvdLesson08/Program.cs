using Microsoft.AspNetCore.Identity;
using NvdLesson08.Models.DataModels;
using NvdLesson08.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IPasswordHasher<NvdMember>, PasswordHasher<NvdMember>>();
builder.Services.AddSingleton<INvdMemberRepository, InMemoryNvdMemberRepository>();

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

app.Run();
