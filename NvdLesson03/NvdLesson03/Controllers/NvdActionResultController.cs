using System.Text;
using Microsoft.AspNetCore.Mvc;
using NvdLesson03.Models;

namespace NvdLesson03.Controllers;

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
            "ContentResult - Lesson03 của Nguyễn Văn Đạt (2410900021)",
            "text/plain",
            Encoding.UTF8);
    }

    [HttpGet("json")]
    public JsonResult JsonDemo()
    {
        return Json(new
        {
            resultType = nameof(JsonResult),
            student = "Nguyễn Văn Đạt",
            studentId = "2410900021",
            lesson = 3
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
        const string content = "Lesson03 - Controller in ASP.NET Core MVC\nSinh viên: Nguyễn Văn Đạt\nMã sinh viên: 2410900021";
        return File(
            Encoding.UTF8.GetBytes(content),
            "text/plain",
            "NguyenVanDat_2410900021_Lesson03.txt");
    }

    [HttpGet("trang-thai")]
    public StatusCodeResult StatusCodeDemo()
    {
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpGet("khong-tim-thay")]
    public NotFoundObjectResult NotFoundDemo()
    {
        return NotFound(new { status = 404, message = "Dữ liệu minh họa không tồn tại." });
    }

    [HttpGet("partial-view")]
    public PartialViewResult PartialViewDemo()
    {
        NvdStudentInfo student = new()
        {
            FullName = "Nguyễn Văn Đạt",
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
