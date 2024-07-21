using BadmintonEventsWebApp.Data.Enums;
using BadmintonEventsWebApp.Models;

namespace BadmintonEventsWebApp.ViewModels
{
    public class CreateTournamentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Address Address { get; set; }

        public IFormFile Image { get; set; }

        public MatchCategory MatchCategory { get; set; }

        public AgeCategory AgeCategory { get; set; }
    }
}
