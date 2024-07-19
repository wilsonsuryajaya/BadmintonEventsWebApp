using Microsoft.AspNetCore.Mvc;

namespace BadmintonEventsWebApp.Controllers
{
    public class TournamentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
