
using Jap.MessageBus;

namespace Jap.Services.OrderAPI.Messages
{
    public class PaymentRequestMessages: BaseMessage
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string CardNumber{ get; set; }
        public string CVV { get; set; }
        public string EspiryMonthYear { get; set; }
        public double? OrderTotal { get; set; }
        public string Email { get; set; }
    }
}
