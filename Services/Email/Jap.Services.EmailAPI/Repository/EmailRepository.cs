using Jap.Services.EmailAPI.Messages;
using Jap.Services.EmailAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Jap.Services.EmailAPI.Models;

namespace Jap.Services.EmailAPI.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<AppDbContext> _context;

        public EmailRepository(DbContextOptions<AppDbContext> context)
        {
            _context = context;
        }

        public async Task SendAndLogEmail(UpdatePaymentResultMessage message)
        {
            //implement email sender service
            EmailLog emailLog = new EmailLog()
            {
                Email = message.Email,
                EmailSent = DateTime.Now,
                Log = $"Order: {message.OrderId}. We have received your order and are processing it."
            };

            await using var _db = new AppDbContext(_context);
            _db.EmailLogs.Add(emailLog);
            await _db.SaveChangesAsync();
        }
    }
}
