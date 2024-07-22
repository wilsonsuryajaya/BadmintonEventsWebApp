using BadmintonEventsWebApp.Models;

namespace BadmintonEventsWebApp.ViewModels
{
    public class DashboardViewModel
    {
        public List<Tournament> Tournaments { get; set; }
        public List<Club> Clubs { get; set; }
    }
}
