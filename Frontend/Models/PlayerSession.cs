namespace Frontend.Models
{
    public class PlayerSession
    {
        public PlayerSession(string id, string name, int score)
        {
            Id = id;
            Name = name;
            Score = score;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
