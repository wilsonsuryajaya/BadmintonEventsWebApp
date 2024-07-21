using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BadmintonEventsWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public int? TotalPoints { get; set; }
        [ForeignKey("Address")]
        public int AddressId {  get; set; }
        public Address? Address { get; set; }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<Tournament> Tournaments { get; set; }
    }
}
