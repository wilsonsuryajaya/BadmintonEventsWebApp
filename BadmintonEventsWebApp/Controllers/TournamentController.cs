using BadmintonEventsWebApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace BadmintonEventsWebApp.Controllers
{
    public class TournamentController : Controller
    {
        private ApplicationDbContext _context;

        public TournamentController( ApplicationDbContext context )
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tournaments = _context.Tournaments.ToList();
            return View( tournaments );
        }
    }
}
