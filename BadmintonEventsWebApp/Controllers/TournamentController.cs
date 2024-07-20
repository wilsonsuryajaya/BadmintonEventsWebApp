using BadmintonEventsWebApp.Data;
using BadmintonEventsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Detail( int id )
        {
            // Include = sql join is expensive
            Tournament tournament = _context.Tournaments.Include( a => a.Address ).FirstOrDefault( x => x.Id == id );
            return View( tournament );
        }
    }
}
