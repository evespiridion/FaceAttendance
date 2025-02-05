using Microsoft.AspNetCore.Mvc;

namespace FaceAttendance.Web.Controllers
{
    public class EnrollUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
