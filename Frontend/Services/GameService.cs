using Frontend.Models;
using Frontend.Services.Interfaces;
using System.Text.Json;

namespace Frontend.Services
{
    public class GameService : IGameService
    {
        public PlayerSession Session { get; set; }
        private IQueueService sqs;

        public GameService(IQueueService sqs)
        {
            Console.WriteLine("started game service");
            Session = new PlayerSession();
            Console.WriteLine(Session.Id);
            this.sqs = sqs;
        }
        public string getSessionId()
        {
            return Session.Id;
        }
        public void setName(string name)
        {
            Session.Name = name;
        }
        public string getName()
        {
            return Session.Name;
        }
        public async Task IncrementScore()
        {
            Session.Score++;
            await sqs.SendMessageAsync(JsonSerializer.Serialize(Session));

        }
    }
}
