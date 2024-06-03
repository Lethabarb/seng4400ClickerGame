using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Newtonsoft.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PostToDynamo;

public class Function
{

    private static readonly AmazonDynamoDBClient dynamoDbClient = new AmazonDynamoDBClient();
    private static readonly string tableName = "PlayerSessions"; // Replace with your DynamoDB table name

    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {

    }

    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
    /// to respond to SQS messages.
    /// </summary>
    /// <param name="evnt">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public string HandleSQSEvent(SQSEvent sqsEvent, ILambdaContext context)
    {
        Console.WriteLine($"Beginning to process {sqsEvent.Records.Count} records...");

        foreach (var record in sqsEvent.Records)
        {
            Console.WriteLine($"Message ID: {record.MessageId}");
            Console.WriteLine($"Event Source: {record.EventSource}");

            Console.WriteLine($"Record Body:");
            Console.WriteLine(record.Body);
        }

        Console.WriteLine("Processing complete.");

        return $"Processed {sqsEvent.Records.Count} records.";
    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        try
        {
            // Deserialize the SQS message body to a custom object
            var myData = JsonConvert.DeserializeObject<PlayerSession>(message.Body);

            var item = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { S = "testID"} },
                    { "Name", new AttributeValue { S = "testName" } }, // Example attribute
                    { "Score", new AttributeValue { N = "testScore" } } // Example attribute
                };

            var request = new PutItemRequest
            {
                TableName = tableName,
                Item = item
            };

            var response = await dynamoDbClient.PutItemAsync(request);

            context.Logger.LogLine($"Successfully inserted record with Id: Test");
        }
        catch (Exception ex)
        {
            context.Logger.LogLine($"Error inserting record: {ex.Message}");
        }
    }

    private class PlayerSession
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}