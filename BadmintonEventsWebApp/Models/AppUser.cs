namespace BadmintonEventsWebApp.Models
{
    public class AppUser
    {
        public int? TotalPoints { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Tournament> Tournaments { get; set; }
    }
}
