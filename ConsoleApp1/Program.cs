using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using Amazon.SQS;
using Amazon.SQS.Model;
namespace AWSExampleFederation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Assuming you have already obtained temporary credentials
            string accessKeyId = "ASIAVURCVOKB2SL56X4M";
            string secretAccessKey = "v7+s4QV1APNRPKX1mqctlyYvSN+Q9rJnRtCzAOjC";
            string sessionToken = "IQoJb3JpZ2luX2VjEOH//////////wEaCXVzLXdlc3QtMiJHMEUCIQCgqu4iBJWdv0Gm1Ys9VlnaC7uUbRW+NYF6+f09ZOdFkwIgU47iVWKz6HmNNJWNTRs5ftcETIkKnjt2MZhlHE08PwcqqAIIahABGgwzODc2OTM1MDcyMDMiDIrpI2u982oJA9SOgCqFAtUxaiyNBBtSizmW331BVr0+ND7zaYE/hECArBvUX9KlbO9QcgRs+zY8WBfVSoxrNpEBiYrXf8E5uVyn8KUt8yNLEAJgvxNN92vaRrFbzZEhZvrr8A4PtDs7/4pzBk5RkS/pdzQ6GSpQYwpZH/OkOZ1/jKsQn36bG9hQLX0joaQ0tBmcMrBWVfTkJuNhxOlBETdK7CRTHctrWdQY5jurMKICDUjnFpG6ueMBvhjaD+fI9Ehc0OfAxBaldVbrfNAYEACVG6CAFNxemcQBESt9Gn0hnJr87JO9AFiXVw4idXVUF/8k1iA+cqeA8yCfkfYfdwYDqKUONdsjh+oTHyZT4PazLqoGDzC1hO+yBjqdAdog8CRYx/oVtnDNwGp4DFwmo3NZCjPKp4hwKiysUu+IiK4EuwSdl35rBPRr8/4tTZizjzGYV2BHDwPYWOj+/eVNayfSj+CzvseSb00YhAiofy5Gm5ory05D0PuxBIkdNRd4oBTsbNkLl3QICK2LqtTATcb2ZaAtNKSTa37MX6Zy7/zARFTHmmX/k6YYGunsYRkYTmbnzu/pC/Zigao=";


            // Create AWS credentials object
            var awsCredentials = new SessionAWSCredentials(accessKeyId, secretAccessKey, sessionToken);

            // Use the credentials to create an Amazon S3 client
            using var s3Client = new AmazonS3Client(awsCredentials, RegionEndpoint.USEast1);

            // Perform an S3 operation, for example, listing buckets
            await ListS3BucketsAsync(s3Client);



        }

        private static async Task ListS3BucketsAsync(IAmazonS3 s3Client)
        {
            try
            {
                var response = await s3Client.ListBucketsAsync();
                Console.WriteLine("Buckets:");
                foreach (var bucket in response.Buckets)
                {
                    Console.WriteLine(bucket.BucketName);
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered on server. Message:'{e.Message}' when listing buckets");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown error encountered on server. Message:'{e.Message}' when listing buckets");
            }
        }

    }
}