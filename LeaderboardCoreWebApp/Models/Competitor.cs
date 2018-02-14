namespace LeaderboardCoreWebApp.Models
{
    public class Competitor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int LeaderboardId { get; set; }
    }
}