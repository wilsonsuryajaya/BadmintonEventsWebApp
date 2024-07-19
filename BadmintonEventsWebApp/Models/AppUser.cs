using System.ComponentModel.DataAnnotations;

namespace BadmintonEventsWebApp.Models
{
    public class AppUser
    {
        [Key]
        public string Id { get; set; }
        public int? TotalPoints { get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Tournament> Tournaments { get; set; }
    }
}
