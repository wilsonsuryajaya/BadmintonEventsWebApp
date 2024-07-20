using BadmintonEventsWebApp.Data;
using BadmintonEventsWebApp.Interfaces;
using BadmintonEventsWebApp.Models;
using Microsoft.EntityFrameworkCore;


namespace BadmintonEventsWebApp.Repository
{
    public class TournamentRepository : ITournamentRepository
    {
        private ApplicationDbContext _context;

        public TournamentRepository( ApplicationDbContext context )
        {
            _context = context;
        }

        public bool Add( Tournament tournament )
        {
            _context.Add( tournament );
            return Save();
        }

        public bool Delete( Tournament tournament )
        {
            _context.Remove( tournament );
            return Save();
        }

        public async Task<IEnumerable<Tournament>> GetAll()
        {
            return await _context.Tournaments.ToListAsync();
        }

        public async Task<Tournament> GetByIdAsync( int id )
        {
            return await _context.Tournaments.Include( a => a.Address ).FirstOrDefaultAsync( b => b.Id == id );
        }

        public async Task<IEnumerable<Tournament>> GetClubByCity( string city )
        {
            return await _context.Tournaments.Where( b => b.Address.City.Contains( city ) ).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update( Tournament tournament )
        {
            _context.Update( tournament );
            return Save();
        }
    }
}
