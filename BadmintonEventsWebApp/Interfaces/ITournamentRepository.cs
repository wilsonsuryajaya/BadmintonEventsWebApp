using BadmintonEventsWebApp.Models;

namespace BadmintonEventsWebApp.Interfaces
{
    public interface ITournamentRepository
    {
        Task<IEnumerable<Tournament>> GetAll();

        Task<Tournament> GetByIdAsync( int id );

        Task<IEnumerable<Tournament>> GetClubByCity( string city );

        bool Add( Tournament club );

        bool Update( Tournament club );

        bool Delete( Tournament club );

        bool Save();
    }
}
