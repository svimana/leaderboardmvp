using LeaderboardCoreWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardCoreWebApp.Data
{
    public static class DbInitializer
    {
        /// <summary>
        /// Static class to initialize LeaderboardContext with some test data
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(LeaderboardContext context)
        {
            context.Database.EnsureCreated();

            // Look for any leaderboards.
            if (!context.Leaderboards.Any())
            {
                context.Leaderboards.Add(new Leaderboard { Name = "Default" });
                context.SaveChanges();
            }

            // Look for any competitors.
            if (!context.Competitors.Any())
            {

                var competitors = new Competitor[]
                {
                    new Competitor{Name="Iron Man", LeaderboardId=1},
                    new Competitor{Name="Strong Fighter", LeaderboardId=1},
                    new Competitor{Name="Nency123", LeaderboardId=1},
                    new Competitor{Name="Kunfuist", LeaderboardId=1}
                };

                foreach (Competitor cmp in competitors)
                {
                    context.Competitors.Add(cmp);
                }

                context.SaveChanges();
            }
            if (!context.Subscribers.Any())
            {
                context.Subscribers.Add(new Subscriber { Name = "Subscriber A", LeaderboardId = 1 });
                context.Subscribers.Add(new Subscriber { Name = "Subscriber B", LeaderboardId = 1 });
                context.SaveChanges();
            }
        }
    }
}
