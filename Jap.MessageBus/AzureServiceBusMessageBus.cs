using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace Jap.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        private readonly string _connectionString;

        public AzureServiceBusMessageBus(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task PublishMessage(BaseMessage massage, string topicName)
        {
            if (_connectionString == null && string.IsNullOrEmpty(_connectionString)) return;

            var client = new ServiceBusClient(_connectionString);

            // create the sender
            ServiceBusSender sender = client.CreateSender(topicName);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            var jsonMessage = JsonConvert.SerializeObject(massage);
            ServiceBusMessage busMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            // send the message
            await sender.SendMessageAsync(busMessage);
            
            //close connect
            await sender.CloseAsync();
        }
    }
}
