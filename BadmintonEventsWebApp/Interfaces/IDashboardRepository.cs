using BadmintonEventsWebApp.Models;

namespace BadmintonEventsWebApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Tournament>> GetAllUserTournaments();

        Task<List<Club>> GetAllUserClubs();

        Task<AppUser> GetUserById( string id );

        Task<AppUser> GetByIdNoTracking( string id );

        bool Update( AppUser user );

        bool Save();
    }
}
