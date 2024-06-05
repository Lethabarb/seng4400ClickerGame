using Frontend.Models;
using Newtonsoft.Json;
using System.Text;

namespace Frontend.Services
{
    public class LeaderboardService
    {
        HttpClient client;
        public List<LeaderboardPosition> positions = new List<LeaderboardPosition> {
        new LeaderboardPosition {Name = "-", Score = 0},
        new LeaderboardPosition {Name = "-", Score = 0},
        new LeaderboardPosition {Name = "-", Score = 0},
        new LeaderboardPosition {Name = "-", Score = 0},
        new LeaderboardPosition {Name = "-", Score = 0},
        new LeaderboardPosition {Name = "-", Score = 0},
        new LeaderboardPosition {Name = "-", Score = 0},
        new LeaderboardPosition {Name = "-", Score = 0},
        new LeaderboardPosition {Name = "-", Score = 0},
        new LeaderboardPosition {Name = "-", Score = 0}
        };
        public LeaderboardService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://4cd8xnayn7.execute-api.us-east-1.amazonaws.com");
        }

        public List<LeaderboardPosition> getBoard()
        {
            return positions;
        }

        public async Task getPositions()
        {
            var res = await client.GetAsync("https://4cd8xnayn7.execute-api.us-east-1.amazonaws.com/ProcessLeaderboard");
            Stream receiveStream = await res.Content.ReadAsStreamAsync();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            var json = readStream.ReadToEnd();
            positions = JsonConvert.DeserializeObject<List<LeaderboardPosition>>(json);
            Console.WriteLine(positions[0].Name);
        }
    }
}
