using Azure.Messaging.ServiceBus;
using Jap.MessageBus;
using Jap.Services.OrderAPI.Messages;
using Newtonsoft.Json;
using PaymentProcess;
using System.Text;

namespace Jap.Services.PaymentAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string _azureServiceBusConnection;
        private readonly string _subscriptionPayment;
        private readonly string _orderPaymentProcessTopic;
        private readonly string _orderUpdatePaymentResultTopic;

        private ServiceBusProcessor _processor;
        private readonly IPayment _payment;
        private readonly IConfiguration _configuration;
        private readonly IMessageBus _messageBus;


        public AzureServiceBusConsumer(IConfiguration configuration, IMessageBus messageBus, IPayment payment)
        {
            _configuration = configuration;
            _messageBus = messageBus;
            _payment = payment;

            _azureServiceBusConnection = _configuration.GetValue<string>("AzureServiceBusConnection");
            _subscriptionPayment = _configuration.GetValue<string>("OrderPaymentProcessSubscription");
            _orderPaymentProcessTopic = _configuration.GetValue<string>("OrderPaymentProcessTopic");
            _orderUpdatePaymentResultTopic = _configuration.GetValue<string>("OrderUpdatePaymentResultTopic");

            if (_azureServiceBusConnection == null)
            {
                var client = new ServiceBusClient(_azureServiceBusConnection);

                _processor = client.CreateProcessor(_orderPaymentProcessTopic, _subscriptionPayment);
            }
        }

        public async Task Start()
        {
            if (_processor != null)
            {
                _processor.ProcessMessageAsync += ProcessPayments;
                _processor.ProcessErrorAsync += ErrorHandler;
                await _processor.StartProcessingAsync();
            }
        }

        public async Task Stop()
        {
            if (_processor != null)
            {
                await _processor.StopProcessingAsync();
                await _processor.DisposeAsync();
            }
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());

            return Task.CompletedTask;
        }

        private async Task ProcessPayments(ProcessMessageEventArgs args)
        {
            var message = args.Message;

            if (message == null)
            {
                var body = Encoding.UTF8.GetString(message.Body);

                PaymentRequestMessages paymentReqMessage = JsonConvert.DeserializeObject<PaymentRequestMessages>(body);

                var result = _payment.Processor();

                UpdatePaymentResultMessage updatePaymentResultMessage = new()
                {
                    Status = result,
                    OrderId = paymentReqMessage.OrderId
                };

                try
                {
                    await _messageBus.PublishMessage(updatePaymentResultMessage, _orderUpdatePaymentResultTopic);
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
