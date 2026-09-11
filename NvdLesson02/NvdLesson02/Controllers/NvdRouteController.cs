using Microsoft.AspNetCore.Mvc;

namespace NvdLesson02.Controllers;

/// <summary>
/// Controller minh họa attribute routing, route parameters, constraints và query string.
/// </summary>
[Route("dinh-tuyen")]
public class NvdRouteController : Controller
{
    [HttpGet("")]
    public ViewResult Index()
    {
        ViewBag.NamedRouteUrl = Url.RouteUrl(
            "nvd-student-profile",
            new { id = 2_410_900_021L });

        return View();
    }

    [HttpGet("xin-chao/{name?}")]
    public ViewResult Greeting(string name = "bạn")
    {
        ViewData["RouteTitle"] = "Route có tham số tùy chọn";
        ViewData["RouteValue"] = $"Xin chào {name}!";
        ViewData["CurrentRoute"] = "/dinh-tuyen/xin-chao/{name?}";
        return View("Result");
    }

    [HttpGet("bai-hoc/{lesson:int:min(1):max(99)}")]
    public ViewResult Lesson(int lesson)
    {
        ViewData["RouteTitle"] = "Route có ràng buộc kiểu và phạm vi";
        ViewData["RouteValue"] = $"Bạn đang xem Lesson {lesson}.";
        ViewData["CurrentRoute"] = "/dinh-tuyen/bai-hoc/{lesson:int:min(1):max(99)}";
        return View("Result");
    }

    [HttpGet("ho-so/{id:long:min(1000000000):max(9999999999)}", Name = "nvd-student-profile")]
    public ViewResult StudentProfile(long id)
    {
        ViewData["RouteTitle"] = "Named route và ràng buộc mã sinh viên";
        ViewData["RouteValue"] = $"Hồ sơ sinh viên có mã {id}.";
        ViewData["CurrentRoute"] = "/dinh-tuyen/ho-so/{id:long}";
        return View("Result");
    }

    [HttpGet("tim-kiem")]
    public ViewResult Search(string keyword = "MVC", int page = 1)
    {
        ViewData["RouteTitle"] = "Nhận dữ liệu từ query string";
        ViewData["RouteValue"] = $"Từ khóa: {keyword} - Trang: {page}";
        ViewData["CurrentRoute"] = "/dinh-tuyen/tim-kiem?keyword=MVC&page=1";
        return View("Result");
    }
}
