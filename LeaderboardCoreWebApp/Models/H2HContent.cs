using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardCoreWebApp.Models
{
    public class H2HContent
    {
        public Competitor First { get; private set; }
        public Competitor Second { get; private set; }
        public Competitor Winner { get; set; }

        public IEnumerable<string> Rules { get; private set; }

        public H2HContent(Competitor first, Competitor second, IEnumerable<string> rules)
        {
            this.First = first;
            this.Second = second;
            this.Rules = rules;
        }
    }
}
