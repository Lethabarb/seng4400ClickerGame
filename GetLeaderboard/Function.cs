using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Lambda.APIGatewayEvents;
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

    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        List<string> attributes = new List<string> { "Name", "Score" };
        ScanResponse res = await dynamoDbClient.ScanAsync(tableName, attributes);
        //string output = JsonConvert.SerializeObject(res.Items);
        List<LeaderboardPosition> allPositions = new List<LeaderboardPosition>();
        List<LeaderboardPosition> positions = new List<LeaderboardPosition>();

        DynamoDBContext dBContext = new DynamoDBContext(dynamoDbClient);
        foreach (var item in res.Items)
        {
            var doc = Document.FromAttributeMap(item);
            var myModel = dBContext.FromDocument<LeaderboardPosition>(doc);
            allPositions.Add(myModel);
        }
        allPositions.Sort(new ScoreComparer());
        if (allPositions.Count > 10)
        {
            positions = allPositions.GetRange(0, 10);
        } 
        else
        {
            for (int i = allPositions.Count - 1; i < 10; i++)
            {
                allPositions.Add(new LeaderboardPosition { Name = "-", Score = 0 });
            }
            positions = allPositions;
        }
        var response = new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = JsonConvert.SerializeObject(positions),
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };
        return response;
    }
}