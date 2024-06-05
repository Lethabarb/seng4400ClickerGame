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
    private static readonly AmazonDynamoDBClient dynamoDbClient = new AmazonDynamoDBClient("ASIAVURCVOKB64J5VIMT", "ILlbQHh7nqfJYbFIoSSJZjPS+gZA6mKxWtHrJW/h", "IQoJb3JpZ2luX2VjEC0aCXVzLXdlc3QtMiJHMEUCIQCpk8teUVc4dgaP4w0/mf13P07QIMcXI6MpF60BwyN51wIgWRbA1iXZmjOwGjoWPJKnP3Y2JlVmjrW1kzAuVhNDU0wqsQIItv//////////ARABGgwzODc2OTM1MDcyMDMiDMW2mWpgITaoK76WECqFAo7MyI2dvaJlWGRFmV3HSzR167aVKMwhqFA5GUuU7aR/atJVLxQpf7ruhGxtsms17bkw6BASr2xGyUDMrk1ixa5jPeaJaS1AQ3KmRTklC2zq2Rd/91x1MKi2zAGpCje5TjaCrnNMU9tGPljp3cvl+qbMknaexZuKoJoqKqT8scfZC1Vlr6bPDTQmjOo6R7Ew64M7dU3sEQg151n3Dau0FDHUUoUUtv2UNqBfq4Nhb8etuWvCY0yuVpwOgMhD3a/UZMtE4vlYeMTNDUzE7KSIgik4K0+KNxJsAddY7G48OdHJVVovCJhwIdsmPNuhRYBCs9dxh/IQV4puT7Y1zgY4aLvMfF3wBTDz6f+yBjqdAVBd4DHQVNix+gS1qmPG4tYoG7S3VOYj7kqe1pBq9AhlAJeNQK9LLJR/1XTR7ap3G1Zr5E5L4XYO/5Uiblex5BjakLQzKOklB1JiGv4wLGkbk48OUl8vh0UVrpWRhZIiyQY+3MuYQHyIPcQiandurz3rvck2ieXwxGybKrKR81IeUZj4NByMRvFEMNMCN+LDJevvtEKIS80hXWFvMMU=", RegionEndpoint.USEast1);
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