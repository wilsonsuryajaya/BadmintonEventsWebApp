using BadmintonEventsWebApp.Data;
using BadmintonEventsWebApp.Interfaces;
using BadmintonEventsWebApp.Models;
using BadmintonEventsWebApp.Repository;
using BadmintonEventsWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BadmintonEventsWebApp.Controllers
{
    public class TournamentController : Controller
    {
        private ITournamentRepository _tournamentRepository;
        private IPhotoService _photoService;

        public TournamentController( ITournamentRepository tournamentRepository, IPhotoService photoService )
        {
            _tournamentRepository = tournamentRepository;
            _photoService = photoService;
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
        public async Task<IActionResult> Create( CreateTournamentViewModel tournamentVM )
        {
            if ( ModelState.IsValid )
            {
                var result = await _photoService.AddPhotoAsync( tournamentVM.Image );

                var tournament = new Tournament
                {
                    Title = tournamentVM.Title,
                    Description = tournamentVM.Description,
                    Image = result.Url.ToString(),
                    MatchCategory = tournamentVM.MatchCategory,
                    Address = new Address
                    {
                        Id = tournamentVM.Address.Id,
                        Street = tournamentVM.Address.Street,
                        City = tournamentVM.Address.City,
                        State = tournamentVM.Address.State,
                    }
                };
                _tournamentRepository.Add( tournament );
                return RedirectToAction( "index" );
            }
            else
            {
                ModelState.AddModelError( "", "Photo upload failed" );
            }
            return View( tournamentVM );
        }
    }
}
