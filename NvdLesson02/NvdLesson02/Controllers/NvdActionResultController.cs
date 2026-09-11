using System.Text;
using Microsoft.AspNetCore.Mvc;
using NvdLesson02.Models;

namespace NvdLesson02.Controllers;

/// <summary>
/// Mỗi Action bên dưới trả về một loại ActionResult thường dùng trong ASP.NET Core MVC.
/// </summary>
[Route("ket-qua")]
public class NvdActionResultController : Controller
{
    [HttpGet("")]
    public ViewResult Index()
    {
        return View();
    }

    [HttpGet("noi-dung")]
    public ContentResult ContentDemo()
    {
        return Content(
            "ContentResult - Bài làm của Nguyen van dat (2410900021)",
            "text/plain",
            Encoding.UTF8);
    }

    [HttpGet("json")]
    public JsonResult JsonDemo()
    {
        return Json(new
        {
            resultType = nameof(JsonResult),
            student = "Nguyen van dat",
            studentId = "2410900021",
            lesson = 2
        });
    }

    [HttpGet("chuyen-action")]
    public RedirectToActionResult RedirectToActionDemo()
    {
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("chuyen-url")]
    public RedirectResult RedirectDemo()
    {
        return Redirect("/");
    }

    [HttpGet("tai-tep")]
    public FileContentResult FileDemo()
    {
        const string content = "Lesson02 - Controller in ASP.NET Core MVC\nSinh viên: Nguyen van dat\nMã sinh viên: 2410900021";
        return File(Encoding.UTF8.GetBytes(content), "text/plain", "NguyenVanDat_2410900021.txt");
    }

    [HttpGet("trang-thai")]
    public StatusCodeResult StatusCodeDemo()
    {
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpGet("khong-tim-thay")]
    public NotFoundObjectResult NotFoundDemo()
    {
        return NotFound(new
        {
            status = 404,
            message = "Dữ liệu minh họa không tồn tại."
        });
    }

    [HttpGet("partial-view")]
    public PartialViewResult PartialViewDemo()
    {
        NvdStudentInfo student = new()
        {
            FullName = "Nguyen van dat",
            StudentId = "2410900021",
            ClassName = "K24CNT2",
            Subject = "Phát triển ứng dụng web với công nghệ .NET"
        };

        return PartialView("_StudentCard", student);
    }

    [HttpGet("rong")]
    public EmptyResult EmptyDemo()
    {
        return new EmptyResult();
    }
}
