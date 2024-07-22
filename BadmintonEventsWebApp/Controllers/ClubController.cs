using BadmintonEventsWebApp.Data;
using BadmintonEventsWebApp.Interfaces;
using BadmintonEventsWebApp.Models;
using BadmintonEventsWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace BadmintonEventsWebApp.Controllers
{
    public class ClubController : Controller
    {
        private IClubRepository _clubRepository;
        private IPhotoService _photoService;

        public ClubController( IClubRepository clubRepository, IPhotoService photoService )
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
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
        public async Task<IActionResult> Create( CreateClubViewModel clubVM )
        {
            if ( ModelState.IsValid )
            {
                var result = await _photoService.AddPhotoAsync( clubVM.Image );

                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    ClubCategory = clubVM.ClubCategory,
                    Address = new Address
                    {
                        Id = clubVM.Address.Id,
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State,
                    }
                };
                _clubRepository.Add( club );
                return RedirectToAction( "index" );
            }
            else
            {
                ModelState.AddModelError( "", "Photo upload failed" );
            }
            return View( clubVM );
        }

        public async Task<IActionResult> Edit( int id )
        {
            var club = await _clubRepository.GetByIdAsync( id );
            if ( club == null )
                return View( "error" );
            // Can you automapper
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId.Value,
                Address = club.Address,
                URL = club.Image,
                ClubCategory = club.ClubCategory
            };
            return View( clubVM );
        }

        [HttpPost]
        public async Task<IActionResult> Edit( int id, EditClubViewModel clubVM )
        {
            if ( !ModelState.IsValid )
            {
                ModelState.AddModelError( "", "Failed to edit club" );
                return View( "Edit", clubVM );
            }

            var userClub = await _clubRepository.GetByIdAsyncNoTracking( id );

            if ( userClub == null )
            {
                return View( "Error" );
            }

            userClub.Title = clubVM.Title;
            userClub.Description = clubVM.Description;
            userClub.ClubCategory = clubVM.ClubCategory;
            if ( userClub.Address == null )
                userClub.Address = new Address();
            userClub.Address.Id = clubVM.Address.Id;
            userClub.Address.Street = clubVM.Address.Street;
            userClub.Address.City = clubVM.Address.City;
            userClub.Address.State = clubVM.Address.State;

            if ( clubVM.Image != null )
            {
                if ( !string.IsNullOrEmpty( userClub.Image ) )
                {
                    try
                    {
                        await _photoService.DeletePhotoAsync( userClub.Image );
                    }
                    catch ( Exception ex )
                    {
                        ModelState.AddModelError( "", "Could not delete photo" );
                        return View( "Edit", clubVM );
                    }
                }

                var photoResult = await _photoService.AddPhotoAsync( clubVM.Image );

                userClub.Image = photoResult.Url.ToString();
            }

            _clubRepository.Update( userClub );
            return RedirectToAction( "Index" );
        }

        [HttpGet]
        public async Task<IActionResult> Delete( int id )
        {
            var clubDetails = await _clubRepository.GetByIdAsync( id );
            if ( clubDetails == null ) 
                return View( "Error" );
            return View( clubDetails );
        }

        [HttpPost, ActionName( "Delete" )]
        public async Task<IActionResult> DeleteClub( int id )
        {
            var clubDetails = await _clubRepository.GetByIdAsync( id );

            if ( clubDetails == null )
            {
                return View( "Error" );
            }

            if ( !string.IsNullOrEmpty( clubDetails.Image ) )
            {
                _ = _photoService.DeletePhotoAsync( clubDetails.Image );
            }

            _clubRepository.Delete( clubDetails );
            return RedirectToAction( "Index" );
        }
    }
}
