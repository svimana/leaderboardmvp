using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardCoreWebApp.Models
{
    public class Leaderboard
    {
        [Column("Id")]
        public int LeaderboardId { get; set; }
        public string Name { get; set; }
        public ICollection<Competitor> Competitors { get; set; }
    }
}
