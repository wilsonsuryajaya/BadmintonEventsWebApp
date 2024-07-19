using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BadmintonEventsWebApp.Data.Enums;

namespace BadmintonEventsWebApp.Models
{
    public class Tournament
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? Image { get; set; }

        public DateTime? StartTime { get; set; }

        public double? EntryFee { get; set; }

        public string? Website { get; set; }

        public string? Twitter { get; set; }

        public string? Facebook { get; set; }

        public string? Contact { get; set; }

        [ForeignKey( "Address" )]
        public int AddressId { get; set; }

        public Address Address { get; set; }

        public AgeCategory AgeCategory { get; set; }

        public MatchCategory MatchCategory { get; set; }

        [ForeignKey( "AppUser" )]
        public string? AppUserId { get; set; }

        public AppUser? AppUser { get; set; }
    }
}
