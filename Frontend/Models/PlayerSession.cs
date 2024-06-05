namespace Frontend.Models
{
    public class PlayerSession
    {
        public string SessionId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public PlayerSession()
        {
            Guid g = Guid.NewGuid();
            SessionId = g.ToString();
            Score = 0;
        }
    }
}
