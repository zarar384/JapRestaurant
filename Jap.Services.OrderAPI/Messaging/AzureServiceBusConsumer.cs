using Azure.Messaging.ServiceBus;
using Jap.Services.OrderAPI.Messages;
using Jap.Services.OrderAPI.Models;
using Jap.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using System.Text;

namespace Jap.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly string _azureServiceBusConnection;
        private readonly string _subscriptionCheckOut;
        private readonly string _checkoutMessageTopic;

        private ServiceBusProcessor _checkoutProcessor;
        private readonly IConfiguration _configuration;

        private readonly OrderRepository _orderRepository;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _configuration = configuration;

            _azureServiceBusConnection = _configuration.GetValue<string>("AzureServiceBusConnection");
            _subscriptionCheckOut = _configuration.GetValue<string>("SubscriptionCheckOut");
            _checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");

            if (_azureServiceBusConnection == null)
            {
                var client = new ServiceBusClient(_azureServiceBusConnection);
                _checkoutProcessor = client.CreateProcessor(_checkoutMessageTopic, _subscriptionCheckOut);
            }
        }

        public async Task Start()
        {
            if (_checkoutProcessor != null)
            {
                _checkoutProcessor.ProcessMessageAsync += OnCheckOutMessageReceived;
                _checkoutProcessor.ProcessErrorAsync += ErrorHandler;
                await _checkoutProcessor.StartProcessingAsync();
            }
        }

        public async Task Stop()
        {
            if (_checkoutProcessor != null)
            {
                await _checkoutProcessor.StopProcessingAsync();
                await _checkoutProcessor.DisposeAsync();
            }
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());

            return Task.CompletedTask;
        }

        private async Task OnCheckOutMessageReceived(ProcessMessageEventArgs args)
        {
            var message = args.Message;

            if (message == null)
            {
                var body = Encoding.UTF8.GetString(message.Body);

                CheckoutHeaderDto checkoutHeaderDto = JsonConvert.DeserializeObject<CheckoutHeaderDto>(body);

                OrderHeader orderHeader = new()
                {
                    UserId = checkoutHeaderDto.UserId,
                    FirstName = checkoutHeaderDto.FirstName,
                    LastName = checkoutHeaderDto.LastName,
                    OrderDetails = new List<OrderDetails>(),
                    CardNumber = checkoutHeaderDto.CardNumber,
                    CouponCode = checkoutHeaderDto.CouponCode,
                    CVV = checkoutHeaderDto.CVV,
                    DiscountTotal = checkoutHeaderDto.DiscountTotal,
                    Email = checkoutHeaderDto.Email,
                    ExpiryMonthYear = checkoutHeaderDto.ExpiryMonthYear,
                    OrderTime = DateTime.Now,
                    OrderTotal = checkoutHeaderDto.OrderTotal,
                    PaymentStatus = false,
                    Phone = checkoutHeaderDto.Phone,
                    PickupDateTime = checkoutHeaderDto.PickupDateTime
                };

                foreach (var details in checkoutHeaderDto.CartDetails)
                {
                    OrderDetails orderDetails = new()
                    {
                        ProductId = details.ProductId,
                        ProductName = details.Product.Name,
                        Price = details.Product.Price,
                        Count = details.Count,
                    };

                    orderHeader.CartTotalItems += details.Count;
                    orderHeader.OrderDetails.Add(orderDetails);
                }

                await _orderRepository.AddOrder(orderHeader);
            }
        }
    }
}
