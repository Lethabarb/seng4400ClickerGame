using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
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
        List<string> attributes = new List<string> { "Name", "Score" };
        ScanResponse res = await dynamoDbClient.ScanAsync(tableName, attributes);
        //string output = JsonConvert.SerializeObject(res.Items);
        List<LeaderboardPosition> positions = new List<LeaderboardPosition>();

        DynamoDBContext dBContext = new DynamoDBContext(dynamoDbClient);
        foreach (var item in res.Items)
        {
            var doc = Document.FromAttributeMap(item);
            var myModel = dBContext.FromDocument<LeaderboardPosition>(doc);
            positions.Add(myModel);
        }
        positions.Sort(new ScoreComparer());
        if (positions.Count > 10)
        {
            List<LeaderboardPosition> top10 = positions.GetRange(0, 10);
            return top10;

        } else
        {
            for (int i = positions.Count - 1; i < 10; i++)
            {
                positions.Add(new LeaderboardPosition { Name = "-", Score = 0 });
            }
            return positions;
        }
    }
}