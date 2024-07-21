using BadmintonEventsWebApp.Data;
using BadmintonEventsWebApp.Interfaces;
using BadmintonEventsWebApp.Models;
using BadmintonEventsWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
