using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Frontend.Services;
using System.Text.Json;

string accessKeyId = "ASIAVURCVOKB2SL56X4M";
string secretAccessKey = "v7+s4QV1APNRPKX1mqctlyYvSN+Q9rJnRtCzAOjC";
string sessionToken = "IQoJb3JpZ2luX2VjEOH//////////wEaCXVzLXdlc3QtMiJHMEUCIQCgqu4iBJWdv0Gm1Ys9VlnaC7uUbRW+NYF6+f09ZOdFkwIgU47iVWKz6HmNNJWNTRs5ftcETIkKnjt2MZhlHE08PwcqqAIIahABGgwzODc2OTM1MDcyMDMiDIrpI2u982oJA9SOgCqFAtUxaiyNBBtSizmW331BVr0+ND7zaYE/hECArBvUX9KlbO9QcgRs+zY8WBfVSoxrNpEBiYrXf8E5uVyn8KUt8yNLEAJgvxNN92vaRrFbzZEhZvrr8A4PtDs7/4pzBk5RkS/pdzQ6GSpQYwpZH/OkOZ1/jKsQn36bG9hQLX0joaQ0tBmcMrBWVfTkJuNhxOlBETdK7CRTHctrWdQY5jurMKICDUjnFpG6ueMBvhjaD+fI9Ehc0OfAxBaldVbrfNAYEACVG6CAFNxemcQBESt9Gn0hnJr87JO9AFiXVw4idXVUF/8k1iA+cqeA8yCfkfYfdwYDqKUONdsjh+oTHyZT4PazLqoGDzC1hO+yBjqdAdog8CRYx/oVtnDNwGp4DFwmo3NZCjPKp4hwKiysUu+IiK4EuwSdl35rBPRr8/4tTZizjzGYV2BHDwPYWOj+/eVNayfSj+CzvseSb00YhAiofy5Gm5ory05D0PuxBIkdNRd4oBTsbNkLl3QICK2LqtTATcb2ZaAtNKSTa37MX6Zy7/zARFTHmmX/k6YYGunsYRkYTmbnzu/pC/Zigao=";

QueueService sqs = new QueueService();

await sqs.NextMessage();

while (sqs.getMessage() == "no content")
{
    Console.WriteLine("new");
    await sqs.NextMessage();
}
Console.WriteLine(sqs.getMessage());
// Create AWS credentials object
//var awsCredentials = new SessionAWSCredentials(accessKeyId, secretAccessKey, sessionToken);
//AmazonSQSClient client = new AmazonSQSClient(awsCredentials, RegionEndpoint.USEast1);

//SendMessageRequest req = new SendMessageRequest();
//req.QueueUrl = "https://sqs.us-east-1.amazonaws.com/387693507203/ClickerQueue.fifo";
//req.MessageBody = "hello";
//req.MessageGroupId = "test";
//req.MessageDeduplicationId = "test";

//SendMessageResponse res = await client.SendMessageAsync(req);
//Console.WriteLine(res.HttpStatusCode);