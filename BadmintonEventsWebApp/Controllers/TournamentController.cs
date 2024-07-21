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

        public async Task<IActionResult> Edit( int id )
        {
            var tournament = await _tournamentRepository.GetByIdAsync( id );
            if ( tournament == null )
                return View( "error" );
            // Can you automapper
            var tournamentVM = new EditTournamentViewModel
            {
                Title = tournament.Title,
                Description = tournament.Description,
                AddressId = tournament.AddressId,
                Address = tournament.Address,
                URL = tournament.Image,
                MatchCategory = tournament.MatchCategory,
                AgeCategory = tournament.AgeCategory,
            };
            return View( tournamentVM );
        }

        [HttpPost]
        public async Task<IActionResult> Edit( int id, EditTournamentViewModel tournamentVM )
        {
            if ( !ModelState.IsValid )
            {
                ModelState.AddModelError( "", "Failed to edit tournament" );
                return View( "Edit", tournamentVM );
            }

            var userTournament = await _tournamentRepository.GetByIdAsyncNoTracking( id );

            if ( userTournament == null )
            {
                return View( "Error" );
            }

            userTournament.Title = tournamentVM.Title;
            userTournament.Description = tournamentVM.Description;
            userTournament.MatchCategory = tournamentVM.MatchCategory;
            userTournament.AgeCategory = tournamentVM.AgeCategory;
            if ( userTournament.Address == null )
                userTournament.Address = new Address();
            userTournament.Address.Id = tournamentVM.Address.Id;
            userTournament.Address.Street = tournamentVM.Address.Street;
            userTournament.Address.City = tournamentVM.Address.City;
            userTournament.Address.State = tournamentVM.Address.State;

            if ( tournamentVM.Image != null )
            {
                if ( !string.IsNullOrEmpty( userTournament.Image ) )
                {
                    try
                    {
                        await _photoService.DeletePhotoAsync( userTournament.Image );
                    }
                    catch ( Exception ex )
                    {
                        ModelState.AddModelError( "", "Could not delete photo" );
                        return View( "Edit", tournamentVM );
                    }
                }

                var photoResult = await _photoService.AddPhotoAsync( tournamentVM.Image );

                userTournament.Image = photoResult.Url.ToString();
            }

            _tournamentRepository.Update( userTournament );
            return RedirectToAction( "Index" );
        }
    }
}
