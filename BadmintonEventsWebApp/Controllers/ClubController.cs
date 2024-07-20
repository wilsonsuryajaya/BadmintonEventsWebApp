using BadmintonEventsWebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace BadmintonEventsWebApp.Controllers
{
    public class ClubController : Controller
    {
        private ApplicationDbContext _context;

        public ClubController( ApplicationDbContext context )
        {
            _context = context;    
        }


        public IActionResult Index()             // Controller
        {
            var clubs = _context.Clubs.ToList(); // Model
            return View( clubs );                // View
        }
    }
}
