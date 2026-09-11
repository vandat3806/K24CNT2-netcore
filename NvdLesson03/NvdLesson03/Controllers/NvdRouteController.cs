using Microsoft.AspNetCore.Mvc;

namespace NvdLesson03.Controllers;

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
        SetResult(
            "Route có tham số tùy chọn",
            $"Xin chào {name}!",
            "/dinh-tuyen/xin-chao/{name?}");
        return View("Result");
    }

    [HttpGet("bai-hoc/{lesson:int:min(1):max(99)}")]
    public ViewResult Lesson(int lesson)
    {
        SetResult(
            "Route có ràng buộc kiểu và phạm vi",
            $"Bạn đang xem Lesson {lesson}.",
            "/dinh-tuyen/bai-hoc/{lesson:int:min(1):max(99)}");
        return View("Result");
    }

    [HttpGet("ho-so/{id:long:min(1000000000):max(9999999999)}", Name = "nvd-student-profile")]
    public ViewResult StudentProfile(long id)
    {
        SetResult(
            "Named route và ràng buộc mã sinh viên",
            $"Hồ sơ sinh viên có mã {id}.",
            "/dinh-tuyen/ho-so/{id:long}");
        return View("Result");
    }

    [HttpGet("tim-kiem")]
    public ViewResult Search(string keyword = "MVC", int page = 1)
    {
        SetResult(
            "Nhận dữ liệu từ query string",
            $"Từ khóa: {keyword} - Trang: {page}",
            "/dinh-tuyen/tim-kiem?keyword=MVC&page=1");
        return View("Result");
    }

    private void SetResult(string title, string value, string route)
    {
        ViewData["RouteTitle"] = title;
        ViewData["RouteValue"] = value;
        ViewData["CurrentRoute"] = route;
    }
}
