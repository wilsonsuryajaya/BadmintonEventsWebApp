using BadmintonEventsWebApp.Data;
using BadmintonEventsWebApp.Interfaces;
using BadmintonEventsWebApp.Models;
using BadmintonEventsWebApp.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadmintonEventsWebApp.Controllers
{
    public class TournamentController : Controller
    {
        private ITournamentRepository _tournamentRepository;

        public TournamentController( ITournamentRepository tournamentRepository )
        {
            _tournamentRepository = tournamentRepository;
        }

        public async Task<IActionResult> Index()
        {
            var tournaments = await _tournamentRepository.GetAll();
            return View( tournaments );
        }

        public async Task<IActionResult> Detail( int id )
        {
            // Include = sql join is expensive
            Tournament tournament = await _tournamentRepository.GetByIdAsync( id );
            return View( tournament );
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create( Tournament tournament )
        {
            if ( !ModelState.IsValid )
            {
                return View( tournament );
            }
            _tournamentRepository.Add( tournament );
            return RedirectToAction( "index" );
        }
    }
}
