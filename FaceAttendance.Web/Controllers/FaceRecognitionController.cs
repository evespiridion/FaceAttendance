using Microsoft.AspNetCore.Mvc;

namespace FaceAttendance.Web.Controllers
{
    public class FaceRecognitionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
