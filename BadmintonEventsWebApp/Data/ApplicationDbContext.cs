using BadmintonEventsWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BadmintonEventsWebApp.Data
{
    /// <summary>
    /// This is allows us to be able to pull data from the database.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<AppUser> // IdentityDbContext<AppUser, AppRole> 
    {
        DbContextOptions<ApplicationDbContext> _options;
        
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options ) : base( options )
        {

        }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<Club> Clubs { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}
