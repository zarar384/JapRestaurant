using Azure.Messaging.ServiceBus;
using Jap.MessageBus;
using Jap.Services.EmailAPI.Messages;
using Jap.Services.EmailAPI.Repository;
using Newtonsoft.Json;
using System.Text;

namespace Jap.Services.EmailAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string _azureServiceBusConnection;
        private readonly string _subscriptionEmail;
        private readonly string _orderUpdatePaymentResultTopic;

        private ServiceBusProcessor _orderUpdatePaymentStatusProcessor;

        private readonly IConfiguration _configuration;

        private readonly EmailRepository _emailRepository;

        public AzureServiceBusConsumer(EmailRepository emailRepository, IConfiguration configuration)
        {
            _emailRepository = emailRepository;
            _configuration = configuration;

            _azureServiceBusConnection = _configuration.GetValue<string>("AzureServiceBusConnection");
            _subscriptionEmail = _configuration.GetValue<string>("SubscriptionEmail");
            _orderUpdatePaymentResultTopic = _configuration.GetValue<string>("OrderUpdatePaymentResultTopic");

            if (_azureServiceBusConnection == null)
            {
                var client = new ServiceBusClient(_azureServiceBusConnection);
                //_checkoutProcessor = client.CreateProcessor(_checkoutMessageTopic, _subscriptionCheckOut);
                _orderUpdatePaymentStatusProcessor = client.CreateProcessor(_orderUpdatePaymentResultTopic, _subscriptionEmail);

            }
        }

        public async Task Start()
        {
            if (_subscriptionEmail != null)
            {
                _orderUpdatePaymentStatusProcessor.ProcessMessageAsync += OnOrderPaymentUpdateReceived;
                _orderUpdatePaymentStatusProcessor.ProcessErrorAsync += ErrorHandler;
                await _orderUpdatePaymentStatusProcessor.StartProcessingAsync();
            }
        }

        public async Task Stop()
        {
            if (_subscriptionEmail != null)
            {
                await _orderUpdatePaymentStatusProcessor.StopProcessingAsync();
                await _orderUpdatePaymentStatusProcessor.DisposeAsync();
            }
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());

            return Task.CompletedTask;
        }

        private async Task OnOrderPaymentUpdateReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;

            if (message == null)
            {
                var body = Encoding.UTF8.GetString(message.Body);

                UpdatePaymentResultMessage objMsg = JsonConvert.DeserializeObject<UpdatePaymentResultMessage>(body);

                try
                {
                    await _emailRepository.SendAndLogEmail(objMsg);
                    await args.CompleteMessageAsync(args.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
