using Frontend.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Frontend.Services
{
    public class LeaderboardService
    {
        HttpClient client;
        public List<LeaderboardPosition> positions;
        public LeaderboardService() {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://4cd8xnayn7.execute-api.us-east-1.amazonaws.com");
            Thread t = new Thread(new ThreadStart(getPositions));
        }

        public async void getPositions()
        {
            while (true)
            {
                var res = await client.GetAsync("https://4cd8xnayn7.execute-api.us-east-1.amazonaws.com/ProcessLeaderboard");
                Stream receiveStream = await res.Content.ReadAsStreamAsync();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                var json = readStream.ReadToEnd();
                positions = JsonConvert.DeserializeObject<List<LeaderboardPosition>>(json);
            }
        }
    }
}
