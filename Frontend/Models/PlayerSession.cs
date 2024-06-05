namespace Frontend.Models
{
    public class PlayerSession
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public PlayerSession()
        {
            Guid g = Guid.NewGuid();
            Id = g.ToString();
            Score = 0;
        }
    }
}
