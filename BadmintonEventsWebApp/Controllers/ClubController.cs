using BadmintonEventsWebApp.Data;
using BadmintonEventsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Detail( int id )
        {
            // Include = sql join is expensive
            Club club = _context.Clubs.Include( a => a.Address ).FirstOrDefault( x => x.Id == id );
            return View( "~/Views/Club/Detail.cshtml", model: club );
        }
    }
}
