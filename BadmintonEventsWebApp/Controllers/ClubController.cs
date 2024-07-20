using BadmintonEventsWebApp.Data;
using BadmintonEventsWebApp.Interfaces;
using BadmintonEventsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadmintonEventsWebApp.Controllers
{
    public class ClubController : Controller
    {
        private IClubRepository _clubRepository;

        public ClubController( IClubRepository clubRepository )
        {
            _clubRepository = clubRepository;
        }

        public async Task<IActionResult> Index()                            // Controller
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();       // Model
            return View( clubs );                                           // View
        }

        public async Task<IActionResult> Detail( int id )
        {
            // Include = sql join is expensive
            Club club = await _clubRepository.GetByIdAsync( id );
            return View( "~/Views/Club/Detail.cshtml", model: club );
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create( Club club )
        {
            if ( !ModelState.IsValid )
            {
                return View( club );
            }
            _clubRepository.Add( club );
            return RedirectToAction( "index" );
        }
    }
}
