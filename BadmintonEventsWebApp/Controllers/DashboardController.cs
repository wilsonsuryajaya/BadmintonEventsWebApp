using BadmintonEventsWebApp.Data;
using BadmintonEventsWebApp.Interfaces;
using BadmintonEventsWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BadmintonEventsWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController( IDashboardRepository dashboardRepository )
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userTournaments = await _dashboardRepository.GetAllUserTournaments();
            var userClubs = await _dashboardRepository.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                Tournaments = userTournaments,
                Clubs = userClubs
            };
            return View( dashboardViewModel );
        }
    }
}
