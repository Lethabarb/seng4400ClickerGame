using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.Core;
using Frontend.Models;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace GetLeaderboard;

public class Function
{
    private static readonly AmazonDynamoDBClient dynamoDbClient = new AmazonDynamoDBClient();
    private static readonly string tableName = "PlayerSessions"; // Replace with your DynamoDB table name
    public async Task<List<LeaderboardPosition>> FunctionHandler(ILambdaContext context)
    {
        List<string> attributes = new List<string> { "name", "Score" };
        ScanResponse res = await dynamoDbClient.ScanAsync(tableName, attributes);
        string output = JsonConvert.SerializeObject(res.Items);
        List<LeaderboardPosition> positions = JsonConvert.DeserializeObject<List<LeaderboardPosition>>(output);
        positions.Sort(new ScoreComparer());
        List<LeaderboardPosition> top10 = positions.GetRange(0, 10);
        return top10;
    }
}