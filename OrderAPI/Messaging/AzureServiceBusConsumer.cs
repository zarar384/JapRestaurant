using Azure.Messaging.ServiceBus;
using Jap.Services.OrderAPI.Messages;
using Jap.Services.OrderAPI.Models;
using Jap.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using System.Text;

namespace Jap.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer
    {
        private readonly OrderRepository _orderRepository;

        public AzureServiceBusConsumer(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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

                foreach(var details in checkoutHeaderDto.CartDetails)
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
