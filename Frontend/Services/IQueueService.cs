using Amazon.SQS.Model;
using Frontend.Models;

namespace Frontend.Services
{
    public interface IQueueService
    {
        public Task SendMessageAsync(string message);
        public Task NextMessage();
        public string getMessage();
    }
}
