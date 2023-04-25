using Amazon.SQS;
using Amazon.SQS.Model;

namespace AwsBooksService.Services
{
    public class EventService : IEventService
    {
        private readonly IAmazonSQS _sqsClient;

        public EventService(IAmazonSQS amazonSQS)
        {
            _sqsClient = amazonSQS;
        }

        public async Task<bool> Push(string message)
        {
            var queueUrlResponse = await _sqsClient.GetQueueUrlAsync("BookEvents");

            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = queueUrlResponse.QueueUrl,
                MessageBody = message
            };

            var response = await _sqsClient.SendMessageAsync(sendMessageRequest);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
