using Microsoft.AspNetCore.Mvc;

namespace BadmintonEventsWebApp.Controllers
{
    public class ClubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
