
using LeaderboardCoreWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeaderboardCoreWebApp.Services.Contracts
{
    public interface ILeaderboardService
    {
        Task<IEnumerable<Competitor>> GetAllCompetitors();

        Task<IEnumerable<Subscriber>> GetAllSubscribers();

        Task AddCompetitor(Competitor competitor);

        Task UpdateCompetitor(Competitor competitor);

        Task DeleteCompetitor(int id);

        Task<bool> HasCompetitor(int id);

        Task<Competitor> FindCompetitorByName(string name);

        Task<Competitor> FindCompetitor(int id);

        Task<Leaderboard> CreateLeaderboard(string name);

        Task<Leaderboard> FindLeaderboardByName(string name);

        Task StartRoundRobin();
    }
}
