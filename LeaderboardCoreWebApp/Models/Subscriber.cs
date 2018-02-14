using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardCoreWebApp.Models
{
    public class Subscriber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LeaderboardId { get; set; }
    }
}
