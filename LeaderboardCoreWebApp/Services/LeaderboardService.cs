using LeaderboardCoreWebApp.Data;
using LeaderboardCoreWebApp.Models;
using LeaderboardCoreWebApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardCoreWebApp.Services
{
    /// <summary>
    /// The class to provide business logic to Leaderboard MVP including Data Access Layer (DAL) 
    /// </summary>
    public class LeaderboardService : ILeaderboardService
    {
        private readonly LeaderboardContext context;

        private readonly ILogger<LeaderboardService> logger;

        /// <summary>
        /// Initialises a new instance of the <see cref="LeaderboardService"/> class.
        /// </summary>
        /// <param name="context">The LeaderboardContext object</param>
        public LeaderboardService(LeaderboardContext context, ILogger<LeaderboardService> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        
        public async Task AddCompetitor(Competitor competitor)
        {
            try
            {
                this.context.Add(competitor);
                await this.context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding a competitor.");
            }
        }

        public Task<Leaderboard> CreateLeaderboard(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Leaderboard> FindLeaderboardByName(string name)
        {
            var board = await this.context.Leaderboards.Include(lb => lb.Competitors)
                            .SingleOrDefaultAsync(m => string.CompareOrdinal(m.Name, name) == 0);
            board.Competitors = await this.context.Competitors.Where(c => c.LeaderboardId == board.LeaderboardId).OrderByDescending(c => c.Score).ToListAsync();
            return board;
        }

        public async Task DeleteCompetitor(int id)
        {
            try
            {
                var competitor = await this.FindCompetitor(id);

                if (competitor != null)
                {
                    this.context.Competitors.Remove(competitor);
                    await this.context.SaveChangesAsync();

                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while deleting a competitor.");
            }
        }

        public async Task<Competitor> FindCompetitor(int id)
        {
            return await this.context.Competitors
                            .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Competitor> FindCompetitorByName(string name)
        {
            return await this.context.Competitors
                            .SingleOrDefaultAsync(m => string.CompareOrdinal(m.Name, name) == 0);
        }

        public async Task<IEnumerable<Competitor>> GetAllCompetitors()
        {
            return await this.context.Competitors.OrderBy(c => c.Score).ToListAsync();
        }

        public async Task<bool> HasCompetitor(int id)
        {
            return await this.FindCompetitor(id) != null;
        }

        public async Task UpdateCompetitor(Competitor competitor)
        {
            try
            {
                this.context.Update(competitor);
                await this.context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating a competitor.");
            }
        }

        public async Task<IEnumerable<Subscriber>> GetAllSubscribers()
        {
            return await this.context.Subscribers.ToListAsync();
        }

        public async Task StartRoundRobin()
        {
            // This method simulates H2H Contents in Round Robin format
            try
            {
                var competitors = this.GetAllCompetitors().Result.ToArray();

                for (var i = 0; i < competitors.Length; i++)
                {
                    var mainCompetitor = competitors[i];

                    for (var j = i + 1; j < competitors.Length; j++)
                    {
                        this.logger.LogInformation($"Start H2HContent between {mainCompetitor.Name} and {competitors[j]}");
                        var winner = this.ExecuteH2H(mainCompetitor, competitors[j]);

                        if (winner != null)
                        {
                            await this.UpdateCompetitor(winner);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"An error occurred while executing H2HContent. Exception error {ex.Message}");
            }
        }

        /// <summary>
        /// Validates H2H Content. If its valid base on rules defined by Refereed the winer will be defined.
        /// </summary>
        /// <param name="content">The H2HContent</param>
        /// <returns>The Competitor winner with registered score</returns>
        public Competitor ValidateH2HCotent(H2HContent content)
        {
            // Here we have to validate the H2H Content based on the defined rules and Referees
            // This is just a simulation code
            var rand = new Random();
            var f = rand.Next(0, 1001);

            content.Winner = f % 2 == 0 ? content.First : content.Second;
            content.Winner.Score += rand.Next(1, 20);

            return content.Winner;
        }

        private Competitor ExecuteH2H(Competitor first, Competitor second)
        {
            H2HContent content = new H2HContent(first, second, new List<string>());

            return this.ValidateH2HCotent(content);
        }
    }
}
