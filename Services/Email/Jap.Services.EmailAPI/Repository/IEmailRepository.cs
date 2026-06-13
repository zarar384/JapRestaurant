using Jap.Services.EmailAPI.Messages;

namespace Jap.Services.EmailAPI.Repository
{
    public interface IEmailRepository
    {
        Task SendAndLogEmail(UpdatePaymentResultMessage message);
    }
}
